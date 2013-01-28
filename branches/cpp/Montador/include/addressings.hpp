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

	private:

	map<string,t_addressing> addrs;

};

#endif // ADDRESSINGS_HPP
