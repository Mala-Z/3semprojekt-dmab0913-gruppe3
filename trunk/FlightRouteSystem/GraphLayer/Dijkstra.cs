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

        public List<Vertex> RunDijkstra(Airport from, Airport to, string date)
        {
            Initialize(from, date);

            while (vertexQueue.Count > 0)
            {
                Vertex currentVertex = ShortestDistFromStart();
                listOfCost.Add(currentVertex);
                vertexQueue.Remove(currentVertex);

                foreach (Edge edge in currentVertex.GetEdges())
                {
                    double cost = currentVertex.DistanceFromStart + edge.GetCost();

                    if (cost < edge.To.DistanceFromStart)
                    { 
                        Vertex updatedVertex = edge.To;
                        updatedVertex.DistanceFromStart = cost;
                        updatedVertex.PrevVertex = currentVertex;
                        updatedVertex.EdgeToUse = edge;
                        //listOfCost.Add(updatedVertex);
                    }//end if
                 }
                        

            }//end while

            solutionList = Backtrack(from, to);
            //solutionList = BackTest(updatedVertex, to);

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
            //bool isFound = false;
            //for (int i = 0; i < listOfCost.Count && !isFound; i++)
            //{
            //    if (listOfCost[i].GetAirport().Equals(from))
            //    {
            //        solutionList.Add(listOfCost[i]);
            //        isFound = true;
            //    }
            //}
            solutionList.Reverse();
            return solutionList;
        }

        private Vertex ShortestDistFromStart()
        {
            return vertexQueue.OrderByDescending(v => v.DistanceFromStart).ToList().Last();
            //Vertex smallestDist = new Vertex();
            //smallestDist.DistanceFromStart = Double.PositiveInfinity;
            //foreach (Vertex vertex in vertexQueue)
            //{
            //    if (vertex.DistanceFromStart < smallestDist.DistanceFromStart)
            //    {
            //        smallestDist = vertex;
            //    }
            //}
            //return smallestDist;
        }


        public List<Vertex> Test(Airport from, Airport to, string date)
        {
            List<Vertex> dijkstra = RunDijkstra(from, to, date);

            return dijkstra;
        }


    }
}
