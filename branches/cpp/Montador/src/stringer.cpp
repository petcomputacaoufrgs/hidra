#include <stdio.h>
#include <ctype.h>

#include <list>
#include <string>
#include <utility>

#include "stringer.hpp"
using namespace std;
/**
	*	quebra a string nos divisores passados
	* nao acrescenta elementos vazios a lista
	*/
list<string> stringSplitChar(string text, string dividers)
{
	list<string> sections;

	string::iterator it;
	int i=0,b = 0;
	for(it=text.begin();it!=text.end();it++,i++)
	{
		//se o caractere for um divisor
		if(stringIn(*it,dividers))
		{
			//se houver mais de um caractere para adicionar,adiciona
			if(i>b)
			{
				sections.push_back(text.substr(b,i-b));
			}
			b=i+1;
		}
	}
	//adiciona o ultimo elemento se este tiver mais de um caractere
	if(i>b)
		sections.push_back(text.substr(b,i-b));

	return sections;

}

/**
	*	remove todos os dividers que estiverem nos cantos de s, retornando a nova string
	*/
string stringTrim(string s,string dividers)
{
	int b=0,e = s.size();
	string::iterator begin = s.begin();
	string::reverse_iterator end = s.rbegin();

	//busca o primeiro caractere que nao eh divider
	while(stringIn(*begin,dividers)&&b<e)
	{
		b++;
		begin++;
	}

	//busca o ultimo caractere que nao eh divider
	while(stringIn(*end,dividers)&&b<e)
	{
		e--;
		end++;
	}

	if(b<e)
		return s.substr(b,e-b+1);
	else
		return "";

}

/**
	* verifica se c esta em s
	*/
bool stringIn(char c, string s)
{

	string::iterator it;
	bool found = false;
	for(it=s.begin();it!=s.end() && !found;it++)
	{
		if(c == *it)
			found = true;
	}

	return found;
}

/**
*	substitui cada ocorrencia da primeira string do par pela segunda, para cada par de elements
* retorna a nova string
*/
string stringReplaceAll(string s,list<pair<string,string> > elements)
{
	size_t pos;
	list<pair<string,string> >::iterator it;
	for(it=elements.begin() ; it!=elements.end() ; it++)
	{
		pos = s.find(it->first);

		//npos: valor de retorno de string::find quando nao eh encontrado
		while(pos != string::npos)
		{
			s = s.replace(s.begin()+pos,s.begin()+it->first.size()+pos,it->second);
			pos = s.find(it->first,pos+it->second.size());
		}
	}

	return s;
}

string stringReplaceAll(string s,map<string,string> elements)
{
	size_t pos;
	map<string,string >::iterator it;
	for(it=elements.begin() ; it!=elements.end() ; it++)
	{
		pos = s.find(it->first);

		//npos: valor de retorno de string::find quando nao eh encontrado
		while(pos != string::npos)
		{
			s = s.replace(s.begin()+pos,s.begin()+it->first.size()+pos,it->second);
			pos = s.find(it->first,pos+it->second.size());
		}
	}

	return s;
}

/**
*	compara duas strings ignorando maiusculas e minusculas
*	retorna 0 se forem iguais, >0 se a >b, <0 se a<b
*/
int stringCaselessCompare(string a, string b)
{
	unsigned int i;
	for(i=0 ; i<a.size() ; i++)
	{
		char d = tolower(a[i]);
		char e = tolower(b[i]);
		int dif = d-e;
		if(dif != 0)
			return dif;
	}
	if(a.size() < b.size())
		return 1;
	else
		return 0;
}








