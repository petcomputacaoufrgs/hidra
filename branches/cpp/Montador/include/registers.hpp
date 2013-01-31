#ifndef REGISTERS_HPP
#define REGISTERS_HPP

#include <string>
#include <map>

using namespace std;

typedef struct s_register
{
	string name;
}t_register;

class	Registers
{

	public:

	Registers();

	/**
	*	carrega os registradores que estao definidas na string config
	*/
	void load(string config);

	/**
	*	retorna o numero do registrador caso ele exista,
	* -1 caso nao exista
	*/
	int number(string regName);

	private:

	map<string,t_register> regs;

};

#endif // REGISTERS_HPP
