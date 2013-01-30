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
	printf("11111111111111\n");

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

	printf("Mnemonic:%s\n",inst.mnemonic.c_str());
	printf("Code:%s\n",inst.code.c_str());
	printf("Operands:%s\n",inst.operandExpression.c_str());
	printf("Binary Format:%s\n",inst.binFormat.c_str());

	printf("****\nAddressings:\n");
	for(it=inst.addrs.begin() ; it!=inst.addrs.end() ; it++)
		printf("(%s)\n",it->c_str());

	printf("****\nRegisters:\n");
	for(it=inst.regs.begin() ; it!=inst.regs.end() ; it++)
		printf("%s\n",it->c_str());

	printf("11111111111111\n");
	getchar();

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
