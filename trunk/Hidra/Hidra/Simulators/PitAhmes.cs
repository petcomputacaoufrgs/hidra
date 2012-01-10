
namespace Hidra.Simulators
{
    public abstract class PitAhmes : PeriPitAhmes
    {

        public int JumpOnPositive(int pc, int negative, int endereco)
        {
            if (negative == 0)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnBorrow(int pc, int borrow, int endereco)
        {
            if (borrow == 1)
                return endereco;
            else
                return ++pc;
        }

        public int ShiftLeft(byte ac, out int carry)
        {
            if (ac > 127)
                carry = 1;
            else
                carry = 0;

            int r = (ac << 1);
            return r;
        }

        public int RotateRight(byte ac, out int carry)
        {
            int[] nAc = new int[8];
            int r = ac;

            carry = ac % 2;

            for (int i = 0; i < 8; i++)
            {
                nAc[i] = r % 2;
                r = r / 2;
            }

            return r;
        }

        public void RotateLeft()
        {
            throw new System.NotImplementedException();
        }
    }
}
