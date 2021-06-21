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

        private UInt16 pgWidth;
        private UInt16 pgHeight;

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundWidth
        {
            get { return pgWidth; }
            set
            {
                if (value >= 5)
                {
                    if (value % 2 == 0)
                    {
                        pgWidth = (ushort)(value + 1);
                    }
                    else
                    {
                        pgWidth = (ushort)value;
                    }                    
                }
                else
                {
                    pgWidth = 5;
                }                
            }
        }

        /// <summary>
        /// Te właściwości są odpowiedzialne za szerokość i wysokość planszy boiska, nie mogą być mniejsze od 5x7 i muszą być zawsze nieparzyste. Bramka ma szerokość 3 punktów.
        /// </summary>
        public UInt16 PlaygroundHeight
        {
            get { return pgHeight; }
            set
            {
                if (value >= 7)
                {
                    if (value % 2 == 0)
                    {
                        pgHeight = (ushort)(value + 1);
                    }
                    else
                    {
                        pgHeight = (ushort)value;
                    }
                }
                else
                {
                    pgHeight = 7;
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

            // Wypełniamy całe boisko polem outer; -- warstwa pierwsza
            for (UInt16 x = 0; x < PlaygroundWidth; x++)
            {
                playground.Add(new List<Point>());
                var pgx = playground[x];
                for (UInt16 y = 0; y < PlaygroundHeight; y++)
                {
                    pgx.Add(new Point(x, y, BoardSettings.BoardPoint.Outer));                    
                }
            }

            // Wypełniamy obramowanie boiska polem Border; -- warstwa druga
            for (UInt16 x = 0; x < PlaygroundWidth; x++)
            {
                for (UInt16 y = 0; y < PlaygroundHeight; y++)
                {
                    if (((x == 0) || (x == PlaygroundWidth - 1)) && (y > 0 && y < PlaygroundHeight - 1))
                    {
                        playground[x][y].pointType = BoardSettings.BoardPoint.Border; // ||
                    }
                    if (y == 1 || y == PlaygroundHeight - 2)
                    {
                        playground[x][y].pointType = BoardSettings.BoardPoint.Border; // =
                    }
                }
            }

            // Wypełniamy środek boiska polem Empty -- warstwa trzecia
            for (UInt16 x = 1; x < PlaygroundWidth - 1; x++)
            {
                for (UInt16 y = 2; y < PlaygroundHeight - 2; y++)
                {
                    playground[x][y].pointType = BoardSettings.BoardPoint.Empty;
                }
            }

            // Wypełniamy punkty bramki polem Goal i usuwamy jeden punkt w środku bramki -- warstwa czwarta
            UInt16 halfWidth = (UInt16)(pgWidth / 2);
            for (int i = -1; i < 2; i++)
            {
                playground[halfWidth + i][0].pointType = BoardSettings.BoardPoint.Player1Goal;
                playground[halfWidth + i][PlaygroundHeight - 1].pointType = BoardSettings.BoardPoint.Player2Goal;
                if (i == 0)
                {
                    playground[halfWidth][1].pointType = BoardSettings.BoardPoint.Empty;
                    playground[halfWidth][PlaygroundHeight - 2].pointType = BoardSettings.BoardPoint.Empty;
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
