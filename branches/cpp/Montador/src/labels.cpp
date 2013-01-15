

#include <string>

#include "labels.hpp"

using namespace std;

/**
*	adiciona a definicao de uma label
*/
void Labels::defineLabel(string name,unsigned int pos);

/**
*	retorna a posicao em que a label foi definida
*	conta como uma referencia a label
*/
unsigned int Labels::value(string name);
