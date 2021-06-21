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
        /// <summary>
        /// Klasa Point dziedziczy po klasie Coordinates, zawiera
        /// informacje o współrzędnych punktu i jego rodzaju.
        /// </summary>
        public class Point : Coordinates
        {
            /// <summary>
            /// Pole mówiące o rodzaju danego punktu
            /// </summary>
            public BoardSettings.BoardPoint pointType;

            /// <summary>
            /// Koordynaty punktu (uint, uint)
            /// </summary>
            Coordinates coordinate;
            public Point() => (pointType) = (BoardSettings.BoardPoint.Outer);
            public Point(Coordinates coord, BoardSettings.BoardPoint pt) => (coordinate, pointType) = (coord, pt);
            public Point(uint X, uint Y, BoardSettings.BoardPoint pt)
            {
                coordinate.setXY(X, Y);
                pointType = pt;
            }
            public Point(Coordinates coord) => (coordinate, pointType) = (coord, BoardSettings.BoardPoint.Empty);
        }

        private UInt16 playgroundWidth;
        private UInt16 playgroundHeight;

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość
        /// planszy boiska, nie mogą być mniejsze od 5x5 i muszą być
        /// zawsze nieparzyste.
        /// </summary>
        public UInt16 PlaygroundWidth
        {
            get { return playgroundWidth; }
            set
            {
                if (value >= 5)
                {
                    if (value % 2 == 0)
                    {
                        playgroundWidth = (ushort)(value + 1);
                    }
                    else
                    {
                        playgroundWidth = (ushort)value;
                    }                    
                }
                else
                {
                    playgroundWidth = 5;
                }                
            }
        }

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość
        /// planszy boiska, nie mogą być mniejsze od 5x5 i muszą być
        /// zawsze nieparzyste.
        /// </summary>
        public UInt16 PlaygroundHeight
        {
            get { return playgroundHeight; }
            set
            {
                if (value >= 5)
                {
                    if (value % 2 == 0)
                    {
                        playgroundHeight = (ushort)(value + 1);
                    }
                    else
                    {
                        playgroundHeight = (ushort)value;
                    }
                }
                else
                {
                    playgroundHeight = 5;
                }
            }
        }

        /// <summary>
        /// Plansza do gry w Piłkarzyki
        /// </summary>
        private List<List<Point>> playground;

        public void PlaygroundInit()
        {
            BoardSettings.BoardPoint pt = BoardSettings.BoardPoint.Empty;


            for (ushort x = 0; x <= PlaygroundWidth; x++)
            {
                playground.Add(new List<Point>());
                var pgx = playground[x];
                for (ushort y = 0; y <= playgroundHeight; y++)
                {
                    // Wypełnienie lewej i prawej krawędzi boiska
                    if (x == 0 || x == PlaygroundWidth)
                    {
                        if (y > 0 && y < PlaygroundHeight)
                        {
                            pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Border));
                        }
                        else
                        {
                            pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Outer));
                        }
                    }
                    else // wypełnienie pozostałych części
                    {
                        if (true)
                        {
                            //TODO wypełnienie bramek i wewnętrznej części boiska
                        }
                    }
                }
            }
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

            BoardClass test = new BoardClass();
            

            

        }
    }
}
