#include <stdio.h>

#include <string>

#include "numbers.hpp"

using namespace std;

int main(int argc, char *argv[])
{
	if(argc != 2)
	{
		printf("usage: readnum <number>\n");
		return -1;
	}

	string *n = new string(argv[1]);

	Number *num = new Number();
	int value = num->toInt(*n);

	printf("%d\n",value);

	return 0;

}
