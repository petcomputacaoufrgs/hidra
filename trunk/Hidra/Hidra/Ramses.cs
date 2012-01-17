using System.Windows.Forms;

namespace Hidra
{
    public partial class Ramses : MainWindow
    {

        Simulators.RamsesPericles Rams = new Simulators.RamsesPericles();
        public Ramses()
        {
            InitializeComponent();
            this.instructions.initRamsesInstructions();
            //atualizaVariaveis();
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
            lbl_carryout.Text = carry.ToString();
            if (negative == 1)
                picture_neg.Image = Properties.Resources.luz_acessa;
            else if (negative == 0)
                picture_neg.Image = Properties.Resources.luz_apagada;
            if (zero == 1)
                picture_zero.Image = Properties.Resources.luz_acessa;
            else if (zero == 0)
                picture_zero.Image = Properties.Resources.luz_apagada;
        }

        override public void decodificaInstrucao()
        {
            endereco = byte.Parse(gridData.Rows[pc].Cells[1].Value.ToString());

            numeroInstrucoes++;
            numeroAcessos += 3;


            switch (inst)
            {
                case 0:   //NOP;
                    Rams.Nop();
                    numeroAcessos -= 2;
                    break;
                case 16:  //STA; //A,n
                    Rams.Store(this.rA, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 17: //A,nI
                    Rams.StoreIndirect(this.rA, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 18: //A,#n
                    Rams.StoreImmediat(this.rA, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 19: //A,nX
                    Rams.StoreIndexed(this.rA, this.rX, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 20: //B,n
                    Rams.Store(this.rB, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 21: //B,nI
                    Rams.StoreIndirect(this.rB, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 22: //B,#n
                    Rams.StoreImmediat(this.rB, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 23: //B,nX
                    Rams.StoreIndexed(this.rB, this.rX, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 24: //X,n
                    Rams.Store(this.rX, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 25: //X,nI
                    Rams.StoreIndirect(this.rX, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 26: //X,#n
                    Rams.StoreImmediat(this.rX, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 27: //X,nX
                    Rams.StoreIndexed(this.rX, this.rX, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 28: //?,n
                    Rams.Store(this.rN, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 29: //?,nI
                    Rams.StoreIndirect(this.rN, endereco, ref this.memoria);
                    atualizaPC();
                    break;
                case 30: //?,#n
                    Rams.StoreImmediat(this.rN, this.pc, ref this.memoria);
                    atualizaPC();
                    break;
                case 31: //?,nX
                    Rams.StoreIndexed(this.rN, this.rX, this.pc, ref this.memoria);
                    atualizaPC();
                    break;

                case 32:  //LDA;                   
                    Rams.Load(ref this.rA, endereco, memoria);
                    atualizaVariaveis(this.rA);
                    atualizaPC();
                    break;

                case 33: //A,nI
                    Rams.LoadIndirect(ref this.rA, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 34: //A,#n
                    Rams.LoadImmediat(ref this.rA, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 35: //A,nX
                    Rams.LoadIndexed(ref this.rA, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 36: //B,n
                    Rams.Load(ref this.rB, endereco, memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 37: //B,nI
                    Rams.LoadIndirect(ref this.rB, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 38: //B,#n
                    Rams.LoadImmediat(ref this.rB, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 39: //B,nX
                    Rams.LoadIndexed(ref this.rB, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 40: //X,n
                    Rams.Load(ref this.rX, endereco, memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 41: //X,nI
                    Rams.LoadIndirect(ref this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 42: //X,#n
                    Rams.LoadImmediat(ref this.rX, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 43: //X,nX
                    Rams.LoadIndexed(ref this.rX, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 44: //?,n
                    Rams.Load(ref this.rN, endereco, memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 45: //?,nI
                    Rams.LoadIndirect(ref this.rN, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 46: //?,#n
                    Rams.LoadImmediat(ref this.rN, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 47: //?,nX
                    Rams.LoadIndexed(ref this.rN, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;

                case 48:  //ADD;
                    Rams.AddWithCarry(ref this.rA, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;

                case 49: //A,nI
                    Rams.AddWithCarryIndirect(ref this.rA, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 50: //A,#n
                    Rams.AddWithCarryImmediat(ref this.rA, endereco, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 51: //A,nX
                    Rams.AddWithCarryIndexed(ref this.rA, this.rX, endereco, memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 52: //B,n
                    Rams.AddWithCarry(ref this.rB, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 53: //B,nI
                    Rams.AddWithCarryIndirect(ref this.rB, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 54: //B,#n
                    Rams.AddWithCarryImmediat(ref this.rB, endereco, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 55: //B,nX
                    Rams.AddWithCarryIndexed(ref this.rB, this.rX, endereco, memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 56: //X,n
                    Rams.AddWithCarry(ref this.rX, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 57: //X,nI
                    Rams.AddWithCarryIndirect(ref this.rX, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 58: //X,#n
                    Rams.AddWithCarryImmediat(ref this.rX, endereco, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 59: //X,nX
                    Rams.AddWithCarryIndexed(ref this.rX, this.rX, endereco, memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 60: //?,n
                    Rams.AddWithCarry(ref this.rN, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 61: //?,nI
                    Rams.AddWithCarryIndirect(ref this.rN, endereco, this.memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 62: //?,#n
                    Rams.AddWithCarryImmediat(ref this.rN, endereco, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 63: //?,nX
                    Rams.AddWithCarryIndexed(ref this.rN, this.rX, endereco, memoria, out this.carry);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;

                case 64:  //OR;
                    Rams.Or(ref this.rA, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;

                case 65: //A,nI
                    Rams.OrIndirect(ref this.rA, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 66: //A,#n
                    Rams.OrImmediat(ref this.rA, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 67: //A,nX
                    Rams.OrIndexed(ref this.rA, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 68: //B,n
                    Rams.Or(ref this.rB, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 69: //B,nI
                    Rams.OrIndirect(ref this.rB, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 70: //B,#n
                    Rams.OrImmediat(ref this.rB, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 71: //B,nX
                    Rams.OrIndexed(ref this.rB, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 72: //X,n
                    Rams.Or(ref this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 73: //X,nI
                    Rams.OrIndirect(ref this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 74: //X,#n
                    Rams.OrImmediat(ref this.rX, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 75: //X,nX
                    Rams.OrIndexed(ref this.rX, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 76: //?,n
                    Rams.Or(ref this.rN, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 77: //?,nI
                    Rams.OrIndirect(ref this.rN, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 78: //?,#n
                    Rams.OrImmediat(ref this.rN, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 79: //?,nX
                    Rams.OrIndexed(ref this.rN, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;

                case 80:  //AND;
                    Rams.And(ref this.rA, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;

                case 81: //A,nI
                    Rams.AndIndirect(ref this.rA, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 82: //A,#n
                    Rams.AndImmediat(ref this.rA, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 83: //A,nX
                    Rams.AndIndexed(ref this.rA, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 84: //B,n
                    Rams.And(ref this.rB, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 85: //B,nI
                    Rams.AndIndirect(ref this.rB, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 86: //B,#n
                    Rams.AndImmediat(ref this.rB, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 87: //B,nX
                    Rams.AndIndexed(ref this.rB, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 88: //X,n
                    Rams.And(ref this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 89: //X,nI
                    Rams.AndIndirect(ref this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 90: //X,#n
                    Rams.AndImmediat(ref this.rX, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 91: //X,nX
                    Rams.AndIndexed(ref this.rX, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 92: //?,n
                    Rams.And(ref this.rN, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 93: //?,nI
                    Rams.AndIndirect(ref this.rN, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 94: //?,#n
                    Rams.AndImmediat(ref this.rN, endereco);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 95: //?,nX
                    Rams.AndIndexed(ref this.rN, this.rX, endereco, this.memoria);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;

                case 96:  //NOT A
                    Rams.Not(ref this.rA);
                    atualizaVariaveis(this.rA);
                    numeroAcessos -= 2;
                    break;

                case 97:  //NOT A
                    Rams.Not(ref this.rA);
                    atualizaVariaveis(this.rA);
                    numeroAcessos -= 2;
                    break;
                case 98:  //NOT A
                    Rams.Not(ref this.rA);
                    atualizaVariaveis(this.rA);
                    numeroAcessos -= 2;
                    break;
                case 99:  //NOT A
                    Rams.Not(ref this.rA);
                    atualizaVariaveis(this.rA);
                    numeroAcessos -= 2;
                    break;
                case 100: //B
                    Rams.Not(ref this.rB);
                    atualizaVariaveis(this.rB);
                    numeroAcessos -= 2;
                    break;
                case 101: //B
                    Rams.Not(ref this.rB);
                    atualizaVariaveis(this.rB);
                    numeroAcessos -= 2;
                    break;
                case 102: //B
                    Rams.Not(ref this.rB);
                    atualizaVariaveis(this.rB);
                    numeroAcessos -= 2;
                    break;
                case 103: //B
                    Rams.Not(ref this.rB);
                    atualizaVariaveis(this.rB);
                    numeroAcessos -= 2;
                    break;
                case 104: //X
                    Rams.Not(ref this.rX);
                    atualizaVariaveis(this.rX);
                    numeroAcessos -= 2;
                    break;
                case 105: //X
                    Rams.Not(ref this.rX);
                    atualizaVariaveis(this.rX);
                    numeroAcessos -= 2;
                    break;
                case 106: //X
                    Rams.Not(ref this.rX);
                    atualizaVariaveis(this.rX);
                    numeroAcessos -= 2;
                    break;
                case 107: //X
                    Rams.Not(ref this.rX);
                    atualizaVariaveis(this.rX);
                    numeroAcessos -= 2;
                    break;
                case 108: //?
                    Rams.Not(ref this.rN);
                    atualizaVariaveis(this.rN);
                    numeroAcessos -= 2;
                    break;
                case 109: //?
                    Rams.Not(ref this.rN);
                    atualizaVariaveis(this.rN);
                    numeroAcessos -= 2;
                    break;
                case 110: //?
                    Rams.Not(ref this.rN);
                    atualizaVariaveis(this.rN);
                    numeroAcessos -= 2;
                    break;
                case 111: //?
                    Rams.Not(ref this.rN);
                    atualizaVariaveis(this.rN);
                    numeroAcessos -= 2;
                    break;

                case 112: //SUB A
                    Rams.Subtract(ref this.rA, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;

                case 113: //A,nI
                    Rams.SubtractIndirect(ref this.rA, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 114: //A,#n
                    Rams.SubtractImmediat(ref this.rA, endereco, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 115: //A,nX
                    Rams.SubtractIndexed(ref this.rA, this.rX, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rA);
                    break;
                case 116: //B,n
                    Rams.Subtract(ref this.rB, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 117: //B,nI
                    Rams.SubtractIndirect(ref this.rB, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 118: //B,#n
                    Rams.SubtractImmediat(ref this.rB, endereco, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 119: //B,nX
                    Rams.SubtractIndexed(ref this.rB, this.rX, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rB);
                    break;
                case 120: //X,n
                    Rams.Subtract(ref this.rX, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 121: //X,nI
                    Rams.SubtractIndirect(ref this.rX, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 122: //X,#n
                    Rams.SubtractImmediat(ref this.rX, endereco, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 123: //X,nX
                    Rams.SubtractIndexed(ref this.rX, this.rX, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rX);
                    break;
                case 124: //?,n
                    Rams.Subtract(ref this.rN, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 125: //?,nI
                    Rams.SubtractIndirect(ref this.rN, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 126: //?,#n
                    Rams.SubtractImmediat(ref this.rN, endereco, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;
                case 127: //?,nX
                    Rams.SubtractIndexed(ref this.rN, this.rX, endereco, this.memoria, out borrow);
                    atualizaPC();
                    atualizaVariaveis(this.rN);
                    break;

                case 128: //JMP,n
                    Rams.Jump(ref this.pc, (byte)endereco);
                    numeroAcessos -= 1;
                    break;

                case 129: //JMP,nI
                    Rams.JumpIndirect(ref this.pc, (byte)endereco, this.memoria);
                    break;
                case 130: //JMP,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 131: //JMP,nX
                    Rams.JumpIndexed(ref this.pc, this.rX, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 132: //JMP,n
                    Rams.Jump(ref this.pc, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 133: //JMP,nI
                    Rams.JumpIndirect(ref this.pc, (byte)endereco, this.memoria);
                    break;
                case 134: //JMP,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 135: //JMP,nX
                    Rams.JumpIndexed(ref this.pc, this.rX, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 136: //JMP,n
                    Rams.Jump(ref this.pc, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 137: //JMP,nI
                    Rams.JumpIndirect(ref this.pc, (byte)endereco, this.memoria);
                    break;
                case 138: //JMP,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 139: //JMP,nX
                    Rams.JumpIndexed(ref this.pc, this.rX, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 140: //JMP,n
                    Rams.Jump(ref this.pc, (byte)endereco);
                    numeroAcessos -= 1;
                    break;
                case 141: //JMP,nI
                    Rams.JumpIndirect(ref this.pc, (byte)endereco, this.memoria);
                    break;
                case 142: //JMP,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 143: //JMP,nX
                    Rams.JumpIndexed(ref this.pc, this.rX, (byte)endereco);
                    numeroAcessos -= 1;
                    break;

                case 144: //JN,n
                    Rams.JumpOnNegative(ref this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;

                case 145: //JN,nI
                    Rams.JumpOnNegativeIndirect(ref this.pc, this.negative, endereco, this.memoria);
                    break;
                case 146: //JN,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 147: //JN,nX
                    Rams.JumpOnNegativeIndexed(ref this.pc, this.rX, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 148: //JN,n
                    Rams.JumpOnNegative(ref this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 149: //JN,nI
                    Rams.JumpOnNegativeIndirect(ref this.pc, this.negative, endereco, this.memoria);
                    break;
                case 150: //JN,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 151: //JN,nX
                    Rams.JumpOnNegativeIndexed(ref this.pc, this.rX, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 152: //JN,n
                    Rams.JumpOnNegative(ref this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 153: //JN,nI
                    Rams.JumpOnNegativeIndirect(ref this.pc, this.negative, endereco, this.memoria);
                    break;
                case 154: //JN,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 155: //JN,nX
                    Rams.JumpOnNegativeIndexed(ref this.pc, this.rX, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 156: //JN,n
                    Rams.JumpOnNegative(ref this.pc, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;
                case 157: //JN,nI
                    Rams.JumpOnNegativeIndirect(ref this.pc, this.negative, endereco, this.memoria);
                    break;
                case 158: //JN,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 159: //JN,nX
                    Rams.JumpOnNegativeIndexed(ref this.pc, this.rX, this.negative, endereco);
                    numeroAcessos -= 1;
                    break;

                case 160: //JZ,n
                    Rams.JumpOnZero(ref this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;

                case 161: //JZ,nI
                    Rams.JumpOnZeroIndirect(ref this.pc, this.zero, endereco, this.memoria);
                    break;
                case 162: //JZ,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 163: //JZ,nX
                    Rams.JumpOnZeroIndexed(ref this.pc, this.zero, this.rX, endereco);
                    numeroAcessos -= 1;
                    break;
                case 164: //JZ,n
                    Rams.JumpOnZero(ref this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 165: //JZ,nI
                    Rams.JumpOnZeroIndirect(ref this.pc, this.zero, endereco, this.memoria);
                    break;
                case 166: //JZ,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 167: //JZ,nX
                    Rams.JumpOnZeroIndexed(ref this.pc, this.zero, this.rX, endereco);
                    numeroAcessos -= 1;
                    break;
                case 168: //JZ,n
                    Rams.JumpOnZero(ref this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 169: //JZ,nI
                    Rams.JumpOnZeroIndirect(ref this.pc, this.zero, endereco, this.memoria);
                    break;
                case 170: //JZ,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 171: //JZ,nX
                    Rams.JumpOnZeroIndexed(ref this.pc, this.zero, this.rX, endereco);
                    numeroAcessos -= 1;
                    break;
                case 172: //JZ,n
                    Rams.JumpOnZero(ref this.pc, this.zero, endereco);
                    numeroAcessos -= 1;
                    break;
                case 173: //JZ,nI
                    Rams.JumpOnZeroIndirect(ref this.pc, this.zero, endereco, this.memoria);
                    break;
                case 174: //JZ,#n
                    atualizaPC();
                    numeroAcessos -= 2;
                    break;
                case 175: //JZ,nX
                    Rams.JumpOnZeroIndexed(ref this.pc, this.zero, this.rX, endereco);
                    numeroAcessos -= 1;
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
            this.atualizaTela();
        }

        private void Ramses_Load(object sender, System.EventArgs e)
        {
            memToGrid();
            this.atualizaTela();
        }

        private void txt_ra_DoubleClick(object sender, System.EventArgs e)
        {
            AlteraRegistrador form = new AlteraRegistrador(this, "RA");
            form.Show();
        }

        private void txt_rb_DoubleClick(object sender, System.EventArgs e)
        {
            AlteraRegistrador form = new AlteraRegistrador(this, "RB");
            form.Show();
        }

        private void txt_rx_DoubleClick(object sender, System.EventArgs e)
        {
            AlteraRegistrador form = new AlteraRegistrador(this, "RX");
            form.Show();
        }

    }
}
