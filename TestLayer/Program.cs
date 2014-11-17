using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLayer;


namespace TestLayer
{
    class Program
    {
        private static GraphCtr graph = new GraphCtr();
        static void Main(string[] args)
        {
            Console.WriteLine("Click to add all vertices");
            Console.ReadLine();
            graph.AddAllVertices("10/10/2015");
            Console.WriteLine("All vertices added");
            Console.WriteLine();
            Console.WriteLine("Click to add all edges");
            Console.ReadLine();
            graph.AddAllEdges();
            Console.ReadLine();

        }
    }
}
