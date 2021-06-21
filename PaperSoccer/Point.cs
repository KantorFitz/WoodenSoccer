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
            public BoardSettings.BoardPoint pointType;

            /// <summary>
            /// Koordynaty punktu (uint, uint)
            /// </summary>
            public Point() => (pointType) = (BoardSettings.BoardPoint.Outer);
            public Point(uint X, uint Y, BoardSettings.BoardPoint pt)
            {
                this.setXY(X, Y);
                pointType = pt;
            }
        }
    }
}
