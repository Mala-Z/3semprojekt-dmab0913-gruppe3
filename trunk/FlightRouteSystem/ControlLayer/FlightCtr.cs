using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class FlightCtr
    {     

        public List<Flight> GetAllFlights()
        {
            var db = DBConnection.GetInstance().GetConnection();

            var flights = db.Flights.OrderBy(x => x.flightID);

            return flights.ToList();
        }

        public List<Flight> GetFlightsByDate(string depTime)
        {
            var db = DBConnection.GetInstance().GetConnection();

            var flights = db.Flights.Where(x => x.timeOfDeparture.Contains(depTime)).OrderBy(x => x.flightID);

            return flights.ToList();
        }

    }
}
