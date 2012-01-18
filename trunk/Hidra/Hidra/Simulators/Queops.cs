
namespace Hidra.Simulators
{
    public class Queops : Neander
    {
        public void JumpOnCarry(ref int pc, int carry, int endereco)
        {
            if (carry == 1)
                pc = endereco;
            else
                pc += 1;
        }

        public void JumpOnCarry(ref int pc, int carry, int endereco, ref int nracessos)
        {
            if (carry == 1)
                pc = endereco;
            else
            {
                nracessos -= 1;
                pc += 1;
            }
        }

        public void JumpOnCarryIndirect(ref int pc, int carry, int endereco, byte[] memoria, ref int nracessos)
        {
            if (carry == 1)
                pc = memoria[endereco];
            else
            {
                nracessos -= 2;
                pc += 1;
            }
        }

        public void JumpOnCarryIndexed(ref int pc, int carry, byte rX, int endereco, ref int nracessos)
        {
            int value = endereco + rX;
            if (value > 255)
            {
                value -= 256;
            }

            if (carry == 1)
                pc = value;
            else
            {
                nracessos -= 1;
                pc += 1;
            }
        }
    }
}
