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

        override public void atualizaTela()
        {
            txt_ac.Text = ac.ToString();
            txt_pc.Text = pc.ToString();
            lbl_negative.Text = negative.ToString();
            lbl_zero.Text = zero.ToString();
            txt_acessos.Text = numeroAcessos.ToString();
            txt_instrucoes.Text = numeroInstrucoes.ToString();
        }

        override public void decodificaInstrucao()
        {
            endereco = byte.Parse(gridData.Rows[pc].Cells[1].Value.ToString());

            numeroInstrucoes++;
            numeroAcessos += 3;

            switch (inst)
            {
                case 0:   //NOP;
                    Neand.Nop();
                    numeroAcessos -= 2;
                    break;
                case 16:  //STA;
                    this.memoria[endereco] = Neand.Store(this.ac);
                    atualizaPC();
                    break;
                case 32:  //LDA;
                    this.ac = Neand.Load(endereco, memoria);
                    atualizaPC();
                    break;
                case 48:  //ADD;
                    this.ac = (byte)Neand.Add(this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 64:  //OR;
                    this.ac = (byte)Neand.Or(this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 80:  //AND;
                    this.ac = (byte)Neand.And(this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 96:  //NOT;
                    this.ac = (byte)Neand.Not(this.ac);
                    numeroAcessos -= 2;
                    break;
                case 128: //JMP;
                    this.pc = Neand.Jump((byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 144: //JN;
                    this.pc = (byte)Neand.JumpOnNegative(this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 160: //JZ;
                    this.pc = (byte)Neand.JumpOnZero(this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 240: //HLT;
                    this.hlt = Neand.Halt();
                    numeroAcessos -= 2;
                    break;

                //qualquer numero diferente dos de cima
                default:
                    numeroAcessos -= 2;
                    break;                
            }

            memToGrid();
            atualizaVariaveis();
            this.atualizaTela();
        }

        private void Neander_Load(object sender, EventArgs e)
        {
            this.atualizaTela();
        }  
    }
}
