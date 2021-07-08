using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

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
        private List<List<Point>> _playground = new();

        /// <summary>
        /// Współrzędne piłki w grze
        /// </summary>
        private Coord _ball;

        /// <summary>
        /// Lista 2D, przechowującą kolejne ruchy gracza pierwszego na parzystych indeksach i gracza drugiego na nieparzystych indeksach
        /// </summary>
        private List<List<Edge>> _allPlayerMoves = new();

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
        private int HalfWidth => PlaygroundWidth / 2;

        /// <summary>
        /// Właściwość zwracająca połowę wysokości boiska
        /// </summary>
        private int HalfHeight => PlaygroundHeight / 2;

        /// <summary>
        /// Metoda inicjuje puste boisko, czyści wszystko, wypełnia krawędziami
        /// </summary>
        /// <param name="width">Zadana szerokość boiska, nie mniejsza niż 5</param>
        /// <param name="height">Zadana wysokość boiska, nie mniejsza niż 7</param>
        public void Init(int width = 0, int height = 0)
        {
            _playground.Clear();
            PlaygroundWidth = width;
            PlaygroundHeight = height;
            _ball = new Coord(HalfWidth, HalfHeight);

            // Wypełniamy całe boisko polem outer; -- warstwa pierwsza
            for (int x = 0; x < PlaygroundWidth; x++)
            {
                _playground.Add(new List<Point>());
                var pgx = _playground[x];
                for (int y = 0; y < PlaygroundHeight; y++)
                {
                    pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Outer));                    
                }
            }

            // Wypełniamy obramowanie boiska polem Border; -- warstwa druga
            for (int x = 0; x < PlaygroundWidth; x++)
            {
                for (int y = 0; y < PlaygroundHeight; y++)
                {
                    if (((x == 0) || (x == PlaygroundWidth - 1)) && y > 0 && y < PlaygroundHeight - 1)
                    {
                        _playground[x][y].SetType(BoardSettings.BoardPoint.Border); // ||
                    }
                    if (y == 1 || y == PlaygroundHeight - 2)
                    {
                        _playground[x][y].SetType(BoardSettings.BoardPoint.Border); // =
                    }
                }
            }

            // Wypełniamy środek boiska polem Empty -- warstwa trzecia
            for (int x = 1; x < PlaygroundWidth - 1; x++)
            {
                for (int y = 2; y < PlaygroundHeight - 2; y++)
                {
                    _playground[x][y].SetType(BoardSettings.BoardPoint.Empty);
                }
            }

            // Wypełniamy punkty bramki polem Goal i usuwamy jeden punkt w środku bramki -- warstwa czwarta
            for (int i = -1; i < 2; i++)
            {
                _playground[HalfWidth + i][0].SetType(BoardSettings.BoardPoint.Player1Goal);
                _playground[HalfWidth + i][PlaygroundHeight - 1].SetType(BoardSettings.BoardPoint.Player2Goal);
                if (i == 0)
                {
                    _playground[HalfWidth][1].SetType(BoardSettings.BoardPoint.Empty);
                    _playground[HalfWidth][PlaygroundHeight - 2].SetType(BoardSettings.BoardPoint.Empty);
                }
            }
        }

        /// <summary>
        /// Metoda zwracająca wszystkie punkty sąssiadujące wokoło punktu xy
        /// </summary>
        /// <param name="xy">Koordynaty badanego punktu</param>
        /// <returns>Listę punktów i ich rodzajów</returns>
        public List<Point> GetAllPossibleNeighbourPoints(Coord xy)
        {
            List<Point> neighbours = new();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue; // samego siebie nie zwracamy
                    }
                    if ((xy.GetX() + x < 0) || (xy.GetY() + y < 0))
                    {
                        continue; //nie wychodzimy poza górną i lewą stronę boiska
                    }
                    if ((xy.GetX() + x == PlaygroundWidth) || (xy.GetY() + y == PlaygroundHeight))
                    {
                        continue; //nie wychodimy poza dolną i prawą stronę boiska
                    }
                    neighbours.Add(_playground[xy.GetX() + x][xy.GetY() + y]); //Zwróć wszystkie sąsiadujące punkty
                }
            }
            return neighbours;
        }

        /// <summary>
        /// Przedstawia boisko zapisane w postaci punktów, na boisko zapisane w postaci krawędzi.
        /// </summary>
        /// <returns>Listę krawędzi</returns>
        public List<Edge> BoardToEdgeList()
        {
            List<Edge> result = new();

            // Ta pętla realizuje iteracje poziome 
            for (int y = 0; y < PlaygroundHeight; y++)
            {
                for (int x = 0; x < PlaygroundWidth - 1; x++)
                {
                    BoardSettings.BoardPoint p_ = _playground[x][y].GetType();
                    BoardSettings.BoardPoint _p = _playground[x+1][y].GetType();

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

            // Ta pętla realizuje iteracje pionowe 
            for (int x = 0; x < PlaygroundWidth; x++)
            {
                for (int y = 0; y < PlaygroundHeight - 1; y++)
                {
                    BoardSettings.BoardPoint p_ = _playground[x][y].GetType();
                    BoardSettings.BoardPoint _p = _playground[x][y + 1].GetType();

                    if (p_ == BoardSettings.BoardPoint.Outer || _p == BoardSettings.BoardPoint.Outer)
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x, y + 1), BoardSettings.BoardPoint.Outer));
                        continue;
                    }
                    if (p_ == BoardSettings.BoardPoint.Border && _p == p_)
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x, y + 1), BoardSettings.BoardPoint.Border));
                        continue;
                    }
                    if (
                        (p_ == BoardSettings.BoardPoint.Border && _p == BoardSettings.BoardPoint.Empty) ||
                        (p_ == BoardSettings.BoardPoint.Empty && _p == BoardSettings.BoardPoint.Border) ||
                        (p_ == BoardSettings.BoardPoint.Empty && p_ == _p)
                        )
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x, y + 1), BoardSettings.BoardPoint.Empty));
                        continue;
                    }
                    if ( p_ == BoardSettings.BoardPoint.Player1Goal && (_p == BoardSettings.BoardPoint.Border || _p == BoardSettings.BoardPoint.Empty) )
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x, y + 1), _p));
                        continue;
                    }
                    if ( (p_ == BoardSettings.BoardPoint.Border || p_ == BoardSettings.BoardPoint.Empty ) && (_p == BoardSettings.BoardPoint.Player2Goal) )
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x, y + 1), p_));
                        continue;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Metoda zwraca wszystkie krawędzie możliwe do wykorzystania w grze,
        /// czyli krawędzie o typie Empty
        /// </summary>
        /// <param name="xy">Współrzędne zadanego punktu</param>
        public List<Edge> GetAllPossibleNeighbourEdges(Coord xy)
        {
            List<Edge> result = new();

            var points = GetAllPossibleNeighbourPoints(xy);

            foreach (var item in points)
            {
                result.Add(new Edge(xy, new Coord(item.GetX(), item.GetY())));
            }

            return result;
        }

        /// <summary>
        /// Metoda zwraca te krawędzie, które można wykorzystać jako dodatkowy ruch
        /// </summary>
        /// <param name="xy">Współrzędne zadanego punktu</param>
        /// <returns>Lista(Edge)</returns>
        public List<Edge> GetAllUnoccupiedNeighbourEdges(Coord xy)
        {
            List<Edge> result = new();
            foreach (var apne in GetAllPossibleNeighbourEdges(xy))
            {
                bool found = false;
                foreach (var list in _allPlayerMoves)
                {
                    foreach (var playerEdge in list)
                    {
                        if (playerEdge.Equals(apne))
                        {
                            found = true;
                        }
                    }
                }
                if (found == false)
                {
                    Point pointStart = _playground[apne.GetStartingPoint().GetX()][apne.GetStartingPoint().GetY()];
                    Point pointEnd = _playground[apne.GetEndingPoint().GetX()][apne.GetEndingPoint().GetY()];

                    //Jeśli któryś z punktów jest spoza boiska
                    if (pointStart.GetType() == BoardSettings.BoardPoint.Outer ||
                        pointEnd.GetType() == BoardSettings.BoardPoint.Outer)
                    {
                        continue;
                    }

                    //Jeśli p1 i p2 są typu border i nie są przekątne
                    if (pointStart.GetType() == BoardSettings.BoardPoint.Border &&
                        pointEnd.GetType() == BoardSettings.BoardPoint.Border)
                    {
                        if (!apne.IsDiagonal())
                        {
                            continue;
                        }
                    }

                    // Jeśli to bramka gracza 1
                    if (pointStart.GetType() == BoardSettings.BoardPoint.Player1Goal && pointEnd.GetType() == BoardSettings.BoardPoint.Player1Goal)
                    {
                        continue;
                    }

                    // Jeśli to bramka gracza 2
                    if (pointStart.GetType() == BoardSettings.BoardPoint.Player2Goal && pointEnd.GetType() == BoardSettings.BoardPoint.Player2Goal)
                    {
                        continue;
                    }

                    //Jeśli krawędź jest przekątna lub pionowa i jest od bramki do granicy
                    if (apne.IsDiagonal())
                    {
                        if (pointEnd.GetX() != HalfWidth)
                        {
                            if (pointStart.GetType() == BoardSettings.BoardPoint.Player1Goal && pointEnd.GetType() == BoardSettings.BoardPoint.Border)
                            {
                                continue;
                            }
                            if (pointEnd.GetType() == BoardSettings.BoardPoint.Player1Goal && pointStart.GetType() == BoardSettings.BoardPoint.Border)
                            {
                                continue;
                            }
                            if (pointStart.GetType() == BoardSettings.BoardPoint.Player2Goal && pointEnd.GetType() == BoardSettings.BoardPoint.Border)
                            {
                                continue;
                            }
                            if (pointEnd.GetType() == BoardSettings.BoardPoint.Player2Goal && pointStart.GetType() == BoardSettings.BoardPoint.Border)
                            {
                                continue;
                            }
                        }
                    }

                    if (apne.IsVertical())
                    {
                        if (pointStart.GetType() == BoardSettings.BoardPoint.Player1Goal && pointEnd.GetType() == BoardSettings.BoardPoint.Border)
                        {
                            continue;
                        }
                        if (pointEnd.GetType() == BoardSettings.BoardPoint.Player1Goal && pointStart.GetType() == BoardSettings.BoardPoint.Border)
                        {
                            continue;
                        }
                        if (pointStart.GetType() == BoardSettings.BoardPoint.Player2Goal && pointEnd.GetType() == BoardSettings.BoardPoint.Border)
                        {
                            continue;
                        }
                        if (pointEnd.GetType() == BoardSettings.BoardPoint.Player2Goal && pointStart.GetType() == BoardSettings.BoardPoint.Border)
                        {
                            continue;
                        }
                    }

                    result.Add(apne);
                }
            }
            return result;
        }

        /// <summary>
        /// Metoda rysuje boisko na obiekcie Canvas
        /// </summary>
        public void Draw(ref Canvas canvas)
        {
            const int space = 20;

            canvas.Children.Clear();


            foreach (var item in BoardToEdgeList())
            {
                var line = new Line();

                switch (item.GetType())
                {
                    case BoardSettings.BoardPoint.Empty:
                        line.Stroke = Brushes.LightBlue;
                        line.StrokeThickness = 1;
                        break;
                    case BoardSettings.BoardPoint.Player1Goal:
                        line.Stroke = Brushes.Red;
                        line.StrokeThickness = 3;
                        break;
                    case BoardSettings.BoardPoint.Player2Goal:
                        line.Stroke = Brushes.Blue;
                        line.StrokeThickness = 3;
                        break;
                    case BoardSettings.BoardPoint.Border:
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 1;
                        break;

                    default:
                        break;
                }

                line.X1 = item.GetStartingPoint().GetX() * space;
                line.X2 = item.GetEndingPoint().GetX() * space;
                line.Y1 = item.GetStartingPoint().GetY() * space;
                line.Y2 = item.GetEndingPoint().GetY() * space;
                canvas.Children.Add(line);

                var ell = new Ellipse();
                ell.Stroke = SystemColors.WindowTextBrush;
                ell.Width = 10;
                ell.Height = 10;
                canvas.Children.Add(ell);
                Canvas.SetLeft(ell, HalfWidth*20-5);
                Canvas.SetTop(ell, HalfHeight*20-5);

            }


            //TODO Dopracuj rysowanie



        }
    }
}
