using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSoccer
{
    public class Edge
    {
        private Coord Starting_point;
        private Coord Ending_point;
        private GameSettings.Player Owner;

        public Edge(Coord p1, Coord p2, GameSettings.Player owner)
        {
            Starting_point = p1;
            Ending_point = p2;
            Owner = owner;
        }
        public Coord GetStartingPoint()
        {
            return Starting_point;
        }
        public Coord GetEndingPoint()
        {
            return Ending_point;
        }
        public GameSettings.Player GetOwner()
        {
            return Owner;
        }
    }
}
