
namespace Hidra.Simulators
{
    public abstract class PeriPitAhmes : Cromag
    {
        public int Subtract(byte ac, int endereco, byte[] memoria, out int borrow)
        {
            int r = ac - memoria[endereco];

            if (r < 0)
            {
                borrow = 1;
                return r + 256;
            }
            else
            {
                borrow = 0;
                return r;
            }
        }
    }
}
