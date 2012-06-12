using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montador
{
	public class Definicoes
	{
		List<Label> labels = new List<Label>();

		/*
		 * Adiciona a definicao de uma label na lista
		 * se ja existir uma definicao com o mesmo nome, escreve um erro na saida
		 */
		public void adicionaDef(string label, int linha, Escritor saida)
		{
			Label lab;
			int p = this.labels.FindIndex(o => o.nome == label);
			if (p >= 0)
			{
				lab = this.labels[p];
				if (lab.linhaDef >= 0)
					saida.errorOut(Escritor.ERRO, linha, "Redefinição da label: " + label);
				else
				{
					lab.linhaDef = linha;
				}
			}
			else
			{
				lab = new Label(label);
				lab.linhas.Add(linha);
				this.labels.Add(lab);
			}
		}

		/*
		 * adiciona a referencia a uma label
		 */
		public void adicionaRef(string label, int linha)
		{
			Label lab = labels.Find(o => o.nome == label);
			lab.linhas.Add(linha);
		}

		/*
		 * verifica se todas as labels usadas foram definidas
		 * gera erro se alguma label nao definida for usada,
		 * gera avisos se alguma label definida nao foi usada
		 */
		public void verificaLabels(Escritor saida)
		{
			foreach (Label label in this.labels)
			{
				if (label.linhas.Count == 0)
				{
					saida.errorOut(saida.avisos, label.linhaDef, "Label não utilizada: " + label.nome);
				}
			}
		}

		/*
		 * atribui um valor a uma label
		 */
		public void atribuiDef(string label, int valor)
		{
			Label lab = this.labels.Find(o => o.nome == label);
			lab.valor = valor;
		}
	}
}
