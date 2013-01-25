#include <stdio.h>

#include <string>

#include "machine.hpp"

using namespace std;

Machine::Machine()
{

}

/**
*	carrega as caracteristicas da maquina que estao definidas na string config
*/
void Machine::load(string *config)
{
	printf("Machine:\nLine:\n%s\n",config->c_str());
}

/**
*	retorna o numero de bits do PC
*/
int Machine::getPCSize()
{
	return 8;
}

/**
*	escreve uma palavra na memoria, respeitando a endianess da maquina
*	a palavra de entrada deve usar a notacao little-endian
*/
void Machine::writeWord(unsigned char *word,unsigned int wordSize,Memory *memory,unsigned int position)
{
	unsigned char buffer[wordSize];
	unsigned char *wat = buffer;
	//se for big-endian, inverte a ordem
	if(this->bigEndian)
	{
		int i,j;
		for(j=0,i=wordSize-1; i>=0 ;j++, i--)
			buffer[i] = word[j];
	}
	else
		wat = word;

	memory->writeArray(wat,wordSize,position);
}

/**
*	escreve o valor na memoria, respeitando a endianess da maquina
*/
void Machine::writeValue(int value,unsigned int wordSize,Memory *memory,unsigned int position)
{
	if(this->bigEndian)
	{
		int i;
		for(i=wordSize-1;i>=0;i--)
		{
			//escreve o byte menos significativo do valor
			memory->writeValue(value&0xFF,position+i);
			value>>=8;
		}
	}
	else
	{
		unsigned int i;
		for(i=0;i<wordSize;i++,position++)
		{
			//escreve o byte menos significativo do valor
			memory->writeValue(value&0xFF,position);
			value>>=8;
		}
	}
}
