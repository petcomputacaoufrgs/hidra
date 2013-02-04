#ifndef ADDRESSINGS_HPP
#define ADDRESSINGS_HPP

#include <string>
#include <map>

using namespace std;

typedef struct s_addressing
{
	string exp;
	string code;
	bool relative;
}t_addressing;

class Addressings
{

	public:

	Addressings();

	/**
	*	carrega os modos de enderecamento que estao definidos na string config
	*/
	void load(string config);

	/**
	*	retorna a estrutura do modo de enderecamento com o nome dado
	*/
	t_addressing getAddressing(string name);

	private:

	map<string,t_addressing> addrs;	//associa o nome do modo de enderecamento com seus atributos

};

#endif // ADDRESSINGS_HPP
