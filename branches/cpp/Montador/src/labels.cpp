

#include <string>
#include <map>
#include <set>

#include "labels.hpp"

using namespace std;

/**
*	adiciona a definicao de uma label
*/
void Labels::defineLabel(string name,unsigned int pos)
{

	if(this->defs.find(name) == this->defs.end())
	{
		this->defs[name] = pos;
		//marca como nao referenciada
		this->unrefs.insert(name);
	}
}

/**
*	retorna a posicao em que a label foi definida
*	conta como uma referencia a label
*/
unsigned int Labels::value(string name)
{
	map<string,unsigned int>::iterator i = this->defs.find(name);
	if(i == this->defs.end())
	{
		//label referenciada mas nao definida
		return -1;
	}
	else
	{
		//marca a label como referenciada
		this->unrefs.erase(name);
		return i->second;
	}
}
