
namespace Hidra.Simulators
{
    public abstract class VoltaNeader : Simulator
    {
        public void AddWithCarry(ref byte register, int endereco, byte[] memoria, out int carry)
        {
            int r = register + memoria[endereco];
            if (r > 255)
            {
                carry = 1;
                register = (byte)(r - 256);
            }
            else
            {
                carry = 0;
                register = (byte)r;
            }
        }

        public void AddWithCarryIndirect(ref byte register, int endereco, byte[] memoria, out int carry)
        {
            int r = register + memoria[memoria[endereco]];
            if (r > 255)
            {
                carry = 1;
                register = (byte)(r - 256);
            }
            else
            {
                carry = 0;
                register = (byte)r;
            }
        }

        public void AddWithCarryImmediat(ref byte register, int endereco, out int carry)
        {
            int r = register + endereco;
            if (r > 255)
            {
                carry = 1;
                register = (byte)(r - 256);
            }
            else
            {
                carry = 0;
                register = (byte)r;
            }
        }

        public void AddWithCarryIndexed(ref byte register, byte rX, int endereco, byte[] memoria, out int carry)
        {
            int value = rX + endereco;
            if (value > 255)
            {
                value -= 256;
            }

            int r = register + memoria[value];
            if (r > 255)
            {
                carry = 1;
                register = (byte)(r - 256);
            }
            else
            {
                carry = 0;
                register = (byte)r;
            }
        }

        public void Add(ref byte ac, int endereco, byte[] memoria)
        {
            if(ac + memoria[endereco] > 255)
                ac = (byte)(ac + memoria[endereco] - 256);
            else
                ac = (byte)(ac + memoria[endereco]);
        }

        public void And(ref byte register, int endereco, byte[] memoria)
        {
            register = (byte)(register & memoria[endereco]);
        }

        public void AndIndirect(ref byte register,int endereco, byte[] memoria)
        {
            register = (byte)(register & memoria[memoria[endereco]]);
        }

        public void AndImmediat(ref byte register, int endereco)
        {
            register = (byte)(register & endereco);
        }

        public void AndIndexed(ref byte register, byte rX, int endereco, byte[] memoria)
        {
            int value = rX + endereco;
            if (value > 255)
            {
                value -= 256;
            }
            register = (byte)(register & memoria[value]);
        }

        public void Or(ref byte register, int endereco, byte[] memoria)
        {
            register = (byte)(register | memoria[endereco]);
        }

        public void OrIndirect(ref byte register, int endereco, byte[] memoria)
        {
            register = (byte)(register | memoria[memoria[endereco]]);
        }

        public void OrImmediat(ref byte register,int endereco)
        {
            register = (byte)(register | endereco);
        }

        public void OrIndexed(ref byte register, byte rX, int endereco, byte[] memoria)
        {
            int value = rX + endereco;
            if (value > 255)
            {
                value -= 256;
            }
            register = (byte)(register | memoria[value]);
        }

        public void Not(ref byte register)
        {
            register = (byte)(255 - register);
        }

        public void Nop()
        {            
        }

        public void Jump(ref int pc, byte endereco)
        {
            pc = endereco;
        }

        public void JumpIndirect(ref int pc, byte endereco, byte[] memoria)
        {
            pc = memoria[endereco];
        }

        public void JumpIndexed(ref int pc, byte rX, byte endereco)
        {
            int value = endereco + rX;

            if (value > 255)
            {
                value -= 256;
            }

            pc = value;
        }

    }
}
