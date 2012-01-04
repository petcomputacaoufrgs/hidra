using System;
using System.IO;
using System.Windows.Forms;

namespace Hidra
{
   
    public partial class MainWindow : Form
    {
        #region VARIÁVEIS
        public string streamText;
        const int memSize = 256;
        public int pc, negative, zero;
        public byte ac, inst;
        public byte[] memoria;
        public Instructions instructions;
        #endregion

        public MainWindow()
        {   
            InitializeComponent();
            this.ac = 0;
            this.pc = this.negative = this.zero = 0;
            this.memoria = new byte[memSize];
            this.instructions = new Instructions();
        }

        public void memToGrid()
        {
            for (int i = 0; i < memSize; i++)
            {
                gridData.Rows[i].Cells[1].Value = memoria[i];
            }
        }

        public void gridToMem()
        {
            for (int i = 0; i < gridData.RowCount; i++)
            {
                memoria[i] = byte.Parse(gridData.Rows[i].Cells[1].Value.ToString());
            }
        }

        public void atualizaTela_PC_AC_NEG_ZERO()
        {
            txt_ac.Text = ac.ToString();
            txt_pc.Text = pc.ToString();
            lbl_negative.Text = negative.ToString();
            lbl_zero.Text = zero.ToString();
        }

        //funções para override
        private void gridInstructions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;

            if (e.RowIndex >= 0 && e.ColumnIndex == 1 && gridInstructions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                if (int.TryParse(gridInstructions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out valor) && valor < memSize)
                {
                    gridInstructions.Rows[e.RowIndex].Cells[2].Value = this.instructions.getInstructionCode(valor);
                }
            }
        }

        public virtual void decodificaInstrucao()
        { }

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
                gridToMem();
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

        public void atualizaPC()
        {
            pc++;
            if (pc > 255)
                pc = 0;
        }

        private void btn_passoapasso_Click(object sender, EventArgs e)
        {           
            byte.TryParse(gridData.Rows[pc].Cells[1].Value.ToString(), out inst);
            atualizaPC();
            this.decodificaInstrucao();
        }

        private void btn_rodar_Click(object sender, EventArgs e)
        {

        }

    }
}
