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

        private static void createTestFlight(int monthNo, int year)
        {
            for (int i = 1; i < 32; i++)
            {
                int day = 0;
                if (i < 10)
                {
                    day = 0 + i;
                }
                else
                {
                    day = i;
                }

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/"+ monthNo + "/" + year +" 12:30", 0.5, 200, 1, 3, 1, 0); //Fra Aalborg til KBH
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 0.4, 180, 1, 2, 2, 0); //Fra Aalborg til Billund

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.2, 330, 2, 4, 4, 0); //Fra Billund til London
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 2.5, 400, 2, 8, 3, 0); //Fra Billund til Rom
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 0.4, 180, 2, 1, 6, 0); //Fra Billund til Aalborg

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.5, 300, 3, 4, 4, 0); //Fra KBH til London
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 2.7, 390, 3, 8, 3, 0); //Fra KBH til Rom
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 0.5, 200, 3, 1, 1, 0); //Fra KBH til Aalborg
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.0, 325, 3, 6, 4, 0); //Fra KBH til HAM

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.2, 330, 4, 2, 4, 0); //Fra London til Billund
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.5, 300, 4, 3, 4, 0); //Fra London til KBH
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 8.5, 880, 4, 7, 6, 0); //Fra London til Washington
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 0.75, 275, 4, 6, 2, 0); //Fra London til HAM
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 10.50, 750, 4, 5, 2, 0); //Fra London til NY

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 2.5, 400, 8, 2, 1, 0); //Fra Rom til Billund
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 2.7, 390, 8, 3, 2, 0); //Fra Rom til KBH
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 2.0, 240, 8, 6, 3, 0); //Fra Rom til HAM

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 8.5, 880, 7, 4, 6, 0); //Fra Washington til London
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 9, 600, 7, 6, 6, 0); //Fra Washington til HAM
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.5, 350, 7, 5, 6, 32); //Fra Washington til NY

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 0.75, 275, 6, 4, 4, 0); //Fra HAM til London
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 9, 600, 6, 7, 6, 0); //Fra HAM til Washington
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.0, 325, 6, 3, 4, 0); //Fra HAM til KBH
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 7.75, 800, 6, 5, 6, 0); //Fra HAM til NY
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 2.0, 230, 6, 8, 3, 0); //Fra HAM til Rom

                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 7.75, 800, 5, 6, 6, 95); //Fra NY til HAM
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/" + year +" 12:30", 1.5, 350, 5, 7, 6, 32); //Fra NY til Washington
                flightCtr.AddFlight(day + "/" + monthNo + "/" + year +" 12:00", day + "/" + monthNo + "/2014 12:30", 10.50, 750, 5, 4, 2, 0); //Fra London til LON

            }
            
            Console.WriteLine("Test flights created");
        }

        static void Main(string[] args)
        {
            var startAirport = airportCtr.GetAirportByID(1);
            var endAirport = airportCtr.GetAirportByID(8); //Fejl ved forsøg på id 5, 6, 7 og 8

            //createTestFlight(11,2014);
            //createTestFlight(12,2014);
            //createTestFlight(01,2015);
            //createTestFlight(02,2015);
            //createTestFlight(03,2015);

            var dijkstra = new Dijkstra(new GraphCtr(airportCtr.GetAllAirports()), startAirport, "16/11/2014");

            ObservableCollection<Airport> shortestPath = dijkstra.ShortestPath(startAirport, endAirport);

            foreach (var airport in shortestPath)
            {
                Console.WriteLine(airport.name);
            }

            Console.ReadLine();
        }
    }
}
