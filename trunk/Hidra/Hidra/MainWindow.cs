using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            }
        }

        private void gridInstructions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                switch (int.Parse((string)(gridInstructions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
                {
                    case 0: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.NOP;
                        break;
                    case 16: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.STA;
                        break;
                }
            }
        }
    }
}
