using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace PaperSoccer
{
    public partial class BoardClass
    {
        /// <summary>
        /// Wysokość i szerokość planszy
        /// </summary>
        private int _pgWidth;
        private int _pgHeight;

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
        public int PlaygroundWidth
        {
            get => _pgWidth;
            set
            {
                if (value >= 5)
                {
                    if (value % 2 == 0)
                    {
                        _pgWidth = value + 1;
                    }
                    else
                    {
                        _pgWidth = value;
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
        public int PlaygroundHeight
        {
            get => _pgHeight;
            set
            {
                if (value >= 7)
                {
                    if (value % 2 == 0)
                    {
                        _pgHeight = value + 1;
                    }
                    else
                    {
                        _pgHeight = value;
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
        public int HalfWidth => PlaygroundWidth / 2;

        /// <summary>
        /// Właściwość zwracająca połowę wysokości boiska
        /// </summary>
        public int HalfHeight
        {
            get { return PlaygroundHeight / 2; }
        }

        public void Init(int width = 0, int height = 0)
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

        public List<Edge> BoardToEdgeList()
        {
            List<Edge> result = new();

            // Ta pętla realizuje iteracje poziome 
            for (int y = 0; y < PlaygroundHeight - 1; y++)
            {
                for (int x = 0; x < PlaygroundWidth - 1; x++)
                {
                    BoardSettings.BoardPoint p_ = _playground[x][y].PointType;
                    BoardSettings.BoardPoint _p = _playground[x+1][y].PointType;

                    if (p_ == BoardSettings.BoardPoint.Outer || _p == BoardSettings.BoardPoint.Outer)
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x + 1, y), BoardSettings.BoardPoint.Outer));
                        continue;
                    }
                    if (p_ == BoardSettings.BoardPoint.Border && _p == p_)
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x + 1, y), BoardSettings.BoardPoint.Border));
                        continue;
                    }
                    if (
                        (p_ == BoardSettings.BoardPoint.Border && _p == BoardSettings.BoardPoint.Empty) ||
                        (p_ == BoardSettings.BoardPoint.Empty && _p == BoardSettings.BoardPoint.Border) ||
                        (p_ == BoardSettings.BoardPoint.Empty && p_ == _p)
                        )
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x + 1, y), BoardSettings.BoardPoint.Empty));
                        continue;
                    }
                    if ((p_ == BoardSettings.BoardPoint.Player1Goal || p_ == BoardSettings.BoardPoint.Player2Goal) && p_ == _p)
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x + 1, y), p_));
                        continue;
                    }                    
                }
            }
            return result;
        }

        public void Draw(ref Canvas canvas)
        {
            const int space = 25;
            const int stroke = 1;
            const int bigStroke = 3;
            System.Windows.Media.SolidColorBrush colorBrush = System.Windows.Media.Brushes.Black;

            


        }
    }
}
