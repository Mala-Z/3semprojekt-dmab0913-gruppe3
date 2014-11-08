using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class GraphCtr
    {
        private AirportCtr airportCtr = new AirportCtr(); // vertices
        private FlightCtr flightCtr = new FlightCtr(); //edges

        private Dictionary<Airport[], double> edges;
        public List<Airport> airports { get; private set; }

        //public int Length { get { return airports.Count; } }

        /// <summary>
        /// Opretter grafen med alle Airport objekter
        /// </summary>
        /// <param name="airports">List of all Airports</param>
        public GraphCtr(List<Airport> airports)
        {
            edges = new Dictionary<Airport[], double>();
            this.airports = airports;
        }

        /// <summary>
        /// Tilføjer edges til grafen
        /// </summary>
        /// <param name="from">Airport object vi flyver fra</param>
        /// <param name="to">Airport object vi flyver til</param>
        /// <param name="cost">Prisen mellem de to vertices (her travelTime)</param>
        public void Add(Airport from, Airport to, double cost)
        {
            if (!airports.Contains(from) || !airports.Contains(to))
                throw new Exception("Invalid node");

            edges.Add(new[] { from, to }, cost);
        }

        /// <summary>
        /// Hvis der er en edge mellem lufthavn from til lufthavn to, returner prisen (her travlTime)
        /// </summary>
        /// <param name="from">Airport object vi flyver fra</param>
        /// <param name="to">Airport object vi flyver til</param>
        /// <returns>Prisen (her travelTime)</returns>
        public double Cost(Airport from, Airport to)
        {
            double cost = 0;
            foreach (Airport[] key in edges.Keys)
            {
                if (key[0].airportID.Equals(from.airportID) && key[1].airportID.Equals(to.airportID))
                {
                    cost = edges[key];
                    Console.WriteLine(key[0].name + " til " + key[1].name + " time: " + cost);
                }
                //throw new Exception("Edge not found");
            }

            return cost;

        }
    }
}