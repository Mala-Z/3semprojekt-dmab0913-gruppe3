using System.Collections.Generic;
using DatabaseLayer;
using ControlLayer;

namespace GraphLayer
{
    /// <summary>
    /// Represents a vertex in a graph
    /// </summary>
    public class Vertex
    {

        private readonly Airport _airport;
        private List<Flight> _flightsFromAiport;
        private List<Edge> _edges;
        private readonly string _date;
        private readonly FlightCtr _flightCtr;
        public double DistanceFromStart { get; set; }
        public Vertex PrevVertex { get; set; }
        public Edge EdgeToUse { get; set; }


        public Vertex(Airport airport, string date)
        {
            var main = new MainCtr();
            _flightCtr = main.FlightCtr;
            _airport = airport;
            _date = date;
            _flightsFromAiport = AddFlights();
            _edges = new List<Edge>();
        }

        public Vertex()
        {
            
        }

        public override bool Equals(object obj)
        {
            Vertex other = (Vertex)obj;
            return _airport.airportID == other.GetAirport().airportID;
        }

        public void SetEdges(List<Edge> edges)
        {
            _edges = edges;
        }

        public List<Flight> AddFlights()
        {
            return _flightCtr.GetFlightsFrom(_airport, _date);
        }

        public void SetFlights(List<Flight> flights)
        {
            _flightsFromAiport = flights;
        }

        public List<Flight> GetFlights()
        {
            return _flightsFromAiport;
        }

        public Airport GetAirport()
        {
            return _airport;
        }

        public List<Edge> GetEdges()
        {
            return _edges;
        }
    }
}



