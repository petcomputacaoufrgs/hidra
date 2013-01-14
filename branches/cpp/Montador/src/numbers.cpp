#include <stdlib.h>
#include <stdio.h>

#include <string>

#include "numbers.hpp"
#include "defs.hpp"

using namespace std;

Number::Number(){}
/**
* determina o tipo do numero (decimal, binario ou hexadecimal),retornando-o
* retorna INVALID se o numero nao estiver no formato adequado
*/
e_numType Number::numberType(string n)
{
	unsigned int end = n.size()-1;
	char d = n[end];
	switch(d)
	{
		default:
			end++;
		case 'd':
		case 'D':
			end--;
			int i;
			for(i=end; i >0 ; i--)
			{
				if(n[i]<'0' || n[i]>'9')
					return INVALID;
			}
			if(n[0]=='-' || (n[0]>='0' && n[0]<='9'))
				return DECIMAL;
			else
				return INVALID;
			break;
		case 'b':
		case 'B':
			end--;
			for(i=end; i >0 ; i--)
			{
				if(n[i]<'0' || n[i]>'1')
					return INVALID;
			}
			if(n[0]=='-' || (n[0]>='0' && n[0]<='1'))
				return BINARY;
			else
				return INVALID;
			break;
		case 'h':
		case 'H':
			end--;
			for(i=end; i >0 ; i--)
			{
				if(n[i]>='0' && n[i]<='9')
					continue;
				else if(n[i]>='A' && n[i]<='F')
					continue;
				else if(n[i]>='a' && n[i]<='f')
					continue;
				else
					return INVALID;
			}
			if(n[0]=='-' || (n[0]>='0' && n[0]<='9'))
				return HEXADECIMAL;
			else
				return INVALID;
			break;
	}
}


/**
	* converte os caracteres do numero para seus respectivos valores
	* o vetor values deve ser grande o suficiente
	* o numero n deve ser valido
	*/
void Number::convertDigits(string n, unsigned char *values,e_numType type)
{
	int min = 0;
	int i;
	int max = n.size();
	switch(type)
	{
		case DECIMAL:
			if(n[max-1] == 'd' || n[max-1] == 'D')
				max--;
			if(n[0] == '-')
				min=1;

			for(i=min ; i<max ; i++)
				values[i] = n[i]-'0';
			break;
		case BINARY:
			if(n[0] == '-')
				min=1;

			int i;
			for(i=min ; i<max ; i++)
				values[i] = n[i]-'0';
			break;
		case HEXADECIMAL:
			if(n[0] == '-')
				min=1;

			for(i=min ; i<max ; i++)
			{
				if(n[i]>='0' && n[i]<='9')
					values[i] = n[i] - '0';
				else if(n[i]>='A' && n[i]<='F')
					values[i] = n[i] - 'A' + 10;
				else
					values[i] = n[i] - 'a' + 10;
			}
			break;
	}
}

/**
*	converte o numero para um inteiro
*	caso seja maior que um, trunca-o, retornando somente os bits menos significativos
* o ultimo caractere determina o tipo do numero:
* b/B - binario
* d/D - decimal
* h/H - hexadecimal
* nada/algarismo - decimal
* se for encontrado um numero desconhecido, retorna 0 (REVER ISSO)
*/
int Number::toInt(string n)
{
	int base;
	int power = 1;
	int end = n.size()-1;
	e_numType type = numberType(n);
	char last = n[end];
	switch(type)
	{
		case DECIMAL:
			if(last != 'd' && last!='D')
				end++;
			base = 10;
			break;
		case BINARY:
			base = 2;
			break;
		case HEXADECIMAL:
			base = 16;
			break;
		case INVALID:
			return 0;
	}
	int begin = 0;
	bool negative = false;
	if(n[0] == '-')
	{
		negative = true;
		begin = 1;
	}

	unsigned char values[n.size()];
	convertDigits(n,values,type);

	//ignora o digito que indica a base
	end--;
	int val = 0;
	//calcula o valor do numero
	int i;
	for(i=end; i>=begin ; i--)
	{
		val += values[i]*power;
		power *= base;
	}

	if(negative)
		val = -val;

	return val;
}
	/**
	*	converte o numero para um array de bytes com notacao little-endian
	*	o ultimo caractere determina o tipo do numero:
	*	b/B - binario
	*	d/D - decimal
	*	h/H - hexadecimal
	*	nada/algarismo - decimal
	*	escreve o numero de bytes do numero em size
	*	se o numero fo invalido, retorna NULL e escreve 0 em size
	*/
unsigned char *toByteArray(string n, int *size)
{

	unsigned char values[n.size()];
	char *number;
	e_numType type = numberType(n);

	if(type == INVALID)
	{
		*size = 0;
		return NULL;
	}

	convertDigits(n,values,type);

	int first;
	int last = n.size()-1;
	if(n[0] == '-')
		first=1;
	if(n[last] <'0' || n[last]>'9')
		last--;

	int size = last-first+1;
	int i;
	int max;
	int byte;
	switch(type)
	{
		case BINARY:

			//cada digito representa um bit
			//numero de bytes = digitos/8
			max = ceil(size/8.0);
			number = (char *)malloc(max);

			for(byte=0 ; byte<max ; byte++)
			{
				int pos = byte*8;
				number[byte] = 0;
				for(i=0 ; i<7 && pos<size ; i++,pos++)
				{
					number[byte] |= 1<<i*values[pos];
				}
			}

			break;
		case DECIMAL:
			//numero de bytes = log2(10^(digitos+1)-1)/8
			//                = (digitos+1)*log2(10)/8
			max = ceil(((size+1)*LOG10)/8);
			number = (char *)malloc(max);
			break;
		case HEXADECIMAL:
			//numero de bytes = digitos/2
			max = ceil(size/2.0)
			number = (char *)malloc(max);
			break;
	}

}





