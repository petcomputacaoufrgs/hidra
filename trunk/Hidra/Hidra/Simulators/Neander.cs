
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

        public int JumpOnNegative(int pc, int negative, int endereco)
        {
            if (negative == 1)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnZero(int pc, int zero, int endereco)
        {
            if (zero == 1)
                return endereco;
            else
                return ++pc;
        }
    }
}
