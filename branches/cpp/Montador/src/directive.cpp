
#include <string>

#include "directives.hpp"
#include "stringer.hpp"
#include "memory.hpp"
#include "defs.hpp"

using namespace std;

/**
* executa a diretiva, retornando em qual byte a montagem deve continuar
*/
unsigned int execute(string directive,string operands,Memory *memory,unsigned int currentByte)
{
	//muda o proximo byte par amontagem
	if(stringCaselessCompare(directive,"org"))
	{
		//o operando deve ser um numero
		Number n;
		return n.toInt(operands);
	}
	else if(stringCaselessCompare(directive,"db"))
	{
		int value = Number.toInt(operands);
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
}
