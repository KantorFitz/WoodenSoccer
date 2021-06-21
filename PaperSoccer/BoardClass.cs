using System;
using System.Collections.Generic;

namespace PaperSoccer
{
    public partial class BoardClass
    {
        /// <summary>
        /// Wysokość i szerokość planszy
        /// </summary>
        private UInt16 pgWidth;
        private UInt16 pgHeight;

        /// <summary>
        /// Plansza do gry w Piłkarzyki
        /// </summary>
        private List<List<Point>> playground = new List<List<Point>>();

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
}
