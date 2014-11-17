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
        private List<Flight> flightsFromAiport;
        private Dictionary<Vertex, Edge> edges;
        private string date;
        private FlightCtr flightCtr;


        public Vertex(Airport airport, string date)
        {
            flightCtr = new FlightCtr();
            this.airport = airport;
            this.date = date;
            flightsFromAiport = AddFlights();
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
            return flightCtr.GetFlightsFrom(airport, date);
        }

        public void SetFlights(List<Flight> flights)
        {
            this.flightsFromAiport = flights;
        }


        public List<Flight> GetFlights()
        {
            return flightsFromAiport;
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



