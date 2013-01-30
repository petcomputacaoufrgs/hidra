#ifndef DIRECTIVES_HPP
#define DIRECTIVES_HPP

class Directives
{

	public:

	/**
	* executa a diretiva, retornando em qual byte a montagem deve continuar
	*/
	unsigned int execute(string directive,string operands,Memory *memory,unsigned int currentByte);

	private:

};

#endif // DIRECTIVES_HPP

