
namespace Hidra.Simulators
{
    public abstract class PitAhmes : PeriPitAhmes
    {

        public int JumpOnPositive(int pc, int negative, int endereco)
        {
            if (negative == 0)
                return endereco;
            else
                return ++pc;
        }

        public int JumpOnBorrow(int pc, int borrow, int endereco)
        {
            if (borrow == 1)
                return endereco;
            else
                return ++pc;
        }

        public void ShiftLeft()
        {
            throw new System.NotImplementedException();
        }

        public void RotateRight()
        {
            throw new System.NotImplementedException();
        }

        public void RotateLeft()
        {
            throw new System.NotImplementedException();
        }
    }
}
