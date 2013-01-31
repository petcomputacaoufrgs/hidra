#ifndef INSTRUCTIONS_HPP
#define INSTRUCTIONS_HPP

#include <string>
#include <list>
#include <map>

using namespace std;

typedef struct s_instruction
{
	unsigned int size;
	string mnemonic;
	string operandExpression;
	list<string> addrs;
	list<string> regs;
	string binFormat;

}t_instruction;

class Instructions
{

	public:

	Instructions();

	/**
	*	carrega as instrucoes que estao definidas na string config
	*/
	void load(string config);

	/**
	*	determina se o dado mnemonico corresponde a uma instrucao ou nao
	*/
	bool isInstruction(string mnemonic);

	/**
	*	gera o codigo binario de uma instrucao usando notacao little-endian
	* retorna o array com esse codigo
	* escreve o numero de bytes em size
	*/
	unsigned char* assemble(string mnemonic, string operands,int *size);

	private:

	map<string,list<t_instruction> > insts;	//relaciona o mnemonico com a estrutura
};

#endif // INSTRUCTIONS_HPP
