
namespace Hidra.Simulators
{
    public class Neander : VoltaNeader
    {

        public byte Store(byte ac)
        {
            return ac;
        }

        public byte Load(int endereco, byte[] memoria)
        {
            return memoria[endereco];            
        }

        public void JumpOnNegative()
        {
            throw new System.NotImplementedException();
        }

        public void JumpOnZero()
        {
            throw new System.NotImplementedException();
        }
    }
}
