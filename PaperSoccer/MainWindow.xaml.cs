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
                clicked.IsEnabled= false;
                foreach (var child in wpPanel.Children)
                {
                    (child as Button).IsEnabled = false;
                }

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
            






        }
    }
}
