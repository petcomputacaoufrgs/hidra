using System.Windows.Forms;

namespace Hidra
{
    public partial class Ahmes : MainWindow
    {
        Simulators.Ahmes Ahme = new Simulators.Ahmes();

        public Ahmes()
        {
            InitializeComponent();
            this.instructions.initAhmesInstructions();
            this.borrow = 0;
            this.carry = 0;
            this.overflow = 0;
            atualizaVariaveis();
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
            if (borrow == 1)
                picture_borrow.Image = Properties.Resources.luz_acessa;
            else if (borrow == 0)
                picture_borrow.Image = Properties.Resources.luz_apagada;
            if (carry == 1)
                picture_carry.Image = Properties.Resources.luz_acessa;
            else if (carry == 0)
                picture_carry.Image = Properties.Resources.luz_apagada;
            if (overflow == 1)
                picture_overflow.Image = Properties.Resources.luz_acessa;
            else if (overflow == 0)
                picture_overflow.Image = Properties.Resources.luz_apagada;

        }

        override public void decodificaInstrucao()
        {
            endereco = byte.Parse(gridData.Rows[pc].Cells[1].Value.ToString());

            numeroInstrucoes++;
            numeroAcessos += 3;

            switch (inst)
            {
                case 0:   //NOP;
                    Ahme.Nop();
                    numeroAcessos -= 2;
                    break;
                case 16:  //STA;
                    Ahme.Store(this.ac, endereco, ref this.memoria);
                    atualizaPC();
                    break;                
                case 32:  //LDA;                   
                    Ahme.Load(ref this.ac,endereco, memoria);
                    atualizaPC();
                    break;
                case 48:  //ADD;
                    Ahme.AddWithCarry(ref this.ac, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    break;
                case 64:  //OR;
                    Ahme.Or(ref this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 80:  //AND;
                    Ahme.And(ref this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 96:  //NOT;
                    Ahme.Not(ref this.ac);
                    numeroAcessos -= 2;
                    break;
                case 112: //SUB
                    Ahme.Subtract(ref this.ac, endereco, this.memoria, out borrow);
                    atualizaPC();
                    break;       
                case 128: //JMP;
                    Ahme.Jump(ref this.pc, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 144: //JN;
                    this.pc = Ahme.JumpOnNegative(this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 148: //JP
                    this.pc = Ahme.JumpOnPositive(this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 152: //JV
                    this.pc = Ahme.JumpOnOverflow(this.pc, this.overflow, endereco);
                    numeroAcessos -= 1;
                    break;
                case 156: //JNV
                    this.pc = Ahme.JumpOnNotOverflow(this.pc, this.overflow, endereco);
                    numeroAcessos -= 1;
                    break;
                case 160: //JZ;
                    this.pc = (byte)Ahme.JumpOnZero(this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 164: //JNZ
                    this.pc = Ahme.JumpOnNotZero(this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 176: //JC
                    this.pc = Ahme.JumpOnCarry(this.pc, this.carry, endereco);
                    numeroAcessos -= 1;
                    break;
                case 180: //JNC
                    this.pc = Ahme.JumpOnNotCarry(this.pc, this.carry, endereco);
                    numeroAcessos -= 1;
                    break;
                case 184: //JB
                    this.pc = Ahme.JumpOnBorrow(this.pc, this.borrow, endereco);
                    numeroAcessos -= 1;                    
                    break;
                case 188: //JNB
                    this.pc = Ahme.JumpOnNotBorrow(this.pc, this.borrow, endereco);
                    numeroAcessos -= 1;
                    break;
                case 224: //SHR
                    this.ac = (byte)Ahme.ShiftRight(this.ac, out carry);
                    numeroAcessos -= 2;
                    break;
                case 225: //SHL
                    this.ac = (byte)Ahme.ShiftLeft(this.ac, out carry);
                    numeroAcessos -= 2;
                    break;
                case 226: //ROR
                    this.ac = Ahme.RotateRight(this.ac, out carry);
                    numeroAcessos -= 2;
                    break;
                case 227: //ROL
                    this.ac = Ahme.RotateLeft(this.ac, out carry);
                    numeroAcessos -= 2;
                    break;
                case 240: //HLT;
                    this.hlt = Ahme.Halt();
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

        private void Ahmes_Load(object sender, System.EventArgs e)
        {
            memToGrid();
            this.atualizaTela();
        }
    }
}
