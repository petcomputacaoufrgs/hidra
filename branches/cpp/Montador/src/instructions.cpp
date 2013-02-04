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
	inst.addressingNames = stringSplitChar(*(it++),",");

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
unsigned char* assemble(string mnemonic, string operands,int *size,Addressings addressings,Labels labels, Registers registers)
{

	//busca todas as instrucoes com o dado mnemonico
	map<string,t_instruction>::iterator it = this->insts.find(mnemonic);
	//se nao encontrou
	if(it==this->insts.end())
		throw (eUnknownInstruction);

	list<t_instruction> matches = it->second;
	list<t_instruction>::iterator jt;
	bool inOk = false;	//se foi encontrada a instrucao certa
	t_instruction i;

	list<string> opRegs;//codigo dos registradores que aparecem como operando
	list<string> opAddrs;//codigo dos modos de enderecamento
	list<string> opNumbers;//numeros que aparecem
	list<string> opLabels; //labels que aparecem

	for(jt=matches.begin() ; jt!=matches.end() && !inOk; jt++)
	{
		i = *jt;
		Expression e(i.operandExpression);

		list<string>::iterator addr;
		//cria uma lista com todas as expressoes dos modos de enderecamento
		list<string> expr;
		for(addr=i.addressingNames.begin() ; addr!=i.addressingNames.end() ; addr++)
		{
			t_addressing a = addressings.getAddressing(*addr);
			expr.push_back(a.exp);
		}

		list<t_match > matches = e.findAllSub(operands,expr);

		//verifica se as variaveis sao do tipo adequado
		list<t_match>::iterator imatch;

		//limpa as listas de operandos
		opLabels = list<string>();
		opRegs = list<string>();
		opNumbers = list<string>();
		opAddrs = list<string>();

		Number number;
		bool opOk = true;
		for(imatch=matches.begin() ; imatch!=matches.end() && ok ; imatch++)
		{
			opOk = false;
			t_match m = *imatch;

			if(m.subtype[VAR_REGISTER] || m.subtype[VAR_ANYTHING])
			{
				if(registers.exists(m.element))
				{
					opRegs.push_back(m.element);
					opAddrs.push_back(m.subCode[VAR_REGISTER]);
					opOk=true;
				}
			}
			if(m.subtype[VAR_NUMBER] || m.subtype[VAR_ANYTHING] || m.subtype[VAR_ADDRESS])
			{
				if(number.exists(m.element))
				{
					opNumbers.push_back(m.element);
					opAddrs.push_back(m.subCode[VAR_NUMBER]);
					opOk=true;
				}
			}
			if(m.subtype[VAR_LABEL] || m.subtype[VAR_ANYTHING] || m.subtype[VAR_ADDRESS])
			{
				if(labels.exists(m.element))
				{
					opLabels.push_back(m.element);
					opAddrs.push_back(m.subCode[VAR_REGISTER]);
					opOk=true;
				}
			}
		}//end for match
		//se todos os operandos estao corretos, essa eh a instrucao certa
		if(opOk)
			inOk = true;
	}//end for instruction

	//se nenhuma instrucao satisfez, a linha esta incorreta
	if(!inOk)
		throw(eInvalidFormat);
	else
	{
		//i contem a ultima instrucao avaliada
		string code = replaceOperands(i.binFormat,opRegs,opLabels,opNumbers,opAddrs,registers,labels,addressings);
		Number n;
		unsigned char *code = n.toByteArray(code,size);
		return code;
	}
}
