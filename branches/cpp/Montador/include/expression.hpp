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
}t_match;

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

