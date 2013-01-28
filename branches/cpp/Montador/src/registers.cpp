#include <stdio.h>

#include <string>

#include "registers.hpp"

using namespace std;

Registers::Registers()
{

}

/**
*	carrega os registradores que estao definidas na string config
*/
void Registers::load(string config)
{
	printf("Registers:\nLine:\n%s\n",config.c_str());
}

/**
*	retorna o numero do registrador caso ele exista,
* -1 caso nao exista
*/
int Registers::number(string *regName)
{
	return 0;
}
