#ifndef EXPRESSION_HPP
#define EXPRESSION_HPP

#include <list>

using namespace std;

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
	list<pair<string,string> > findAll(string phrase,string expression = "");

	private:

	string exp;
}

#endif // EXPRESSION_HPP

