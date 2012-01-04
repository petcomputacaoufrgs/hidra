
namespace Hidra.Simulators
{
    public abstract class VoltaNeader : Simulator
    {
        public int Add(byte ac, int endereco, byte[] memoria)
        {            
            return ac + memoria[endereco];
        }

        public void And()
        {
            throw new System.NotImplementedException();
        }

        public void Or()
        {
            throw new System.NotImplementedException();
        }

        public void Not()
        {
            throw new System.NotImplementedException();
        }

        public void Nop()
        {
            
        }

        public void Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
