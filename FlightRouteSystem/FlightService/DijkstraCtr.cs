using System.Collections.Generic;
using DatabaseLayer;
using GraphLayer;

namespace FlightService
{
    public class DijkstraCtr
    {
        private readonly Dijkstra _dijkstra;
        public DijkstraCtr()
        {
            _dijkstra = new Dijkstra();
        }

        public List<Flight> Run(Airport from, Airport to, string date, bool usePrice)
        {

            List<Flight> flights = new List<Flight>();

            foreach (var v in _dijkstra.RunDijkstra(from, to, date, usePrice))
            {
                flights.Add(v.EdgeToUse.VertexEdge);
            }

            return flights;
        }

        public List<Flight> RunDikjstraCheapest(Airport from, Airport to, string date)
        {
            List<Flight> fList = new List<Flight>();
            fList.Clear();
            foreach (var v in _dijkstra.RunDijkstra(@from, to, date, true))
            {
                //Hvis der allerede er en Flight med samme ID i listen
                if (!fList.Contains(v.EdgeToUse.VertexEdge))
                {
                    fList.Add(v.EdgeToUse.VertexEdge);
                }
            }
            return fList;
        }

        public List<Flight> RunDikjstraFastest(Airport from, Airport to, string date)
        {
            List<Flight> fList = new List<Flight>();
            foreach (var v in _dijkstra.RunDijkstra(@from, to, date, false))
            {
                //Hvis der allerede er en Flight med samme ID i listen
                if (!fList.Contains(v.EdgeToUse.VertexEdge))
                {
                    fList.Add(v.EdgeToUse.VertexEdge);
                }
            }
            return fList;
        }
    }
}
