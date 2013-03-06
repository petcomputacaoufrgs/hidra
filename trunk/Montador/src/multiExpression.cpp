

#include <boost/regex.hpp>

#include "expression.hpp"
#include "multiExpression.hpp"

	/**
	  * todas as variaveis da expressao inicial correspondem a qualquer uma das subexpressoes
	  */
	MultiExpression::MultiExpression(list<Expression> subexpressions,string expression)
	{
		//determina o numero de variaveis da expressao
		unsigned int amount=0;
		for({bool escape = false; unsigned int i=0} ; i<expression.size() ; i++)
		{
			if(escape)
			{
				escape = false;
				continue;
			}
		}
	}

	/**
    * se a frase satisfizer a expressao, faz o match entre as variaveis da frase com as da expressao
    * retorna uma lista dos matches feitos
    * se a frase nao satisfizer, throws eUnmatchedExpression
    */
	list<t_match > MultiExpression::findAllSub(string phrase);
