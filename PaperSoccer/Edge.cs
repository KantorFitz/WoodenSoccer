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
            bool xy1Start = GetStartingPoint().Equals(obj.GetStartingPoint());
            bool xy1End = GetEndingPoint().Equals(obj.GetEndingPoint());
            bool normal = xy1End && xy1Start;

            bool xys1xye2 = GetStartingPoint().Equals(obj.GetEndingPoint());
            bool xye1xys2 = GetEndingPoint().Equals(obj.GetStartingPoint());
            bool reversed1 = xys1xye2 && xye1xys2;

            bool xys2xye1 = obj.GetEndingPoint().Equals(GetStartingPoint());
            bool xye2xys1 = obj.GetStartingPoint().Equals(GetEndingPoint());
            bool reversed2 = xys2xye1 && xye2xys1;

            return normal || reversed1 || reversed2;
        }
    }
}
