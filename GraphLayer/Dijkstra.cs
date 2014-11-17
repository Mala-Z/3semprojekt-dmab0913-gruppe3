using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //Queue for the vertices to be evaluated
        private List<Vertex> vertexQueue = new List<Vertex>();

        public Dijkstra()
        {
            graph = new Graph();
            dist = new Dictionary<Vertex, double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        private void Initialize(Airport start, string date)
        {
            graph.AddAllVertices(date);
            graph.AddAllEdges();

            Vertex startVertex = (Vertex) graph.GetVertices().Where(x => x.GetAirport().airportID == start.airportID).Take(1);

            // Set distance to all vertices to infinity
            for (int i = 0; i < graph.GetVertices().Count; i++)
            {
                dist[graph.GetVertices()[i]] = Double.PositiveInfinity;

                vertexQueue.Add(graph.GetVertices()[i]);
            }

            // Set distance to 0 for starting point and the previous node to null (-1) 
            dist[startVertex] = 0;
            
            //path[start] = default(Airport);
        }

        private Vertex GetNextVertex()
        {
            double min = Double.PositiveInfinity;
            Vertex vertex = default(Vertex);

            // Search through queue to find the next node having the smallest distance 
            foreach (Vertex v in vertexQueue)
            {
                if (dist[v] <= min)
                {
                    min = dist[v];
                    vertex = v;
                }
            }

            vertexQueue.Remove(vertex);

            return vertex;
        }

        public void RunDijkstra(Airport start, string date)
        {
            Initialize(start, date);

            while (vertexQueue.Count > 0)
            {
                Vertex from = GetNextVertex();

                /* Find the nodes that u connects to and perform relax */
                for (int vi = 0; vi < vertexQueue.Count; vi++)
                {
                    Vertex to = graph.GetVertices()[vi];
                    
                    //TODO Tjek om der er en edge
                    Edge edge = (Edge)from.GetEdges().Where(x => x.Key.Equals(from) 
                        && x.Value.To.Equals(to));
                    /* Check for an edge between u and v */
                    if (edge.GetCost() > 0)
                    {
                        /* Edge exists, relax the edge */
                        if (dist[to] > dist[from] + edge.GetCost())
                        {
                            dist[to] = dist[from] + edge.GetCost();
                            //path[to] = from;
                        }
                    }
                }
            }
            
        }

        public List<Flight> ShortestPath(Airport from, Airport to)
        {
            var result = new List<Flight>();
            var shortestPath = new List<Flight>();

            while (!EqualityComparer<Airport>.Default.Equals(to, default(Airport)))
            {
                shortestPath.Add(to);
                to = path[to];
            }
            shortestPath.Reverse();

            shortestPath.ForEach(result.Add);

            return result;
        }

    }
}
