using System;
using System.Collections.Generic;
using System.Dynamic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Linq;
using System.Runtime.Serialization;
using DatabaseLayer;

namespace GraphLayer
{
    public class Edge
    {

        public Edge(Flight flight, Vertex to, Vertex from)
        {
            VertexEdge = flight;
            this.To = to;
            this.From = from;
        }

      
        public double GetCost(bool usePrice)
        {
            double cost = -1;
            
            if (VertexEdge != null)
            {
                if (usePrice)
                {
                    cost = System.Convert.ToDouble(VertexEdge.price);
                }
                else
                {
                    cost = System.Convert.ToDouble(VertexEdge.traveltime);
                }
                
            }
            return cost;
        }

        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public Flight VertexEdge { get; set; }
    }
}
