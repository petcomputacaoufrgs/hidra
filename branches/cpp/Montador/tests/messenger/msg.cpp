#include <stdio.h>

#include "messenger.hpp"

int main(int argc, char *argv[])
{
	if(argc != 2)
	{
		printf("usage: messenger <filename>\n");
		return 1;
	}

	Messenger msg (argv[1]);

	return 0;
}
