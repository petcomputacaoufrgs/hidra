using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Registrador
	{

		public string nome;
		public byte[] codigo;
		public int bits;

		public Registrador(string nome, byte[] codigo,int bits)
		{
			this.nome = nome;
			this.codigo = codigo;
			this.bits = bits;
		}

		public Registrador() 
		{ }

		public int regCompare(Registrador r0, Registrador r1)
		{
			return String.Compare(r0.nome, r1.nome);
		}
	}
}
