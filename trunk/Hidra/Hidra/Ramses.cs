using System.Windows.Forms;

namespace Hidra
{
    public partial class Ramses : MainWindow
    {
        public Ramses()
        {
            InitializeComponent();
            this.instructions.initRamsesInstructions();
        }
    }
}
