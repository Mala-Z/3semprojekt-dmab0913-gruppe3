using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ControlLayer;
using GraphLayer;
using System.Linq;
using System.Data.Linq;
using DatabaseLayer;


namespace GraphLayer
{
    public class Graph : IAbstractGraph
    {
        private AirportCtr airportCtr;

        private List<Vertex> _vertices;


        public Graph()
        {
            airportCtr = new AirportCtr();
            _vertices = new List<Vertex>();
        }

        public void AddAllVertices(string date)
        {
            foreach (Airport airport in airportCtr.GetAllAirports())
            {
                Vertex v = new Vertex(airport, date);
                _vertices.Add(v);
            }
        }

        public void AddAllEdges()
        {

            foreach (Vertex vertex in _vertices)
            {
                List<Edge> edges = new List<Edge>();
                foreach (Flight flight in vertex.GetFlights())
                {
                    bool isFound = false;
                    Vertex startVertex = vertex;

                    for (int i = 0; i < _vertices.Count && !isFound; i++)
                    {
                        Vertex endVertex = _vertices[i];
                        if ((endVertex.GetAirport().airportID == flight.to))
                        {
                            Edge edge = new Edge(flight, endVertex, startVertex);
                            edges.Add(edge);
                            Console.WriteLine(Convert.ToString(flight.flightID) + " Edge added from " + startVertex.GetAirport().name + " to " + endVertex.GetAirport().name);
                            isFound = true;
                        }
                    }

                }
                vertex.setEdges(edges);

            }
            Console.WriteLine("All edges added");
        }

        public bool ContainsVertex(Vertex vertex)
        {
            return _vertices.Contains(vertex);
        }

  
        public List<Vertex> GetVertices()
        {
            return _vertices;
        }

        public bool IsEmpty()
        {
            return _vertices.Count == 0;
        }

        public int GetNoOfVertices()
        {
            return _vertices.Count;
        }

        //public int GetNoOfEdges()
        //{
        //    int count = 0;
        //    foreach (LinkedList<Vertex> l in _adjList)
        //        count = count + l.Count;
        //    return count;//if undirected count/2
        //}

        public void Clear()
        {
            //Init();
        }
    }







}

