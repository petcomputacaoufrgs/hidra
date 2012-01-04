using System;
using System.Windows.Forms;

namespace Hidra
{
    public partial class Neander : MainWindow
    {
        const int memSize = 256;
        Simulators.Neander Neand = new Simulators.Neander();
        byte endereco;
        
                         

        public Neander()
        {
            InitializeComponent();            
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
            }
        }

        
        
        private void Neander_Load(object sender, EventArgs e)
        {
            criaMemoria();
            for (int i = 0; i < memSize; i++)
            {
                memoria[i] = 0;                
            }
        }

        override public void gridInstructions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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


        override public void decodificaInstrucao()
        {
            endereco = byte.Parse(gridData.Rows[pc].Cells[1].Value.ToString());

            switch (inst)
            {
                case 0:   //NOP;
                    Neand.Nop();
                    break;
                case 16:  //STA;
                    this.memoria[endereco] = Neand.Store(this.ac);
                    break;
                case 32:  //LDA;
                    this.ac = Neand.Load(endereco, memoria);
                    break;
                case 48:  //ADD;
                    Neand.Add();
                    break;
                case 64:  //OR;
                    Neand.Or();
                    break;
                case 80:  //AND;
                    Neand.And();
                    break;
                case 96:  //NOT;
                    Neand.Not();
                    break;
                case 128: //JMP;
                    Neand.Jump();
                    break;
                case 144: //JN;
                    Neand.JumpOnNegative();
                    break;
                case 160: //JZ;
                    Neand.JumpOnZero();
                    break;
                case 240: //HLT;
                    Neand.Halt();
                    break;

                default: 
                    break;                
            }
         //   memToGrid();           
            atualizaTela_PC_AC_NEG_ZERO();
        }



        private void btn_rodar_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}
