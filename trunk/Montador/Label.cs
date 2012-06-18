using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Label
	{
		public string nome;
		public int linhaDef = -1;	//a linha em que a label foi definida. sera -1 caso nao tenha sido definida
		public List<int> linhas = new List<int>();
		public int valor;

		public Label(string nome)
		{
			this.nome = nome;
		}
	}
}
