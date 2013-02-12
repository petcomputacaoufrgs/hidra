/**
* Copyright 2013 Marcelo Millani
*	This file is part of hidrasm.
*
* hidrasm is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* hidrasm is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with hidrasm.  If not, see <http://www.gnu.org/licenses/>
*/

#ifndef EXPRESSION_HPP
#define EXPRESSION_HPP

#include <list>

typedef enum {VAR_REGISTER,VAR_ADDRESS,VAR_LABEL,VAR_NUMBER,VAR_ANYTHING,VAR_TOTAL} e_expVar;

using namespace std;

typedef struct s_match
{
	unsigned char type;	//tipo da variavel na expressao
	unsigned char subtype[VAR_TOTAL]; //tipo da variavel nas subexpressoes
	string element;	//a variavel encontrada
	string subCode[VAR_TOTAL]; //codigo da subexpressao
	bool relative[VAR_TOTAL];	//se o enderecamento eh relativo ao PC ou nao
}t_match;

typedef struct s_
{
	unsigned int t;	//indice da letra da frase
	unsigned int e; //indice da letra da expressao
	unsigned char *expression;
}t_

class Expression
{
	public:
	Expression();

	Expression(string expression);

	/**
	* se a frase satisfizer a expressao, faz o match entre as variaveis da frase com as da expressao
	* se nenhuma expressao for passado, usa a do construtor
	* retorna uma lista de pares onde o primeiro elemento eh a variavel e o segundo, seu tipo
	* se a frase nao satisfizer, throws eUnmatchedExpression
	*/
	list<pair<string,char> > findAll(string phrase,string expression = "");

	/**
	* todas as variaveis da expressao inicial correspondem a qualquer uma das subexpressoes
	* se a frase satisfizer a expressao, faz o match entre as variaveis da frase com as da expressao
	* se nenhuma expressao for passado, usa a do construtor
	* retorna uma lista de pares onde o primeiro elemento eh a variavel e o segundo, seu tipo
	* se a frase nao satisfizer, throws eUnmatchedExpression
	*/
	list<t_match > findAllSub(string phrase, list<string> subexpressions, string expression);

	/**
	* retorna a string da expressao
	*/
	string expression();

	private:

	string exp;
};


/**
* verifica se o caractere passado eh um dos reservados para variaveis
*/
bool isVarChar(char c);

/**
* verifica se o caractere eh reservado
*/
bool isReserved(char c);

#endif // EXPRESSION_HPP

