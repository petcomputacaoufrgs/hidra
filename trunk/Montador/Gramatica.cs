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

		public Linguagem linguagem;


		/*
		 * recebe um string e converte para um inteiro
		 * numberos decimais terminam por 'd',
		 * hexadecimais terminam por 'h',
		 * números binarios podem terminar por 'b', mas nao precisam
		 */
		public int paraInteiro(string numero)
		{
			//se o numero eh binario, decimal ou hexadecimal
			char tipo = numero[numero.Length - 1];
			int num = 0;

			switch (tipo)
			{
				//decimal
				case 'd':
				case 'D':
					num = (int)System.Convert.ToDecimal(numero);
					return num;
				case 'h':
				case 'H':
					for(int i = numero.Length -1,p =1;i>=0;i--,p*=16)
					{
						num += (int)(p * Char.GetNumericValue(numero[i]));
					}
					return num;
				case '0':
				case '1':
				case 'b':
					for(int i = numero.Length -1,p = 1;i>=0;i--,p*=2)
					{
						num += (int)(p * Char.GetNumericValue(numero[i]));
					}
					return num;
			}

			return 0;
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
		 * se for um endereco, escreve em nome o nome desse endereco ou registrador caso nao seja um numero
         */
        public int identificaTipo(string palavra, Gramatica gramatica, ref string nome)
        {
			int tipo;
            if (ehNumero(palavra))
                return (int)Tipos.ENDERECO;
            //se nao for um numero, verifica se eh alguma palavra conhecida
			tipo = linguagem.identificaTipo(palavra,ref nome);
			if (tipo != (int)Tipos.INVALIDO)
				return tipo;

            if (ehLabel(palavra))
            {
                if(palavra[palavra.Length -1] == ':')
                    return (int)Tipos.DEFLABEL;
                else
                    return (int)Tipos.ENDERECO;
            }
			else if(ehString(palavra))
				return (int)Tipos.ENDERECO;

            //se nao for nada conhecido, entao eh invalido
            return (int)Tipos.INVALIDO;

        }

		/*
         * recebe os tipos dos elementos a direita e a esquerda de uma virgula
         * retorna o tipo da concatenacao
         * ex:
         * ENDERECO,ENDERECAMENTO -> ENDERECO
         * INSTRUCAO,REGISTRADOR -> INVALIDO
		 * 
		 * OBS: provavelmente essa funcao eh desnecessaria
         */
		public int identificaTipo(int esquerda, int direita, Gramatica gramatica)
		{
			if (esquerda == (int)Tipos.ENDERECO && direita == (int)Tipos.ENDERECAMENTO)
				return (int)Tipos.ENDERECO;
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
			char[] invalid = {',','\'','\"' ,':'};

            if (Char.IsDigit(palavra[0]))
            {
                return false;
            }
			for (int i = 0; i < palavra.Length-1;i++)
			{
				//se for um dos caracteres invalidos
				if (Array.Exists(invalid,c => c == palavra[i]))
					return false;
			}

			if(palavra[palavra.Length-1] == ':')
				return true;
			else if (Array.Exists(invalid,c => c == palavra[palavra.Length-1]))
				return false;
			return true;
        }

		/*
		 * determina se a palavra eh uma definicao de string, ou seja
		 * comeca por ' ou " e termina pelo mesmo caractere
		 */

		public bool ehString(string palavra)
		{
			char final;
			bool escape = false;

			if (palavra[0] != '\'' && palavra[0] != '\"')
				return false;

			final = palavra[0];
			//verifica se termina pelo mesmo simbolo que comeca
			//e esse simbolo nao aparece em nenhum outro lugar da string
			for (int i = 1; i < palavra.Length-1; i++)
			{
				if (!escape)
				{
					if (palavra[i] == '\\')
						escape = true;
					//se o simbolo de final de string esta no meio dela, nao eh uma string
					if (palavra[i] == final)
						return false;
				}
				else
				{
					escape = false;
				}
			}

			//se a palavra termina pelo caractere de final e este nao foi escapado, eh uma string
			return (palavra[palavra.Length - 1] == final && !escape);
		}

		/*
		 * verifica se os tipos de uma linha são validos
		 * linhas sao do tipo
		 * 0?.(1).(3+4)* ou
		 * 0?.(2).(4)*
		 * retorna true se forem
		 */
		public bool verificaTipos(Linha linha,Escritor saida,Definicoes defs)
		{
			Instrucao inst;
			int i = 0;
			int j = 1;
			int size = linha.tipos.Length;
			//a linha pode comecar por definicao de label
			if (linha.tipos[0] == 0)
			{
				defs.adicionaDef(linha.preprocessado[0].Substring(0,linha.preprocessado[0].Length-1),linha.linhaFonte,saida);
				i = 1;
				size--;
			}
			//a linha eh a definicao de uma label apenas
			if (i >= linha.tipos.Length)
				return true;

			if (linha.tipos[i] == (int)Tipos.INSTRUCAO)
			{
				inst = this.linguagem.instrucoes.Find(o => o.mnemonico == linha.preprocessado[i]);
				if(size < inst.formato.Length)
					saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Número incorreto de operandos. Esperava-se " + (inst.formato.Length - 1) + ", encontrou-se " + (size - 1));
				//verifica se ha algo diferente de registradores e enderecos
				for (i++; i < linha.tipos.Length; i++)
				{
					if (j >= inst.formato.Length)
					{
						if(linha.tipos[i] != (int)Tipos.ENDERECAMENTO || linha.tipos[i-1] != (int)Tipos.ENDERECO)
							saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Número incorreto de operandos. Esperava-se " + (inst.formato.Length-1) + ", encontrou-se " + (size-1));
						break;
					}
					if (linha.tipos[i] != inst.formato[j])
					{
						switch (linha.tipos[i])
						{
							case (int)Tipos.INVALIDO:
								saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Palavra inválida: " + linha.preprocessado[i]);
								break;
							case (int)Tipos.DEFLABEL:
								saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Labels só podem ser definidas no início da linha.");
								break;
							case (int)Tipos.INSTRUCAO:
							case (int)Tipos.DIRETIVA:
								saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Não pode ter mais de uma instrução ou diretiva por linha.");
								break;
							case (int)Tipos.ENDERECAMENTO:
								if (linha.tipos[i - 1] == (int)Tipos.ENDERECO)
								{
									size--;
									j--;
								}
								else
									saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Modos de endereçamento devem ser precedidos por endereços.");
								break;
						}
					}
					else if (linha.tipos[i] == (int)Tipos.ENDERECO)
					{
						if (ehLabel(linha.preprocessado[i]))
						{
							defs.adicionaRef(linha.preprocessado[i], linha.linhaFonte);
						}
					}
					j++;
				}
			}
			else if (linha.tipos[i] == (int)Tipos.DIRETIVA)
			{
				//se for a diretiva org, apenas 1 operando deve existir
				if(linha.preprocessado[i] == "ORG")
				{
					if(size != 2)
						saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Número incorreto de operandos. Esperava-se 1, encontrou-se " + (size - 1));
				}
				i++;
				//o restante da linha so pode conter enderecos
				while (i < linha.tipos.Length)
				{
					switch (liha.tipos[i])
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
				saida.errorOut(Escritor.ERRO, linha.linhaFonte, "Instrução ou diretiva inválida: " + linha.preprocessado[i]);
			}
			return true;
		}
    }
}
