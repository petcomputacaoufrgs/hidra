
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

        public void AddIndirect(ref byte ac, int endereco, byte[] memoria)
        {
            if (ac + memoria[endereco] > 255)
                ac = (byte)(ac + memoria[endereco] - 256);
            else
                ac = (byte)(ac + memoria[endereco]);
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
