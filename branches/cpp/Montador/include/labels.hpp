#ifndef LABELS_HPP
#define LABELS_HPP

#include <string>
#include <map>
#include <set>

using namespace std;

class Labels
{

	public:

	/**
	*	adiciona a definicao de uma label
	*/
	void define(string name,unsigned int pos,unsigned int line);

	/**
	*	retorna a posicao em que a label foi definida
	*	conta como uma referencia a label
	*/
	unsigned int value(string name);

	/**
	* retorna a linha em que a label foi definida
	* nao conta como uma referencia
	*/
	unsigned int line(string name);

	private:

	map<string,unsigned int> defs;	//labels definidas
	map<string,unsigned int> lines;	//linhas em que as labels foram definidas
	set<string> unrefs;	//labels que ainda nao foram referenciadas

};
#endif // LABELS_HPP
