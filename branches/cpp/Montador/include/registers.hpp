#ifndef REGISTERS_HPP
#define REGISTERS_HPP

#include <string>

using namespace std;

class	Registers
{

	public:

	Registers();

	/**
	*	carrega os registradores que estao definidas na string config
	*/
	void load(string *config);

	/**
	*	retorna o numero do registrador caso ele exista,
	* -1 caso nao exista
	*/
	int number(string *regName);

};

#endif // REGISTERS_HPP
