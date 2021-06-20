using System;
using System.Collections.Generic;
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
    public class Coordinates
    {
        uint x;
        uint y;

        public Coordinates() => (x, y) = (0, 0);
        public Coordinates(uint X, uint Y) => (x, y) = (X, Y);
        public uint X()
        {
            return x;
        }
        public void X(uint x)
        {
            this.x = x;
        }
        public uint Y()
        {
            return y;
        }
        public void Y(uint y)
        {
            this.y = y;
        }
        public void setXY(uint X, uint Y) => (x, y) = (X, Y);
        public Coordinates getXY(uint X, uint Y)
        {
            return this;
        }
    }

    public class BoardClass
    {
        public class Point : Coordinates
        {
            public BoardSettings.BoardPoint pointType;
            Coordinates coordinate;
            public Point() => (pointType) = (BoardSettings.BoardPoint.Empty);
            public Point(Coordinates coord, BoardSettings.BoardPoint pt) => (coordinate, pointType) = (coord, pt);
            public Point(Coordinates coord) => (coordinate, pointType) = (coord, BoardSettings.BoardPoint.Empty);
        }
        public List<List<Point>> Playground = new List<List<Point>>();


    }


    public partial class MainWindow : Window
    {
        
        private string player1Name = "";
        private string player2Name = "";

        public string Player1Name { get; set; }
        public string Player2Name { get; set; }

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
    }
}
