using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hidra.Simulators;

namespace Hidra
{
   
    public partial class MainWindow : Form
    {
        const int memSize = 256;

        public MainWindow()
        {   
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            gridData.Rows.Add(memSize);
            gridInstructions.Rows.Add(memSize);

            for (int i = 0; i < memSize; i++)
            {
                gridData.Rows[i].Cells[0].Value = i;
                gridInstructions.Rows[i].Cells[0].Value = i;
                gridData.Rows[i].Cells[1].Value = 0;
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
                    }

                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    if (int.TryParse(gridData.Rows[e.RowIndex].Cells[1].Value.ToString(), out valor) && valor  < memSize)
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

    }
}
