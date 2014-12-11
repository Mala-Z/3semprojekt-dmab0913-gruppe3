 using System;
 using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Linq;
 using System.Runtime.Serialization;
 using DatabaseLayer;

namespace ControlLayer
{
    [DataContract]
    public class FlightCtr
    {
        private readonly dmab0913_3DataContext _db;

        public FlightCtr(dmab0913_3DataContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Get all Flights
        /// </summary>
        /// <returns>Returns a list of all Flight objects</returns>
        public List<Flight> GetAllFlights()
        {
            var flights = _db.Flights.OrderBy(x => x.flightID).ToList();

            return flights;
        }

        public List<Flight> GetAllFlightsByDate(DateTime fromDate)
        {
            DateTime nextDay = fromDate.AddDays(1);
            var flights = from f in _db.Flights
                          where (f.timeOfArrival.Contains(fromDate.ToShortDateString()) || f.timeOfArrival.Contains(nextDay.ToShortDateString()))
                          select f;
            return flights.ToList();
        }

        public Flight GetFlightByID(int id)
        {
            var flight = _db.Flights.SingleOrDefault(f => f.flightID == id);
            return flight;
        }

        /// <summary>
        /// Get all Flights flying from a specific date
        /// </summary>
        /// <returns>Returns a list of all Flight objects who contains depTime</returns>
        public List<Flight> GetFlightsByDate(string date)
        {
            var flights = _db.Flights.Where(x => x.timeOfDeparture.Contains(date)).OrderBy(x => x.flightID).ToList();
            return flights;
        }

        /// <summary>
        /// Get all Flights flying from a specific Airport
        /// </summary>
        /// <returns>Returns a list of all Flight objects who contains depTime</returns>
        public List<Flight> GetFlightsFrom(Airport start, string date)
        {
            var flights = _db.Flights.Where(x => x.Airport.Equals(start) && x.timeOfDeparture.Contains(date)).OrderBy(x => x.flightID).ToList();
            return flights;
        }

        public bool CreateNewFlight(string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats)
        {
            bool returnValue = true;
            var flight = new Flight
            {
                timeOfDeparture = timeOfDepature,
                timeOfArrival = timeOfArrival,
                traveltime = travelTime,
                price = price,
                @from = @from,
                to = to,
                airplaneID = airplaneID,
                takenSeats = takenSeats
            };

            _db.Flights.InsertOnSubmit(flight);

            try
            {
                _db.SubmitChanges();
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        public bool UpdateFlight(int id, string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats)
        {
            bool returnValue = true;
            var flight = GetFlightByID(id);

            if (flight != null)
            {
                flight.timeOfDeparture = timeOfDepature;
                flight.timeOfArrival = timeOfArrival;
                flight.traveltime = travelTime;
                flight.price = price;
                @flight.from = @from;
                flight.to = to;
                flight.airplaneID = airplaneID;
                flight.takenSeats = takenSeats;

                try
                {
                    _db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public bool DeleteFlight(int id)
        {
            bool returnValue = true;
            var flight = GetFlightByID(id);

            if (flight != null)
            {
                _db.Flights.DeleteOnSubmit(flight);
                try
                {
                    _db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public IEnumerable<Flight> GetFlightsFromBooking(int bookingId)
        {
            MainCtr mainCtr = new MainCtr();
            List<Flight> flights = new List<Flight>();
            mainCtr.BookingCtr.GetBookingFlights(bookingId).ToList().ForEach(bf => flights.Add(GetFlightByID(bf.flightID)));
            return flights;
        }

    }
}
