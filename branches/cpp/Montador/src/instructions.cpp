#include <stdio.h>

#include <string>

#include "instructions.hpp"
#include "stringer.hpp"
#include "defs.hpp"

using namespace std;

Instructions::Instructions()
{

}

/**
*	carrega as instrucoes que estao definidas na string config
*/
void Instructions::load(string config)
{
	list<string> words = stringReadWords(config,"'\"",'\\','#');

	list<string>::iterator it=words.begin();

	t_instruction inst;
	inst.code = *(it++);

	if(it==words.end()) throw (eInvalidFormat);
	inst.mnemonic = *(it++);

	if(it==words.end()) throw (eInvalidFormat);
	inst.operandExpression = *(it++);

	if(it==words.end()) throw (eInvalidFormat);
	inst.addrs = stringSplitChar(*(it++),",");

	if(it==words.end()) throw (eInvalidFormat);
	inst.regs = stringSplitChar(*(it++),",");

	if(it==words.end()) throw (eInvalidFormat);
	inst.binFormat = *it;

	this->insts[inst.mnemonic] = inst;
}

/**
*	determina se o dado mnemonico corresponde a uma instrucao ou nao
*/
bool Instructions::isInstruction(string mnemonic)
{
	return true;
}

/**
*	gera o codigo binario de uma instrucao, escrevendo-o em word (notacao little-endian, sempre)
* word deve ser grande o suficiente para guardar a instrucao
*	retorna o numero de bytes da palavra
*/
int Instructions::assemble(string mnemonic, string operands,unsigned char *word)
{
	return 0;
}
