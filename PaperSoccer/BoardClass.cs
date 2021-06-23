using System;
using System.Collections.Generic;

namespace PaperSoccer
{
    public partial class BoardClass
    {
        /// <summary>
        /// Wysokość i szerokość planszy
        /// </summary>
        private UInt16 _pgWidth;
        private UInt16 _pgHeight;

        /// <summary>
        /// Plansza do gry w Piłkarzyki
        /// </summary>
        private List<List<Point>> _playground = new List<List<Point>>();

        /// <summary>
        /// Współrzędne piłki w grze
        /// </summary>
        private Coord _ball;


        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundWidth
        {
            get => _pgWidth;
            set
            {
                if (value >= 5)
                {
                    if (value % 2 == 0)
                    {
                        _pgWidth = (ushort)(value + 1);
                    }
                    else
                    {
                        _pgWidth = (ushort)value;
                    }                    
                }
                else
                {
                    _pgWidth = 5;
                }                
            }
        }

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundHeight
        {
            get => _pgHeight;
            set
            {
                if (value >= 7)
                {
                    if (value % 2 == 0)
                    {
                        _pgHeight = (ushort)(value + 1);
                    }
                    else
                    {
                        _pgHeight = (ushort)value;
                    }
                }
                else
                {
                    _pgHeight = 7;
                }
            }
        }

        /// <summary>
        /// Właściwość zwracająca połowę szerokości boiska
        /// </summary>
        public UInt16 HalfWidth => (UInt16)(PlaygroundWidth / 2);

        /// <summary>
        /// Właściwość zwracająca połowę wysokości boiska
        /// </summary>
        public UInt16 HalfHeight
        {
            get { return (UInt16)(PlaygroundHeight / 2); }
        }

        public void Init(UInt16 width = 0, UInt16 height = 0)
        {
            _playground.Clear();
            PlaygroundWidth = width;
            PlaygroundHeight = height;
            _ball = new Coord(HalfWidth, HalfHeight);

            // Wypełniamy całe boisko polem outer; -- warstwa pierwsza
            for (UInt16 x = 0; x < PlaygroundWidth; x++)
            {
                _playground.Add(new List<Point>());
                var pgx = _playground[x];
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
                        _playground[x][y].PointType = BoardSettings.BoardPoint.Border; // ||
                    }
                    if (y == 1 || y == PlaygroundHeight - 2)
                    {
                        _playground[x][y].PointType = BoardSettings.BoardPoint.Border; // =
                    }
                }
            }

            // Wypełniamy środek boiska polem Empty -- warstwa trzecia
            for (UInt16 x = 1; x < PlaygroundWidth - 1; x++)
            {
                for (UInt16 y = 2; y < PlaygroundHeight - 2; y++)
                {
                    _playground[x][y].PointType = BoardSettings.BoardPoint.Empty;
                    if (x == HalfWidth && y == HalfHeight)
                    {
                        _playground[x][y].PointType = BoardSettings.BoardPoint.Ball;
                    }
                }
            }

            // Wypełniamy punkty bramki polem Goal i usuwamy jeden punkt w środku bramki -- warstwa czwarta
            for (int i = -1; i < 2; i++)
            {
                _playground[HalfWidth + i][0].PointType = BoardSettings.BoardPoint.Player1Goal;
                _playground[HalfWidth + i][PlaygroundHeight - 1].PointType = BoardSettings.BoardPoint.Player2Goal;
                if (i == 0)
                {
                    _playground[HalfWidth][1].PointType = BoardSettings.BoardPoint.Empty;
                    _playground[HalfWidth][PlaygroundHeight - 2].PointType = BoardSettings.BoardPoint.Empty;
                }
            }
            List<Point> t = new List<Point>();
            t = GetAllPossibleNeighbourPoints(new Coord(3, 5));
        }

        public List<Point> GetAllPossibleNeighbourPoints(Coord xy)
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
                    if ((xy.X() + x < 0) || (xy.Y() + y < 0))
                    {
                        continue; //nie wychodzimy poza górną i lewą stronę boiska
                    }
                    if ((xy.X() + x == PlaygroundWidth) || (xy.Y() + y == PlaygroundHeight))
                    {
                        continue; //nie wychodimy poza dolną i prawą stronę boiska
                    }
                    neighbours.Add(_playground[(UInt16)(xy.X() + x)][(UInt16)(xy.Y() + y)]); //Zwróć wszystkie sąsiadujące punkty
                }
            }
            return neighbours;
            //TODO Dokończ metodę zwracającą wzystkie krawędzie wokół punktu.
        } 
    }
}
