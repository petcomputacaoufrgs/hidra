using System.Windows.Forms;

namespace Hidra
{
    public partial class Ahmes : MainWindow
    {
        public Ahmes()
        {
            InitializeComponent();
            this.instructions.initAhmesInstructions();
        }
    }
}
