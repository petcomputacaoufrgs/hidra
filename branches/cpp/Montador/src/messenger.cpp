#include <stdio.h>

#include <string>
#include <map>

#include "messenger.hpp"
#include "stringer.hpp"
#include "file.hpp"

using namespace std;

/**
*	inicializa sem nenhuma mensagem
*/
Messenger::Messenger()
{
	this->msgs = new map<unsigned int,t_message>();
}

Messenger::~Messenger()
{
	delete this->msgs;
}

/**
*	inicializa e carrega as mensagens do arquivo
*/
Messenger::Messenger(const char *filename)
{
	this->msgs = new map<unsigned int,t_message>();
	this->load(filename);
}

/**
*	carrega as mensagens de erro e avisos de um arquivo
*/
void Messenger::load(const char *filename)
{
	list<string> *lines = fileReadLines(filename);

	list<string>::iterator it;

	for(it=lines->begin() ; it!=lines->end() ; it++)
	{
		string line = stringTrim(*it," \t");

		if(line[0] == '#')
			continue;
		else
		{
			bool error;
			unsigned int i;
			//determina o numero da mensagem
			for(i=0 ; i<line.size() && line[i]!=' ' && line[i]!='\t' ; i++)
				;
			//se a linha terminou
			if(i==line.size())
				continue;

			unsigned int num;
			sscanf(line.substr(0,i).c_str(),"%u",&num);

			//determina se eh um erro ou aviso
			while(i<line.size() && (line[i]==' ' || line[i]=='\t'))
				i++;
			//se a linha terminou
			if(i==line.size())
				continue;

			if(line[i]=='e')
				error = true;
			else if(line[i] == 'w')
				error = false;
			else
				continue;

			//determina o inicio da mensagem
			for(i++ ; i<line.size() && (line[i]==' ' || line[i]=='\t') ; i++)
				;
			//se a linha terminou
			if(i==line.size())
				continue;

			//o restante da linha eh a mensagem
			t_message msg;
			msg.message = line.substr(i,line.size()-i);
			msg.error = error;

			this->msgs->insert(pair<unsigned int,t_message>(num,msg));
		}

	}
}

/**
*	gera a mensagem com o codigo dado, escrevendo-a em stream
*	usa as informacoes de status para gerar a mensagem
*/
void Messenger::generateMessage(unsigned int code,t_status *status,FILE *stream)
{

}













