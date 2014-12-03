using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using GraphLayer;

namespace FlightService
{
    public class Dijkstra
    {
        public Dijkstra()
        {
            
        }

        public List<Vertex> runDikjstra(Airport from, Airport to, string date, bool usePrice)
        {
            Dijkstra dijk = new Dijkstra();
            return dijk.runDikjstra(from, to, date, usePrice);
        }
        
    }

    
}
