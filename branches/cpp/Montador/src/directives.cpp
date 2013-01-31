
#include <string>

#include "directives.hpp"
#include "stringer.hpp"
#include "numbers.hpp"
#include "memory.hpp"
#include "defs.hpp"

using namespace std;

/**
* executa a diretiva, retornando em qual byte a montagem deve continuar
*/
unsigned int execute(string directive,string operands,Memory *memory,unsigned int currentByte)
{
	Number n;
	//muda o proximo byte par amontagem
	if(stringCaselessCompare(directive,"org"))
	{
		//o operando deve ser um numero
		return n.toInt(operands);
	}
	else if(stringCaselessCompare(directive,"db"))
	{
		int value = n.toInt(operands);
		memory->writeValue((unsigned char)value,currentByte);
		currentByte++;
	}
	else if(stringCaselessCompare(directive,"dw"))
	{

	}
	else if(stringCaselessCompare(directive,"dab"))
	{

	}
	else if(stringCaselessCompare(directive,"daw"))
	{

	}

	return currentByte;
}

/**
* verifica se a string passada corresponde a uma diretiva
*/
bool Directives::isDirective(string directive)
{

	if(stringCaselessCompare(directive,"org"))
		return true;
	else if(stringCaselessCompare(directive,"db"))
		return true;
	else if(stringCaselessCompare(directive,"dw"))
		return true;
	else if(stringCaselessCompare(directive,"dab"))
		return true;
	else if(stringCaselessCompare(directive,"daw"))
		return true;
	return false;
}
