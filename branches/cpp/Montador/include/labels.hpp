#ifndef LABELS_HPP
#define LABELS_HPP

#include <string>

using namespace std;

class Labels
{

	public:

	/**
	*	adiciona a definicao de uma label
	*/
	void defineLabel(string name,unsigned int pos);

	/**
	*	retorna a posicao em que a label foi definida
	*	conta como uma referencia a label
	*/
	unsigned int value(string name);

};
#endif // LABELS_HPP
