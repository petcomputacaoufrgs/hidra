using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Enderecamento
	{

		public string formato;
		public byte[] codigo;

		public Enderecamento(string formato, byte[] codigo)
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
			Gramatica gram = new Gramatica();
			int pos;
			int l =0, r =0;
			int pr = palavra.Length-1;
			string formato = "";
			Enderecamento end;

			for (pos = 0; pos < lista.Count; pos++)
			{
				end = lista[pos];

				formato = end.formato;
				l = 0;
				r = formato.Length - 1;

				//percorre o formato pelos dois lados ate encontrar o endereco,
				//representado pelo simbolo 'E'
				while (formato[l] != 'E' || formato[r] != 'E')
				{
					if (formato[l] != 'E')
					{
						if (palavra[l] != formato[l] && palavra[l] != ' ')
						{
							break;
						}
						l++;
					}
					if (formato[r] != 'E')
					{
						if (palavra[pr] != formato[r] && palavra[pr] != ' ')
							break;
						r--;
					}
					pr--;
				}
				if (formato[l] == 'E' && formato[r] == 'E')
				{
					if (gram.ehLabel(palavra,l,pr) || gram.ehNumero(palavra,l,pr) || gram.ehString(palavra,l,pr))
					{
						endereco = new String(palavra.ToCharArray(), l, pr - l+1);
						if(gram.ehLabel(endereco))
							return pos;
						if(gram.ehNumero(endereco))
							return pos;
						if (gram.ehString(endereco))
							return pos;
					}
				}
			}
			return -1;
		}
	}//class
}//namespace
