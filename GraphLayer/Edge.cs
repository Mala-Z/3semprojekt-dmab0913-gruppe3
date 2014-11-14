using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Linq;
using DatabaseLayer;

namespace GraphLayer
{
    public class Edge
    {
        private Vertex from;
        private Vertex to;
        private Flight vertexEdge;

        public Edge(Flight flight, Vertex to, Vertex from)
        {
            vertexEdge = flight;
            this.to = to;
            this.from = from;
        }

        public double GetCost()
        {
            return (double)vertexEdge.traveltime;
        }
    }
}
