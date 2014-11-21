using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Console.WriteLine("Hit enter to see from Aalborg to London - fastest traveltime");
            Console.ReadLine();
            Dijkstra dijkstra = new Dijkstra();
            var shortestpath = dijkstra.RunDijkstra(airCtr.GetAirportByID(1), airCtr.GetAirportByID(4), "17/11/2014", false);
            double time = 0;
            double price = 0;
            foreach (var v in shortestpath)
            {
                Console.WriteLine(v.EdgeToUse.VertexEdge.flightID + " from: " + airCtr.GetAirportByID(v.EdgeToUse.VertexEdge.from).name + " to: " + airCtr.GetAirportByID(v.EdgeToUse.VertexEdge.to).name
                    + " Price: " + v.EdgeToUse.VertexEdge.price + "kr. Traveltime: " + v.EdgeToUse.VertexEdge.traveltime);
                time += (double) v.EdgeToUse.VertexEdge.traveltime;
                price += (double) v.EdgeToUse.VertexEdge.price;
            }
            Console.WriteLine("");
            Console.WriteLine("Total traveltime: " + time);
            Console.WriteLine("Total price: " + price);
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("Hit enter to see from Aalborg to London -  cheapest price");
            Console.ReadLine();
            Dijkstra dijkstra2 = new Dijkstra();
            var shortestpath2 = dijkstra2.RunDijkstra(airCtr.GetAirportByID(1), airCtr.GetAirportByID(4), "17/11/2014", true);
            double time2 = 0;
            double price2 = 0;
            foreach (var v2 in shortestpath2)
            {
                Console.WriteLine(v2.EdgeToUse.VertexEdge.flightID + " from: " + airCtr.GetAirportByID(v2.EdgeToUse.VertexEdge.from).name + " to: " + airCtr.GetAirportByID(v2.EdgeToUse.VertexEdge.to).name
                    + " Price: " + v2.EdgeToUse.VertexEdge.price + "kr. Traveltime: " + v2.EdgeToUse.VertexEdge.traveltime);
                time2 += (double)v2.EdgeToUse.VertexEdge.traveltime;
                price2 += (double)v2.EdgeToUse.VertexEdge.price;
            }
            Console.WriteLine("");
            Console.WriteLine("Total traveltime: " + time2);
            Console.WriteLine("Total price: " + price2);
            Console.ReadLine();
            Console.ReadLine();

        }
    }
}
