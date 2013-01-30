#ifndef INSTRUCTIONS_HPP
#define INSTRUCTIONS_HPP

#include <string>
#include <list>

using namespace std;

typedef struct s_instruction
{
	string code;
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
	bool isInstruction(string *mnemonic);

	/**
	*	gera o codigo binario de uma instrucao, escrevendo-o em word (notacao little-endian, sempre)
	* word deve ser grande o suficiente para guardar a instrucao
	*	retorna o numero de bytes da palavra
	*/
	int assemble(string *mnemonic, string *operands,unsigned char *word);

	private:
};

#endif // INSTRUCTIONS_HPP
