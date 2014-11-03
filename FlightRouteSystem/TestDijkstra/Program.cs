using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlLayer;

namespace TestDijkstra
{
    class Program
    {
        static FlightCtr flightCtr = new FlightCtr();

        private static void createTestFlight()
        {
            flightCtr.AddFlight("03/11/2014 12:00", "03/11/2014 14:30", 2.5, 100.50, 1, 3, 1, 0); //Fra Aalborg til KBH
            flightCtr.AddFlight("03/11/2014 12:30", "03/11/2014 14:00", 1.5, 130.50, 1, 2, 2, 0); //Fra Aalborg til Billund
            flightCtr.AddFlight("03/11/2014 12:00", "03/11/2014 14:00", 3.5, 100.50, 2, 4, 1, 0); //Fra Billund til London
            flightCtr.AddFlight("03/11/2014 12:00", "03/11/2014 14:00", 4.0, 100.50, 3, 4, 4, 0); //Fra KBH til London
            Console.WriteLine("Test flights created");
        }

        static void Main(string[] args)
        {
            createTestFlight();
            Console.ReadLine();
        }
    }
}
