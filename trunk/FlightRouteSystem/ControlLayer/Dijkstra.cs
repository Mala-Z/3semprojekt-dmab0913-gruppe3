using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
namespace ControlLayer
{
    public class Dijkstra
    {
        private AirportCtr airportCtr = new AirportCtr(); // vertices
        private FlightCtr flightCtr = new FlightCtr();
        private GraphCtr graphCtr;

        private Dictionary<Airport, double> dist = new Dictionary<Airport, double>();
        private Dictionary<Airport, Airport> path = new Dictionary<Airport, Airport>();
        //the final path
        private List<Airport> shortestPath = new List<Airport>();

        //Queue for the vertices to beevaluated
        private List<Airport> airportQueue = new List<Airport>();

        //Start airport
        private Airport startAirport;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="len"></param>
        private void Initialize(Airport start, string date)
        {
            graphCtr = new GraphCtr(airportCtr.GetAllAirports());

            // Set distance to all vertices to infinity
            for (int i = 0; i < airportCtr.GetAllAirports().Count; i++)
            {
                dist[graphCtr.airports[i]] = Double.PositiveInfinity;

                airportQueue.Add(graphCtr.airports[i]);
            }

            /* Set distance to 0 for starting point and the previous node to null (-1) */
            dist[start] = 0;
            path[start] = default(Airport);

            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Airport GetNextVertex()
        {
            double min = Double.PositiveInfinity;
            Airport Vertex = default(Airport);

            /* Search through queue to find the next node having the smallest distance */
            foreach (Airport airport in airportQueue)
            {
                if (dist[airport] <= min)
                {
                    min = dist[airport];
                    Vertex = airport;
                }
            }

            airportQueue.Remove(Vertex);

            return Vertex;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startAirport"></param>
        public Dijkstra(GraphCtr graphCtr, Airport startAirport, string date)
        {
            this.graphCtr = graphCtr;
            this.startAirport = startAirport;

            Initialize(startAirport, date);
            //Add edges
            foreach (Flight flight in flightCtr.GetFlightsByDate(date))
            {
                graphCtr.Add(flight.Airport, flight.Airport1, Convert.ToDouble(flight.traveltime));
            }

            while (airportQueue.Count > 0)
            {
                Airport from = GetNextVertex();

                /* Find the nodes that u connects to and perform relax */
                for (int vi = 0; vi < airportQueue.Count; vi++)
                {
                    Airport to = graphCtr.airports[vi];
                    /* Checks for edges with negative weight */
                    if (graphCtr.Cost(from, to) < 0)
                    {
                        throw new ArgumentException("Graph contains negative edge(s)");
                    }

                    /* Check for an edge between u and v */
                    if (graphCtr.Cost(from, to) > 0)
                    {
                        /* Edge exists, relax the edge */
                        if (dist[to] > dist[from] + graphCtr.Cost(from, to))
                        {
                            dist[to] = dist[from] + graphCtr.Cost(from, to);
                            path[to] = from;
                        }
                    }
                }
            }
        }

        public ObservableCollection<Airport> ShortestPath(Airport from, Airport to1)
        {
            if (!IsNullable(from))
            {
                Debug.WriteLine("Warning, type <" + typeof(Airport).ToString() + "> is not nullable");
            }
            var result = new ObservableCollection<Airport>();
            Airport to = to1;
            var shortest_path = new List<Airport>();
            
            while (!EqualityComparer<Airport>.Default.Equals(to, default(Airport)))
            {
                shortest_path.Add(to);
                to = path[to];
            }
            shortest_path.Reverse();

            shortest_path.ForEach(x => result.Add(x));

            return result;
        }

        static bool IsNullable(Airport obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(Airport);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }
    }
}
