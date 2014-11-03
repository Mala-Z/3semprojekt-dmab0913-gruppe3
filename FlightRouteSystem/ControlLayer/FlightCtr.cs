using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class FlightCtr
    {     
        /// <summary>
        /// Get all Flights
        /// </summary>
        /// <returns>Returns a list of all Flight objects</returns>
        public List<Flight> GetAllFlights()
        {
            var db = DBConnection.GetInstance().GetConnection();

            var flights = db.Flights.OrderBy(x => x.flightID);

            return flights.ToList();
        }

        /// <summary>
        /// Get all Flights flying from a specific date
        /// </summary>
        /// <returns>Returns a list of all Flight objects who contains depTime</returns>
        public List<Flight> GetFlightsByDate(Airport start, string date)
        {
            var db = DBConnection.GetInstance().GetConnection();

            var flights = db.Flights.Where(x => x.timeOfDeparture.Contains(date) && x.Airport.Equals(start)).OrderBy(x => x.flightID);

            return flights.ToList();
        }

        /// <summary>
        /// Get all Flights flying from a specific Airport
        /// </summary>
        /// <returns>Returns a list of all Flight objects who contains depTime</returns>
        public List<Flight> GetFlightsFrom(Airport start)
        {
            var db = DBConnection.GetInstance().GetConnection();

            var flights = db.Flights.Where(x => x.Airport.Equals(start)).OrderBy(x => x.flightID);

            return flights.ToList();
        }

    }
}
