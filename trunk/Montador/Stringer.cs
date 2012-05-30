using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Stringer
	{
		/*
		 * le o conteúdo da primeira string, a partir de inicio, dentro de fonte
		 * e escreve a partir da posicao p do destino
		 * fonte[inicio-1] deve ser um caractere de inicio de string
		 */
		public void parse(string fonte, int inicio, char[] destino, ref int p)
		{
			char c;
			bool escape = false;
			char final = fonte[inicio-1];
			for (; inicio < fonte.Length; inicio++)
			{
				c = fonte[inicio];
				if (c == final && !escape)
				{
					break;
				}
				else
				{
					if (escape)
					{
						switch (c)
						{
							case '0':
								destino[p] = '\0';break;
							case 'a':
								destino[p] = '\a'; break;
							case 'b':
								destino[p] = '\b'; break;
							case 't':
								destino[p] = '\t'; break;
							case 'n':
								destino[p] = '\n'; break;
							case 'v':
								destino[p] = '\v'; break;
							case 'f':
								destino[p] = '\f'; break;
							case 'r':
								destino[p] = '\r'; break;
							case 'e':	//esc
								destino[p] = (char)(27);break;
							default:
								destino[p] = c;break;
						}

						p++;
						escape = false;
					}
					//caractere de escape
					else if (c == '\\')
					{
						escape = true;
					}
					else
					{
						destino[p] = c;
						p++;
					}
				}
			}
			//return inicio;
		}
	}
}
