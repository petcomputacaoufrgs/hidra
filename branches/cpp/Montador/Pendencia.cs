using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Pendencia
	{

		public string nome;
		public int nlinha;
		public int nbyte;
		public int tamanho;	//numero de bytes que o valor deve ter

		public Pendencia(string nome, int nlinha,int tamanho, int nbyte)
		{
			this.nome = nome;
			this.nlinha = nlinha;
			this.nbyte = nbyte;
			this.tamanho = tamanho;
		}
	}
}
