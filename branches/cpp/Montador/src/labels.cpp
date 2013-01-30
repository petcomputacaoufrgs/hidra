

#include <string>
#include <map>
#include <set>

#include "labels.hpp"
#include "defs.hpp"

using namespace std;

/**
*	adiciona a definicao de uma label
*/
void Labels::defineLabel(string name,unsigned int pos,unsigned int line)
{

	if(this->defs.find(name) == this->defs.end())
	{
		this->defs[name] = pos;
		this->lines[name] = line;
		//marca como nao referenciada
		this->unrefs.insert(name);
	}
}

/**
*	retorna a posicao em que a label foi definida
*	conta como uma referencia a label
* se a label ainda nao foi definida, throws eUndefinedLabel
*/
unsigned int Labels::value(string name)
{
	map<string,unsigned int>::iterator i = this->defs.find(name);
	if(i == this->defs.end())
	{
		//label referenciada mas nao definida
		throw(eUndefinedLabel);
	}
	else
	{
		//marca a label como referenciada
		this->unrefs.erase(name);
		return i->second;
	}
}

/**
* retorna a linha em que a label foi definida
* nao conta como uma referencia
* se a label ainda nao foi definida, throws eUndefinedLabel
*/
unsigned int line(string name)
{
	map<string,unsigned int>::iterator i = this->lines.find(name);
	if(i == this->defs.end())
	{
		//label referenciada mas nao definida
		throw(eUndefinedLabel);
	}
	else
	{
		return i->second;
	}
}
