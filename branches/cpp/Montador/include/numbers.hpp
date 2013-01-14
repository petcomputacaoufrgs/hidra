#ifndef NUMBERS_HPP
#define NUMBERS_HPP

using namespace std;

typedef enum {BINARY,DECIMAL,HEXADECIMAL,INVALID} e_numType;

class Number
{
	public:
	Number();

	/**
	*	converte o numero para um inteiro
	*	caso seja maior que um, trunca-o, retornando somente os bits menos significativos
	* o ultimo caractere determina o tipo do numero:
	* b/B - binario
	* d/D - decimal
	* h/H - hexadecimal
	* nada/algarismo - decimal
	*/
	int toInt(string n);

	/**
	*	converte o numero para um array de bytes com notacao big-endian
	*	o ultimo caractere determina o tipo do numero:
	*	b/B - binario
	*	d/D - decimal
	*	h/H - hexadecimal
	*	nada/algarismo - decimal
	*	escreve o numero de bytes do numero em size
	*/
	unsigned char *toByteArray(string n, int *size);

	/**
	* determina o tipo do numero (decimal, binario ou hexadecimal),retornando-o
	* retorna INVALID se o numero nao estiver no formato adequado
	*/
	e_numType numberType(string n);

	/**
	* converte os caracteres do numero para seus respectivos valores
	* o vetor values deve ser grande o suficiente
	* o numero n deve ser valido
	*/
	void convertDigits(string n, unsigned char *values,e_numType type);
};

#endif // NUMBERS_HPP

