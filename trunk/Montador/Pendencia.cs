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

		public Pendencia(string nome, int nlinha, int nbyte)
		{
			this.nome = nome;
			this.nlinha = nlinha;
			this.nbyte = nbyte;
		}
	}
}
