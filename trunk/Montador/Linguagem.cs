using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Montador
{
    public class Linguagem
    {
        public List<Instrucao> instrucoes;
		public List<Registrador> registradores;
		public List<Enderecamento> enderecamentos;
		public string[] diretivas = { "DAB", "DAW", "DB", "DW", "ORG" };
		
		public enum Tipos { DEFLABEL, INSTRUCAO, DIRETIVA, REGISTRADOR, ENDERECO, INVALIDO };

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
			int[] formato;
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
					formato = new int[words.Length-1];
					for (int i = 1; i < words.Length; i++)
					{
						if (words[i] == "r")
							formato[i-1] = (int)Tipos.REGISTRADOR;
						else if (words[i] == "end")
							formato[i-1] = (int)Tipos.ENDERECO;
						else
							formato[i-1] = (int)Tipos.INSTRUCAO;
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

			this.registradores.Sort(reg.regCompare);
			this.instrucoes.Sort(inst.instCompare);
		}

		/*
		 * verifica se a palavra eh conhecida
		 * retorna um valor de enum Tipos
		 * se for um enderecamento, escreve em nome o nome do endereco ou registrador encontrado
		 */
		public int identificaTipo(string palavra,ref string nome)
		{
			Enderecamento end = new Enderecamento();

			if (Array.FindIndex(this.diretivas, o => o == palavra ) >= 0)
				return (int)Tipos.DIRETIVA;
			if (this.registradores.FindIndex(o => o.nome == palavra) >= 0)
				return (int)Tipos.REGISTRADOR;
			if (this.instrucoes.FindIndex(o => o.mnemonico == palavra) >= 0)
				return (int)Tipos.INSTRUCAO;
			if (end.identifica(palavra,this.enderecamentos,ref nome) >= 0)
				return (int)Tipos.ENDERECO;

			return (int)Tipos.INVALIDO;
		}

		public void print()
		{
			Console.WriteLine("Instrucoes:");
			foreach (Instrucao inst in this.instrucoes)
			{
				Console.WriteLine(inst.mnemonico);
				foreach (int f in inst.formato)
				{
					Console.Write(f + " ");
				}
				Console.Write("\n");
			}
		}
    }
}
