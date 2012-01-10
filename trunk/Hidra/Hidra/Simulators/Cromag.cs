
namespace Hidra.Simulators
{
    public class Cromag : Queops
    {
        public int ShiftRight(byte ac, out int carry)
        {
            carry = ac % 2;            

            int r = (ac >> 1);
            return r;
        }
    }
}
