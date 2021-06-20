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
    public class Point
    {
        Point()
        {
            x = 0;
            y = 0;
        }
        Point(uint X, uint Y)
        {
            x = X;
            y = Y;
        }
        private uint x { get; set; }
        private uint y { get; set; }
    }

    public class Board
    {
        //TODO
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
