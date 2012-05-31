using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	class Enderecamento
	{

		public string formato;
		public int codigo;

		public Enderecamento(string formato, int codigo)
		{
			this.formato = formato;
			this.codigo = codigo;
		}

		public Enderecamento();

		/*
		 * identifica se a palavra satisfaz um dos enderecamentos
		 * retorna o indice na lista do enderecamento adequado se satisfizer,
		 * -1 caso nao satisfaca
		 */
		public int identifica(string palavra, List<Enderecamento> lista)
		{

		}

	}
}
