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

        public MainWindow()
        {
            InitializeComponent();
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



            throw new NotImplementedException();
        }
    }
}
