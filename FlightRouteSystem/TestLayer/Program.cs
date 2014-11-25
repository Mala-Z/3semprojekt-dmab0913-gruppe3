using System;
using System.Collections.Generic;
using System.Linq;
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
            AirportCtr airCtr = new AirportCtr();

            Console.WriteLine("Hit enter to see from Aalborg to London - fastest traveltime and cheapest price");
            //Console.ReadLine();

            #region Multiple threads test

            Airport AAL = airCtr.GetAirportByID(1);
            Airport LHR = airCtr.GetAirportByID(4);
            IEnumerable<Vertex> shortestPathByPriceList = null;
            IEnumerable<Vertex> shortestPathByTravelTimeList = null;
            var shortestPathByPriceThread = new Thread(() => shortestPathByPriceList = RunDijkstra(AAL, LHR, "17/11/2014", true));
            var shortestPathByTravelTimeThread = new Thread(() => shortestPathByTravelTimeList = RunDijkstra(AAL, LHR, "17/11/2014", false));
            shortestPathByPriceThread.Start();
            shortestPathByTravelTimeThread.Start();

            shortestPathByPriceThread.Join();
            shortestPathByTravelTimeThread.Join();
            
            PrintInfo(shortestPathByPriceList);
            PrintInfo(shortestPathByTravelTimeList);
            Console.ReadLine();

            #endregion


        }

        private static List<Vertex> RunDijkstra(Airport from, Airport to, string date, bool usePrice)
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
