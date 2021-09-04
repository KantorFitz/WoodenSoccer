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
            /// Prywatne pole mówiące o rodzaju danego punktu
            /// </summary>
            private BoardSettings.BoardPoint type;

            public new BoardSettings.BoardPoint GetType() => type;

            public BoardSettings.BoardPoint SetType(BoardSettings.BoardPoint boardPoint) => type = boardPoint;

            /// <summary>
            /// Domyślny konstruktor
            /// </summary>
            public Point()
            {
                type = BoardSettings.BoardPoint.Outer;
            }

            /// <summary>
            /// Konstruktor z parametrami
            /// </summary>
            /// <param name="x">Koordynaty wierszy</param>
            /// <param name="y">Koordynaty kolumn</param>
            /// <param name="pt">Rodzaj punktu</param>
            public Point(int x, int y, BoardSettings.BoardPoint pt)
            {
                SetXY(x, y);
                type = pt;
            }
        }
    }
}