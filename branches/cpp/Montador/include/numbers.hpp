#ifndef NUMBERS_HPP
#define NUMBERS_HPP

using namespace std;

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
	int toInt(string *n);

	/**
	*	converte o numero para um array de bytes com notacao big-endian
	*	o ultimo caractere determina o tipo do numero:
	*	b/B - binario
	*	d/D - decimal
	*	h/H - hexadecimal
	*	nada/algarismo - decimal
	*	escreve o numero de bytes do numero em size
	*/
	unsigned char *toByteArray(string *n, int *size);
};

#endif // NUMBERS_HPP

