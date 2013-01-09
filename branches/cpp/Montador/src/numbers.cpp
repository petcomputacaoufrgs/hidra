#include <stdlib.h>
#include <stdio.h>

#include <string>

#include <numbers.hpp>

using namespace std;

Number::Number(){}

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
int Number::toInt(string *number)
{
	string n = *number;
	//ultimo digito
	char num[n.size()];
	int end = n.size()-1;
	int begin = 0;
	char d = n[end];
	int power = 1;
	int base;
	bool negative = false;
	printf("d:%c\n",d);
	switch(d)
	{
		default:
			//se nao eh um algarismo
			if(d<'0' || d>'9')
				return 0;
			end++;
		case 'd':
		case 'D':
			base = 10;
			//converte para numeros
			unsigned int i;
			for(i=0;i< n.size(); i++)
			{
				if(n[i]>='0' && n[i]<='9')
					num[i] = n[i] -'0';
				else if(i==0 && n[i]=='-')
				{
					negative = true;
					begin++;
				}
				else
					return 0;
			}
			break;
		case 'b':
		case 'B':
			base = 2;
			//converte para numeros
			for(i=0;i< n.size()-1; i++)
			{
				if(n[i]>='0' && n[i]<='1')
					num[i] = n[i] -'0';
				else if(i==0 && n[i]=='-')
				{
					negative = true;
					begin++;
				}
				else
					return 0;
			}
			break;
		case 'h':
		case 'H':
			base = 16;
			//verifica se o primeiro digito eh um algarismo
			int temp = 0;
			if(n[temp] == '-')
				temp++;
			if(n[temp]<'0' || n[temp]>'9')
				return 0;

			//converte para numeros
			for(i=0;i< n.size()-1; i++)
			{
				if(n[i]>='0' && n[i]<='9')
					num[i] = n[i] -'0';
				else if(n[i] >='A' && n[i]<='F')
					num[i] = n[i] -'A' + 10;
				else if(n[i] >='a' && n[i]<='f')
					num[i] = n[i] -'a' + 10;
				else if(i==0 && n[i]=='-')
				{
					negative = true;
					begin++;
				}
				else
					return 0;
			}
			break;
	}

	//ignora o digito que indica a base
	end--;
	int val = 0;
	//calcula o valor do numero
	int i;
	for(i=end; i>=begin ; i--)
	{
		val += num[i]*power;
		power *= base;
	}

	if(negative)
		val = -val;

	return val;


}
