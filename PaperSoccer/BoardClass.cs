﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static PaperSoccer.BoardSettings.Direction;

namespace PaperSoccer
{
    public partial class BoardClass
    {
        public Brush Player1Color { get; set; }
        public Brush Player2Color { get; set; }
        
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

        public GameSettings.Player GetCurrentPlayer()
        {
            if (_allPlayerMoves.Count == 0)
            {
                return GameSettings.Player.Player1;
            }
            else
            {
                return (_allPlayerMoves.Count % 2 == 0) ? GameSettings.Player.Player2 : GameSettings.Player.Player1;
            }
        }

        public void AddNewPlayerMove(Edge move)
        {
            _allPlayerMoves.Add(new());
            var x1 = move.GetStartingPoint().GetX();
            var x2 = move.GetEndingPoint().GetX();
            var y1 = move.GetStartingPoint().GetY();
            var y2 = move.GetEndingPoint().GetY();
            _allPlayerMoves[^1].Add(new Edge(new Coord(x1, y1), new Coord(x2, y2))); //Ugly workaround
        }

        public void AddCurrentPlayerMove(Edge move)
        {
            var x1 = move.GetStartingPoint().GetX();
            var x2 = move.GetEndingPoint().GetX();
            var y1 = move.GetStartingPoint().GetY();
            var y2 = move.GetEndingPoint().GetY();
            _allPlayerMoves[^1].Add(new Edge(new Coord(x1, y1), new Coord(x2, y2))); // same here
        }

        public Coord GetBallCoord()
        {
            return _ball;
        }

        private BoardSettings.PlayerState HasMove = BoardSettings.PlayerState.CanStopHere;
        private GameSettings.GameResult GameResult = GameSettings.GameResult.Unknown;

        public GameSettings.GameResult GetGameResult()
        {
            return GameResult;
        }

        public BoardSettings.PlayerState PlayerMoveStatus()
        {
            return HasMove;
        }

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
            HasMove = BoardSettings.PlayerState.HasMove;

            // Wypełniamy całe boisko polem outer; -- warstwa pierwsza
            for (var x = 0; x < PlaygroundWidth; x++)
            {
                _playground.Add(new List<Point>());
                var pgx = _playground[x];
                for (var y = 0; y < PlaygroundHeight; y++)
                {
                    pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Outer));
                }
            }

            // Wypełniamy obramowanie boiska polem Border; -- warstwa druga
            for (var x = 0; x < PlaygroundWidth; x++)
            {
                for (var y = 0; y < PlaygroundHeight; y++)
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
            for (var x = 1; x < PlaygroundWidth - 1; x++)
            {
                for (var y = 2; y < PlaygroundHeight - 2; y++)
                {
                    _playground[x][y].SetType(BoardSettings.BoardPoint.Empty);
                }
            }

            // Wypełniamy punkty bramki polem Goal i usuwamy jeden punkt w środku bramki -- warstwa czwarta
            for (var i = -1; i < 2; i++)
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

            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
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
            for (var y = 0; y < PlaygroundHeight; y++)
            {
                for (var x = 0; x < PlaygroundWidth - 1; x++)
                {
                    var p_ = _playground[x][y].GetType();
                    var _p = _playground[x + 1][y].GetType();

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
            for (var x = 0; x < PlaygroundWidth; x++)
            {
                for (var y = 0; y < PlaygroundHeight - 1; y++)
                {
                    var p_ = _playground[x][y].GetType();
                    var _p = _playground[x][y + 1].GetType();

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
                    if (p_ == BoardSettings.BoardPoint.Player1Goal && (_p == BoardSettings.BoardPoint.Border || _p == BoardSettings.BoardPoint.Empty))
                    {
                        result.Add(new Edge(new Coord(x, y), new Coord(x, y + 1), _p));
                        continue;
                    }
                    if ((p_ == BoardSettings.BoardPoint.Border || p_ == BoardSettings.BoardPoint.Empty) && (_p == BoardSettings.BoardPoint.Player2Goal))
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
                var found = false;
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
                    var pointStart = _playground[apne.GetStartingPoint().GetX()][apne.GetStartingPoint().GetY()];
                    var pointEnd = _playground[apne.GetEndingPoint().GetX()][apne.GetEndingPoint().GetY()];

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
                        line.Stroke = Player1Color;
                        line.StrokeThickness = 3;
                        break;

                    case BoardSettings.BoardPoint.Player2Goal:
                        line.Stroke = Player2Color;
                        line.StrokeThickness = 3;
                        break;

                    case BoardSettings.BoardPoint.Border:
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 1;
                        break;

                    default:
                        line.Stroke = Brushes.White;
                        line.StrokeThickness = 1;
                        break;
                }

                line.X1 = item.GetStartingPoint().GetX() * space;
                line.X2 = item.GetEndingPoint().GetX() * space;
                line.Y1 = item.GetStartingPoint().GetY() * space;
                line.Y2 = item.GetEndingPoint().GetY() * space;
                canvas.Children.Add(line);
            }

            for (var index = 0; index < _allPlayerMoves.Count; index++)
            {
                foreach (var item in _allPlayerMoves[index])
                {
                    Line line = new()
                    {
                        Stroke = (index % 2 == 0) ? Player1Color : Player2Color,
                        X1 = item.GetStartingPoint().GetX() * space,
                        X2 = item.GetEndingPoint().GetX() * space,
                        Y1 = item.GetStartingPoint().GetY() * space,
                        Y2 = item.GetEndingPoint().GetY() * space
                    };
                    canvas.Children.Add(line);
                }
            }
            //TODO Dopracuj rysowanie

            var ell = new Ellipse();
            ell.Stroke = (_allPlayerMoves.Count == 0) ? Player1Color : (canvas.Children[^1] as Line).Stroke;
            ell.Width = 10;
            ell.Height = 10;
            canvas.Children.Add(ell);
            Canvas.SetLeft(ell, _ball.GetX() * space - ell.Width / 2);
            Canvas.SetTop(ell, _ball.GetY() * space - ell.Height / 2);
        }

        /// <summary>
        /// Przesuwa piłkę o jedno miejsce w zadanym kiedunku, ustawia stan ruchu gracza
        /// </summary>
        /// <param name="direction">Kierunek</param>
        public void MoveBallInDirection(BoardSettings.Direction direction)
        {
            var (x, y) = direction switch
            {
                NW => (-1, -1),
                N => (0, -1),
                NE => (1, -1),
                W => (-1, 0),
                E => (1, 0),
                SW => (-1, 1),
                S => (0, 1),
                SE => (1, 1),
                _ => (0, 0)
            };

            _ball.SetXY(_ball.GetX() + x, _ball.GetY() + y);
            HasMove = BoardSettings.PlayerState.HasMove;

            var isStart = false;
            var isEnd = false;

            // Ta pętla sprawdza czy koordynaty piłki są na łączeniu krawędzi
            foreach (var item in _allPlayerMoves.SelectMany(list => list))
            {
                if (item.GetEndingPoint().Equals(_ball))
                {
                    isStart = true;
                    continue;
                }
                if (item.GetStartingPoint().Equals(_ball))
                {
                    isEnd = true;
                }
            }

            if (isEnd && isStart)
                HasMove = BoardSettings.PlayerState.HasBounce;
            else
                HasMove = BoardSettings.PlayerState.HasMove;

            // Jeżeli piłka jest na granicy boiska, może się odbić
            if (_playground[_ball.GetX()][_ball.GetY()].GetType() == BoardSettings.BoardPoint.Border)
                HasMove = BoardSettings.PlayerState.HasBounce;

            var lastPointType =
                _playground[_allPlayerMoves[^1][^1].GetEndingPoint().GetX()][
                    _allPlayerMoves[^1][^1].GetEndingPoint().GetY()].GetType();

            switch (lastPointType)
            {
                case BoardSettings.BoardPoint.Player1Goal when GetCurrentPlayer() == GameSettings.Player.Player1:
                    GameResult = GameSettings.GameResult.Player2Won;
                    break;
                case BoardSettings.BoardPoint.Player1Goal:
                case BoardSettings.BoardPoint.Player2Goal when GetCurrentPlayer() == GameSettings.Player.Player1:
                    GameResult = GameSettings.GameResult.Player1Won;
                    break;
                case BoardSettings.BoardPoint.Player2Goal:
                    GameResult = GameSettings.GameResult.Player2Won;
                    break;
            }
        }
    }
}