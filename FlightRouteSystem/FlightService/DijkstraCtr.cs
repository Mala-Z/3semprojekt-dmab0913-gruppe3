using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using GraphLayer;

namespace FlightService
{
    public class DijkstraCtr
    {
        private Dijkstra dijk;
        public DijkstraCtr()
        {
            dijk = new Dijkstra();
        }

        public List<Flight> runDikjstra(Airport from, Airport to, string date, bool usePrice)
        {

            List<Flight> flights = new List<Flight>();

            foreach (var v in dijk.RunDijkstra(from, to, date, usePrice))
            {
                flights.Add(v.EdgeToUse.VertexEdge);
            }

            return flights;
        }

        public List<Flight> RunDikjstraCheapest(Airport from, Airport to, string date)
        {
            List<Flight> fList = new List<Flight>();
            fList.Clear();
            fList = dijk.RunDijkstra(@from, to, date, true).Select(v => v.EdgeToUse.VertexEdge).ToList();
            return fList;
        }

        public List<Flight> RunDikjstraFastest(Airport from, Airport to, string date)
        {
            List<Flight> fList = new List<Flight>();
            fList.Clear();
            fList = dijk.RunDijkstra(@from, to, date, false).Select(v => v.EdgeToUse.VertexEdge).ToList();
            return fList;
        }
    }

    
}
