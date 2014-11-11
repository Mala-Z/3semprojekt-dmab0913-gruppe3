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


        public Vertex(Airport airport)
        {
            this.airport = airport;
            flights = AddFlights();
        }

        public override bool Equals(object obj)
        {
            Vertex other = (Vertex)obj;
            return airport.Equals(other.airport);
        }

        public List<Flight> AddFlights()
        {
            var db = DBConnection.GetInstance().GetConnection();
            var list = db.Flights.Where(f => f.from == airport.airportID).Select(f => f).ToList();
            return list;
        }

        public List<Vertex> AddEdges()
        {
            var db = DBConnection.GetInstance().GetConnection();
            var list = db.Flights.Where(f => f.from == airport.airportID).Select(f => f).ToList();
            var listEdges = new List<Vertex>();
            foreach (Flight f in list)
            {
                var v = db.Airports.Where(a => a.airportID == f.to).Select(a => a);
                listEdges.Add();
            }
            return listEdges;
        }

        public List<Flight> getFlights()
        {
            return flights;
        }

    }
}



