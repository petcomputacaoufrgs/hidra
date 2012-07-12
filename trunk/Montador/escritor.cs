using System;
using System.IO;

namespace Montador
{
	public class Escritor : Dados
	{
		public enum Message { UndefinedLabel, RedefinedLabel,InvalidMachine,IncorrectNumOperands,InvalidInstructionOrDirective,
								InvalidWord,IncorrectNumInstructions,IncorrectLabelDef,InvalidOperand,
								UnusedLabel,TruncatedValue,OverwrittenMemory};

		public int erros = 0;
		public int avisos = 0;

		public string arquivo;
		/**
		 * tipo: ERRO ou AVISO
		 * nlinha eh o numero da linha em que ocorreu o erro/aviso
		 * escreve o erro ou aviso no arquivo dado
		 * formato:
		 * TIPO: descricao (linha nlinha)
		 */
		public void errorOut(Message tipo, params Object[] vals)
		{
			string linha = " (linha )".Insert(8, vals[0].ToString());
			string texto = "";

			//determina se eh um erro ou um aviso
			switch (tipo)
			{
				case Message.IncorrectNumOperands:
					texto = String.Format("ERRO: Esperava-se {0} operandos, encontrou-se {1}",vals[1],vals[2]);
					erros++;
					break;
				case Message.InvalidInstructionOrDirective:
					texto = String.Format("ERRO: Instrução ou diretiva inválida:{0}",vals[1]);
					erros++;
					break;
				case Message.InvalidMachine:
					texto = String.Format("ERRO: Máquina inválida:{0}",vals[1]);
					erros++;
					break;
				case Message.RedefinedLabel:
					texto = String.Format("ERRO: Label redefinida: {0}. A definição anterior estava na linha {1}",vals[1],vals[2]);
					erros++;
					break;
				case Message.UndefinedLabel:
					texto = String.Format("ERRO: Label não definida: {0}",vals[1]);
					erros++;
					break;
				case Message.InvalidWord:
					texto = String.Format("ERRO: Palavra inválida: {0}",vals[1]);
					erros++;
					break;
				case Message.IncorrectNumInstructions:
					texto = String.Format("ERRO: Só pode haver uma instrução ou diretiva por linha");
					erros++;
					break;
				case Message.IncorrectLabelDef:
					texto = String.Format("ERRO: Labels devem ser definidas no início da linha");
					erros++;
					break;
				case Message.InvalidOperand:
					texto = String.Format("ERRO: Operando inválido:{0}",vals[1]);
					break;

				case Message.UnusedLabel:
					texto = String.Format("AVISO: Label não utilizada: {0}",vals[1]);
					avisos++;
					break;
				case Message.TruncatedValue:
					texto = String.Format("AVISO: O valor ({0}) possui {1} bytes e foi truncado para {2}.",vals[1],vals[2],vals[3]);
					avisos++;
					break;
			}

			//acrescenta a descricao do erro e a linha em que ocorreu
			if(tipo != Message.InvalidMachine)
				texto += linha;

			//abre o arquivo para appending
			System.IO.StreamWriter file = new System.IO.StreamWriter(this.arquivo, true);
			try
			{
				file.WriteLine(texto);
			}
			finally
			{
				file.Close();
			}
		}

		/**
		 * escreve o arquivo binario para a maquina dada
		 * 
		 */
		public void escreveBinario(int maquina, byte[] binario, string arquivo)
		{

		}

	}
}