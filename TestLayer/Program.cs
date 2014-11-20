﻿using System;
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
            
            Console.WriteLine("Click to initialize and run dijkstra");
            Dijkstra dijkstra = new Dijkstra();
            var shortestpath = dijkstra.Test(airCtr.GetAirportByID(1), airCtr.GetAirportByID(5), "17/11/2014");
            Console.WriteLine("Click to show path");
            Console.ReadLine();
            Console.WriteLine("From: " + shortestpath.First().EdgeToUse.From.GetAirport().name + " to " );
            foreach (var v in shortestpath)
            {
                Console.WriteLine(v.GetAirport().name + " to");
            }
            Console.ReadLine();
            Console.ReadLine();

        }
    }
}
