#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include <string>

#include "sha1.hpp"

#include "montador.hpp"

using namespace std;

/**
	*	cria um montador, especificando quais sao as propriedades da maquina/arquiterua para qual os codigos serao montados
	*/
	//Montador::Montador(Instructions *inst, Registers *reg, Machine *machine, Adressing *adr);

	//dumy
	Montador::Montador()
	{

	}

	/**
	*	monta o codigo assembly passado
	* escreve em size o tamanho da memoria
	* retorna a memoria gerada
	*/
	//char *Montador::assembleCode(string code,int *size);

	/**
	*	cria o arquivo binario para a memoria
	* o arquivo tera o seguinte formato:
	* primeiro byte: versao (0, no caso)
	* nome da maquina, terminado por um '\0'
	* SHA1 do resto do arquivo (20 bytes)
	* dump da memoria (size bytes)
	*/
	void Montador::createBinaryV0(string filename,string machineName,char *memory, int size)
	{
		FILE *fl = fopen(filename.c_str(),"wb");
		//escreve a versão
		char version = 0;
		fwrite(&version,1,1,fl);
		//nome da maquina
		fwrite(machineName.c_str(),1,machineName.size(),fl);
		//termina com '\0'
		char zero = '\0';
		fwrite(&zero,1,1,fl);
		//calcula o SHA1 do arquivo
		//comeca concatenando o arquivo
		char *cat = (char *)malloc(1+machineName.size()+1+size);
		unsigned int pos=0;
		cat[pos++] = 0;
		memcpy(cat+pos,machineName.c_str(),machineName.size());
		pos+=machineName.size();
		cat[pos++]='\0';
		//copia a memoria
		memcpy(cat+pos,memory,size);
		pos+=size;

		//calcula o SHA1
		SHA1 *shaCalc = new SHA1();
		shaCalc->Input(cat,pos);
		unsigned int *sha = (unsigned int *)malloc(20);	//SHA1 = 160 bits = 20 bytes
		shaCalc->Result(sha);

		//escreve o SHA1
		fwrite(sha,1,20,fl);

		//faz um dump da memoria
		fwrite(memory,1,size,fl);
		fclose(fl);
		delete shaCalc;
		free(cat);
		free(sha);
	}

	/**
	*	monta uma linha, escrevendo seu codigo binario a partir de memory[byte]
	* line eh a linha a ser montado
	* se houver alguma label que ainda nao foi definida, reserva espaco e adiciona a pendencia na pilha
	* se for encontrada a definicao de uma label, acrescenta-a as Labels conhecidas
	* retorna a posicao da memoria em que a proxima linha deve comecar
	*/
	//int Montador::assembleLine(string line, char *memory,int byte, stack<int,string> pendencies,Labels labels);
