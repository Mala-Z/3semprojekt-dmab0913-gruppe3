using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLayer;
using GraphLayer;
using ControlLayer;


namespace TestLayer
{
    class Program
    {
        private static Graph graph = new Graph();
        static void Main(string[] args)
        {
            //AirportCtr airCtr = new AirportCtr();

            //Console.WriteLine("Hit enter to see from Aalborg to London - fastest traveltime and cheapest price");
            ////Console.ReadLine();

            //#region Multiple threads test
            ////Create airports
            //Airport AAL = airCtr.GetAirportByID(1);
            //Airport LHR = airCtr.GetAirportByID(4);
            ////Create variables to contain list from RunDijkstra()
            //IEnumerable<Vertex> shortestPathByPriceList = null;
            //IEnumerable<Vertex> shortestPathByTravelTimeList = null;
            ////Create threads that runs RunDijkstra and saves to variables
            //var shortestPathByPriceThread = new Thread(() => shortestPathByPriceList = RunDijkstra(AAL, LHR, "17/11/2014", true));
            //var shortestPathByTravelTimeThread = new Thread(() => shortestPathByTravelTimeList = RunDijkstra(AAL, LHR, "17/11/2014", false));
            ////Start threads
            //shortestPathByPriceThread.Start();
            //shortestPathByTravelTimeThread.Start();
            ////Join threads
            //shortestPathByPriceThread.Join();
            //shortestPathByTravelTimeThread.Join();
            ////Print the info from lists
            //PrintInfo(shortestPathByPriceList);
            //PrintInfo(shortestPathByTravelTimeList);
            //Console.ReadLine();
            //#endregion

            var bookingCtr = new BookingCtr();
            var flightCtr = new FlightCtr();

            Booking booking = bookingCtr.GetBookingByID(1);
            Flight f = flightCtr.GetFlightByID(10);

            var db = new dmab0913_3DataContext();
            var bf = new BookingFlight
            {
                Booking = booking,
                //bookingID = booking.bookingID,
                Flight = f
                //flightID = f.flightID
            };
            db.BookingFlights.InsertOnSubmit(bf);
            db.SubmitChanges();

        }

        private static IEnumerable<Vertex> RunDijkstra(Airport from, Airport to, string date, bool usePrice)
        {
            AirportCtr airportCtr = new AirportCtr();
            Dijkstra dijkstra = new Dijkstra();

            var result = dijkstra.RunDijkstra(from, to, date, usePrice);

            return result;
        }

        private static void PrintInfo(IEnumerable<Vertex> shortestpath)
        {
            AirportCtr airportCtr = new AirportCtr();
            double time = 0;
            double price = 0;
            Console.WriteLine("Travel route:");
            foreach (var v in shortestpath)
            {
                Console.WriteLine(v.EdgeToUse.VertexEdge.flightID + " from: " +
                                  airportCtr.GetAirportByID(v.EdgeToUse.VertexEdge.@from).name + " to: " +
                                  airportCtr.GetAirportByID(v.EdgeToUse.VertexEdge.to).name
                                  + " Price: " + v.EdgeToUse.VertexEdge.price + "kr. Traveltime: " +
                                  v.EdgeToUse.VertexEdge.traveltime);
                time += (double)v.EdgeToUse.VertexEdge.traveltime;
                price += (double)v.EdgeToUse.VertexEdge.price;
            }
            Console.WriteLine("Totals - Price: {0}, Traveltime: {1}", price, time);
        }
    }
}
