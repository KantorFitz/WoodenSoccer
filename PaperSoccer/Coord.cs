namespace PaperSoccer
{
    public class Coord
    {
        uint _x;
        uint _y;

        public Coord()
        {
            _x = _y = 0;
        }

        public Coord(uint x, uint y)
        {
            this._x = x;
            this._y = y;
        }

        public Coord GetXy(uint x, uint y)
        {
            return this;
        }
        public void SetXy(uint x, uint y) => (_x, _y) = (x, y);
        public uint X()
        {
            return _x;
        }
        public void X(uint x)
        {
            this._x = x;
        }
        public uint Y()
        {
            return _y;
        }
        public void Y(uint y)
        {
            this._y = y;
        }
    }
}