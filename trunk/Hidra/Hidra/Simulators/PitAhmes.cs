
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

        public byte RotateRight(byte ac, out int carry)
        {
            carry = ac & 0x01; 
            ac = (byte)(ac >> 1);
            ac += (byte)(carry * 128);

            return ac;
        }

        public byte RotateLeft(byte ac, out int carry)
        {
            carry = ac & 128;
            ac = (byte)(ac << 1);
            ac += (byte)carry;

            return ac;
        }
    }
}
