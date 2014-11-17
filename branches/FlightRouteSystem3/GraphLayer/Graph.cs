﻿using System;
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
                    int index = 0;
                    bool isfound = false;
                    Vertex startVertex = vertex;

                    while (index < _vertices.Count() && !isfound)
                    {
                        if ((_vertices[index].GetAirport().airportID == flight.to))
                        {
                            Vertex endVertex = _vertices[index];
                            Edge edge = new Edge(flight, startVertex, endVertex);
                            isfound = true;

                            edges.Add(edge);
                            Console.WriteLine(Convert.ToString(flight.flightID) + " Edge added from " + startVertex.GetAirport().name + " to " + endVertex.GetAirport().name);

                            
                        }
                        else
                        {
                            index++;
                        }
                    }


                }
                vertex.setEdges(edges);

            }
        }


        //_adjList[startIndex].AddFirst(endVertex);
        ////if undirected also:
        ////int endIndex = _vertices.IndexOf(endVertex);
        ////_adjList[endIndex].AddFirst(startVertex);
        /// 
        public bool ContainsVertex(Vertex vertex)
        {
            return _vertices.Contains(vertex);
        }

       

        //public override IList<Vertex> GetAdjacencies(Vertex vertex)
        //{
        //    int vertexIndex = _vertices.IndexOf(vertex);
        //    return new List<Vertex>(_adjList[vertexIndex]);
        //}

  
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

