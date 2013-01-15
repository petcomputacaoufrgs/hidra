#include <stdio.h>

using namespace std;

int main(int argc,char *argv[])
{
	bool help = false;

	if(argc <=3)
		help = true;
	else
	{
		int i;
		for(i=1;i<argc;i++)
		{

		}
	}

	//mostra mensagem de ajuda
	if(help)
	{
		printf("usage: hidrassembler [OPTIONS...] machine=<machine> src=<source file>\n\n");
		printf("available options:\n");
		printf("output=<filename>\t generated binary will be written to <filename> instead of stdout\n");
		printf("warnings=<filename>\t generated warnings will be written to <filename> instead of stderr\n");
		printf("errors=<filename>\t generated errors will be written to <filename> instead of stderr\n");
		printf("symbols=<filename>\t defined symbols will be written to <filename>\n");
	}

}
