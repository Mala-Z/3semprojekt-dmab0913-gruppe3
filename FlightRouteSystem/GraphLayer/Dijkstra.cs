using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseLayer;

namespace GraphLayer
{
    public class Dijkstra
    {
        readonly Graph _graph = new Graph();
        private readonly List<Vertex> _listOfCost;
        //Queue for the vertices to be evaluated
        private readonly List<Vertex> _vertexQueue;
        private List<Vertex> _solutionList;

        public Dijkstra()
        {
            _graph = new Graph();
            _listOfCost = new List<Vertex>();
            _vertexQueue = new List<Vertex>();
            _solutionList = new List<Vertex>();
        }

        /// <summary>
        /// Initialize for Dijkstra. Adds all vertices and edges, and sets all distances from start to infinity
        /// </summary>
        /// <param name="start">The aiport to fly from</param>
        /// <param name="date">The date to fly from</param>
        private void Initialize(Airport start, string date)
        {
            _graph.AddAllVertices(date);
            _graph.AddAllEdges();

            Vertex startVertex = _graph.GetVertices().Where(v => v.GetAirport().airportID == start.airportID).ToList().First();

            // Set distance to 0 for starting point and the previous node to null (-1) 
            startVertex.DistanceFromStart = 0;
            startVertex.PrevVertex = null;

            // Set distance to all vertices to infinity
            for (int i = 0; i < _graph.GetVertices().Count; i++)
            {
                if (!_graph.GetVertices()[i].Equals(startVertex))
                {
                    _graph.GetVertices()[i].DistanceFromStart = Double.PositiveInfinity;
                    _graph.GetVertices()[i].PrevVertex = null;
                }
                _vertexQueue.Add(_graph.GetVertices()[i]);
            }
        }

        
        public List<Vertex> RunDijkstra(Airport from, Airport to, string date, bool usePrice)
        {
            _solutionList.Clear();
            Initialize(from, date);

            while (_vertexQueue.Count > 0)
            {
                Vertex currentVertex = ShortestDistFromStart();
                _listOfCost.Add(currentVertex);
                _vertexQueue.Remove(currentVertex);

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

            _solutionList = Backtrack(from, to);
            return _solutionList;
        }

       
        private List<Vertex> Backtrack(Airport from, Airport to)
        {
            for (int i = 0; i < _listOfCost.Count; i++)
            {
                if (_listOfCost[i].GetAirport().airportID.Equals(to.airportID))
                {
                    while (_listOfCost[i].PrevVertex != null)
                    {
                        _solutionList.Add(_listOfCost[i]);
                        _listOfCost[i] = _listOfCost[i].PrevVertex;
                    }
                }
            }
            _solutionList.Reverse();
            return _solutionList;
        }

        
        private Vertex ShortestDistFromStart()
        {
            return _vertexQueue.OrderByDescending(v => v.DistanceFromStart).ToList().Last();
        }
    }
}
