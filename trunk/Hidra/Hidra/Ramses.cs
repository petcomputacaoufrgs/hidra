using System.Windows.Forms;

namespace Hidra
{
    public partial class Ramses : MainWindow
    {
        Simulators.RamsesPericles Rams = new Simulators.RamsesPericles();
        public int carry;
        public byte rA, rB, rX,rN;

        public Ramses()
        {
            InitializeComponent();
            this.instructions.initRamsesInstructions();
            this.carry = 0;
            this.rA = 0;
            this.rB = 0;
            this.rX = 0;
            this.rN = 0;
        }

        override public void atualizaTela()
        {
            txt_ac.Text = ac.ToString();
            txt_pc.Text = pc.ToString();
            txt_ra.Text = rA.ToString();
            txt_rb.Text = rB.ToString();
            txt_rx.Text = rX.ToString();
            txt_acessos.Text = numeroAcessos.ToString();
            txt_instrucoes.Text = numeroInstrucoes.ToString();
            lbl_negative.Text = negative.ToString();
            lbl_zero.Text = zero.ToString();
            lbl_carryout.Text = carry.ToString();
        }

        override public void decodificaInstrucao()
        {
            endereco = byte.Parse(gridData.Rows[pc].Cells[1].Value.ToString());

            numeroInstrucoes++;
            numeroAcessos += 3;

            atualizaPC();
            switch (inst)
            {
                case 0:   //NOP;
                    Rams.Nop();
                    numeroAcessos -= 2;
                    voltaPC();
                    break;
                case 16:  //STA; //A,n
                    Rams.Store(this.rA, endereco, ref this.memoria);                    
                    break;                 
                    case 17: //A,nI
                        Rams.StoreIndirect(this.rA, endereco, ref this.memoria);                   
                        break;
                    case 18: //A,#n
                        Rams.StoreImmediat(this.rA, this.pc, ref this.memoria);
                        break;
                    case 19: //A,nX
                        Rams.StoreIndexed(this.rA, this.rX, this.pc, ref this.memoria);
                        break;
                    case 20: //B,n
                        Rams.Store(this.rB, endereco, ref this.memoria);   
                        break;
                    case 21: //B,nI
                        Rams.StoreIndirect(this.rB, endereco, ref this.memoria); 
                        break;
                    case 22: //B,#n
                        Rams.StoreImmediat(this.rB, this.pc, ref this.memoria);
                        break;
                    case 23: //B,nX
                        Rams.StoreIndexed(this.rB, this.rX, this.pc, ref this.memoria);
                        break;
                    case 24: //X,n
                        Rams.Store(this.rX, endereco, ref this.memoria);   
                        break;
                    case 25: //X,nI
                        Rams.StoreIndirect(this.rX, endereco, ref this.memoria); 
                        break;
                    case 26: //X,#n
                        Rams.StoreImmediat(this.rX, this.pc, ref this.memoria);
                        break;
                    case 27: //X,nX
                        Rams.StoreIndexed(this.rX, this.rX, this.pc, ref this.memoria);
                        break;
                    case 28: //?,n
                        Rams.Store(this.rN, endereco, ref this.memoria);   
                        break;
                    case 29: //?,nI
                        Rams.StoreIndirect(this.rN, endereco, ref this.memoria); 
                        break;
                    case 30: //?,#n
                        Rams.StoreImmediat(this.rN, this.pc, ref this.memoria);
                        break;
                    case 31: //?,nX
                        Rams.StoreIndexed(this.rN, this.rX, this.pc, ref this.memoria);
                        break;

                case 32:  //LDA;                   
                    Rams.Load(ref this.rA,endereco, memoria);
                    break;

                    case 33: //A,nI
                        Rams.LoadIndirect(ref this.rA,endereco, this.memoria);
                        break;
                    case 34: //A,#n
                        Rams.LoadImmediat(ref this.rA, endereco);
                        break;
                    case 35: //A,nX
                        Rams.LoadIndexed(ref this.rA, this.rX, endereco, this.memoria);
                        break;
                    case 36: //B,n
                        Rams.Load(ref this.rB,endereco, memoria);
                        break;
                    case 37: //B,nI
                        Rams.LoadIndirect(ref this.rB, endereco, this.memoria);
                        break;
                    case 38: //B,#n
                        Rams.LoadImmediat(ref this.rB, endereco);
                        break;
                    case 39: //B,nX
                        Rams.LoadIndexed(ref this.rB, this.rX, endereco, this.memoria);
                        break;
                    case 40: //X,n
                        Rams.Load(ref this.rX, endereco, memoria);
                        break;
                    case 41: //X,nI
                        Rams.LoadIndirect(ref this.rX, endereco, this.memoria);
                        break;
                    case 42: //X,#n
                        Rams.LoadImmediat(ref this.rX, endereco);
                        break;
                    case 43: //X,nX
                        Rams.LoadIndexed(ref this.rX, this.rX, endereco, this.memoria);
                        break;
                    case 44: //?,n
                        Rams.Load(ref this.rN, endereco, memoria);
                        break;
                    case 45: //?,nI
                        Rams.LoadIndirect(ref this.rN, endereco, this.memoria);
                        break;
                    case 46: //?,#n
                        Rams.LoadImmediat(ref this.rN, endereco);
                        break;
                    case 47: //?,nX
                        Rams.LoadIndexed(ref this.rN, this.rX, endereco, this.memoria);
                        break;

                case 48:  //ADD;
                    Rams.AddWithCarry( ref this.rA, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    break;

                    case 49: //A,nI
                        Rams.AddWithCarryIndirect(ref this.rA, endereco, this.memoria, out this.carry);
                        break;
                    case 50: //A,#n
                        Rams.AddWithCarryImmediat(ref this.rA, endereco, out this.carry);
                        break;
                    case 51: //A,nX
                        Rams.AddWithCarryIndexed(ref this.rA, this.rX, endereco, memoria, out this.carry);
                        break;
                    case 52: //B,n
                        Rams.AddWithCarry(ref this.rB, endereco, this.memoria, out this.carry);
                        break;
                    case 53: //B,nI
                        Rams.AddWithCarryIndirect(ref this.rB, endereco, this.memoria, out this.carry);
                        break;
                    case 54: //B,#n
                        Rams.AddWithCarryImmediat(ref this.rB, endereco, out this.carry);
                        break;
                    case 55: //B,nX
                        Rams.AddWithCarryIndexed(ref this.rB, this.rX, endereco, memoria, out this.carry);
                        break;
                    case 56: //X,n
                        Rams.AddWithCarry(ref this.rX, endereco, this.memoria, out this.carry);
                        break;
                    case 57: //X,nI
                        Rams.AddWithCarryIndirect(ref this.rX, endereco, this.memoria, out this.carry);
                        break;
                    case 58: //X,#n
                        Rams.AddWithCarryImmediat(ref this.rX, endereco, out this.carry);
                        break;
                    case 59: //X,nX
                        Rams.AddWithCarryIndexed(ref this.rX, this.rX, endereco, memoria, out this.carry);
                        break;
                    case 60: //?,n
                        Rams.AddWithCarry(ref this.rN, endereco, this.memoria, out this.carry);
                        break;
                    case 61: //?,nI
                        Rams.AddWithCarryIndirect(ref this.rN, endereco, this.memoria, out this.carry);
                        break;
                    case 62: //?,#n
                        Rams.AddWithCarryImmediat(ref this.rN, endereco, out this.carry);
                        break;
                    case 63: //?,nX
                        Rams.AddWithCarryIndexed(ref this.rN, this.rX, endereco, memoria, out this.carry);
                        break;

                case 64:  //OR;
                    Rams.Or(ref this.rA, endereco, this.memoria);
                    break;
                    
                    case 65: //A,nI
                        Rams.OrIndirect(ref this.rA, endereco, this.memoria);
                        break;
                    case 66: //A,#n
                        Rams.OrImmediat(ref this.rA, endereco, this.memoria);
                        break;
                    case 67: //A,nX
                        Rams.OrIndexed(ref this.rA, this.rX, endereco, this.memoria);
                        break;
                    case 68: //B,n
                        Rams.Or(ref this.rB, endereco, this.memoria);
                        break;
                    case 69: //B,nI
                        Rams.OrIndirect(ref this.rB, endereco, this.memoria);
                        break;
                    case 70: //B,#n
                        Rams.OrImmediat(ref this.rB, endereco, this.memoria);
                        break;
                    case 71: //B,nX
                        Rams.OrIndexed(ref this.rB, this.rX, endereco, this.memoria);
                        break;
                    case 72: //X,n
                        Rams.Or(ref this.rX, endereco, this.memoria);
                        break;
                    case 73: //X,nI
                        Rams.OrIndirect(ref this.rX, endereco, this.memoria);
                        break;
                    case 74: //X,#n
                        Rams.OrImmediat(ref this.rX, endereco, this.memoria);
                        break;
                    case 75: //X,nX
                        Rams.OrIndexed(ref this.rX, this.rX, endereco, this.memoria);
                        break;
                    case 76: //?,n
                        Rams.Or(ref this.rN, endereco, this.memoria);
                        break;
                    case 77: //?,nI
                        Rams.OrIndirect(ref this.rN, endereco, this.memoria);
                        break;
                    case 78: //?,#n
                        Rams.OrImmediat(ref this.rN, endereco, this.memoria);
                        break;
                    case 79: //?,nX
                        Rams.OrIndexed(ref this.rN, this.rX, endereco, this.memoria);
                        break;

                case 80:  //AND;
                    Rams.And(ref this.rA, endereco, this.memoria);
                    break;

                    case 81: //A,nI
                        Rams.AndIndirect(ref this.rA, endereco, this.memoria);
                        break;
                    case 82: //A,#n
                        Rams.AndImmediat(ref this.rA, endereco, this.memoria);
                        break;
                    case 83: //A,nX
                        Rams.AndIndexed(ref this.rA, this.rX, endereco, this.memoria);
                        break;
                    case 84: //B,n
                        Rams.And(ref this.rB, endereco, this.memoria);
                        break;
                    case 85: //B,nI
                        Rams.AndIndirect(ref this.rB, endereco, this.memoria);
                        break;
                    case 86: //B,#n
                        Rams.AndImmediat(ref this.rB, endereco, this.memoria);
                        break;
                    case 87: //B,nX
                        Rams.AndIndexed(ref this.rB, this.rX, endereco, this.memoria);
                        break;
                    case 88: //X,n
                        Rams.And(ref this.rX, endereco, this.memoria);
                        break;
                    case 89: //X,nI
                        Rams.AndIndirect(ref this.rX, endereco, this.memoria);
                        break;
                    case 90: //X,#n
                        Rams.AndImmediat(ref this.rX, endereco, this.memoria);
                        break;
                    case 91: //X,nX
                        Rams.AndIndexed(ref this.rX, this.rX, endereco, this.memoria);
                        break;
                    case 92: //?,n
                        Rams.And(ref this.rN, endereco, this.memoria);
                        break;
                    case 93: //?,nI
                        Rams.AndIndirect(ref this.rN, endereco, this.memoria);
                        break;
                    case 94: //?,#n
                        Rams.AndImmediat(ref this.rN, endereco, this.memoria);
                        break;
                    case 95: //?,nX
                        Rams.AndIndexed(ref this.rN, this.rX, endereco, this.memoria);
                        break;

                case 96:  //NOT A
                    this.ac = (byte)Rams.Not(this.ac);
                    numeroAcessos -= 2;
                    break;

                    case 100: //B
                        break;
                    case 104: //X
                        break;
                    case 108: //?
                        break;

                case 112: //SUB
                  //  this.ac = (byte)Rams.Subtract(this.ac, endereco, this.memoria, out borrow);
                    atualizaPC();
                    break;

                    case 113: //A,nI
                        break;
                    case 114: //A,#n
                        break;
                    case 115: //A,nX
                        break;
                    case 116: //B,n
                        break;
                    case 117: //B,nI
                        break;
                    case 118: //B,#n
                        break;
                    case 119: //B,nX
                        break;
                    case 120: //X,n
                        break;
                    case 121: //X,nI
                        break;
                    case 122: //X,#n
                        break;
                    case 123: //X,nX
                        break;
                    case 124: //?,n
                        break;
                    case 125: //?,nI
                        break;
                    case 126: //?,#n
                        break;
                    case 127: //?,nX
                        break;

                case 128: //JMP,n
                    this.pc = Rams.Jump((byte)endereco);
                    numeroAcessos -= 1;
                    break;

                    case 129: //A,nI
                        break;
                    case 130: //A,#n
                        break;
                    case 131: //A,nX
                        break;

                case 144: //JN,n
                    this.pc = Rams.JumpOnNegative(this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;

                    case 145: //A,nI
                        break;
                    case 146: //A,#n
                        break;
                    case 147: //A,nX
                        break;

                case 160: //JZ,n
                    this.pc = (byte)Rams.JumpOnZero(this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;

                    case 161: //A,nI
                        break;
                    case 162: //A,#n
                        break;
                    case 163: //A,nX
                        break;

                case 176: //JC,n
                    this.pc = Rams.JumpOnCarry(this.pc, this.carry, endereco);
                    numeroAcessos -= 1;
                    break;

                    case 177: //A,nI
                        break;
                    case 178: //A,#n
                        break;
                    case 179: //A,nX
                        break;

                case 192: //JSR,n
                    break;
                case 193: //nI
                    break;
                case 194: //#n
                    break;
                case 195: //nX
                    break;

                case 208: //NEG A
                    break;

                    case 212: //B
                        break;
                    case 216: //X
                        break;
                    case 220: //?
                        break;

                case 224: //SHR
                    this.ac = (byte)Rams.ShiftRight(this.ac, out carry);
                    numeroAcessos -= 2;
                    break;

                    case 228: //B
                        break;
                    case 232: //X
                        break;
                    case 236: //?
                        break;

                case 240: //HLT;
                    this.hlt = Rams.Halt();
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

        private void Ramses_Load(object sender, System.EventArgs e)
        {
            memToGrid();
            this.atualizaTela();
        }
    }
}
