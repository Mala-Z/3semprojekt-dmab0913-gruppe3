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

            Console.WriteLine("Hit enter torun dijkstra");
            Console.ReadLine();
            Dijkstra dijkstra = new Dijkstra();
            var shortestpath = dijkstra.Test(airCtr.GetAirportByID(1), airCtr.GetAirportByID(7), "17/11/2014");
            foreach (var v in shortestpath)
            {
                Console.WriteLine(v.EdgeToUse.VertexEdge.flightID + " from: " + airCtr.GetAirportByID(v.EdgeToUse.VertexEdge.from).name + " to: " + airCtr.GetAirportByID(v.EdgeToUse.VertexEdge.to).name
                    + " Price: " + v.EdgeToUse.VertexEdge.price + "kr. Traveltime: " + v.EdgeToUse.VertexEdge.traveltime);
            }
            Console.ReadLine();
            Console.ReadLine();

        }
    }
}
