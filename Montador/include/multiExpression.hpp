#ifndef MULTIEXPRESSION_HPP
#define MULTIEXPRESSION_HPP

#include <list>

#include <boost/regex.hpp>

#include "expression.hpp"

using namespace std;

class MultiExpression
{

	public:
	/**
	  * todas as variaveis da expressao inicial correspondem a qualquer uma das subexpressoes
	  */
	MultiExpression(list<Expression> subexpressions,string expression);

	/**
    * se a frase satisfizer a expressao, faz o match entre as variaveis da frase com as da expressao
    * retorna uma lista dos matches feitos
    * se a frase nao satisfizer, throws eUnmatchedExpression
    */
	list<t_match > findAllSub(string phrase);

	protected:

	boost::regex regexp; //a expressao regular utilizada
	strin exp;           //a string da expressao regular
	char *vars;
	char *subvars;

};

#endif // MULTIEXPRESSION_HPP

