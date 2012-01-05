using System;
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

        private void btn_ramses_Click(object sender, EventArgs e)
        {
            Ramses ramses = new Ramses();
            ramses.ShowDialog();
            this.Close();
        }
    }
}
