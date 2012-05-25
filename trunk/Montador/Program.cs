using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
    class Program
    {        
        /**
         * o primeiro argumento eh o arquivo que contem o codigo fonte
         * o segundo eh o nome do arquivo de saida (sem a extensao)
         * o terceiro eh o nome da maquina
         * a extensao do arquivo do binario sera .mem
         * a extensao do arquivo com os erros e avisos sera .err
         */
        static void Main(string[] args)
        {
            DateTime t0 = DateTime.Now;
            Dados dados = new Dados();
            Gramatica gram = new Gramatica();
            
            Escritor esc = new Escritor();
            Codigo code = new Codigo();
            //se nenhuma maquina foi passada, produz um erro
            if (args.Length < 3)
            {                
                return;
            }

            
            int i,max;
            Boolean achou = false;
            string maquina = args[2].ToLower();
            string saidaErro = args[1]+".err";  //nome do arquivo para a saida de erro
			esc.arquivo = saidaErro;
            System.IO.File.Delete(saidaErro);
            string erro = "";
            max = maquina.Length;

            //verifica se a maquina recebida eh uma das maquinas disponiveis
            for (i = 0; i < max && !achou; i++)
            {
                if (maquina == dados.maquinasDisponiveis[i])
                {
                    achou = true;
                }
            }
            //se a maquina nao foi encontrada, escreve um erro
            if (!achou)
            {
                erro = maquina + " não é uma maquina válida.";
                esc.errorOut(Escritor.ERRO,0,erro);
                return;
            }
            //carrega a gramatica para a maquina
            gram.carrega(maquina);

            //le o codigo fonte
            code.lerCodigo(args[0]);
            code.print();

            //code.converteHexa();
            //code.identificaTipos(gram);
            //code.print();
            
            //verifica se as linhas sao validas para a maquina
            if (code.ehValido(gram,esc) == false)
            {
                return;
            }
			code.defs.verificaLabels(esc);

            //code.Monta();


			/*
            foreach (string[] a in code.preprocessado)
            {
                foreach (string b in a)
                {
                    if(gram.ehNumero(b))
                    {
                        Console.WriteLine(b);
                    }
                }
            }*/
            /*
            Console.Write("Instruções:\n");

            foreach (string inst in gram.instrucoes)
            {
                Console.WriteLine(inst);
            }

            Console.WriteLine("Tipos:\n");

            foreach (int[] a in code.tipos)
            {
                foreach (int b in a)
                {
                    Console.Write(b + " ");
                }
                Console.Write("\n");
            }
            */
        }
    }
}
