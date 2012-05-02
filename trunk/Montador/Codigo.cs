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
         * limpa a linha, removendo os espaços nas pontas
         * e deixando-a no formato:
         * instrucao operandos
         * ex:
         * "    add a 127 ,x " -> "ADD A 127,X"
         * se a linha for um comentario, retorna a propria string, em maiusculas e sem os espacos nos cantos
         */
        private string limpaLinha(string linha)
        {
            //converte para maiusculas e remove os espacos nos cantos
            linha = linha.ToUpper().Trim();
            if (linha.Length == 0)
                return linha;
            if (linha[0] == COMENTARIO)
                return linha;

            //separa a linha em "instrucao [operandos...]"
            char[] whitespace = {' ','\t' };
            string[] palavras = linha.Split(whitespace);
            string op;
            linha = palavras[0];

            //deixa a linha no formato adequado
            for(int i = 1; i<palavras.Length;i++)
            {
                op = palavras[i];
                //se o caracter anteiror foi uma virgula, nao insere um espaco
                if (linha[linha.Length - 1] == ',')
                    linha += op;
                //se for uma virgula, cola-a na palavra anterior
                else if (op == ",")
                    linha += ',';
                else if (op != "")
                    linha += " " + op;
            }

            return linha;
        }



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
                    this.preprocessado.Add(limpaLinha(linha));
                }
            }

            //remove os comentarios e os espacos desnecessarios
            string l;
            for(int i = 0,max = this.preprocessado.Count;i<max;i++,linhaAtual++)
            {
                l = this.preprocessado[i];
                if (l.Length == 0)
                {
                    continue;
                }
                Console.WriteLine(l);
                //se for um comentario, remove
                if (l[0] == COMENTARIO)
                {
                    this.preprocessado.RemoveAt(i);
                    max--;
                    i--;
                }
                else
                {
                    //acrescenta o numero dessa linha
                    this.linhasFonte.Add(linhaAtual);
                }
            }

            return;

        }
    }
}
