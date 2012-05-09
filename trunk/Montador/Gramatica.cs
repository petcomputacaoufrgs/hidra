using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
    class Gramatica
    {

        public string[] inst;
        public enum Tipos { DEFLABEL, INSTRUCAO, DIRETIVA, REGISTRADOR, ENDERECO, ENDERECAMENTO, INVALIDO };
        private const char IMEDIATO = '#';

        private List<string> instrucoes;
        private List<string> diretivas;
        private List<string> registradores;
        private List<string> enderecamentos;


        /**
         * independente da maquina, cada linha do codigo fonte pode ser:
         * 
         * inst
         * inst end
         * inst r
         * inst r end
         * label
         * diretiva
         * 
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
            if (palavra.IndexOf(',') != -1)
            {
                aux = palavra.Split(',');
                esq = identificaTipo(aux[0], gramatica);
                dir = identificaTipo(aux[1], gramatica);

                return identificaTipo(esq, dir, gramatica);
            }

            if (ehNumero(palavra))
                return (int)Tipos.ENDERECO;
            //se nao for um numero, verifica se eh alguma palavra conhecida
            if (this.diretivas.BinarySearch(palavra) != -1)
                return (int)Tipos.DIRETIVA;
            if (this.instrucoes.BinarySearch(palavra) != -1)
                return (int)Tipos.INSTRUCAO;
            if (this.registradores.BinarySearch(palavra) != -1)
                return (int)Tipos.REGISTRADOR;
            if (this.enderecamentos.BinarySearch(palavra) != -1)
                return (int)Tipos.ENDERECAMENTO;

            if (ehLabel(palavra))
                return (int)Tipos.ENDERECO;

            //se nao for nada conhecido, entao eh invalido
            return (int)Tipos.INVALIDO;

        }

        /*
         * verifica se a string corresponde a um numero em hexa ou em decimal, podendo ser precedida ou nao por IMEDIATO ou '-'
         */
        private bool ehNumero(string palavra)
        {
            int i = 0;
            bool hexa = false;
            bool numero = true;
            if (palavra[0] == IMEDIATO || palavra[0] == '-')
                i++;

            if (palavra[i] == '0')
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
            if (palavra[i] == 'H' && hexa && i == palavra.Length)
                return true;
            return false;

        }

        public int identificaTipo(int esquerda, int direita, Gramatica gramatica)
        {
            return 0;
        }
    }
}
