using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
    class Instrucao
    {
        public string mnemonico;
        public int enderecos;   //numero de enderecos usados pela instrucao
        public int registradores;   //numero de registradores usados pela instrucao
        public int codigo; //codigo numerico da instrucao (nao inclui modo de enderecamento nem registradores usados. Pode-se usar OR bitwise para produzir o codigo binario final da instrucao)

    }
}
