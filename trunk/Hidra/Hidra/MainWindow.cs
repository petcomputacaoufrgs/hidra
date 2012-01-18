using System;
using System.IO;
using System.Windows.Forms;

namespace Hidra
{
    public partial class MainWindow : Form
    {
        #region VARIÁVEIS
        public string streamText;
        public const int memSize = 256;
        public bool hlt;
        public int pc, negative, zero, endereco, numeroInstrucoes, numeroAcessos;
        public byte ac, inst;
        public byte[] memoria;
        public Instructions instructions;
        public byte rA, rB, rX, rN;
        public int carry, borrow, overflow;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.ac = 0;
            this.pc = this.negative = this.endereco = 0;
            this.zero = 1;
            this.memoria = new byte[memSize];
            this.instructions = new Instructions();
            this.hlt = false;
            this.numeroInstrucoes = 0;
            this.numeroAcessos = 0;
            this.carry = 0;
            this.rA = this.rB = this.rX = this.rN = 0;

            criaMemoria();
        }

        private void criaMemoria()
        {
            for (int i = 0; i < memSize; i++)
            {
                gridData.Rows.Add();
                gridInstructions.Rows.Add();
                gridData.Rows[i].Cells[0].Value = i;
                gridData.Rows[i].Cells[1].Value = 0;
                gridInstructions.Rows[i].Cells[0].Value = i;

                this.memoria[i] = 0;
            }
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

        //funções para override
        public virtual void decodificaInstrucao()
        { }

        public virtual void atualizaTela()
        { }

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

        private void gridInstructions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                if (gridData.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    if (int.TryParse(gridInstructions.Rows[e.RowIndex].Cells[1].Value.ToString(), out valor) && valor < memSize)
                    {
                        gridData.Rows[e.RowIndex].Cells[1].Value = gridInstructions.Rows[e.RowIndex].Cells[1].Value;
                        this.gridInstructions_CellEndEdit(sender, e);
                    }
                }
                else
                {
                    gridData.Rows[e.RowIndex].Cells[1].Value = 0;
                    gridData.Rows[e.RowIndex].Cells[1].Value = gridInstructions.Rows[e.RowIndex].Cells[1].Value;
                }
                gridToMem();                
            }
        }

        private void gridData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int valor = 0;

            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                if (gridData.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    if (int.TryParse(gridData.Rows[e.RowIndex].Cells[1].Value.ToString(), out valor) && valor < memSize)
                    {
                        gridInstructions.Rows[e.RowIndex].Cells[1].Value = gridData.Rows[e.RowIndex].Cells[1].Value;
                        this.gridInstructions_CellEndEdit(sender, e);
                    }
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

            string arquivo, msg;
            string[] msgSplit;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
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
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName))
                {
                    for (int i = 0; i < 256; i++)
                    {
                        file.WriteLine(i + " " + gridInstructions.Rows[i].Cells[1].Value.ToString());
                    }
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

                this.memoria[i] = 0;
            }
        }

        public void atualizaNegative(byte register)
        {
            negative = (register > 127) ? 1 : 0;
        }

        public void atualizaZero(byte register)
        {
            zero = (register == 0) ? 1 : 0;
        }

        public void atualizaPC()
        {
            pc = (pc == 255) ? 0 : pc+1;
        }

        public void voltaPC()
        {
            pc = (pc == 0) ? 255 : pc-1;
        }

        public void atualizaVariaveis(byte register)
        {
            atualizaNegative(register);
            atualizaZero(register);
        }

        private void executeInstruction()
        {
            byte.TryParse(gridData.Rows[pc].Cells[1].Value.ToString(), out inst);
            atualizaPC();
            this.decodificaInstrucao();
        }

        private void btn_passoapasso_Click(object sender, EventArgs e)
        {
            executeInstruction();
        }

        private void btn_rodar_Click(object sender, EventArgs e)
        {
            while (pc != 255 && hlt == false)
            {
                executeInstruction();
            }
        }

        private void zerarPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pc = 0;
            numeroAcessos = 0;
            numeroInstrucoes = 0;
            this.atualizaTela();
            hlt = false;
        }

        private void txt_pc_DoubleClick(object sender, EventArgs e)
        {
            AlteraRegistrador form = new AlteraRegistrador(this, "PC");
            form.Show();
        }
    }
}
