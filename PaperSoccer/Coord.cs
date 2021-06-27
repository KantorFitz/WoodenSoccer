﻿namespace PaperSoccer
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

        public void SetXy(int x, int y)
        {
            (_x, _y) = (x, y);
        }

        public int GetX()
        {
            return _x;
        }
        public void SetX(int x)
        {
            _x = x;
        }
        public int GetY()
        {
            return _y;
        }
        public void SetY(int y)
        {
            _y = y;
        }
    }
}