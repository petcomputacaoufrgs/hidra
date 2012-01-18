
namespace Hidra.Simulators
{
    public class Cromag : Queops
    {
        public void ShiftRight(ref byte register, out int carry)
        {
            carry = register % 2;

            int r = (register >> 1);
            register = (byte)r;
        }
    }
}
