using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLayer;
using DatabaseLayer;


namespace TestDijkstra
{
    class Program
    {
        static FlightCtr flightCtr = new FlightCtr();
        static AirportCtr airportCtr = new AirportCtr();

        private static void createTestFlight()
        {
            flightCtr.AddFlight("03/11/2014 12:00", "03/11/2014 14:30", 0.5, 100.50, 1, 3, 1, 0); //Fra Aalborg til KBH
            flightCtr.AddFlight("03/11/2014 12:30", "03/11/2014 14:00", 1.5, 130.50, 1, 2, 2, 0); //Fra Aalborg til Billund
            flightCtr.AddFlight("03/11/2014 12:00", "03/11/2014 14:00", 3.5, 100.50, 2, 4, 1, 0); //Fra Billund til London
            flightCtr.AddFlight("03/11/2014 12:00", "03/11/2014 14:00", 1.5, 100.50, 3, 4, 4, 0); //Fra KBH til London
            Console.WriteLine("Test flights created");
        }

        static void Main(string[] args)
        {
            var startAirport = airportCtr.GetAirportByID(1);
            var endAirport = airportCtr.GetAirportByID(4);
            //createTestFlight();
            var dijkstra = new Dijkstra(new GraphCtr(airportCtr.GetAllAirports()), startAirport, "03/11/2014");

            ObservableCollection<Airport> shortestPath = dijkstra.ShortestPath(startAirport, endAirport);

            foreach (var airport in shortestPath)
            {
                Console.WriteLine(airport.name);
            }

            Console.ReadLine();
        }
    }
}
