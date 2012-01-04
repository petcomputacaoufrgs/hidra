using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hidra.Simulators;
using System.IO;

namespace Hidra
{
   
    public partial class MainWindow : Form
    {
        public string streamText;
        const int memSize = 256;

        public MainWindow()
        {   
            InitializeComponent();
        }

        public virtual void gridInstructions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gridInstructions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                if (int.TryParse(gridInstructions.Rows[e.RowIndex].Cells[1].Value.ToString(), out valor) && valor < memSize)
                {
                    gridData.Rows[e.RowIndex].Cells[1].Value = gridInstructions.Rows[e.RowIndex].Cells[1].Value;
                    this.gridInstructions_CellEndEdit(sender, e);
                }
                else
                {
                    gridData.Rows[e.RowIndex].Cells[1].Value = 0;
                    gridData.Rows[e.RowIndex].Cells[1].Value = gridInstructions.Rows[e.RowIndex].Cells[1].Value;
                }
            }
        }

        private void gridData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;

            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                if (int.TryParse(gridData.Rows[e.RowIndex].Cells[1].Value.ToString(), out valor) && valor < memSize)
                {
                    gridInstructions.Rows[e.RowIndex].Cells[1].Value = gridData.Rows[e.RowIndex].Cells[1].Value;
                    this.gridInstructions_CellEndEdit(sender, e);
                }
                else
                {
                    gridData.Rows[e.RowIndex].Cells[1].Value = 0;
                    gridInstructions.Rows[e.RowIndex].Cells[1].Value = gridData.Rows[e.RowIndex].Cells[1].Value;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string arquivo, msg;
            string[] msgSplit;
            arquivo = openFileDialog1.FileName;
            using (StreamReader texto = new StreamReader(arquivo))
            {
                while ((msg = texto.ReadLine()) != null)
                {
                    msgSplit = msg.Split(' ');
                    if (msgSplit.Length > 1)
                        gridData.Rows[int.Parse(msgSplit[0])].Cells[1].Value = msgSplit[1];
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName))
            {
                for (int i = 0; i < 256; i++)
                {
                    file.WriteLine(i + " " + gridInstructions.Rows[i].Cells[1].Value.ToString());
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About formAbout = new About();
            formAbout.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < memSize; i++)
            {
                gridData.Rows[i].Cells[0].Value = i;
                gridData.Rows[i].Cells[1].Value = 0;
                gridInstructions.Rows[i].Cells[0].Value = i;
            }
        }
    }
}
