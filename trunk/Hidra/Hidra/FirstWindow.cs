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
    public partial class FirstWindow : Form
    {
        public FirstWindow()
        {
            InitializeComponent();
        }

        private void btn_neander_Click(object sender, EventArgs e)
        {
            Neander neander = new Neander();
            neander.ShowDialog();
            this.Close();
        }

        private void btn_ahmes_Click(object sender, EventArgs e)
        {
            Ahmes ahmes = new Ahmes();
            ahmes.ShowDialog();
            this.Close();
        }
    }
}
