using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Montador
{
    public class Linguagem
    {
		public enum Endianness { Little, Big };
		public Endianness endianess = Endianness.Little;
        public List<Instrucao> instrucoes;
		public List<Registrador> registradores;
		public List<Enderecamento> enderecamentos;
		public string[] diretivas = { "DAB", "DAW", "DB", "DW", "ORG" };
		public int tamanhoEndereco;

		/*
         * carrega os dados relativos a maquina fornecida
         * TODO: tratar casos em que os arquivos nao existem
         * 
         * arquivos usados (data/maquina/):
         * inst.txt, formato:
         * codigo mnemonico [r...] [end...]
         * ex: 10010000 add r end
         * 
         * end.txt, modos de enderecameto
         * d: direto
         * i: indireto
         * #: imediato
         * x: indexado
         * p: relativo ao PC
         * 
         * reg.txt, registradores, uma palavra por linha, precedida pelo seu codigo
         */
		public void carrega(string maquina)
		{
			Registrador reg = new Registrador();
			Instrucao inst = new Instrucao();
			Gramatica gram = new Gramatica();
			this.instrucoes = new List<Instrucao>();
			this.registradores = new List<Registrador>();
			this.enderecamentos = new List<Enderecamento>();

			string linha;
			Gramatica.Tipos[] formato;
			string[] words;
			string arquivo = "data/" + maquina;
			char[] space = { ' ' };

			//adiciona as instrucoes
			using (StreamReader file = new StreamReader(arquivo + "/inst.txt"))
			{
				while ((linha = file.ReadLine()) != null)
				{
					words = linha.Split(space);

					//determina o formato da instrucao
					formato = new Gramatica.Tipos[words.Length-1];
					for (int i = 1; i < words.Length; i++)
					{
						if (words[i] == "r")
							formato[i-1] = Gramatica.Tipos.REGISTRADOR;
						else if (words[i] == "end")
							formato[i - 1] = Gramatica.Tipos.ENDERECO;
						else
							formato[i - 1] = Gramatica.Tipos.INSTRUCAO;
					}
					this.instrucoes.Add(new Instrucao(words[1].ToUpper(), formato, gram.leCodigo(words[0])));
				}
			}

			//adiciona os registradores
			using (StreamReader file = new StreamReader(arquivo + "/reg.txt"))
			{
				while ((linha = file.ReadLine()) != null)
				{
					words = linha.Split(space);
					this.registradores.Add(new Registrador(words[1].ToUpper(), gram.leCodigo(words[0])));
				}
			}

			//adiciona os modos de enderecamentos
			using (StreamReader file = new StreamReader(arquivo + "/end.txt"))
			{
				while ((linha = file.ReadLine()) != null)
				{
					words = linha.Split(space);
					this.enderecamentos.Add(new Enderecamento(words[1].ToUpper(), gram.leCodigo(words[0])));
				}
			}

			//determina as caracteristicas da maquina
			using (StreamReader file = new StreamReader(arquivo + "/maq.txt"))
			{
				//endianess
				linha = file.ReadLine();
				//formato de cada instrucao
				linha = file.ReadLine();
				//numero de bits do endereco
				linha = file.ReadLine();
				this.tamanhoEndereco = gram.paraInteiro(linha);

				if (this.tamanhoEndereco % 8 == 0)
					this.tamanhoEndereco /= 8;
				else
				{
					this.tamanhoEndereco /= 8;
					this.tamanhoEndereco += 1;
				}
			}

			this.registradores.Sort(reg.regCompare);
			this.instrucoes.Sort(inst.instCompare);
		}

		/*
		 * verifica se a palavra eh conhecida
		 * retorna um valor de enum Tipos
		 * se for um enderecamento, escreve em nome o nome do endereco ou registrador encontrado e em enderecamento,
		 * o codigo do modo utilizado
		 */
		public Gramatica.Tipos identificaTipo(string palavra,ref string nome, ref byte[] enderecamento)
		{
			Enderecamento end = new Enderecamento();
			Gramatica gram = new Gramatica();

			if (Array.FindIndex(this.diretivas, o => o == palavra ) >= 0)
				return Gramatica.Tipos.DIRETIVA;
			if (this.registradores.FindIndex(o => o.nome == palavra) >= 0)
				return Gramatica.Tipos.REGISTRADOR;
			if (this.instrucoes.FindIndex(o => o.mnemonico == palavra) >= 0)
				return Gramatica.Tipos.INSTRUCAO;
			if (end.identifica(palavra,this.enderecamentos,ref nome,ref enderecamento) >= 0)
				return Gramatica.Tipos.ENDERECO;
			if(gram.ehDefLabel(palavra,0,palavra.Length-1))
				return Gramatica.Tipos.DEFLABEL;
			if (gram.ehArray(palavra))
				return Gramatica.Tipos.ENDERECO;
			Console.WriteLine(String.Format("Pal:{0}",palavra));

			return Gramatica.Tipos.INVALIDO;
		}

		public void print()
		{
			Console.WriteLine("Instrucoes:");
			foreach (Instrucao inst in this.instrucoes)
			{
				Console.WriteLine(inst.mnemonico);
				foreach (byte b in inst.codigo)
				{
					Console.Write(b);
				}
				Console.WriteLine("");
				foreach (int f in inst.formato)
				{
					Console.Write(f + " ");
				}
				Console.Write("\n");
			}
		}
    }
}
