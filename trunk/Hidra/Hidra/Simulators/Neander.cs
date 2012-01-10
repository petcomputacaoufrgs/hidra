
namespace Hidra.Simulators
{
    public class Neander : VoltaNeader
    {

        public void Store(byte ac, int endereco, ref byte[] memoria)
        {
            memoria[endereco] = ac;             
        }

        public void StoreIndirect(byte register, int endereco, ref byte[] memoria)
        {
            int r = memoria[endereco];
            memoria[r] = register; 
        }

        public void StoreImmediat(byte register, int pc, ref byte[] memoria)
        {
            memoria[pc-1] = register;
        }

        public void StoreIndexed(byte register, byte rX, int endereco, ref byte[] memoria)
        {
            memoria[endereco + rX] = register;
        }



        public void Load(ref byte ac, int endereco, byte[] memoria)
        {
            ac= memoria[endereco];            
        }

        public void LoadIndirect(ref byte register, int endereco,  byte[] memoria)
        {
            register = memoria[memoria[endereco]];
        }

        public void LoadImmediat(ref byte register,int endereco)
        {
            register = (byte)endereco;
        }

        public void LoadIndexed(ref byte register, byte rX, int endereco, byte[] memoria)
        {
            int value = endereco + rX;
            if (value > 255)
            {
                value -= 256;
            }
            register = memoria[value];
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
