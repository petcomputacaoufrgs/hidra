using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Enderecamento
	{

		public string formato;
		public int codigo;

		public Enderecamento(string formato, int codigo)
		{
			this.formato = formato;
			this.codigo = codigo;
		}

		public Enderecamento()
		{

		}

		/*
		 * identifica se a palavra satisfaz um dos enderecamentos
		 * retorna o indice na lista do enderecamento satisfeito,
		 * -1 caso nao satisfaca
		 * escreve o nome do endereco ou registrador em endereco se algum enderecamento for satisfeito,
		 * nao altera caso nao tenha sido encontrado nenhum enderecamento adequado
		 */
		public int identifica(string palavra, List<Enderecamento> lista, ref string endereco)
		{
			int pos;
			int l, r;
			string formato;
			Enderecamento end;

			for (pos = 0; pos < lista.Count; pos++)
			{
				end = lista[pos];

				formato = end.formato;
				l = 0;
				r = formato.Length - 1;

				//percorre o formato pelos dois lados ate encontrar o endereco,
				//representado pelo simbolo 'e'
				while (formato[l] != 'e' || formato[r] != 'e')
				{
					if (formato[l] != 'e')
					{
						if (palavra[l] != formato[l])
							break;
						l++;
					}
					if (formato[r] != 'e')
					{
						if (palavra[r] != formato[r])
							break;
						r--;
					}
				}
				if (formato[l] == 'e' && formato[r] == 'e')
				{
					endereco = new String(palavra.ToCharArray(), l, r - l);
					return pos;
				}
			}

			return -1;
		}
	}//class
}//namespace
