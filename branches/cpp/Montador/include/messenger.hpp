#ifndef MESSENGER_HPP
#define MESSENGER_HPP

#include <string>
#include <list>
#include <map>

#include "instructions.hpp"
#include "addressings.hpp"
#include "registers.hpp"
#include "machine.hpp"
#include "labels.hpp"

using namespace std;

typedef struct s_status
{
	unsigned int position;	//a posicao do proximo byte a ser escrito
	unsigned int lastOrgLine;	//a linha do codigo fonte em que ocorreu o ultimo ORG
	int value;	//valor do operando, sem truncar
	unsigned int expectedOperands;
	unsigned int foundOperands;
	string operand;	//o operando junto com seu modo de enderecamento
	string label;	//a ultima label lida (referencia ou definicao)
	string mnemonic;	//mnemonico da ultima instrucao ou diretiva lida
	Labels *labels;	//todas as labels
	Instructions *insts; //todas as instrucoes
	Machine *machine;	//a maquina para a qual esta-se gerando o binario
}t_status;

typedef struct s_message
{
	string message;
	bool error;
}t_message;

class Messenger
{

	public:

	/**
	*	inicializa sem nenhuma mensagem
	*/
	Messenger();

	/**
	*	inicializa e carrega as mensagens do arquivo
	*/
	Messenger(const char *filename);

	~Messenger();

	/**
	*	carrega as mensagens de erro e avisos de um arquivo
	*/
	void load(const char *filename);

	/**
	*	gera a mensagem com o codigo dado, escrevendo-a em stream
	*	usa as informacoes de status para gerar a mensagem
	*/
	void generateMessage(unsigned int code,t_status *status,FILE *stream);

	private:

	/**
	*	atualiza o valor de todas as variaveis utilizadas que esta em this->variables
	*/
	void updateVariables(t_status *status);

	map<unsigned int, t_message> msgs;

	map<string,string> variables;	//asocia cada variavel a seu valor

};

#endif // MESSENGER_HPP
