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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxRegister.Text != "")
            {
                int r = int.Parse(textBoxRegister.Text);

                if (reg == "PC")
                    instMainWindow.pc = r;
                else if (reg == "AC")
                    instMainWindow.ac = (byte)r;
                else if (reg == "RA")
                    instMainWindow.rA = (byte)r;
                else if (reg == "RB")
                    instMainWindow.rB = (byte)r;
                else if (reg == "RX")
                    instMainWindow.rX = (byte)r;

                instMainWindow.atualizaTela();
            }

            this.Close();
        }

        private void textBoxRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
