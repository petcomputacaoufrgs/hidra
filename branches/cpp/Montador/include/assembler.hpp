#ifndef ASSEMBLER_HPP
#define ASSEMBLER_HPP

#include <string>
#include <stack>
#include <list>

using namespace std;

//dummy
typedef int Labels;
typedef int Adressing;
typedef int Machine;
typedef int Registers;
typedef int Instructions;

typedef enum {CAT_NONE,CAT_INST,CAT_ADDR,CAT_REGI} e_category;

class Assembler
{

	public:
	/**
	*	cria um montador, especificando quais sao as propriedades da maquina/arquiterua para qual os codigos serao montados
	*/
	Assembler(Instructions *inst, Registers *reg, Machine *machine, Adressing *adr);

	/**
	*	le as caracteristicas da arquitetura que estao no arquivo dado
	*/
	Assembler::Assembler(const char *filename);

	//dummy
	Assembler();

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
	Instructions *inst;
	Registers *regs;
	Adressing *addr;
	Machine *mach;

	/**
	*	monta uma linha, escrevendo seu codigo binario a partir de memory[byte]
	* line eh a linha a ser montado
	* se houver alguma label que ainda nao foi definida, reserva espaco e adiciona a pendencia na pilha
	* se for encontrada a definicao de uma label, acrescenta-a as Labels conhecidas
	* retorna a posicao da memoria em que a proxima linha deve comecar
	*/
	int assembleLine(string line, char *memory,int byte, stack<int,string> pendencies,Labels labels);

};

#endif // ASSEMBLER_HPP

