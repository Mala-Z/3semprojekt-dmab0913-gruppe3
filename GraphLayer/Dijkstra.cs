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
            Vertex updatedVertex = null;

            while (vertexQueue.Count > 0)
            {
                Vertex currentVertex = ShortestDistFromStart();
                vertexQueue.Remove(currentVertex);
                //listOfCost.Add(currentVertex);

                //foreach (Vertex vertex in vertexQueue)
                //{ 
                    //foreach (Edge edge in currentVertex.GetEdges())
                    foreach (Edge edge in currentVertex.GetEdges())
                    {
                        //if (edge.To.Equals(vertex))
                        //{
                        double cost = currentVertex.DistanceFromStart + edge.GetCost();

                            if (cost < edge.GetCost())
                            { 
                                updatedVertex = edge.To;
                                updatedVertex.DistanceFromStart = cost;
                                updatedVertex.PrevVertex = currentVertex;
                                //listOfCost.Add(updatedVertex);
                            }//end if
                        //}
                        
                    }//end foreach
                //}

            }//end while

            //solutionList = Backtrack(from, to);
            solutionList = BackTest(updatedVertex, to);

            return solutionList;
        }

        private List<Vertex> BackTest(Vertex updatedVertex, Airport to)
        {
            while (updatedVertex !=null)
            {
                solutionList.Add(updatedVertex);

                if (updatedVertex.GetAirport().airportID == to.airportID)
                {
                    updatedVertex = null;
                }
                else
                {
                    updatedVertex = updatedVertex.PrevVertex;
                }
                
            }

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
            return solutionList;
        }

        private Vertex ShortestDistFromStart()
        {
            return vertexQueue.OrderByDescending(v => v.DistanceFromStart).ToList().Last();
            
        }


        public List<Vertex> Test(Airport from, Airport to, string date)
        {
            List<Vertex> dijkstra = RunDijkstra(from, to, date);

            return dijkstra;
        }


    }
}
