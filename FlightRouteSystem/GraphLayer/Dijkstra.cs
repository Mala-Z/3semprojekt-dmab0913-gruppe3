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
        private List<Vertex> solutionList; 

        public Dijkstra()
        {
            graph = new Graph();
            listOfCost = new List<Vertex>();
            vertexQueue = new List<Vertex>();
            solutionList = new List<Vertex>();
        }

        /// <summary>
        /// Initialize for Dijkstra. Adds all vertices and edges, and sets all distances from start to infinity
        /// </summary>
        /// <param name="start">The aiport to fly from</param>
        /// <param name="date">The date to fly from</param>
        private void Initialize(Airport start, string date)
        {
            graph.AddAllVertices(date);
            graph.AddAllEdges();

            Vertex startVertex = graph.GetVertices().Where(v => v.GetAirport().airportID == start.airportID).ToList().First();

            // Set distance to 0 for starting point and the previous node to null (-1) 
            startVertex.DistanceFromStart = 0;
            startVertex.PrevVertex = null;

            // Set distance to all vertices to infinity
            for (int i = 0; i < graph.GetVertices().Count; i++)
            {
                if (!graph.GetVertices()[i].Equals(startVertex))
                {
                    graph.GetVertices()[i].DistanceFromStart = Double.PositiveInfinity;
                    graph.GetVertices()[i].PrevVertex = null;
                }
                vertexQueue.Add(graph.GetVertices()[i]);
            }
        }

        public List<Vertex> RunDijkstra(Airport from, Airport to, string date, bool usePrice)
        {
            Initialize(from, date);

            while (vertexQueue.Count > 0)
            {
                Vertex currentVertex = ShortestDistFromStart();
                listOfCost.Add(currentVertex);
                vertexQueue.Remove(currentVertex);

                foreach (Edge edge in currentVertex.GetEdges())
                {
                    double cost = currentVertex.DistanceFromStart + edge.GetCost(usePrice);

                    if (cost < edge.To.DistanceFromStart)
                    { 
                        Vertex updatedVertex = edge.To;
                        updatedVertex.DistanceFromStart = cost;
                        updatedVertex.PrevVertex = currentVertex;
                        updatedVertex.EdgeToUse = edge;
                    }//end if
                 }
                        

            }//end while

            solutionList = Backtrack(from, to);

            return solutionList;
        }

        private List<Vertex> Backtrack(Airport from, Airport to)
        {
            for (int i = 0; i < listOfCost.Count; i++)
            {
                if (listOfCost[i].GetAirport().Equals(to))
                {
                    while (listOfCost[i].PrevVertex != null)
                    {
                        solutionList.Add(listOfCost[i]);
                        listOfCost[i] = listOfCost[i].PrevVertex;
                    }
                }
            }
            solutionList.Reverse();
            return solutionList;
        }

        private Vertex ShortestDistFromStart()
        {
            return vertexQueue.OrderByDescending(v => v.DistanceFromStart).ToList().Last();
        }



    }
}
