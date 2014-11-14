using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer;

namespace GraphLayer
{
    /// <summary>
    /// Represents a vertex in a graph
    /// </summary>
    public class Vertex
    {
        private Airport airport;
        private List<Flight> flights;
        private Dictionary<Vertex, Edge> edges;



        public Vertex(Airport airport)
        {
            this.airport = airport;
            flights = AddFlights();
            edges = new Dictionary<Vertex, Edge>();
        }

        public override bool Equals(object obj)
        {
            Vertex other = (Vertex)obj;
            return airport.airportID == other.GetAirport().airportID;
        }


        public void setEdge(Dictionary<Vertex, Edge> edge)
        {
            edges = edge;
        }

        public List<Flight> AddFlights()
        {
            var db = DBConnection.GetInstance().GetConnection();
            var list = db.Flights.Where(f => f.from == airport.airportID).Select(f => f).ToList();
            return list;
        }

        public void setFlights(List<Flight> flights)
        {
            this.flights = flights;
        }


        public List<Flight> getFlights()
        {
            return flights;
        }

        public Airport GetAirport()
        {
            return airport;
        }

    }
}



