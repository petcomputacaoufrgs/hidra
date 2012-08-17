using System;
using System.IO;
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
			Gramatica gram = new Gramatica();

			Escritor esc = new Escritor();
			Codigo code = new Codigo();
			//se nenhuma maquina foi passada, produz um erro
			if (args.Length < 3)
			{
				Console.WriteLine("Uso:");
				Console.WriteLine("montador <codigo_fonte> <destino> <maquina>");
				return;
			}
			string maquina = args[2].ToLower();
			string saidaErro = args[1] + ".err";	//nome do arquivo para a saida de erro
			string saidaMem = args[1] + ".mem";
			esc.arquivo = saidaErro;
			System.IO.File.Delete(saidaErro);

			//carrega a gramatica para a maquina
			gram.linguagem.carrega(maquina);
			//gram.linguagem.print();

			//le o codigo fonte
			code.lerCodigo(args[0]);
			//code.print();

			//code.converteHexa();
			code.identificaTipos(gram);
			//code.print();

			//verifica se as linhas sao validas para a maquina
			if (code.ehValido(gram, esc) == false)
			{
				return;
			}

			//code.print();

			code.defs.verificaLabels(esc);
			//code.print();

			byte[] binario;

			if (esc.erros == 0)
			{
				binario = code.montar(256, gram.linguagem, esc);

				using (BinaryWriter binWriter = new BinaryWriter(File.Open(saidaMem, FileMode.Create)))
				{
					binWriter.Write(binario);
				}
			}

			//code.print();
		}
	}
}
