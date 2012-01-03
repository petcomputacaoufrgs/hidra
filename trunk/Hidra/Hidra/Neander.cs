using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Hidra
{
    public partial class Neander : MainWindow
    {
        const int memSize = 256;

        public Neander()
        {
            InitializeComponent();
            
        }

        private void Neander_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < memSize; i++)
            {
                gridData.Rows.Add();
                gridInstructions.Rows.Add();
                gridData.Rows[i].Cells[0].Value = i;
                gridData.Rows[i].Cells[1].Value = 0;
                gridInstructions.Rows[i].Cells[0].Value = i;
            }
        }
        private void gridInstructions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == 1 && gridInstructions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (int.TryParse(gridInstructions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out valor) && valor < memSize)
                {
                    switch (valor)
                    {
                        case 0: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.NOP;
                            break;
                        case 16: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.STA;
                            break;
                        case 32: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.LDA;
                            break;
                        case 48: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.ADD;
                            break;
                        case 64: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.OR;
                            break;
                        case 80: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.AND;
                            break;
                        case 96: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.NOT;
                            break;
                        case 128: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JMP;
                            break;
                        case 144: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JN;
                            break;
                        case 160: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JZ;
                            break;
                        case 240: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.HLT;
                            break;
                        default: gridInstructions.Rows[e.RowIndex].Cells[2].Value = valor;
                            break;

                    }

                }
            }
        }
        private void gridInstructions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                if (int.TryParse(gridInstructions.Rows[e.RowIndex].Cells[1].Value.ToString(), out valor) && valor < memSize)
                {
                    gridData.Rows[e.RowIndex].Cells[1].Value = gridInstructions.Rows[e.RowIndex].Cells[1].Value;
                    gridInstructions_CellEndEdit(sender, e);
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
                    gridInstructions_CellEndEdit(sender, e);
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
                    if(msgSplit.Length > 1)
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
