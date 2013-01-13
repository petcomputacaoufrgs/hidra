#ifndef REGISTERS_HPP
#define REGISTERS_HPP

class	Registers
{

	public:

	/**
	*	carrega os registradores que estao definidas na string config
	*/
	void load(string *config);

	/**
	*	retorna o numero do registrador caso ele exista,
	* -1 caso nao exista
	*/
	int registerNumber(string *regName);

}

#endif // REGISTERS_HPP
