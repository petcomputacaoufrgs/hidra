using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Linha
	{
		public string[] preprocessado;	//a linha depois do preprocessamento
		public Gramatica.Tipos[] tipos;
		public Gramatica.SubTipos[] subTipos;
		public int linhaFonte;	//a linha correspondente no codigo fonte original
		public string[] nomes;	//os nomes dos enderecos,labels e registradores usados, na ordem em que aparecem
		public List<byte[]> enderecamento;	//o codigo do modo de cada um dos modos de enderecamento utilizados
		public int bytes = 1;

		public Linha(string[] preprocessado,int nlinha)
		{
			this.preprocessado = preprocessado;
			this.tipos = new Gramatica.Tipos[preprocessado.Length];
			this.subTipos = new Gramatica.SubTipos[preprocessado.Length];
			this.nomes = new string[preprocessado.Length];
			this.enderecamento = new List<byte[]>();

			this.linhaFonte = nlinha;
		}
	}
}
