#ifndef DIRECTIVES_HPP
#define DIRECTIVES_HPP

#include <string>

#include "memory.hpp"

using namespace std;

class Directives
{

	public:

	/**
	* executa a diretiva, retornando em qual byte a montagem deve continuar
	*/
	unsigned int execute(string directive,string operands,Memory *memory,unsigned int currentByte);

	/**
	* verifica se a string passada corresponde a uma diretiva
	*/
	bool isDirective(string name);

	private:

};

#endif // DIRECTIVES_HPP

