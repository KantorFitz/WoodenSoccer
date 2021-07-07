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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(this);


            settingsWindow.ShowDialog();

            BoardClass test = new BoardClass();




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _board.Init();

            var tet = _board.BoardToEdgeList();

            var line = new Line();
            line.X1 = int.Parse(tbx1.Text);
            line.Y1 = int.Parse(tby1.Text);
            line.X2 = int.Parse(tbx2.Text);
            line.Y2 = int.Parse(tby2.Text);

            line.Stroke = System.Windows.Media.Brushes.DarkRed;
            line.StrokeThickness = 3;
            cnvPaint.Children.Add(line);
            Canvas jdf;


        }
    }
}
