Expression::Expression()
{

}

Expression::Expression(string expression)
{
	this->exp = expression;
}


/**
* se a frase satisfizer a expressao, faz o match entre as variaveis da frase com as da expressao
* se nenhuma expressao for passado, usa a do construtor
* retorna uma lista de pares onde o primeiro elemento eh a variavel e o segundo, seu tipo
* se a frase nao satisfizer, throws eUnmatchedExpression
*/
list<pair<string,string> > Expression::findAll(string phrase,string expression = "")
{
	list<pair<string,string> > vars;

	if(expression=="")
		expression=this->exp;




}
