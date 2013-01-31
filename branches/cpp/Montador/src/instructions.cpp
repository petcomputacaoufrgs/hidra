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
	Number n;
	inst.size = n.toInt(*(it++));

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

	this->insts[inst.mnemonic].push_back(inst);
}

/**
*	determina se o dado mnemonico corresponde a uma instrucao ou nao
*/
bool Instructions::isInstruction(string mnemonic)
{
	if(this->insts.find(mnemonic)!=this->insts.end())
		return true;
	else
		return false;
}

/**
*	gera o codigo binario de uma instrucao usando notacao little-endian
* retorna o array com esse codigo
* escreve o numero de bytes em size
*/
unsigned char* assemble(string mnemonic, string operands,int *size)
{

	unsigned char *code;

	return NULL;
}
