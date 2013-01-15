#ifndef MEMORY_HPP
#define MEMORY_HPP

class Memory
{

	public:

	/**
	*	inicializa uma memoria com suporte de, pelo menos, size bytes
	*/
	Memory(unsigned int size);

	/**
	*	escreve um valor numa determinada posicao da memoria
	*/
	void writeValue(unsigned char value,unsigned int pos);

	/**
	*	escreve todos os valores do array a partir da posicao startPos na memoria
	*/
	void writeArray(unsigned char *array, unsigned int arraySize, unsigned int startPos);

	/**
	*	le um o valor que esta na posicao dada da memoria
	*/
	unsigned char readValue(unsigned int pos);

	/**
	*	le amount posicoes da memoria a partir da posicao dada, escrevendo os valores em array
	*/
	void readArray(unsigned char *array, unsigned int amount, unsigned int pos);

	/**
	*	retorna uma forma compactada da memoria
	*	o vetor retornado nao deve ser desalocado
	* (por enquanto, eh apenas o dump da memoria, sem nenhuma compactacao)
	*/
	unsigned char *pack(unsigned int *size);

	private:

	unsigned char *area;
	unsigned int size;

};

#endif // MEMORY_HPP

