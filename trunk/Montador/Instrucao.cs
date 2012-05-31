using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
    public class Instrucao
    {
        public string mnemonico;
		public int[] formato;	//formato da instrucao. usar os codigos de Gramatica.Tipos
        public int codigo; //codigo numerico da instrucao (nao inclui modo de enderecamento nem registradores usados. Pode-se usar OR bitwise para produzir o codigo binario final da instrucao)

        public Instrucao(string mnemonico, int[] formato, int codigo)
        {
			this.mnemonico = mnemonico;
			this.formato = formato;
			this.codigo = codigo;
        }
		public Instrucao();

		public int instCompare(Instrucao i0, Instrucao i1)
		{
			return String.Compare(i0.mnemonico, i1.mnemonico);
		}
    }
}
