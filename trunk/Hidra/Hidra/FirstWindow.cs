using System;
using System.Windows.Forms;
using Hidra.Simulators;

namespace Hidra
{
    public partial class FirstWindow : Form
    {
        private string simulador;

        public string Simulador
        {
            get { return simulador; }
        }

        public FirstWindow()
        {
            InitializeComponent();
        }

        private void btn_neander_Click(object sender, EventArgs e)
        {
            simulador = "Neander";
            this.Close();
        }

        private void btn_ahmes_Click(object sender, EventArgs e)
        {
            simulador = "Ahmes";
            this.Close();
        }

        private void btn_ramses_Click(object sender, EventArgs e)
        {
            simulador = "Ramses";
            this.Close();
        }

        private void btn_cromag_Click(object sender, EventArgs e)
        {
            simulador = "Cromag";
            this.Close();
        }
    }
}
