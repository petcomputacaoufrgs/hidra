using System;
using System.Windows.Forms;

namespace Hidra
{
    public partial class Neander : MainWindow
    {
        Simulators.Neander Neand = new Simulators.Neander();
        
        public Neander()
        {
            InitializeComponent();
            this.instructions.initNeanderInstructions();
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
                    this.ac = byte.Parse(Neand.Add(this.ac, endereco, this.memoria).ToString());
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

            memToGrid();           
            atualizaTela_PC_AC_NEG_ZERO();
        }

        private void btn_rodar_Click(object sender, EventArgs e)
        {

        }        
    }
}
