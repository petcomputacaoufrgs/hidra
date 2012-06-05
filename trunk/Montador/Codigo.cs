using System;
using System.IO;
using System.Collections.Generic;

namespace Montador
{
    public class Codigo
    {
        const char COMENTARIO = ';';

		public List<Linha> linhas;	//cada linha do codigo fonte

		/**
         * codigo binario ja montado
         */
        public byte[] binario;

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
			Stringer parser = new Stringer();
			char c;
			bool espaco = true;
			bool escape = false; //se o caractere anterior era de escape
			bool copia = false;
			int p = 0;
			char final = '\'';
			char[] parsed = new char[linha.Length+1];

			int prev = 0; //indice do inicio do elemento
			List<string> elem = new List<string>();
			for(int i =0;i<linha.Length;i++)
			{
				c = linha[i];

				if (copia)
				{
					parsed[p++] = c;
					if (!escape)
					{
						//se for um caractere de escape
						if (c == '\\')
							escape = true;
						//se foi encontrado o final da string
						if (c == final)
						{
							copia = false;
						}
					}
					else
					{
						escape = false;
					}
				}
				else
				{
					//se for um espaco, verifica se ele deve ser copiado ou removido
					if (c == ' ' || c == '\t' || c == ',')
					{
						//se o anterior nao era um espaco, copia
						if (!espaco)
						{
							elem.Add(new string(parsed, prev, p - prev));
							parsed[p++] = ' ';
						}
						espaco = true;
					}
					//se for o início de uma string
					else
					{
						//se estavamos copiando espacos e paramos, aqui eh o inicio de um novo elemento
						if (espaco)
							prev = p;
						if (c == '\"' || c == '\'')
						{
							//prev = p;
							copia = true;
							espaco = false;
							parsed[p++] = c;
							final = c;
						}

						//se for um comentario, remove
						else if (c == ';')
							break;
						else
						{
							espaco = false;
							parsed[p++] = Char.ToUpper(c);
						}
					}
				}
			}

			if(!espaco)
				elem.Add(new string(parsed, prev, p - prev));
			
			string[] result = new string[elem.Count];
			for (int i = 0; i < elem.Count; i++)
			{
				result[i] = elem[i];
			}

			return result;

        }



		/**
		 * le o codigo de um arquivo, removendo os comentarios e espacos desnecessarios
		 * atualiza o conteudo de:
		 * Codigo.linhasFonte
		 * Codigo.preprocessado
		 */
		public void lerCodigo(string arquivo)
        {

            this.linhas = new List<Linha>();
			string linha;
			string[] clean;
            int linhaAtual = 1;

            //le o arquivo, removendo espacos antes e depois de cada linha
            using (StreamReader file = new StreamReader(arquivo))
            {
                while ((linha = file.ReadLine()) != null)
                {
					clean = limpaLinha(linha);
					if (clean.Length > 0)
					{
						this.linhas.Add(new Linha(clean, linhaAtual));
					}
					linhaAtual++;
                }
            }

			foreach (Linha l in this.linhas)
			{
				foreach (string w in l.preprocessado)
				{
					Console.Write(w + " ");
				}
				Console.Write("\n");
			}

			/*
            //remove os comentarios
            string l;
            for(int i = 0,max = this.linhas.Count;i<max;i++,linhaAtual++)
            {
                l = this.linhas[i].preprocessado[0];
                if (l.Length == 0)
                {
                    this.linhas.RemoveAt(i);
                    i--;
                    max--;
                    continue;
                }
                //se for um comentario, remove
                if (l[0] == COMENTARIO || l == "")
                {
                    this.linhas.RemoveAt(i);
                    max--;
                    i--;
                }
                else
                {
                    
                }
            }
			*/
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

			for (int i = 0; i < this.linhas.Count; i++)
			{
				for (int j = 0; j < this.linhas[i].preprocessado.Length; j++)
					Console.Write(this.linhas[i].preprocessado[j]+" ");
				Console.WriteLine();
			}

			//verifica se os tipos de cada linha são validos
			for (int i = 0; i < this.linhas.Count; i++)
			{
				gramatica.verificaTipos(this.linhas[i],saida,this.defs);
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
            foreach (Linha linha in this.linhas)
            {
                for(int i = 0;i<linha.preprocessado.Length;i++)
                {
                    numeroHexa = gram.substringHexa(linha.preprocessado[i]);

                    if (numeroHexa != "")
                    {
                        numero = gram.hexa2int(numeroHexa);
                        linha.preprocessado[i] = linha.preprocessado[i].Replace(numeroHexa+"H", numero.ToString());
                    }
                }
            }
        }

        /*
         *  escreve o conteudo as linhas preprocessadas
         */
        public void print()
        {
			int i = 0;
            Console.WriteLine("*******");
            foreach (Linha linha in this.linhas)
            {
				Console.Write(this.linhas[i].linhaFonte + ":\t");
                foreach (string w in linha.preprocessado)
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
            foreach (Linha linha in this.linhas)
            {
                for (int i = 0; i < linha.preprocessado.Length;i++)
                {
                    linha.tipos[linha.preprocessado.Length - 1] = gram.identificaTipo(linha.preprocessado[i],gram);
                    //Console.WriteLine(linha[i] + "\t" + this.tipos[tipos.Count - 1][i]);
                }
            }
        }
    }
}
