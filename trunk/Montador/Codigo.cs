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
         * contem cada linha do codigo apos o preprocessamento (em maiusculas), dividida em palavras
         * cada linha pode ou nao comecar com uma label
         */
        public List<string[]> preprocessado;

        /**
         * codigo binario ja montado
         */
        public byte[] binario;

        /*
         * os tipos de cada palavra
         */
        public List<int[]> tipos;

		public Definicoes defs = new Definicoes();


        /**
         * limpa a linha, removendo os espaços nas pontas
         * e deixando-a no formato:
         * instrucao operandos
         * ex:
         * "    add a 127 ,x " -> {"ADD", "A", "127,X"}
         * "label   :" -> {"label:"}
         * se a linha for um comentario, retorna um array com a propria string
         */
        private string[] limpaLinha(string linha)
        {
			//verifica se ha strings no meio da linha


            //converte para maiusculas e remove os espacos nos cantos
            linha = linha.ToUpper().Trim();
            string[] dividido = {linha};
            if (linha.Length == 0)
                return dividido;
            if (linha[0] == COMENTARIO)
                return dividido;

            //separa a linha em "instrucao [operandos...]"
            char[] whitespace = {' ','\t','\n',',' };
            string[] palavras = linha.Split(whitespace);
            string op;
            int total = 0;  //numero de palavras na linha
            linha = palavras[0];

            //deixa a linha no formato adequado
            for(int i = 1; i<palavras.Length;i++)
            {
                op = palavras[i];
                                
				//se for ':', cola-o na palavra anterior
				if (op == ":")
					linha += op;
				//se for um comentario, remove tudo a direita
				else if (op.IndexOf(COMENTARIO) >= 0)
					break;
				else if (op != "")
				{
					linha += " " + op;
					total++;
				}

            }

            //divide a linha em palavras
            dividido = new string[total];
            dividido = linha.Split(whitespace);

            return dividido;
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
            this.preprocessado = new List<string[]>();
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

            //remove os comentarios
            string l;
            for(int i = 0,max = this.preprocessado.Count;i<max;i++,linhaAtual++)
            {
                l = this.preprocessado[i][0];
                if (l.Length == 0)
                {
                    this.preprocessado.RemoveAt(i);
                    i--;
                    max--;
                    continue;
                }
                //se for um comentario, remove
                if (l[0] == COMENTARIO || l == "")
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

        /**
         * determina se o codigo eh valido
         * 
         * retorna true se for
         */
        public Boolean ehValido(Gramatica gramatica,Escritor saida)
        {
			this.identificaTipos(gramatica);

			for (int i = 0; i < this.tipos.Count; i++)
			{
				for (int j = 0; j < this.tipos[i].Length; j++)
					Console.Write(this.tipos[i][j]+" ");
				Console.WriteLine();
			}

			//verifica se os tipos de cada linha são validos
			for (int i = 0; i < this.tipos.Count; i++)
			{
				gramatica.verificaTipos(this.tipos[i],this.preprocessado[i],this.linhasFonte[i],saida,this.defs);
			}
				

					return true;
        }

        /**
         * converte todas as ocorrencias de numeros em hexadecimal para numeros em decimal
         * altera this.preprocessado
         * TODO: modificar apenas nos campos de endereco para nao alterar definicoes de labels
         * chamar esta funcao depois de substituir o valor das labels no codigo
         */
        public void converteHexa()
        {
            Gramatica gram = new Gramatica();
            string numeroHexa;
            int numero;

            //itera por todas as linhas do fonte
            foreach (string[] linha in this.preprocessado)
            {
                for(int i = 0;i<linha.Length;i++)
                {
                    numeroHexa = gram.substringHexa(linha[i]);

                    if (numeroHexa != "")
                    {
                        numero = gram.hexa2int(numeroHexa);
                        linha[i] = linha[i].Replace(numeroHexa+"H", numero.ToString());
                    }
                }
            }
        }

        /*
         *  escreve o conteudo de this.preprocessado na tela
         */
        public void print()
        {
			int i = 0;
            Console.WriteLine("*******");
            foreach (string[] linha in this.preprocessado)
            {
				Console.Write(this.linhasFonte[i] + ":\t");
                foreach (string w in linha)
                {
                    Console.Write(w+" ");
                }
                Console.Write("\n");
				i++;
            }
            Console.WriteLine("*******");
        }

        /*
         * identifica os tipos de cada elemento das linhas de this.preprocessado
         * altera this.tipos com os devidos codigos
         * exemplo:
         * ["label:", "JMP", "12,0Fh"] -> [DEFLABEL,INSTRUCAO,ENDERECO]
         */
        public void identificaTipos(Gramatica gram)
        {
            this.tipos = new List<int[]>();
            foreach (string[] linha in this.preprocessado)
            {
                tipos.Add(new int[linha.Length]);
                for (int i = 0; i < linha.Length;i++)
                {
                    this.tipos[tipos.Count - 1][i] = gram.identificaTipo(linha[i],gram);
                    //Console.WriteLine(linha[i] + "\t" + this.tipos[tipos.Count - 1][i]);
                }
            }
        }
    }
}
