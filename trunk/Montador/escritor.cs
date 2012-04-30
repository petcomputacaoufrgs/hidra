using System;
using System.IO;

namespace Montador
{
    public class Escritor : Dados
    {
        public const int ERRO = 0;
        public const int AVISO = 1;

        /**
         * tipo: ERRO ou AVISO
         * nlinha eh o numero da linha em que ocorreu o erro/aviso
         * escreve o erro ou aviso no arquivo dado
         * formato:
         * TIPO: descricao (linha nlinha)
         */
        public void errorOut(int tipo, int nlinha, string descricao, string arquivo)
        {
            string erro = "ERRO: ";
            string aviso = "AVISO: ";

            string linha = "(linha )".Insert(7, nlinha.ToString());
            string texto = "";

            //determina se eh um erro ou um aviso
            switch (tipo)
            {
                case ERRO:
                    texto = erro;
                    break;
                case AVISO:
                    texto = aviso;
                    break;
            }

            //acrescenta a descricao do erro e a linha em que ocorreu
            texto = texto + descricao + linha;

            //abre o arquivo para appending
            System.IO.StreamWriter file = new System.IO.StreamWriter(arquivo,true);
            try
            {
                file.WriteLine(texto);
            }
            finally
            {
                file.Close();
            }
        }

        /**
         * escreve o arquivo binario para a maquina dada
         * 
         */
        public void escreveBinario(int maquina, byte[] binario, string arquivo)
        {

        }

    }
}