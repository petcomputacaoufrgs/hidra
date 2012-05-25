using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	class Label
	{
		public string nome;
		public int linha;
		public int valor;

		public Label(string nome, int linha)
		{
			this.nome = nome;
			this.linha = linha;
		}
	}
}
