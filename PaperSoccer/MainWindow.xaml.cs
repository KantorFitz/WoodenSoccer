using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaperSoccer
{
    public partial class MainWindow : Window
    {
        private string _player1Name = "";
        private string _player2Name = "";

        public string Player1Name { get; set; }
        public string Player2Name { get; set; }

        private BoardClass _board = new();
        private GameSettings.GameState _gameState = GameSettings.GameState.NotStarted;
        private GameSettings.Player _currentPlayer = GameSettings.Player.Unknown;

        public MainWindow()
        {
            InitializeComponent();
            cbBoardSize.SelectedIndex = 0;
            if (_gameState != GameSettings.GameState.Started && _gameState != GameSettings.GameState.Finished)
            {
                btnStartGame.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _board.Init(10, 15);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _board.Draw(ref cnvPaint);
        }

        private void cbBoardSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int size = (sender as ComboBox).SelectedIndex;
            int width = 6 + (2 * size);
            int height = 10 + (2 * size);
            _board.Init(width, height);
            _board.Draw(ref cnvPaint);
        }

        private void DirectionButton(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            List<Edge> availableMoves = _board.GetAllUnoccupiedNeighbourEdges(_board.GetBallCoord());

            // Wyłączenie wszystkich przycisków
            foreach (var child in wpPanel.Children)
            {
                (child as Button).IsEnabled = false;
            }

            if (clicked == btnStartGame)
            {
                pnlBoardSettings.IsEnabled = false;
                _gameState = GameSettings.GameState.Started;
                _currentPlayer = GameSettings.Player.Player1;
            }

            if (clicked == btnN)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.N)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.N);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.N);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnS)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.S)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.S);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.S);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnE)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.E)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.E);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.E);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnW)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.W)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.W);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.W);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnNW)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.NW)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.NW);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.NW);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnNE)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.NE)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.NE);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.NE);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnSW)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.SW)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.SW);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.SW);
                            break;
                        }
                    }
                }
            }
            else if (clicked == btnSE)
            {
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.SE)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.SE);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            _board.MoveBallInDirection(BoardSettings.Direction.SE);
                            break;
                        }
                    }
                }
            }
            _board.Draw(ref cnvPaint);

            availableMoves = _board.GetAllUnoccupiedNeighbourEdges(_board.GetBallCoord());
            // Włączenie dostępnych przycisków ruchu
            foreach (var item in availableMoves)
            {
                switch (item.GetDirection())
                {
                    case BoardSettings.Direction.UNKNOWN:
                        break;

                    case BoardSettings.Direction.NW:
                        btnNW.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.N:
                        btnN.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.NE:
                        btnNE.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.W:
                        btnW.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.E:
                        btnE.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.SW:
                        btnSW.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.S:
                        btnS.IsEnabled = true;
                        break;

                    case BoardSettings.Direction.SE:
                        btnSE.IsEnabled = true;
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
            if (_board.GetGameResult() != GameSettings.GameResult.Unknown)
            {
                if (_board.GetGameResult() == GameSettings.GameResult.Player2Won)
                {
                    lblWhoWon.Content = "Gratulacje, gracz Czerwony wygrał!";
                    lblWhoWon.Visibility = Visibility.Visible;
                }
                if (_board.GetGameResult() == GameSettings.GameResult.Player1Won)
                {
                    lblWhoWon.Content = "Gratulacje, gracz Niebieski wygrał!";
                    lblWhoWon.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblWhoWon.Visibility = Visibility.Hidden;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad9:
                    if (btnNE.IsEnabled)
                    {
                        DirectionButton(btnNE, e);
                    }
                    break;

                case Key.NumPad8:
                    if (btnN.IsEnabled)
                    {
                        DirectionButton(btnN, e);
                    }
                    break;

                case Key.NumPad7:
                    if (btnNW.IsEnabled)
                    {
                        DirectionButton(btnNW, e);
                    }
                    break;

                case Key.NumPad6:
                    if (btnE.IsEnabled)
                    {
                        DirectionButton(btnE, e);
                    }
                    break;

                case Key.NumPad5:
                    if (btnStartGame.IsEnabled)
                    {
                        DirectionButton(btnStartGame, e);
                    }
                    break;

                case Key.NumPad4:
                    if (btnW.IsEnabled)
                    {
                        DirectionButton(btnW, e);
                    }
                    break;

                case Key.NumPad3:
                    if (btnSE.IsEnabled)
                    {
                        DirectionButton(btnSE, e);
                    }
                    break;

                case Key.NumPad2:
                    if (btnS.IsEnabled)
                    {
                        DirectionButton(btnS, e);
                    }
                    break;

                case Key.NumPad1:
                    if (btnSW.IsEnabled)
                    {
                        DirectionButton(btnSW, e);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}