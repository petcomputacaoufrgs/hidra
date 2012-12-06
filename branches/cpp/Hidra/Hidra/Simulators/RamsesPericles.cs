
namespace Hidra.Simulators
{
    public class RamsesPericles : PeriPitAhmes
    {
        public void JumpToSubroutine()
        {
            throw new System.NotImplementedException();
        }

        public void Negate(ref byte register)
        {
            register = (byte)(256 - register);
        }
    }
}
