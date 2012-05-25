using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Definicoes
	{
		List<Label> defs = new List<Label>();
		List<Label> referencias = new List<Label>();

		/*
		 * Adiciona a definicao de uma label na lista
		 * se ja existir uma definicao com o mesmo nome, escreve um erro na saida
		 */
		public void adicionaDef(string label, int linha, Escritor saida)
		{
			if (defs.FindIndex(o => o.nome == label) >= 0)
			{
				saida.errorOut(Escritor.ERRO, linha, "Redefinição da label: " + label);
			}
			else
			{
				defs.Add(new Label(label, linha));
			}
		}

		/*
		 * adiciona a referencia a uma label
		 */
		public void adicionaRef(string label, int linha)
		{
			referencias.Add(new Label(label,linha));
		}

		/*
		 * verifica se todas as labels usadas foram definidas
		 * gera erro se alguma label nao definida for usada,
		 * gera avisos se alguma label definida nao foi usada
		 */
		public void verificaLabels(Escritor saida)
		{
			foreach (Label uso in referencias)
			{
				if (defs.FindIndex(o => o.nome == uso.nome) < 0)
				{
					saida.errorOut(Escritor.ERRO,uso.linha,"Label nao definida: "+uso.nome);
				}
			}

			foreach(Label def in defs)
			{
				if(referencias.FindIndex(o => o.nome == def.nome) <0)
					saida.errorOut(Escritor.AVISO, def.linha, "Label nao utilizada: " + def.nome);
			}
		}
	}
}
