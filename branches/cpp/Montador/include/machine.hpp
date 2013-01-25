#ifndef MACHINE_HPP
#define MACHINE_HPP

#include <string>

#include "memory.hpp"

using namespace std;

class	Machine
{

	public:

	Machine();

	/**
	*	carrega as caracteristicas da maquina que estao definidas na string config
	*/
	void load(string *config);

	/**
	*	retorna o numero de bits do PC
	*/
	int getPCSize();

	/**
	*	escreve uma palavra na memoria, respeitando a endianess da maquina
	*	a palavra de entrada deve usar a notacao little-endian
	*/
	void writeWord(unsigned char *word,unsigned int wordSize,Memory *memory,unsigned int position);

	/**
	*	escreve o valor na memoria, respeitando a endianess da maquina
	*/
	void writeValue(int value,unsigned int wordSize,Memory *memory,unsigned int position);

	private:

	bool bigEndian;
};

#endif // MACHINE_HPP
