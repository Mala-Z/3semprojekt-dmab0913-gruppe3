using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace GraphLayer
{
    class Dijkstra
    {
        Graph graph = new Graph();

        private Dictionary<Vertex, double> dist;

        public Dijkstra(Airport start, string date)
        {
            graph = new Graph();
            dist = new Dictionary<Vertex, double>();

            graph.AddAllVertices(date);
            graph.AddAllEdges();
            Initialize(start);
        }

        private void Initialize(Airport start)
        {
            var startVertex = graph.GetVertices().Where(x => x.GetAirport().airportID == start.airportID);

            // Set distance to all vertices to infinity
            for (int i = 0; i < graph.GetVertices().Count; i++)
            {
                dist[graphCtr.airports[i]] = Double.PositiveInfinity;

                airportQueue.Add(graphCtr.airports[i]);
            }

            // Set distance to 0 for starting point and the previous node to null (-1) 
            dist[startVertex] = 0;
            path[start] = default(Airport);
        }

    }
}
