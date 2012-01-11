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
    public partial class AlteraRegistrador : Form
    {
        MainWindow instMainWindow;
        public AlteraRegistrador(MainWindow formMain)
        {           
           InitializeComponent();
           instMainWindow = formMain;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            instMainWindow.pc = int.Parse(textBoxRegister.Text);
            instMainWindow.atualizaTela();           
            this.Close();
        }

        private void textBoxRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }         
        }
    }
}
