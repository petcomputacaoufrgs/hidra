using System.Windows.Forms;

namespace Hidra
{
    public partial class Ahmes : MainWindow
    {
        Simulators.Ahmes Ahme = new Simulators.Ahmes();
        public int borrow, carry, overflow;

        public Ahmes()
        {
            InitializeComponent();
            this.instructions.initAhmesInstructions();
            this.borrow = 0;
            this.carry = 0;
            this.overflow = 0;
        }

        override public void atualizaTela()
        {
            txt_ac.Text = ac.ToString();
            txt_pc.Text = pc.ToString();
            lbl_negative.Text = negative.ToString();
            lbl_zero.Text = zero.ToString();
            txt_acessos.Text = numeroAcessos.ToString();
            txt_instrucoes.Text = numeroInstrucoes.ToString();
            lbl_borrowout.Text = borrow.ToString();
            lbl_carryout.Text = carry.ToString();
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
                    this.memoria[endereco] = Ahme.Store(this.ac);
                    atualizaPC();
                    break;
                case 32:  //LDA;
                    this.ac = Ahme.Load(endereco, memoria);
                    atualizaPC();
                    break;
                case 48:  //ADD;
                    this.ac = (byte)Ahme.Add(this.ac, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    break;
                case 64:  //OR;
                    this.ac = (byte)Ahme.Or(this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 80:  //AND;
                    this.ac = (byte)Ahme.And(this.ac, endereco, this.memoria);
                    atualizaPC();
                    break;
                case 96:  //NOT;
                    this.ac = (byte)Ahme.Not(this.ac);
                    numeroAcessos -= 2;
                    break;
                case 112: //SUB
                    this.ac = (byte)Ahme.Subtract(this.ac, endereco, this.memoria, out borrow);
                    atualizaPC();
                    break;       
                case 128: //JMP;
                    this.pc = Ahme.Jump((byte)endereco);
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
                    break;
                case 227: //ROL
                    this.ac = Ahme.RotateLeft(this.ac, out carry);
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
            this.atualizaTela();
        }
    }
}
