using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
    public class Instrucao
    {
        public string mnemonico;
		public Gramatica.Tipos[] formato;	//formato da instrucao.
        public byte[] codigo; //codigo numerico da instrucao (nao inclui modo de enderecamento nem registradores usados. Pode-se usar OR bitwise para produzir o codigo binario final da instrucao)

        public Instrucao(string mnemonico, Gramatica.Tipos[] formato, byte[] codigo)
        {
			this.mnemonico = mnemonico;
			this.formato = formato;
			this.codigo = codigo;
        }
		public Instrucao()
		{

		}

		public int instCompare(Instrucao i0, Instrucao i1)
		{
			return String.Compare(i0.mnemonico, i1.mnemonico);
		}
    }
}
