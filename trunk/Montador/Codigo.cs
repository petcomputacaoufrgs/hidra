using System;
using System.IO;
using System.Collections.Generic;

namespace Montador
{
    public class Codigo
    {
        const char COMENTARIO = ';';
		public enum Tipos { DEFLABEL, INSTRUCAO, DIRETIVA, REGISTRADOR, ENDERECO, INVALIDO };
		public List<Linha> linhas;	//cada linha do codigo fonte

		public Definicoes defs = new Definicoes();	//definicoes de labels
		
        public byte[] binario;

		

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
					if (c == ' ' || c == '\t')
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

		/*
		* identifica os tipos de cada elemento das linhas
		* altera os campos de this.linhas
		* exemplo:
		* ["label:", "JMP", "12,0Fh"] -> [DEFLABEL,INSTRUCAO,ENDERECO]
		*/
		public void identificaTipos(Gramatica gram)
		{
			for (int j = 0; j < this.linhas.Count;j++ )
			{
				Linha linha = this.linhas[j];
				for (int i = 0; i < linha.preprocessado.Length; i++)
				{
					gram.identificaTipo(ref linha, gram);
				}
			}
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
         *  escreve o conteudo das linhas
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
					Console.Write(w + " ");
				}
				Console.Write("\n");

				foreach(int t in linha.tipos)
					Console.Write(t + " ");
				Console.Write("\n");

				i++;
			}
			Console.WriteLine("*******");
		}

		/*
		 * monta o codigo,
		 * retorna o binario resultante, que nao necessariamente utiliza todo o espaco
		 * mas nao vai exceder o tamanho definido
		 */
		public byte[] montar(int tamanho,Linguagem linguagem)
		{
			byte[] memoria = new byte[tamanho];
			byte[] regMask,endMask;
			Instrucao inst;
			int b = 0;

			foreach(Linha linha in this.linhas)
			{
				for (int i = 0; i < linha.tipos.Length;i++ )
				{
					//definicao de label
					switch (linha.tipos[i])
					{
						case (int)Tipos.DEFLABEL:
							this.defs.atribuiDef(linha.nomes[i], b);
							break;
						case (int)Tipos.INSTRUCAO:
							inst = linguagem.instrucoes.Find(o => o.mnemonico == linha.preprocessado[i]);
							
							//determina a codificacao dos registradores e dos enderecos
							regMask = mascaraRegistradores(linha,linguagem);
							endMask = mascaraEnderecamentos(linha,linguagem);

							//faz um ou bitwise com cada uma das mascaras de codigo
							for (int k = 0; k < linha.bytes; k++)
							{
								memoria[b] = regMask[k] | endMask[k] | inst.codigo[k];
							}
							break;
					}
				}
			}
			return memoria;
		}


		/**
		 * determina a mascara de codigos dos registradores utilizados na linha
		 */
		public byte[] mascaraRegistradores(Linha linha,Linguagem lingua)
		{
			byte[] mascara = new byte[linha.bytes];

			for(int t=0;t<linha.tipos.Length;t++)
			{
				if (linha.tipos[t] == (int)Tipos.REGISTRADOR)
				{

				}
			}

			return mascara;

		}
    }
}
