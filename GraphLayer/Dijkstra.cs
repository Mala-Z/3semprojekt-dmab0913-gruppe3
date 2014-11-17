using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace GraphLayer
{
    public class Dijkstra
    {
        Graph graph = new Graph();

        private List<Vertex> listOfCost;
        //Queue for the vertices to be evaluated
        private List<Vertex> vertexQueue;   

        public Dijkstra()
        {
            graph = new Graph();
            listOfCost = new List<Vertex>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        private void Initialize(Airport start, string date)
        {
            graph.AddAllVertices(date);
            graph.AddAllEdges();

            Vertex startVertex = graph.GetVertices().Where(v => v.GetAirport().airportID == start.airportID).ToList().First();

            // Set distance to 0 for starting point and the previous node to null (-1) 
            startVertex.DistanceFromStart = 0;

            // Set distance to all vertices to infinity
            for (int i = 0; i < graph.GetVertices().Count; i++)
            {
                if (!graph.GetVertices()[i].Equals(startVertex))
                {
                    graph.GetVertices()[i].DistanceFromStart = Double.PositiveInfinity;
                }
                vertexQueue.Add(graph.GetVertices()[i]);
            }
        }

        public List<Vertex> RunDijkstra()
        {
            List<Vertex> costPath = new List<Vertex>();

            while (vertexQueue.Count > 0)
            {
                Vertex currentVertex = vertexQueue.OrderByDescending(v => v.DistanceFromStart).ToList().Last();
                vertexQueue.Remove(currentVertex);

                foreach (Edge edge in currentVertex.GetEdges())
                {
                    double cost = currentVertex.DistanceFromStart + edge.GetCost();

                    if (cost < edge.GetCost())
                    {
                        Vertex updatedVertex = edge.To;
                        updatedVertex.DistanceFromStart = cost;
                        costPath.Add(updatedVertex);
                    }//end if
                }//end foreach
            }//end while

            return costPath;
        }


        public List<Vertex> Test(Airport start, Airport to, string date)
        {
            Initialize(start, date);
            List<Vertex> dijkstra = RunDijkstra();
            List<Vertex> shortestPath = new List<Vertex>();
            Vertex endVertex = graph.GetVertices().Where(v => v.GetAirport().airportID == to.airportID).ToList().First();
            bool found = false;

            for (int i = 0; i < dijkstra.Count && !found; i++)
            {
                shortestPath.Add(dijkstra[i]);
                if (dijkstra[i].Equals(endVertex))
                {
                    found = true;
                }
            }

            return shortestPath;
        }


    }
}
