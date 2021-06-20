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
    public class Coordinate
    {
        uint x;
        uint y;

        public Coordinate() => (x, y) = (0, 0);
        public Coordinate(uint X, uint Y) => (x, y) = (X, Y);
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
    }

    public class Board
    {
        public class Point : Coordinate
        {
            public BoardSettings.BoardPoint pointType;
            Coordinate coordinate;
            public Point() { }
            public Point(Coordinate coord, BoardSettings.BoardPoint pt) => (coordinate, pointType) = (coord, pt);



        }


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

            

        }
    }
}
