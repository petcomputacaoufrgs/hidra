#ifndef MACHINE_HPP
#define MACHINE_HPP

class	Machine
{

	public:

	/**
	*	carrega as caracteristicas da maquina que estao definidas na string config
	*/
	void load(string *config);

	/**
	*	retorna o numero de bytes do PC
	*/
	int getPCSize();

	/**
	*	escreve uma palavra na memoria, respeitando a endianess da maquina
	*	a palavra de entrada deve usar a notacao little-endian
	*/
	void writeWord(unsigned char *word,int wordSize,unsigned char *memory, int memorySize);

};

#endif // MACHINE_HPP
