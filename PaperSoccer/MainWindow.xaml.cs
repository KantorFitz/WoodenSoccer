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
                //this.setXY(X, Y);
                this.setXY(X, Y);
                pointType = pt;
            }
            public Point(Coordinates coord) => (coordinate, pointType) = (coord, BoardSettings.BoardPoint.Empty);
        }

        private UInt16 playgroundWidth;
        private UInt16 playgroundHeight;

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
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
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundHeight
        {
            get { return playgroundHeight; }
            set
            {
                if (value >= 7)
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
                    playgroundHeight = 7;
                }
            }
        }

        /// <summary>
        /// Plansza do gry w Piłkarzyki
        /// </summary>
        private List<List<Point>> playground = new List<List<Point>>();

        public void Init(UInt16 width = 0, UInt16 height = 0)
        {
            playground.Clear();
            PlaygroundWidth = width;
            PlaygroundHeight = height;
            BoardSettings.BoardPoint pt = BoardSettings.BoardPoint.Empty;
            UInt16 halfWidth = (UInt16)(playgroundWidth / 2);


            for (UInt16 x = 0; x <= PlaygroundWidth - 1; x++)
            {
                playground.Add(new List<Point>());
                var pgx = playground[x];
                for (UInt16 y = 0; y <= PlaygroundHeight - 1; y++)
                {
                    // Wypełnienie lewej i prawej krawędzi boiska
                    if (x == 0 || x == PlaygroundWidth - 1)
                    {
                        if (y > 0 && y < PlaygroundHeight - 1) 
                        {
                            pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Border));
                        }
                        else
                        {
                            pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Outer));
                        }
                    }
                    else // wypełnienie pozostałych części
                    {   // jeżeli to wiersz pierwszy i przedostatni to jest to krawędź boiska
                        if (y == 1 || y == PlaygroundHeight - 2)
                        {   //jeżeli to kolumna z bramką, to nie nie nie ma tutaj krawędzi
                            if ((x <= halfWidth - 1) || (x >= halfWidth + 1))
                            {
                                pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Border));
                            }
                        }
                        else
                        {   // W przeciwnym wypadku jest to wewnątrz boiska
                            pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Empty));
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

        BoardClass board = new BoardClass();

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
            board.Init();
        }
    }
}
