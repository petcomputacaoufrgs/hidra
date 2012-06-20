using System;
using System.IO;
using System.Collections.Generic;

namespace Montador
{
    public class Codigo
    {
        const char COMENTARIO = ';';
		enum Estado {OK,TRUNCADO, INDEFINIDO};	//estado da geração do binário de um endereço
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
		public byte[] montar(int tamanho,Linguagem linguagem,Escritor saida)
		{
			Gramatica gram = new Gramatica();
			byte[] memoria = new byte[tamanho];
			byte[] regMask,endMask;
			byte[] endereco;
			Instrucao inst;
			int b = 0;
			int num;
			Stack<Pendencia> pendencias = new Stack<Pendencia>();
			Label label;

			int l = 0;
			int i;
			for(Linha linha = this.linhas[0];l<this.linhas.Count;l++, linha = this.linhas[l])
			{
				i=0;
				if(linha.tipos[i] == (int)Gramatica.Tipos.DEFLABEL)
				{
					this.defs.atribuiDef(linha.nomes[i], b);
					i++;
				}
				//definicao de label
				switch (linha.tipos[i])
				{
					case (int)Gramatica.Tipos.INSTRUCAO:
						inst = linguagem.instrucoes.Find(o => o.mnemonico == linha.preprocessado[i]);
							
						//determina a codificacao dos registradores e dos enderecos
						regMask = mascaraRegistradores(linha,linguagem);
						endMask = mascaraEnderecamentos(linha,linguagem);

						//faz um ou bitwise com cada uma das mascaras de codigo
						for (int k = 0; k < linha.bytes; k++)
						{
							memoria[b] = (byte)(regMask[k] | endMask[k] | inst.codigo[k]);
						}

						//escreve os enderecos utilizados
						b += linha.bytes;

						for(int k=i+1;k<linha.tipos.Length;k++)
						{
							if (linha.tipos[k] == (int)Gramatica.Tipos.ENDERECO)
							{
								//verifica se o endereco eh uma label
								int pos = this.defs.labels.FindIndex(o => o.nome == linha.nomes[k]);
								if (pos >= 0)
								{
									label = this.defs.labels[i];
									//se a label ja foi definida
									if (label.valor >= 0)
									{
										endereco = gram.num2byteArray(label.valor);
										//escreve o endereco
										foreach (byte e in endereco)
										{
											memoria[b] = e;
											b++;
										}
									}
									//adiciona na pilha as labels referenciadas que ainda nao foram definidas
									else
									{
										pendencias.Push(new Pendencia(linha.nomes[k], l,b));
										b += linguagem.tamanhoEndereco;
									}
								}
								else
								{
									num = gram.paraInteiro(linha.nomes[k]);
									endereco = gram.num2byteArray(num);
									//escreve o endereco
									foreach (byte e in endereco)
									{
										memoria[b] = e;
										b++;
									}
								}
							}
						}
						break;
					case (int)Gramatica.Tipos.DIRETIVA:
						string tipo = linha.nomes[i];
						i++;
						endereco = this.converteByteArray(linha.nomes[i], linguagem.tamanhoEndereco);
						switch (linha.nomes[i++])
						{
							//se for ORG, muda a posicao em que se está escrevendo
							case "ORG":

								int novaPosicao = gram.paraInteiro(linha.nomes[i]);
								//se a nova posição ficar antes do que estávamos escrevendo,
								//gera um aviso
								if (novaPosicao < b)
								{
									saida.errorOut(Escritor.AVISO, linha.linhaFonte, "A posição "+b+ " pode ter sido sobrescrita.");
								}
								b = novaPosicao;
								i++;
								break;
							//escreve o valor do que estiver a direita em um único byte
							case "DB":
								if (linha.subTipos[i] == (int)Gramatica.SubTipos.NUMERO)
								{
									num = gram.paraInteiro(linha.nomes[i]);
									endereco = gram.num2byteArray(num);
								}
								else if (linha.subTipos[i] == (int)Gramatica.SubTipos.STRING)
								{
									endereco = new byte[1];
									endereco[0] = (byte)linha.nomes[i][0];
								}
								//label
								else
								{
									//se a label ja foi definida, pega seu valor
									label = this.defs.labels.Find(o => o.nome == linha.nomes[i]);
									if (label.valor >=0)
									{
										endereco = gram.num2byteArray(label.valor);
									}
								}

								if (endereco.Length > 1)
									saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O número: " + linha.nomes[i] + " ocupa " + endereco.Length + " bytes e foi truncado.");

								memoria[b] = endereco[0];
								b++;
								break;
							//utiliza exatamente 2 bytes para escrever o número que estiver a direita
							case "DW":

								if (linha.subTipos[i] == (int)Gramatica.SubTipos.NUMERO)
								{
									num = gram.paraInteiro(linha.nomes[i]);
									endereco = gram.num2byteArray(num);
								}
								else if (linha.subTipos[i] == (int)Gramatica.SubTipos.STRING)
								{
									endereco = new byte[2];
									endereco[0] = (byte)linha.nomes[i][0];
									if (linha.nomes[i].Length > 1)
									{
										endereco[1] = (byte)linha.nomes[i][1];
										if (linha.nomes[i].Length > 2)
											saida.errorOut(Escritor.AVISO, linha.linhaFonte, "A string: " + linha.nomes[i] + "ocupa " + linha.nomes[i].Length + " bytes e foi truncada.");
									}
									else
										endereco[1] = 0;
								}
								//label
								else
								{
									//se ja foi definida, calcula o endereco
									label = this.defs.labels.Find(o => o.nome == linha.nomes[i]);
									if (label.valor >= 0)
										endereco = gram.num2byteArray(label.valor);
									//caso contrario, adiciona uma pendencia e pula para a proxima linha
									else
									{
										pendencias.Push(new Pendencia(linha.nomes[i], l, b));
										b += linguagem.tamanhoEndereco;
										break;
									}
								}

								if (endereco.Length > 2)
								{
									saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O número: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");
									memoria[b] = endereco[0];
									memoria[b+1] = endereco[1];
								}
								else
								{
									memoria[b] = endereco[0];
									memoria[b + 1] = endereco[1];
								}
								b+=2;
								break;
							//array de bytes
							case "DAB":

								for (; i < linha.nomes.Length; i++)
								{
									num = gram.paraInteiro(linha.nomes[i]);
									endereco = gram.num2byteArray(num);
									if (endereco.Length != 1)
										saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O número: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");

									memoria[b] = endereco[0];
									b++;
								}

								break;

						}//end switch nome
						break;
				}//end switch tipos
			}//end foreach linha
			return memoria;
		}

		/**
		 * converte um endereco para uma array de bytes que tem exatamente o numero de bytes informado
		 * o endereco pode ser um número, uma string ou uma label
		 * escreve o estado na respectiva variavel
		 */
		public byte[] converteByteArray(string endereco, int nbytes,ref Estado estado)
		{
			Gramatica gram = new Gramatica();
			byte[] vetor;

			if (gram.ehNumero(endereco))
			{
				vetor = gram.num2byteArray(gram.paraInteiro(endereco));
				estado = Estado.OK;
			}
			else if (gram.ehString(endereco))
			{
				vetor = gram.string2byteArray(endereco,nbytes);
				estado = Estado.OK;
			}
			//label
			else
			{
				Label label = this.defs.labels.Find(o => o.nome == endereco);
				if (label.valor >= 0)
					vetor = gram.num2byteArray(label.valor);
				else
					estado = Estado.INDEFINIDO;
			}

			return vetor;
		}
		/**
		 * determina a mascara de codigos dos registradores utilizados na linha
		 */
		public byte[] mascaraRegistradores(Linha linha,Linguagem lingua)
		{
			byte[] mascara = new byte[linha.bytes];
			Registrador reg;
			string nome;

			//zera a mascara
			for (int t = 0; t < mascara.Length; t++)
			{
				mascara[t] = 0;
			}

			for (int t = 0; t < linha.tipos.Length; t++)
			{
				if (linha.tipos[t] == (int)Gramatica.Tipos.REGISTRADOR)
				{
					nome = linha.nomes[t];
					reg = lingua.registradores.Find(o => o.nome == nome);
					mascara = reg.codigo;
				}
			}

			return mascara;
		}

		/**
		 * determina a mascara de codigos dos modos de enderecamento utilizados
		 */
		public byte[] mascaraEnderecamentos(Linha linha, Linguagem lingua)
		{
			byte[] mascara = new byte[linha.bytes];

			//se nenhum modo de enderecamento foi utilizado, zera a mascara
			if (linha.enderecamento.Count == 0)
				for (int i = 0; i < mascara.Length; i++)
				{
					mascara[i] = 0;
				}
			else
			{
				for (int i = 0; i < mascara.Length; i++)
				{
					mascara[i] = linha.enderecamento[0][i];
				}
			}
			return mascara;
		}
    }
}
