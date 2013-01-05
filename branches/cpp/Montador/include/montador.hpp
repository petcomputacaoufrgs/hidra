#ifndef MONTADOR_HPP
#define MONTADOR_HPP


class Montador
{

	public:
	/**
	*	cria um montador, especificando quais sao as propriedades da maquina/arquiterua para qual os codigos serao montados
	*/
	Montador(Instructions *inst, Registers *reg, Machine *machine, Adressing *adr);

	/**
	*	monta o codigo assembly passado
	* escreve em size o tamanho da memoria
	* retorna a memoria gerada
	*/
	char *assembleCode(string code,int *size);

	/**
	*	cria o arquivo binario para a memoria
	* o arquivo tera o seguinte formato:
	* primeiro byte: versao (0, no caso)
	* nome da maquina, terminado por um '\0'
	* md5 do resto do arquivo (16 bytes)
	* dump da memoria (size bytes)
	*/
	void createBinaryV0(string filename,string machineName,char *memory, int size);

	private:

	stack<int,string> *pendecies; //bytes em que ha labels pendentes
	Labels *labels; //labels definidas

	/**
	*	monta uma linha, escrevendo seu codigo binario a partir de memory[byte]
	* line eh a linha a ser montado
	* se houver alguma label que ainda nao foi definida, reserva espaco e adiciona a pendencia na pilha
	* se for encontrada a definicao de uma label, acrescenta-a as Labels conhecidas
	* retorna a posicao da memoria em que a proxima linha deve comecar
	*/
	int assembleLine(string line, char *memory,int byte, stack<int,string> pendencies,Labels labels);

}

#endif // MONTADOR_HPP

