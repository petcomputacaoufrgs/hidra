using System;
using System.Windows.Forms;

namespace Hidra
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FirstWindow firstWindow = new FirstWindow();
            Application.Run(firstWindow);

            switch (firstWindow.Simulador)
            {
                case "Neander":
                    Application.Run(new Neander());
                    break;
            }
        }
    }
}
