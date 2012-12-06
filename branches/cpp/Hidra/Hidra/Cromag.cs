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
    public partial class Cromag : MainWindow
    {
        Simulators.Cromag Crom = new Simulators.Cromag();
        public Cromag()
        {
            InitializeComponent();
            this.instructions.initCromagInstructions();
        }

        override public void atualizaTela()
        {
            txt_ac.Text = ac.ToString();
            txt_pc.Text = pc.ToString();
            txt_acessos.Text = numeroAcessos.ToString();
            txt_instrucoes.Text = numeroInstrucoes.ToString();

            if (negative == 1)
                picture_neg.Image = Properties.Resources.luz_acessa;
            else if (negative == 0)
                picture_neg.Image = Properties.Resources.luz_apagada;

            if (zero == 1)
                picture_zero.Image = Properties.Resources.luz_acessa;
            else if (zero == 0)
                picture_zero.Image = Properties.Resources.luz_apagada;
            if (carry == 1)
                picture_carry.Image = Properties.Resources.luz_acessa;
            else if (carry == 0)
                picture_carry.Image = Properties.Resources.luz_apagada;

            for (int i = 0; i < memSize; i++)
                gridInstructions.Rows[i].Selected = false;
            gridInstructions.Rows[pc].Selected = true;

        }

        override public void decodificaInstrucao()
        {
            endereco = byte.Parse(gridData.Rows[pc].Cells[1].Value.ToString());

            numeroInstrucoes++;
            numeroAcessos += 3;

            switch (inst)
            {
                case 0:   //NOP;
                    Crom.Nop();
                    numeroAcessos -= 2;
                    break;
                case 16:  //STA;
                    Crom.Store(this.ac, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 17:  //STA Indirect
                    Crom.StoreIndirect(this.ac,  endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 32:  //LDA;
                    Crom.Load(ref this.ac, endereco, memoria);
                    atualizaPC();
                    break;
                case 33:  //LDA indirect

                case 48:  //ADD;
                    Crom.Add(ref this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 64:  //OR;
                    Crom.Or(ref this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 80:  //AND;
                    Crom.And(ref this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 96:  //NOT;
                    Crom.Not(ref this.ac);
                    numeroAcessos -= 2;
                    break;
                case 128: //JMP;
                    Crom.Jump(ref this.pc, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 144: //JN;
                    Crom.JumpOnNegative(ref this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 160: //JZ;
                    Crom.JumpOnZero(ref this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 240: //HLT;
                    this.hlt = Crom.Halt();
                    numeroAcessos -= 2;
                    break;

                //qualquer numero diferente dos de cima
                default:
                    numeroAcessos -= 2;
                    break;
            }

            memToGrid();
            atualizaVariaveis(this.ac);
            this.atualizaTela();
        }

        private void txt_ac_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AlteraRegistrador form = new AlteraRegistrador(this, "AC");
            form.Show();
        }
    }
}
