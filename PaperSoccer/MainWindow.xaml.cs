using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            int height = 8 + (2 * size);
            _board.Init(width, height);
            _board.Draw(ref cnvPaint);
        }

        private void DirectionButton(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;
            var availableMoves = _board.GetAllUnoccupiedNeighbourEdges(_board.GetBallCoord());

            if (clicked == btnStartGame)
            {
                pnlBoardSettings.IsEnabled = false;
                _gameState = GameSettings.GameState.Started;
                _currentPlayer = GameSettings.Player.Player1;
            }

            // Wyłączenie wszystkich przycisków
            foreach (var child in wpPanel.Children)
            {
                (child as Button).IsEnabled = false;
            }

            if (clicked == btnN)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.N);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.N)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnS)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.S);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.S)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnE)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.E);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.E)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnW)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.W);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.W)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnNW)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.NW);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.NW)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnNE)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.NE);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.NE)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnSW)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.SW);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.SW)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
                            break;
                        }
                    }
                }
            }
            if (clicked == btnSE)
            {
                _board.MoveBallInDirection(BoardSettings.Direction.SE);
                foreach (var item in availableMoves)
                {
                    if (item.GetDirection() == BoardSettings.Direction.SE)
                    {
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasMove)
                        {
                            _board.AddNewPlayerMove(item);
                            break;
                        }
                        if (_board.PlayerMoveStatus() == BoardSettings.PlayerState.HasBounce)
                        {
                            _board.AddCurrentPlayerMove(item);
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
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad9:
                    if (btnNE.IsEnabled == true)
                    {
                        DirectionButton(btnNE, e);
                    }
                    break;
                case Key.NumPad8:
                    if (btnN.IsEnabled == true)
                    {
                        DirectionButton(btnN, e);
                    }                    
                    break;
                case Key.NumPad7:
                    if (btnNW.IsEnabled == true)
                    {
                        DirectionButton(btnNW, e);
                    }
                    break;
                case Key.NumPad6:
                    if (btnE.IsEnabled == true)
                    {
                        DirectionButton(btnE, e);
                    }
                    break;
                case Key.NumPad5:
                    if (btnStartGame.IsEnabled == true)
                    {
                        DirectionButton(btnStartGame, e);
                    }
                    break;
                case Key.NumPad4:
                    if (btnW.IsEnabled == true)
                    {
                        DirectionButton(btnW, e);
                    }
                    break;
                case Key.NumPad3:
                    if (btnSE.IsEnabled == true)
                    {
                        DirectionButton(btnSE, e);
                    }
                    break;
                case Key.NumPad2:
                    if (btnS.IsEnabled == true)
                    {
                        DirectionButton(btnS, e);
                    }
                    break;
                case Key.NumPad1:
                    if (btnSW.IsEnabled == true)
                    {
                        DirectionButton(btnSW, e);
                    }
                    break;
                default:
                    break;
            }
            if (e.Key == Key.NumPad9)
            {

            }
        }

    }
}
