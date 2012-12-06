
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

        public void JumpOnNegative(ref int pc, int negative, int endereco)
        {
            if (negative == 1)
                pc = endereco;
            else
                ++pc;
        }

        public void JumpOnNegative(ref int pc, int negative, int endereco, ref int nracessos)
        {
            if (negative == 1)
                pc = endereco;
            else
            {
                nracessos -= 1;
                pc += 1;
            }
        }

        public void JumpOnNegativeIndirect(ref int pc, int negative, int endereco, byte[] memoria, ref int nracessos)
        {
            if (negative == 1)
                pc = memoria[endereco];
            else
            {
                nracessos -= 2;
                pc += 1;
            }
        }

        public void JumpOnNegativeIndexed(ref int pc, byte rX, int negative, int endereco, ref int nracessos)
        {
            int value = endereco + rX;
            if (value > 255)
            {
                value -= 256;
            }
            if (negative == 1)
                pc = value;
            else
            {
                nracessos -= 1;
                pc += 1;
            }
        }

        public void JumpOnZero(ref int pc, int zero, int endereco)
        {
            if (zero == 1)
                pc = endereco;
            else
                pc += 1;
        }

        public void JumpOnZero(ref int pc, int zero, int endereco, ref int nracessos)
        {
            if (zero == 1)
                pc = endereco;
            else
            {
                nracessos -= 1;
                pc += 1;
            }
        }

        public void JumpOnZeroIndirect(ref int pc, int zero, int endereco, byte[] memoria, ref int nracessos)
        {
            if (zero == 1)
                pc = memoria[endereco];
            else
            {
                nracessos -= 2;
                pc += 1;
            }
        }

        public void JumpOnZeroIndexed(ref int pc, int zero, byte rX, int endereco,ref int nracessos)
        { 
            int value = endereco + rX;
            if (value > 255)
            {
                value -= 256;
            }

            if (zero == 1)
                pc = value;
            else
            {
                nracessos -= 1;
                pc += 1;
            }
        }
    }
}
