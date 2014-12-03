using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DatabaseLayer;
using ControlLayer;
using System.Runtime.Serialization;

namespace GraphLayer
{
    /// <summary>
    /// Represents a vertex in a graph
    /// </summary>

    [DataContract]
    public class Vertex
    {
        [DataMember]
        private Airport airport;
        [DataMember]
        private List<Flight> flightsFromAiport;
        [DataMember]
        private List<Edge> edges;
        [DataMember]
        private string date;
        [DataMember]
        private FlightCtr flightCtr;


        public Vertex(Airport airport, string date)
        {
            var main = new MainCtr();
            flightCtr = main.FlightCtr;
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

        [DataMember]
        public double DistanceFromStart { get; set; }

        [DataMember]
        public Vertex PrevVertex { get; set; }

        [DataMember]
        public Edge EdgeToUse { get; set; }

    }
}



