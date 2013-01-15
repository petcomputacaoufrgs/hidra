#include <stdio.h>

#include <string>

#include "addressings.hpp"

using namespace std;

Addressings::Addressings()
{

}

/**
*	carrega os modos de enderecamento que estao definidos na string config
*/
void Addressings::load(string *config)
{
	printf("Addressings:\nLine:\n%s\n",config->c_str());
}
