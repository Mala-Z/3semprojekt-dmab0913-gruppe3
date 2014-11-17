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
            
            Console.WriteLine("Click to add all vertices");
            Dijkstra dijkstra = new Dijkstra();
            foreach (Vertex v in dijkstra.Test(airCtr.GetAirportByID(1), airCtr.GetAirportByID(8), "11/11/2014"))
            {
                Console.WriteLine(v.GetAirport().name);
            }
            Console.ReadLine();
            Console.ReadLine();

        }
    }
}
