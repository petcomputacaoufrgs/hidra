using System;
using System.Windows.Forms;

namespace Hidra
{
    public partial class AlteraRegistrador : Form
    {
        MainWindow instMainWindow;
        private string reg;
        
        public AlteraRegistrador(MainWindow formMain, string register)
        {           
           InitializeComponent();
           instMainWindow = formMain;
           reg = register;
        }

        private void textBoxRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '\r')
            {
                bttnOK_Click(sender, e);
            }
        }

        private void bttnOK_Click(object sender, EventArgs e)
        {
            if (textBoxRegister.Text != "")
            {
                int r = int.Parse(textBoxRegister.Text);

                if (reg == "PC")
                {
                    instMainWindow.pc = r;
                }
                else if (reg == "AC")
                {
                    instMainWindow.ac = (byte)r;
                    instMainWindow.atualizaVariaveis(instMainWindow.ac);
                }
                else if (reg == "RA")
                {
                    instMainWindow.rA = (byte)r;
                    instMainWindow.atualizaVariaveis(instMainWindow.rA);
                }
                else if (reg == "RB")
                {
                    instMainWindow.rB = (byte)r;
                    instMainWindow.atualizaVariaveis(instMainWindow.rB);
                }
                else if (reg == "RX")
                {
                    instMainWindow.rX = (byte)r;
                    instMainWindow.atualizaVariaveis(instMainWindow.rX);
                }

                instMainWindow.atualizaTela();
            }

            this.Close();
        }

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
