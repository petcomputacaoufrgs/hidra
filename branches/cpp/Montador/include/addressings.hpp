#ifndef ADDRESSINGS_HPP
#define ADDRESSINGS_HPP

#include <string>

using namespace std;

class Addressings
{

	public:

	Addressings();

	/**
	*	carrega os modos de enderecamento que estao definidos na string config
	*/
	void load(string *config);

};

#endif // ADDRESSINGS_HPP
