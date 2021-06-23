namespace PaperSoccer
{
    public partial class BoardClass
    {
        /// <summary>
        /// Klasa Point dziedziczy po klasie Coordinates, zawiera
        /// informacje o współrzędnych punktu i jego rodzaju.
        /// </summary>
        public class Point : Coord
        {
            /// <summary>
            /// Pole mówiące o rodzaju danego punktu
            /// </summary>
            public BoardSettings.BoardPoint PointType;

            /// <summary>
            /// Koordynaty punktu (uint, uint)
            /// </summary>
            public Point() => (PointType) = (BoardSettings.BoardPoint.Outer);
            public Point(uint x, uint y, BoardSettings.BoardPoint pt)
            {
                this.SetXy(x, y);
                PointType = pt;
            }
        }
    }
}
