using System;
using System.IO;
using System.Collections.Generic;

namespace Montador
{
    public class Codigo
    {
        const char COMENTARIO = ';';

        //contem a linha correspondente ao codigo fonte puro
        //o indice eh a linha do fonte preprocessado (sem os comentarios etc...)
        public List<int> linhasFonte;
        /**
         * contem cada linha do codigo apos o preprocessamento (em maiusculas)
         * cada linha pode ser:
         * uma label ou
         * uma instrucao seguida de seus devidos operandos ou
         * uma diretiva do preprocessador (ORG, db ...)
         */
        public List<string> preprocessado;

        /**
         * codigo binario ja montado
         */
        public byte[] binario;

        /**
         * le o codigo de um arquivo, removendo os comentarios e espacos desnecessarios
         * atualiza o conteudo de:
         * Codigo.linhasFonte
         * Codigo.preprocessado
         */
        public void lerCodigo(string arquivo)
        {

            this.linhasFonte = new List<int>();
            this.preprocessado = new List<string>();
            string linha;

            int linhaAtual = 1;

            //le o arquivo, removendo espacos antes e depois de cada linha
            using (StreamReader file = new StreamReader(arquivo))
            {
                while ((linha = file.ReadLine()) != null)
                {
                    this.preprocessado.Add(linha.Trim().ToUpper());
                }
            }

            //remove os comentarios e os espacos desnecessarios
            string l;
            char[] whiteSpaces = { ' ', '\t' };
            for(int i = 0,max = this.preprocessado.Count;i<max;i++)
            {
                l = this.preprocessado[i];
                //se for um comentario, remove
                if (l[0] == COMENTARIO)
                {
                    this.preprocessado.RemoveAt(i);
                    i--;
                }
                else
                {
                    this.linhasFonte.Add(linhaAtual);
                    linhaAtual++;
                }
            }

            foreach (string la in this.preprocessado)
            {
                Console.WriteLine(la);
            }

            return;

        }
    }
}
