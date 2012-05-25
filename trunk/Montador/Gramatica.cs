using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Montador
{
    public class Gramatica
    {

        public string[] inst;
        public enum Tipos { DEFLABEL, INSTRUCAO, DIRETIVA, REGISTRADOR, ENDERECO, ENDERECAMENTO, INVALIDO };
        private const char IMEDIATO = '#';

        public List<Instrucao> instrucoes;

        public List<string> mnemonicos;
        public string[] diretivas = {"DAB","DAW","DB","DW","ORG"};
        public List<string> registradores;
        public List<string> enderecamentos;

        /*
         * carrega os dados relativos a maquina fornecida
         * TODO: tratar casos em que os arquivos nao existem
         * 
         * arquivos usados (data/maquina/):
         * inst.txt, formato:
         * mnemonico [r...] [end...]
         * ex: add r end
         * 
         * end.txt, modos de enderecameto
         * d: direto
         * i: indireto
         * #: imediato
         * x: indexado
         * p: relativo ao PC
         * 
         * reg.txt, registradores, uma palavra por linha
         */
        public void carrega(string maquina)
        {
			this.instrucoes = new List<Instrucao>();
            this.mnemonicos = new List<string>();
            this.registradores = new List<string>();
            this.enderecamentos = new List<string>();

            string linha;
			int[] formato;
			string[] words;
            string arquivo = "data/" + maquina;
            char[] space = {' '};

            //adiciona o mnemonicos das instrucoes
            using (StreamReader file = new StreamReader(arquivo + "/inst.txt"))
            {
                while ((linha = file.ReadLine()) != null)
                {
					words = linha.Split(space);
                    this.mnemonicos.Add(words[0].ToUpper());

					//determina o formato da instrucao
					formato = new int[words.Length];
					for (int i = 0; i < words.Length; i++)
					{
						if (words[i] == "r")
							formato[i] = (int)Tipos.REGISTRADOR;
						else if(words[i] == "end")
							formato[i] = (int)Tipos.ENDERECO;
						else
							formato[i] = (int)Tipos.INSTRUCAO;
					}
						this.instrucoes.Add(new Instrucao(words[0].ToUpper(),formato,0));
                }
            }

            //adiciona os registradores
            using (StreamReader file = new StreamReader(arquivo + "/reg.txt"))
            {
                while ((linha = file.ReadLine()) != null)
                {
                    this.registradores.Add(linha.Split(space)[0].ToUpper());
                }
            }

            //adiciona os modos de enderecamentos
            using (StreamReader file = new StreamReader(arquivo + "/end.txt"))
            {
                while ((linha = file.ReadLine()) != null)
                {
                    this.enderecamentos.Add(linha.Split(space)[0].ToUpper());
                }
            }

            this.mnemonicos.Sort();
            this.registradores.Sort();
            this.enderecamentos.Sort();
        }

        /**
         * independente da maquina, cada linha do codigo fonte pode ser:
         * 
         * inst
         * inst end
         * inst r
         * inst r end
         * diretiva
         * 
         * precedida ou nao por uma definicao de label
         * um endereco pode ser um numero ou uma palavra, seguido de um modo de enderecamento
         * uma label eh uma palavra seguida de ':'
         * 
         */

        /**
         * retorna a primeria subpalavra da string que seja um numero em hexadecimal
         * como nao eh possivel a existencia de 2 numeros na mesma palavra, apenas o primeiro eh necessario
         * se nao existir tal subpalavra, retorna ""
         * formato:
         * 0.(0-9+A-F)*.h
         */
        public string substringHexa(string palavra)
        {
            char[] numero = new char[palavra.Length];
            int i = 0,p = 0;
            bool achou = false;

            //enquanto nao encontrar a substring
            while (!achou && i < palavra.Length)
            {
                //busca um '0'
                for (; i < palavra.Length && palavra[i] != '0'; i++)
                    ;
                //copia os caracteres para o numero
                for (p = 0; i < palavra.Length && ehDigitoHexa(palavra[i]); numero[p] = palavra[i],p++,i++)
                    ;
                //se o proximo caracter for um 'H', o numero foi encontrado
                if (i < palavra.Length)
                    if (palavra[i] == 'H')
                        achou = true;
            }
            
            string n = new string(numero,0,p);
            
            if (achou)
                return n;
            else
            {
                return "";
            }
            

        }

        // 0-F
        bool ehDigitoHexa(char c)
        {
            //0-9
            if(c.CompareTo('0')>=0 && c.CompareTo('9')<=0)
            {
                return true;
            }
            //A-F
            if (c.CompareTo('A') >= 0 && c.CompareTo('F') <= 0)
            {
                return true;
            }

            return false;
        }

        /**
         * converte uma string em hexa para um numero inteiro
         */
        public int hexa2int(string hexa)
        {
            int numero = 0;
            int i;
            double power = 0;
            for (i = hexa.Length - 1; i >= 0; i--,power++)
            {
                numero += (int)(valorHexa(hexa[i]) * Math.Pow(16, power));
            }

            return numero;
        }

        /**
         * retorna o valor inteiro do caractere hexa d
         * d deve estar em [0,f]
         */
        int valorHexa(char d)
        {
            if (d.CompareTo('A') >= 0)
                return 10 + d.CompareTo('A');
            else
                return d.CompareTo('0');
        }

        /*
         * identifica o tipo de uma palavra (olhar enum Tipos)
         */
        public int identificaTipo(string palavra, Gramatica gramatica)
        {
            int esq, dir;
            string[] aux;
            //se a string contiver uma ',', divide-a em 2
            if (palavra.IndexOf(',') >= 0)
            {
                aux = palavra.Split(',');
                esq = identificaTipo(aux[0], gramatica);
                dir = identificaTipo(aux[1], gramatica);

                return identificaTipo(esq, dir, gramatica);
            }

            if (ehNumero(palavra))
                return (int)Tipos.ENDERECO;
            //se nao for um numero, verifica se eh alguma palavra conhecida
            if (Array.BinarySearch(this.diretivas, palavra) >= 0)
                return (int)Tipos.DIRETIVA;
            if (this.mnemonicos.BinarySearch(palavra) >= 0)
                return (int)Tipos.INSTRUCAO;
            if (this.registradores.BinarySearch(palavra) >= 0)
                return (int)Tipos.REGISTRADOR;
            if (this.enderecamentos.BinarySearch(palavra) >= 0)
                return (int)Tipos.ENDERECAMENTO;

            if (ehLabel(palavra))
            {
                if(palavra[palavra.Length -1] == ':')
                    return (int)Tipos.DEFLABEL;
                else
                    return (int)Tipos.ENDERECO;
            }

            //se nao for nada conhecido, entao eh invalido
            return (int)Tipos.INVALIDO;

        }

        /*
         * verifica se a string corresponde a um numero em hexa ou em decimal, podendo ser precedida ou nao por IMEDIATO ou '-'
         */
        public bool ehNumero(string palavra)
        {
            int i = 0;
            bool hexa = false;
            bool numero = true;
            if (palavra[0] == IMEDIATO || palavra[0] == '-')
                i++;

            if (palavra[i] == '0' && palavra[palavra.Length-1] == 'H')
                hexa = true;

            for (; i < palavra.Length && numero; i++)
            {
                numero = false;
                if ((hexa && ehDigitoHexa(palavra[i])))
                    numero = true;
                else if (Char.IsDigit(palavra[i]))
                    numero = true;
            }

            if (numero)
                return true;
            //se o ultimo caracter encontrado foi um 'H' e este eh o ultimo caracter da string
            //entao eh um numero em hexadeciamal
            if (palavra[i-1] == 'H' && hexa && i == palavra.Length && hexa)
                return true;
            return false;

        }

        /*
         * verifica se uma determinada palavra eh a definicao de uma label
         */
        public bool ehLabel(string palavra)
        {

            if (Char.IsDigit(palavra[0]))
            {
                return false;
            }
            foreach (char c in palavra)
            {
                if (c == ',')
                    return false;
            }
            return true;


        }

        /*
         * recebe os tipos dos elementos a direita e a esquerda de uma virgula
         * retorna o tipo da concatenacao
         * ex:
         * ENDERECO,ENDERECAMENTO -> ENDERECO
         * INSTRUCAO,REGISTRADOR -> INVALIDO
         */
        public int identificaTipo(int esquerda, int direita, Gramatica gramatica)
        {

            if (esquerda == (int)Tipos.ENDERECO && direita == (int)Tipos.ENDERECAMENTO)
                return (int)Tipos.ENDERECO;
            return (int)Tipos.INVALIDO;

        }

		/*
		 * verifica se os tipos de uma linha são validos
		 * linhas sao do tipo
		 * 0?.(1).(3+4)* ou
		 * 0?.(2).(4)*
		 * retorna true se forem
		 */
		public bool verificaTipos(int[] tipos,string[] linha, int nlinha,Escritor saida,Definicoes defs)
		{
			Instrucao inst;
			int i = 0;
			int j = 1;
			int size = tipos.Length;
			//a linha pode comecar por definicao de label
			if (tipos[0] == 0)
			{
				defs.adicionaDef(linha[0].Substring(0,linha[0].Length-1),nlinha,saida);
				i = 1;
				size--;
			}
			//a linha eh a definicao de uma label apenas
			if (i >= tipos.Length)
				return true;

			if (tipos[i] == (int)Tipos.INSTRUCAO)
			{
				inst = this.instrucoes.Find(o => o.mnemonico == linha[i]);
				if(size < inst.formato.Length)
					saida.errorOut(Escritor.ERRO, nlinha, "Número incorreto de operandos. Esperava-se " + (inst.formato.Length - 1) + ", encontrou-se " + (size - 1));
				//verifica se ha algo diferente de registradores e enderecos
				for (i++; i < tipos.Length; i++)
				{
					if (j >= inst.formato.Length)
					{
						if(tipos[i] != (int)Tipos.ENDERECAMENTO || tipos[i-1] != (int)Tipos.ENDERECO)
							saida.errorOut(Escritor.ERRO, nlinha, "Número incorreto de operandos. Esperava-se " + (inst.formato.Length-1) + ", encontrou-se " + (size-1));
						break;
					}
					if (tipos[i] != inst.formato[j])
					{
						switch (tipos[i])
						{
							case (int)Tipos.INVALIDO:
								saida.errorOut(Escritor.ERRO, nlinha, "Palavra inválida: " + linha[i]);
								break;
							case (int)Tipos.DEFLABEL:
								saida.errorOut(Escritor.ERRO, nlinha, "Labels só podem ser definidas no início da linha.");
								break;
							case (int)Tipos.INSTRUCAO:
							case (int)Tipos.DIRETIVA:
								saida.errorOut(Escritor.ERRO, nlinha, "Não pode ter mais de uma instrução ou diretiva por linha.");
								break;
							case (int)Tipos.ENDERECAMENTO:
								if (tipos[i - 1] == (int)Tipos.ENDERECO)
								{
									size--;
									j--;
								}
								else
									saida.errorOut(Escritor.ERRO, nlinha, "Modos de endereçamento devem ser precedidos por endereços.");
								break;
						}
					}
					else if (tipos[i] == (int)Tipos.ENDERECO)
					{
						if(ehLabel)
					}
					j++;
				}
			}
			else if (tipos[i] == (int)Tipos.DIRETIVA)
			{
				//se for a diretiva org, apenas 1 operando deve existir
				if(linha[i] == "ORG")
				{
					if(size != 2)
						saida.errorOut(Escritor.ERRO, nlinha, "Número incorreto de operandos. Esperava-se 1, encontrou-se " + (size - 1));
				}
				i++;
				//o restante da linha so pode conter enderecos
				while (i < tipos.Length)
				{
					switch (tipos[i])
					{
						case (int)Tipos.INSTRUCAO:
						case (int)Tipos.DIRETIVA:
							saida.errorOut(Escritor.ERRO, nlinha, "Não pode ter mais de uma instrução ou diretiva por linha.");
							break;
						case (int)Tipos.REGISTRADOR:
							saida.errorOut(Escritor.ERRO, nlinha, linha[i] + " não é um operando válido.");
							break;
						case (int)Tipos.INVALIDO:
							saida.errorOut(Escritor.ERRO, nlinha, "Palavra inválida: " + linha[i]);
							break;
					}
					i++;
				}
			}
			else
			{
				saida.errorOut(Escritor.ERRO, nlinha, "Instrução ou diretiva inválida: " + linha[i]);
			}
			return true;
		}
    }
}
