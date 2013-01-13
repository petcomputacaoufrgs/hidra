#ifndef MESSENGER_HPP
#define MESSENGER_HPP

typedef struct s_status
{
	int position;	//a posicao do proximo byte a ser escrito
	string *label;	//a ultima label lida (referencia ou definicao)
	string *mnemonic;	//mnemonico da ultima instrucao ou diretiva lida
	Labels *labels;	//todas as labels
	Instructions *insts; //todas as instrucoes
	Machine *machine;	//a maquina para a qual esta-se gerando o binario
}t_status;

class Messenger
{

	public:

	/**
	*	carrega as mensagens de erro e avisos de um arquivo
	*/
	void load(const char *filename);

	/**
	*	gera a mensagem com o codigo dado, escrevendo-a em stream
	*	usa as informacoes de status para gerar a mensagem
	*/
	void generateMessage(int code,t_status *status,FILE *stream);

};

#endif // MESSENGER_HPP
