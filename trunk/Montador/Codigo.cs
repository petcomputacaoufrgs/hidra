using System;
using System.IO;
using System.Collections.Generic;

namespace Montador
{
	public class Codigo
	{
		const char COMENTARIO = ';';
		public enum Estado { OK, TRUNCADO, INDEFINIDO };	//estado da geração do binário de um endereço
		public List<Linha> linhas;	//cada linha do codigo fonte

		public Definicoes defs = new Definicoes();	//definicoes de labels

		public byte[] binario;


		/**
		 * limpa a linha, removendo os espaços nas pontas
		 * e deixando-a no formato:
		 * instrucao operandos
		 * ex:
		 * "		add a 127 ,x " -> {"ADD", "A", "127,X"}
		 * "label	 :" -> {"label:"}
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
			char[] parsed = new char[linha.Length + 1];

			int prev = 0; //indice do inicio do elemento
			List<string> elem = new List<string>();
			for (int i = 0; i < linha.Length; i++)
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
							if (p - prev > 0)
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

			if (!espaco)
				elem.Add(new string(parsed, prev, p - prev));

			string[] result = new string[elem.Count];

			for (int i = 0; i < elem.Count; i++)
			{
				if (elem[i].Length > 0)
				{
					//Console.WriteLine("(" +elem[i] + ")");
					result[i] = elem[i];
				}
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

			/*foreach (Linha l in this.linhas)
			{
				foreach (string w in l.preprocessado)
				{
					Console.Write(w + " ");
				}
				Console.Write("\n");
			}*/
		}

		/**
		 * determina se o codigo eh valido
		 * 
		 * retorna true se for
		 */
		public Boolean ehValido(Gramatica gramatica, Escritor saida)
		{
			this.identificaTipos(gramatica);
			/*
			for (int i = 0; i < this.linhas.Count; i++)
			{
				for (int j = 0; j < this.linhas[i].preprocessado.Length; j++)
					Console.Write(this.linhas[i].preprocessado[j]+" ");
				Console.WriteLine();
			}*/

			//verifica se os tipos de cada linha são validos
			for (int i = 0; i < this.linhas.Count; i++)
			{
				gramatica.verificaTipos(this.linhas[i], saida, this.defs);
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
			for (int j = 0; j < this.linhas.Count; j++)
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
				for (int i = 0; i < linha.preprocessado.Length; i++)
				{
					numeroHexa = gram.substringHexa(linha.preprocessado[i]);

					if (numeroHexa != "")
					{
						numero = gram.hexa2int(numeroHexa);
						linha.preprocessado[i] = linha.preprocessado[i].Replace(numeroHexa + "H", numero.ToString());
					}
				}
			}
		}
		/*
				 *	escreve o conteudo das linhas
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
					Console.Write(s + "|");
				Console.Write("\n");

				foreach (int t in linha.tipos)
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
		 * retorna o binario resultante, usando exatamente o tamanho dado
		 */
		public byte[] montar(int tamanho, Linguagem linguagem, Escritor saida)
		{
			byte[] mem = new byte[tamanho];
			for (int i = 0; i < tamanho; i++)
				mem[i] = 0;
			Stack<Pendencia> pendencias = new Stack<Pendencia>();
			Console.WriteLine("PendSize:{0}", pendencias.Count);
			int pos = 0;
			int lnum = 0;
			foreach (Linha l in this.linhas)
			{
				Console.WriteLine(String.Format("L[0]:{0}",l.preprocessado[0]));
				int i = 0;
				if (l.tipos[0] == Gramatica.Tipos.DEFLABEL)
				{
					this.defs.atribuiDef(l.preprocessado[0], pos);
					i++;
				}
				if (i >= l.preprocessado.Length)
				{
					lnum++;
					continue;
				}
				if (l.tipos[i] == Gramatica.Tipos.INSTRUCAO)
				{
					List<byte> val = montaInstrucao(l,lnum, pos, linguagem, saida, pendencias);
					foreach (byte b in val)
						mem[pos++] = b;
				}
				else
				{
					List<byte> val = montaDiretiva(l,lnum, ref pos, linguagem, saida, pendencias);
					foreach (byte b in val)
						mem[pos++] = b;
				}
				lnum++;
			}
			Console.WriteLine("PendSize:{0}",pendencias.Count);
			foreach (Pendencia p in pendencias)
			{
				Console.WriteLine(String.Format("Pend:{0}\tL:{1}",p.nome,p.nlinha));
				Estado estado = Estado.OK;
				pos = p.nbyte;
				Linha l = this.linhas[p.nlinha];
				byte[] end = converteByteArray(p.nome,p.tamanho,Gramatica.SubTipos.LABEL,ref estado);
				if (estado == Estado.TRUNCADO)
				{
					saida.errorOut(Escritor.Message.TruncatedValue,p.nlinha,p.nome,end.Length,p.tamanho);
				}
				for (int b = 0; b < p.tamanho; b++)
				{
					mem[pos++] = end[b];
				}
			}

			return mem;
		}

		/**
		 *	monta a linha de uma diretiva
		 *	lnum eh a posicao da linha na lista de linhas
		 *	ignora definicoes de labels
		 */
		public List<byte> montaDiretiva(Linha linha,int lnum, ref int pos, Linguagem linguagem, Escritor saida, Stack<Pendencia> pendencias)
		{

			List<byte> memoria = new List<byte>();
			Estado estado = Estado.OK;
			byte[] end;
			int i;
			if (linha.tipos[0] == Gramatica.Tipos.DEFLABEL)
				i = 1;
			else
				i = 0;

			Boolean array = true;
			int size = 0;
			switch (linha.preprocessado[i])
			{
				case "ORG":
					Gramatica gram = new Gramatica();
					end = converteByteArray(linha.nomes[i + 1], linguagem.tamanhoEndereco, linha.subTipos[i + 1], ref estado);
					pos = gram.arrayParaInteiro(end, linguagem.endianess);
					return memoria;
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
			for (i++; i < linha.nomes.Length; i++)
			{
				if (linha.subTipos[i] == Gramatica.SubTipos.ARRAY)
				{
					Gramatica gram = new Gramatica();
					int p = 0;
					int max = linha.nomes[i].Length-1;
					string el = gram.proximoElemento(linha.nomes[i], ref p, max);
					while (el != "")
					{
						Console.WriteLine(String.Format("El:{0}\tT:{1}",el,el.Length));
						Gramatica.SubTipos subt = gram.identificaSubTipo(el);
						end = converteByteArray(el, size, subt, ref estado);

						if (estado == Estado.INDEFINIDO)
						{
							pendencias.Push(new Pendencia(el, linha.linhaFonte,size, pos + memoria.Count));
							//reserva espaco
							for (int j = 0; j < size; j++)
								memoria.Add(0);
						}
						else
						{
							if (estado == Estado.TRUNCADO && subt != Gramatica.SubTipos.STRING)
								saida.errorOut(Escritor.Message.TruncatedValue, el, end.Length, size);
							foreach (byte b in end)
								memoria.Add(b);
						}
						el = gram.proximoElemento(linha.nomes[i], ref p, max);
					}
				}
				else
				{
					end = converteByteArray(linha.nomes[i], size, linha.subTipos[i], ref estado);
					
					if (estado == Estado.INDEFINIDO)
					{
						pendencias.Push(new Pendencia(linha.nomes[i], lnum,size, pos + memoria.Count));
						//reserva espaco
						for (int j = 0; j < size; j++)
							memoria.Add(0);
					}
					else
					{
						if (estado == Estado.TRUNCADO && !array)
							saida.errorOut(Escritor.Message.TruncatedValue, linha.nomes[i], end.Length, size);
						//se for um array, escreve todos os bytes
						if (array)
						{
							foreach (byte b in end)
								memoria.Add(b);
						}
						//se nao for, escreve apenas os primeiros bytes
						else
						{
							for (int b = 0; b < size; b++)
								memoria.Add(end[b]);
						}
					}
				}				
			}

			return memoria;
		}

		/**
		 *	monta os bytes associados a uma linha de instrucao
		 *	lnum eh a posicao da linha na lista de linhas
		 *	ignora definicoes de labels
		 */

		public List<byte> montaInstrucao(Linha linha,int lnum, int pos, Linguagem linguagem, Escritor saida, Stack<Pendencia> pendencias)
		{
			Estado estado = Estado.OK;
			Gramatica gram = new Gramatica();
			byte[] regMask, endMask;
			byte[] endereco;
			Instrucao inst;
			List<byte> memoria = new List<byte>();

			int i;
			if (linha.tipos[0] == Gramatica.Tipos.DEFLABEL)
				i = 1;
			else
				i = 0;

			inst = linguagem.instrucoes.Find(o => o.mnemonico == linha.preprocessado[i]);
			/*Console.WriteLine("Inst:" + inst.mnemonico);
			foreach (byte ba in inst.codigo)
				Console.Write(ba + " ");
			Console.WriteLine("");*/

			//determina a codificacao dos registradores e dos enderecos
			regMask = mascaraRegistradores(linha, linguagem);
			endMask = mascaraEnderecamentos(linha, linguagem);

			//faz um ou bitwise com cada uma das mascaras de codigo
			for (int k = 0; k < linha.bytes; k++)
			{
				memoria.Add((byte)(regMask[k] | endMask[k] | inst.codigo[k]));
			}

			//escreve os enderecos utilizados
			for (int k = i + 1; k < linha.tipos.Length; k++)
			{
				if (linha.tipos[k] == Gramatica.Tipos.ENDERECO)
				{
					endereco = this.converteByteArray(linha.nomes[k], linguagem.tamanhoEndereco, linha.subTipos[k], ref estado);

					Console.WriteLine("Endereco:" + estado);
					foreach (byte e in endereco)
						Console.Write(e + " ");
					Console.WriteLine("");

					if (estado != Estado.INDEFINIDO)
					{
						if (estado == Estado.TRUNCADO)
							saida.errorOut(Escritor.Message.TruncatedValue, linha.linhaFonte, linha.nomes[i], endereco.Length);
						//escreve o endereco
						for (int j = 0; j < linguagem.tamanhoEndereco && j < endereco.Length; j++)
						{
							Console.WriteLine("End:" + endereco[j]);
							memoria.Add(endereco[j]);
						}
					}
					else
					{
						Console.Write(String.Format("{0}Nomes:\n",linha.nomes.Length));
						foreach (string s in linha.nomes)
							Console.Write(s + "\n");
						Console.Write("\n");
						pendencias.Push(new Pendencia(linha.nomes[k], lnum,linguagem.tamanhoEndereco, memoria.Count + pos));
						//reserva o espaco para o endereco
						for (int j = 0; j < linguagem.tamanhoEndereco; j++)
							memoria.Add(0);
					}
				}
			}
			return memoria;
		}

		/**
		 * escreve os bytes referentes a um certo endereco a partir da posicao pos da memoria
		 * retorna o estado do endereco
		 * atualiza pos somente se algo for escrito na memoria
		 */
		public Estado escreveEndereco(byte[] memoria, ref int pos, Linguagem linguagem, string endereco, Gramatica.SubTipos subtipo)
		{
			Estado estado = Estado.OK;
			byte[] bin = converteByteArray(endereco, linguagem.tamanhoEndereco, subtipo, ref estado);
			if (estado != Estado.INDEFINIDO)
			{
				for (int k = 0; k < endereco.Length && k < linguagem.tamanhoEndereco; k++, pos++)
				{
					memoria[pos] = bin[k];
				}
			}

			return estado;

		}

		/**
		 * converte um endereco para um array de bytes 
		 * o tamanho de cada elemento deve ser dado. Se algum elemento for maior que esse tamanho, sera truncado
		 * o endereco pode ser um número, uma string ou uma label
		 * escreve o estado na respectiva variavel
		 */
		public byte[] converteByteArray(string endereco, int tamanho, Gramatica.SubTipos subtipo, ref Estado estado)
		{
			Gramatica gram = new Gramatica();
			byte[] vetor = new byte[1];
			estado = Estado.OK;

			Console.WriteLine(String.Format("Operando:{0}\tSubtipo:{1}",endereco,subtipo));

			switch (subtipo)
			{

				case Gramatica.SubTipos.NUMERO:
					vetor = gram.num2byteArray(gram.paraInteiro(endereco), tamanho, ref estado);
					break;

				case Gramatica.SubTipos.STRING:
					vetor = gram.string2byteArray(endereco, tamanho, ref estado);
					break;
									
				case Gramatica.SubTipos.LABEL:
					Label label = this.defs.labels.Find(o => o.nome == endereco);
					Console.WriteLine(String.Format("Endereco:{0}",endereco));
					if (label.valor >= 0)
						vetor = gram.num2byteArray(label.valor, tamanho, ref estado);
					else
					{
						estado = Estado.INDEFINIDO;
						vetor = new byte[1];
						vetor[0] = 255;
					}
					break;
			}
			return vetor;
		}
		/**
		 * determina a mascara de codigos dos registradores utilizados na linha
		 */
		public byte[] mascaraRegistradores(Linha linha, Linguagem lingua)
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
				if (linha.tipos[t] == Gramatica.Tipos.REGISTRADOR)
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
