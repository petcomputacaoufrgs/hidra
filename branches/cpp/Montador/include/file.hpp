#ifndef FILE_HPP
#define FILE_HPP

#include <string>
#include <list>

using namespace std;

/**
*	le todas as linhas do arquivo, criando uma lista com cada uma delas
*/
list<string> *fileReadLines(const char *filename);

/**
*	le todos os bytes do arquivo, escrevendo seu tamanho em size
* extra indica quantos bytes a mais serao deixados no final do arquivo (nao eh somado a size)
*/
char *fileRead(const char *filename,int *size,int extra);

#endif // FILE_HPP

