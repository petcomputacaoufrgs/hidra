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
		 * escreve o nome do endereco ou registrador em endereco e o respectivo codigo se algum enderecamento for satisfeito,
		 * nao altera caso nao tenha sido encontrado nenhum enderecamento adequado
		 */
		public int identifica(string palavra, List<Enderecamento> lista, ref string endereco,ref byte[] enderecamento,ref Gramatica.SubTipos subt)
		{
			Gramatica gram = new Gramatica();
			int pos;
			int l =0, r =0;
			int pr = palavra.Length-1;
			string formato = "";
			Enderecamento end;

			if (palavra.Length == 0)
				return (int)Gramatica.Tipos.INVALIDO;
			for (pos = 0; pos < lista.Count; pos++)
			{
				end = lista[pos];

				formato = end.formato;
				l = 0;
				r = formato.Length - 1;

				//percorre o formato pelos dois lados ate encontrar o endereco,
				//representado pelo simbolo 'E'
				pr = palavra.Length - 1;
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
						pr--;
					}
				}
				if (formato[l] == 'E' && formato[r] == 'E')
				{
					if (endereco != null)
					{
						endereco = new String(palavra.ToCharArray(), l, pr - l + 1);

						subt = Gramatica.SubTipos.NONE;
						if (gram.ehLabel(endereco))
							subt = Gramatica.SubTipos.LABEL;
						else if(gram.ehNumero(endereco))
							subt = Gramatica.SubTipos.NUMERO;
						else if(gram.ehString(endereco))
							subt = Gramatica.SubTipos.STRING;
						
						if (subt != Gramatica.SubTipos.NONE)
						{
							if (enderecamento.Length < end.codigo.Length)
							{
								Array.Resize<byte>(ref enderecamento, end.codigo.Length);
							}
							for (int k = end.codigo.Length - 1, e = enderecamento.Length - 1; k >= 0; k--, e--)
								enderecamento[e] |= end.codigo[k];
							return pos;
						}
					}
					else
					{
						if (enderecamento.Length < end.codigo.Length)
						{
							Array.Resize<byte>(ref enderecamento, end.codigo.Length);
						}
						for (int k = end.codigo.Length - 1, e = enderecamento.Length - 1; k >= 0; k--, e--)
							enderecamento[e] |= end.codigo[k];
					}
				}
			}
			return -1;
		}
	}//class
}//namespace
