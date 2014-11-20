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
        private List<Edge> edges;
        private string date;
        private FlightCtr flightCtr;


        public Vertex(Airport airport, string date)
        {
            flightCtr = new FlightCtr();
            this.airport = airport;
            this.date = date;
            flightsFromAiport = AddFlights();
            edges = new List<Edge>();
        }

        public Vertex()
        {
            
        }

        public override bool Equals(object obj)
        {
            Vertex other = (Vertex)obj;
            return airport.airportID == other.GetAirport().airportID;
        }


        public void setEdges(List<Edge> edges)
        {
            this.edges = edges;
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

        public List<Edge> GetEdges()
        {
            return edges;
        }

        public double DistanceFromStart { get; set; }

        public Vertex PrevVertex { get; set; }

    }
}



