namespace PaperSoccer
{
    public class Coord
    {
        private int _x;
        private int _y;

        /// <summary>
        /// Domyślny c-tor, x = y = 0;
        /// </summary>
        public Coord()
        {
            _x = _y = 0;
        }

        public Coord(int x, int y)
        {
            (_x, _y) = (x, y);
        }

        public void SetXY(int x, int y) => (_x, _y) = (x, y);

        public int GetX() => _x;

        public void SetX(int x) => _x = x;

        public int GetY() => _y;

        public void SetY(int y) => _y = y;

        /// <summary>
        /// Porównuje koordynaty
        /// </summary>
        /// <param name="obj">Porównywany koordynat Coord</param>
        /// <returns>TRUE jeśli są takie same</returns>
        public bool Equals(Coord obj)
        {
            if (obj == null)
            {
                return false;
            }
            bool xEquals = GetX() == obj.GetX();
            bool yEquals = GetY() == obj.GetY();

            return xEquals && yEquals;
        }
    }
}