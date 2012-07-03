using System;
using System.IO;
using System.Collections.Generic;

namespace Montador
{
    public class Codigo
    {
        const char COMENTARIO = ';';
		public enum Estado {OK,TRUNCADO, INDEFINIDO};	//estado da geração do binário de um endereço
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
							if (p - prev > 0)
								elem.Add(new string(parsed, prev, p - prev));
							prev = p;
							espaco = true;
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
							if(p-prev > 0)
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
							prev = p;
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
				if(elem[i].Length > 0)
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
			Console.WriteLine("*******");
			foreach (Linha linha in this.linhas)
			{
				Console.Write(linha.linhaFonte + ":\t");
				foreach (string w in linha.preprocessado)
				{
					Console.Write(w + " ");
				}
				Console.Write("\n");

				foreach (string s in linha.nomes)
					Console.Write(s + " ");
				Console.Write("\n");

				foreach(int t in linha.tipos)
					Console.Write(t + " ");
				Console.Write("\n");

				foreach (int t in linha.subTipos)
					Console.Write(t + " ");
				Console.Write("\n");
			}
			
			foreach (Label la in this.defs.labels)
			{
				Console.WriteLine(la.nome + "\t" + la.valor + "\t" + la.linhaDef);
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
			Stack<Pendencia> pendencias = new Stack<Pendencia>();

			Codigo.Estado estado = Codigo.Estado.OK;

			int l = 0;
			int i;
            Linha linha;
			Console.WriteLine("list c: " + this.linhas.Count);
			for(linha = this.linhas[0];l<this.linhas.Count;l++)
			{
				linha = this.linhas[l];
				i=0;
				if(linha.tipos[i] == (int)Gramatica.Tipos.DEFLABEL)
				{
					Console.WriteLine("def: " + linha.nomes[i]);
					this.defs.atribuiDef(linha.nomes[i], b);
					i++;
				}
				if (i >= linha.tipos.Length)
					continue;
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
								Console.WriteLine(k+" line:"+linha.linhaFonte);
								endereco = this.converteByteArray(linha.nomes[k], linguagem.tamanhoEndereco, ref estado);

								if (estado != Estado.INDEFINIDO)
								{
									if (estado == Estado.TRUNCADO)
										saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + " ocupa " + endereco.Length + " bytes e foi truncado.");
									//escreve o endereco
									for (int j = 0; j < endereco.Length; j++)
									{
										memoria[b] = endereco[j];
										b++;
									}
								}
								else
								{
									pendencias.Push(new Pendencia(linha.nomes[i], l, b));
								}
							}
						}
						break;
					case (int)Gramatica.Tipos.DIRETIVA:
						string tipo = linha.nomes[i];
						i++;

						if (estado == Estado.INDEFINIDO)
							pendencias.Push(new Pendencia(linha.nomes[i],l,b));

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

								endereco = this.converteByteArray(linha.nomes[i],1, ref estado);
								if (estado != Estado.INDEFINIDO)
								{
									if (endereco.Length > 1)
										saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + " ocupa " + endereco.Length + " bytes e foi truncado.");

									memoria[b] = endereco[0];
								}
								else
									pendencias.Push(new Pendencia(linha.nomes[i], l, b));
								b++;
								break;
							//utiliza exatamente 2 bytes para escrever o número que estiver a direita
							case "DW":

								endereco = this.converteByteArray(linha.nomes[i], 2, ref estado);

								if (estado != Estado.INDEFINIDO)
								{
									if (endereco.Length > 2)
									{
										saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");
										memoria[b] = endereco[0];
										memoria[b + 1] = endereco[1];
									}
									else
									{
										memoria[b] = endereco[0];
										memoria[b + 1] = endereco[1];
									}
								}
								else
								{
									pendencias.Push(new Pendencia(linha.nomes[i], l, b));
								}
								b+=2;
								break;
							//array de bytes
							case "DAB":

								for (; i < linha.nomes.Length; i++)
								{
									endereco = this.converteByteArray(linha.nomes[i], 1, ref estado);
									if (estado != Estado.INDEFINIDO)
									{
										if (endereco.Length != 1)
											saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");

										memoria[b] = endereco[0];
									}
									else
									{
										pendencias.Push(new Pendencia(linha.nomes[i],l,b));
									}
									b++;
								}

								break;
							case "DAW":

								for (; i < linha.nomes.Length; i++)
								{
									endereco = this.converteByteArray(linha.nomes[i], 2, ref estado);
									if (estado != Estado.INDEFINIDO)
									{
										if (endereco.Length != 2)
											saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");

										memoria[b] = endereco[0];
									}
									else
									{
										pendencias.Push(new Pendencia(linha.nomes[i], l, b));
									}
									b++;
								}

								break;
						}//end switch nome
						break;
				}//end switch tipos
				}//end foreach linha

			//resolve as labels pendentes
            Pendencia pend;
            while (pendencias.Count != 0)
            {
                pend = pendencias.Pop();
                i = 0;
                b = pend.nbyte;
                linha = this.linhas[pend.nlinha];

                if (linha.tipos[i] == (int)Gramatica.Tipos.DEFLABEL)
                    i++;

                Boolean array = false;
                int size = linguagem.tamanhoEndereco;
                if(linha.tipos[i] ==  (int)Gramatica.Tipos.DIRETIVA)
                {
                    i++;
                    switch (linha.nomes[i])
                    {
                        case "DB":
                            array = false;
                            size = 1;
                            break;
                        case "DW":
                            array = false;
                            size = 2;
                            break;
                        case "DAB":
                            array = true;
                            size = 1;
                            break;
                        case "DAW":
                            array = true;
                            size = 2;
                            break;
                    }

                    endereco = this.converteByteArray(linha.nomes[i], size, ref estado);

                    if (!array)
                    {
                        if (endereco.Length > size || estado == Estado.TRUNCADO)
                        {
                            saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");
                        }

                        //escreve o valor
                        for (int k = 0; k < size; k++, b++)
                        {
                            memoria[b] = endereco[k];
                        }
                    }
                    else
                    {
                        if(estado == Estado.TRUNCADO)
                            saida.errorOut(Escritor.AVISO, linha.linhaFonte, "O valor: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");
                        //escreve o valor
                        for (int k = 0; k < size; k++, b++)
                            memoria[b] = endereco[k];
                    }

                }
                else
                {
                    i++;
                    endereco = this.converteByteArray(linha.nomes[i],linguagem.tamanhoEndereco,ref estado);

                    if (estado == Estado.TRUNCADO)
                    {
                        saida.errorOut(Escritor.AVISO,linha.linhaFonte, "O valor: " + linha.nomes[i] + "ocupa " + endereco.Length + " bytes e foi truncado.");
                    }

                    //escreve o valor
                    for (int k = 0; k < linguagem.tamanhoEndereco; k++,b++)
                    {
                        memoria[b] = endereco[k];
                    }

                }
            }
        

		return memoria;
	}

		/**
		 * converte um endereco para um array de bytes 
		 * o tamanho de cada elemento deve ser dado. Se algum elemento for maior que esse tamanho, sera truncado
		 * o endereco pode ser um número, uma string ou uma label
		 * escreve o estado na respectiva variavel
		 */
		public byte[] converteByteArray(string endereco, int tamanho,ref Estado estado)
		{
			Gramatica gram = new Gramatica();
			byte[] vetor;

			if (gram.ehNumero(endereco))
			{
				vetor = gram.num2byteArray(gram.paraInteiro(endereco),tamanho,ref estado);
			}
			else if (gram.ehString(endereco))
			{
				vetor = gram.string2byteArray(endereco,tamanho,ref estado);
			}
			//label
			else
			{
				Console.WriteLine("Label:"+endereco);
				Label label = this.defs.labels.Find(o => o.nome == endereco);
				if (label.valor >= 0)
					vetor = gram.num2byteArray(label.valor, tamanho, ref estado);
				else
				{
					estado = Estado.INDEFINIDO;
					vetor = new byte[1];
					vetor[0] = 255;
				}
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
