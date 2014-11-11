using System;
using System.Collections;
using System.Text;
using DatabaseLayer;

namespace GraphLayer
{
    /// <summary>
    /// Represents a vertex in a graph
    /// </summary>
    public class Vertex
    {
        private Airport airport;


        public Vertex(Airport airport)
        {
            this.airport = airport;
        }

        public override bool Equals(object obj)
        {
            Vertex other = (Vertex)obj;
            return airport.Equals(other.airport);
        }

    }
}



