#include <stdio.h>

#include <string>
#include <list>

#include "registers.hpp"
#include "stringer.hpp"
#include "defs.hpp"

using namespace std;

Registers::Registers()
{

}

/**
*	carrega os registradores que estao definidas na string config
*/
void Registers::load(string config)
{

	list<string> words = stringReadWords(config,"",'\0','#');
	if(words.size() != 1)
		throw(eInvalidFormat);

	t_register r;
	r.name = *(words.begin());

	this->regs[r.name] = r;

}

/**
*	retorna o numero do registrador caso ele exista,
* -1 caso nao exista
*/
int Registers::number(string regName)
{
	return 0;
}
