
namespace Hidra.Simulators
{
    public class Ahmes : PitAhmes
    {
        public int Add(byte ac, int endereco, byte[] memoria, out int carry)
        {
            int r = ac + memoria[endereco];
            if (r > 255)
            {
                carry = 1;
                return r - 256;
            }
            else
            {
                carry = 0;
                return r;
            }
        }

        public int JumpOnOverflow(int pc, int overflow, int endereco)
        {
            if (overflow == 1)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnNotOverflow(int pc, int overflow, int endereco)
        {
            if (overflow == 0)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnNotZero(int pc, int zero, int endereco)
        {
            if (zero == 0)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnNotCarry(int pc, int carry, int endereco)
        {
            if (carry == 0)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnNotBorrow(int pc, int borrow, int endereco)
        {
            if (borrow == 0)
                return endereco;
            else
                return ++pc;
        }
    }
}
