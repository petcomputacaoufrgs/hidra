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
		public int valor;
		public List<int> linhas;

		public Label(string nome)
		{
			this.nome = nome;
			this.linhas = new List<int>();
			this.linhaDef = -1;
			this.valor = -1;
		}

		public Label(string nome, int linha)
		{
			this.nome = nome;
			this.linhas = new List<int>();
			this.linhas.Add(linha);
			this.linhaDef = -1;
			this.valor = -1;
		}
	}
}
