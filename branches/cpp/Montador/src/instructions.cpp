#include <stdio.h>

#include <string>

#include "instructions.hpp"

using namespace std;

Instructions::Instructions()
{

}

/**
*	carrega as instrucoes que estao definidas na string config
*/
void Instructions::load(string *config)
{
	printf("Instructions:\nLine:\n%s\n",config->c_str());
}

/**
*	determina se o dado mnemonico corresponde a uma instrucao ou nao
*/
bool Instructions::isInstruction(string *mnemonic)
{
	return true;
}

/**
*	gera o codigo binario de uma instrucao, escrevendo-o em word (notacao little-endian, sempre)
* word deve ser grande o suficiente para guardar a instrucao
*	retorna o numero de bytes da palavra
*/
int Instructions::assemble(string *mnemonic, string *operands,unsigned char *word)
{
	return 0;
}
