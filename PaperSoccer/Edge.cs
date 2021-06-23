using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSoccer
{
    public class Edge
    {
        private readonly Coord _startingPoint;
        private readonly Coord _endingPoint;
        private readonly GameSettings.Player _owner;

        public Edge(Coord p1, Coord p2, GameSettings.Player owner)
        {
            _startingPoint = p1;
            _endingPoint = p2;
            _owner = owner;
        }
        public Coord GetStartingPoint()
        {
            return _startingPoint;
        }
        public Coord GetEndingPoint()
        {
            return _endingPoint;
        }
        public GameSettings.Player GetOwner()
        {
            return _owner;
        }
    }
}
