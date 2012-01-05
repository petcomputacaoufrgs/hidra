
namespace Hidra.Simulators
{
    public abstract class VoltaNeader : Simulator
    {
        public int Add(byte ac, int endereco, byte[] memoria)
        {
            if(ac + memoria[endereco] > 255)
                return ac + memoria[endereco] - 256;
            else
                return ac + memoria[endereco];
        }

        public int And(byte ac, int endereco, byte[] memoria)
        {
            return (ac & memoria[endereco]);
        }

        public int Or(byte ac, int endereco, byte[] memoria)
        {
            return (ac | memoria[endereco]);
        }

        public int Not(byte ac)
        {
            return 255 - ac;
        }

        public void Nop()
        {            
        }

        public byte Jump(byte endereco)
        {
            return endereco;
        }
    }
}
