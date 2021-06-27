namespace PaperSoccer
{
    public class Coord
    {
        int _x;
        int _y;

        public Coord()
        {
            _x = _y = 0;
        }

        public Coord(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public Coord GetXy(int x, int y)
        {
            return this;
        }
        public void SetXy(int x, int y) => (_x, _y) = (x, y);
        public int X()
        {
            return _x;
        }
        public void X(int x)
        {
            this._x = x;
        }
        public int Y()
        {
            return _y;
        }
        public void Y(int y)
        {
            this._y = y;
        }
    }
}