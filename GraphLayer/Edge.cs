using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private Flight vertexEdge;

        public Edge(Flight flight, Vertex to, Vertex from)
        {
            vertexEdge = flight;
            this.To = to;
            this.From = from;
        }

        public double GetCost()
        {
            double cost = -1;
            if (vertexEdge != null)
            {
                cost = System.Convert.ToDouble(vertexEdge.traveltime);
            }
            return cost;
        }

        public Vertex From { get; set; }
        public Vertex To { get; set; }
    }
}
