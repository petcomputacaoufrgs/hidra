
namespace Hidra.Simulators
{
    public class Queops : Neander
    {
        public int JumpOnCarry(int pc, int carry, int endereco)
        {
            if (carry == 1)
                return endereco;
            else
                return ++pc;
        }
    }
}
