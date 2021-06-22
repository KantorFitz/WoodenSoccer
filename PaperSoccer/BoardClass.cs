using System;
using System.Collections.Generic;

namespace PaperSoccer
{
    public partial class BoardClass
    {
        /// <summary>
        /// Wysokość i szerokość planszy
        /// </summary>
        private UInt16 pgWidth;
        private UInt16 pgHeight;

        /// <summary>
        /// Plansza do gry w Piłkarzyki
        /// </summary>
        private List<List<Point>> playground = new List<List<Point>>();

        /// <summary>
        /// Współrzędne piłki w grze
        /// </summary>
        private Coord Ball;


        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundWidth
        {
            get { return pgWidth; }
            set
            {
                if (value >= 5)
                {
                    if (value % 2 == 0)
                    {
                        pgWidth = (ushort)(value + 1);
                    }
                    else
                    {
                        pgWidth = (ushort)value;
                    }                    
                }
                else
                {
                    pgWidth = 5;
                }                
            }
        }

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundHeight
        {
            get { return pgHeight; }
            set
            {
                if (value >= 7)
                {
                    if (value % 2 == 0)
                    {
                        pgHeight = (ushort)(value + 1);
                    }
                    else
                    {
                        pgHeight = (ushort)value;
                    }
                }
                else
                {
                    pgHeight = 7;
                }
            }
        }

        /// <summary>
        /// Właściwość zwracająca połowę szerokości boiska
        /// </summary>
        public UInt16 HalfWidth
        {
            get { return (UInt16)(PlaygroundWidth / 2); }
        }

        /// <summary>
        /// Właściwość zwracająca połowę wysokości boiska
        /// </summary>
        public UInt16 HalfHeight
        {
            get { return (UInt16)(PlaygroundHeight / 2); }
        }

        public void Init(UInt16 width = 0, UInt16 height = 0)
        {
            playground.Clear();
            PlaygroundWidth = width;
            PlaygroundHeight = height;
            Ball = new Coord(HalfWidth, HalfHeight);

            // Wypełniamy całe boisko polem outer; -- warstwa pierwsza
            for (UInt16 x = 0; x < PlaygroundWidth; x++)
            {
                playground.Add(new List<Point>());
                var pgx = playground[x];
                for (UInt16 y = 0; y < PlaygroundHeight; y++)
                {
                    pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Outer));                    
                }
            }

            // Wypełniamy obramowanie boiska polem Border; -- warstwa druga
            for (UInt16 x = 0; x < PlaygroundWidth; x++)
            {
                for (UInt16 y = 0; y < PlaygroundHeight; y++)
                {
                    if (((x == 0) || (x == PlaygroundWidth - 1)) && (y > 0 && y < PlaygroundHeight - 1))
                    {
                        playground[x][y].pointType = BoardSettings.BoardPoint.Border; // ||
                    }
                    if (y == 1 || y == PlaygroundHeight - 2)
                    {
                        playground[x][y].pointType = BoardSettings.BoardPoint.Border; // =
                    }
                }
            }

            // Wypełniamy środek boiska polem Empty -- warstwa trzecia
            for (UInt16 x = 1; x < PlaygroundWidth - 1; x++)
            {
                for (UInt16 y = 2; y < PlaygroundHeight - 2; y++)
                {
                    playground[x][y].pointType = BoardSettings.BoardPoint.Empty;
                    if (x == HalfWidth && y == HalfHeight)
                    {
                        playground[x][y].pointType = BoardSettings.BoardPoint.Ball;
                    }
                }
            }

            // Wypełniamy punkty bramki polem Goal i usuwamy jeden punkt w środku bramki -- warstwa czwarta
            for (int i = -1; i < 2; i++)
            {
                playground[HalfWidth + i][0].pointType = BoardSettings.BoardPoint.Player1Goal;
                playground[HalfWidth + i][PlaygroundHeight - 1].pointType = BoardSettings.BoardPoint.Player2Goal;
                if (i == 0)
                {
                    playground[HalfWidth][1].pointType = BoardSettings.BoardPoint.Empty;
                    playground[HalfWidth][PlaygroundHeight - 2].pointType = BoardSettings.BoardPoint.Empty;
                }
            }
            List<Point> t = new List<Point>();
            t = GetAllPossibleNeighbourPoints(new Coord(3, 5));
        }

        public List<Point> GetAllPossibleNeighbourPoints(Coord XY)
        {
            List<Point> neighbours = new List<Point>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue; // samego siebie nie zwracamy
                    }
                    if ((XY.X() + x < 0) || (XY.Y() + y < 0))
                    {
                        continue; //nie wychodzimy poza górną i lewą stronę boiska
                    }
                    if ((XY.X() + x == PlaygroundWidth) || (XY.Y() + y == PlaygroundHeight))
                    {
                        continue; //nie wychodimy poza dolną i prawą stronę boiska
                    }
                    neighbours.Add(playground[(UInt16)(XY.X() + x)][(UInt16)(XY.Y() + y)]); //Zwróć wszystkie sąsiadujące punkty
                }
            }
            return neighbours;
        }
    }
}
