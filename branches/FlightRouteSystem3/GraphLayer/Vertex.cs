using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer;
using ControlLayer;

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
            FlightCtr flightCtr = new FlightCtr();

            var list = flightCtr.GetFlightsFrom(airport);
            return list;
        }

        public void SetFlights(List<Flight> flights)
        {
            this.flights = flights;
        }


        public List<Flight> GetFlights()
        {
            return flights;
        }

        public Airport GetAirport()
        {
            return airport;
        }

        public Dictionary<Vertex, Edge> GetEdges()
        {
            return edges;
        }

    }
}



