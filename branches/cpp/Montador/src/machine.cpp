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
int getPCSize()
{
	return 8;
}

/**
*	escreve uma palavra na memoria, respeitando a endianess da maquina
*	a palavra de entrada deve usar a notacao little-endian
*/
void writeWord(unsigned char *word,int wordSize,unsigned char *memory, int memorySize)
{

}
