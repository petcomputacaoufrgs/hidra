using System.Windows.Forms;

namespace Hidra
{
    public partial class Ahmes : MainWindow
    {
        const int memSize = 256;

        public Ahmes()
        {
            InitializeComponent();
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
                        case 112: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.SUB;
                            break;
                        case 128: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JMP;
                            break;
                        case 144: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JN;
                            break;
                        case 148: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JP;
                            break;
                        case 152: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JV;
                            break;
                        case 156: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JNV;
                            break;
                        case 160: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JZ;
                            break;
                        case 164: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JNZ;
                            break;
                        case 176: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JC;
                            break;
                        case 180: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JNC;
                            break;
                        case 184: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JB;
                            break;
                        case 188: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.JNB;
                            break;
                        case 240: gridInstructions.Rows[e.RowIndex].Cells[2].Value = Instructions.HLT;
                            break;
                        default: gridInstructions.Rows[e.RowIndex].Cells[2].Value = valor;
                            break;
                    }
                }
            }
        }
    }
}
