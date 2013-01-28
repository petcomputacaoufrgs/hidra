#include <stdio.h>
#include <stdlib.h>

#include <string>
#include <list>

#include "stringer.hpp"
#include "file.hpp"

using namespace std;

/**
*	le todas as linhas do arquivo, criando uma lista com cada uma delas
*/
list<string> fileReadLines(const char *filename)
{
	int size;
	char *raw = fileRead(filename,&size,1);
	list<string> list;
	if(raw == NULL)
		return list;
	raw[size] = '\0';
	string text (raw);
	list = stringSplitChar(text,"\n\r");
	free(raw);

	return list;
}

/**
*	le todos os bytes do arquivo, escrevendo seu tamanho em size
* extra indica quantos bytes a mais serao deixados no final do arquivo (nao eh somado a size)
*/
char *fileRead(const char *filename,int *size,int extra)
{
	FILE *fl = fopen(filename,"rb");
	if(fl == NULL)
		return NULL;

	fseek(fl,0,SEEK_END);
	*size = ftell(fl);
	fseek(fl,0,SEEK_SET);

	char *data = (char *)malloc(*size+extra);
	fread(data,1,*size,fl);
	fclose(fl);
	return data;

}
