
namespace Hidra.Simulators
{
    public abstract class PeriPitAhmes : Cromag
    {
        public void Subtract( ref byte register, int endereco, byte[] memoria, out int borrow)
        {
             register = (byte)(register - memoria[endereco]);

            if (register < 0)
            {
                borrow = 1;
                register = (byte)(register + 256);
            }
            else
            {
                borrow = 0;
            
            }
        }

        public void SubtractIndirect(ref byte register, int endereco, byte[] memoria, out int borrow)
        {
            register = (byte)(register - memoria[memoria[endereco]]);

            if (register < 0)
            {
                borrow = 1;
                register = (byte)(register + 256);
            }
            else
            {
                borrow = 0;

            }
        }

        public void SubtractImmediat(ref byte register, int endereco, out int borrow)
        {
            register = (byte)(register - endereco);

            if (register < 0)
            {
                borrow = 1;
                register = (byte)(register + 256);
            }
            else
            {
                borrow = 0;

            }
        }

        public void SubtractIndexed(ref byte register, byte rX, int endereco, byte[] memoria, out int borrow)
        {

            int value = rX + endereco;

            if (value > 255)
            {
                value -= 256;
            }

            register = (byte)(register - memoria[value]);

            if (register < 0)
            {
                borrow = 1;
                register = (byte)(register + 256);
            }
            else
            {
                borrow = 0;

            }
        }


    }
}
