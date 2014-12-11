using System.Collections.Generic;
using ControlLayer;
using DatabaseLayer;


namespace GraphLayer
{
    public class Graph : IAbstractGraph
    {
 
        private readonly AirportCtr _airportCtr;

        private readonly List<Vertex> _vertices;


        public Graph()
        {
            var main = new MainCtr();
            _airportCtr = main.AirportCtr;
            _vertices = new List<Vertex>();
        }

        public void AddAllVertices(string date)
        {
            foreach (Airport airport in _airportCtr.GetAllAirports())
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
                            isFound = true;
                        }
                    }

                }
                vertex.SetEdges(edges);
            }
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

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

    }
}

