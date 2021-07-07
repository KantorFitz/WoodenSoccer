using System;
using System.Collections.Generic;

namespace PaperSoccer
{
    public class Edge
    {
        private readonly Coord _startingPoint;
        private readonly Coord _endingPoint;
        private readonly GameSettings.Player? _owner;
        private readonly BoardSettings.BoardPoint? _type;

        public Edge(Coord p1, Coord p2, GameSettings.Player owner)
        {
            _startingPoint = p1;
            _endingPoint = p2;
            _owner = owner;
        }

        public Edge(Coord p1, Coord p2, BoardSettings.BoardPoint type)
        {
            _startingPoint = p1;
            _endingPoint = p2;
            _type = type;
        }

        public Coord GetStartingPoint()
        {
            return _startingPoint;
        }
        public Coord GetEndingPoint()
        {
            return _endingPoint;
        }
        public GameSettings.Player? GetOwner()
        {
            return _owner;
        }
        public new BoardSettings.BoardPoint? GetType()
        {
            return _type;
        }
        /// <summary>
        /// Sprawdza czy obie krawędzie mają takie same współrzędne
        /// </summary>
        /// <param name="obj">Parametr jest krawędzią porównywaną</param>
        /// <returns>Jeżeli krawędzie posiadają identyczne współrzędne, zwraca TRUE, w przeciwnym razie FALSE</returns>
        public bool Equals(Edge obj)
        {
            bool x1Equals = this.GetStartingPoint().GetX() == obj.GetStartingPoint().GetX();
            bool y1Equals = this.GetStartingPoint().GetY() == obj.GetStartingPoint().GetY();
            bool x2Equals = this.GetEndingPoint().GetX() == obj.GetEndingPoint().GetX();
            bool y2Equals = this.GetEndingPoint().GetY() == obj.GetEndingPoint().GetY();

            return x1Equals && y1Equals && x2Equals && y2Equals;
        }
    }
}
