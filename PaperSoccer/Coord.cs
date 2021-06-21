namespace PaperSoccer
{
    public class Coord
    {
        uint x;
        uint y;
        public Coord getXY(uint X, uint Y)
        {
            return this;
        }
        public void setXY(uint X, uint Y) => (x, y) = (X, Y);
        public uint X()
        {
            return x;
        }
        public void X(uint x)
        {
            this.x = x;
        }
        public uint Y()
        {
            return y;
        }
        public void Y(uint y)
        {
            this.y = y;
        }
    }
}