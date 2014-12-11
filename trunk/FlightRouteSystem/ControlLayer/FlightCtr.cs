 using System;
using System.Collections.Generic;
 using System.Data.SqlClient;
 using System.Diagnostics;
 using System.Globalization;
 using System.Linq;
 using System.Net.Sockets;
 using System.Reflection;
 using System.Runtime.CompilerServices;
 using System.Runtime.Serialization;
 using System.Text;
 using System.Threading;
 using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    [DataContract]
    public class FlightCtr
    {
        private dmab0913_3DataContext db;

        public FlightCtr(dmab0913_3DataContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Get all Flights
        /// </summary>
        /// <returns>Returns a list of all Flight objects</returns>
        public List<Flight> GetAllFlights()
        {
            var flights = db.Flights.OrderBy(x => x.flightID).ToList();

            return flights;
        }

        public List<Flight> GetAllFlightsByDate(DateTime fromDate)
        {
            //string date1 = fromDate.ToString("dd'/'MM'/'yyyy");
            //Debug.WriteLine(date1);
            DateTime nextDay = fromDate.AddDays(1);
            //string date2 = nextDay.ToString("dd'/'MM'/'yyyy");
            //Debug.WriteLine(date2);

            var flights = from f in db.Flights
                          where (f.timeOfArrival.Contains(fromDate.ToShortDateString()) || f.timeOfArrival.Contains(nextDay.ToShortDateString()))
                        select f;
            return flights.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight GetFlightByID(int id)
        {
            var flight = db.Flights.SingleOrDefault(f => f.flightID == id);

            return flight;
        }

        /// <summary>
        /// Get all Flights flying from a specific date
        /// </summary>
        /// <returns>Returns a list of all Flight objects who contains depTime</returns>
        public List<Flight> GetFlightsByDate(string date)
        {
            var flights = db.Flights.Where(x => x.timeOfDeparture.Contains(date)).OrderBy(x => x.flightID).ToList();

            return flights;
        }

        /// <summary>
        /// Get all Flights flying from a specific Airport
        /// </summary>
        /// <returns>Returns a list of all Flight objects who contains depTime</returns>
        public List<Flight> GetFlightsFrom(Airport start, string date)
        {
            var flights = db.Flights.Where(x => x.Airport.Equals(start) && x.timeOfDeparture.Contains(date)).OrderBy(x => x.flightID).ToList();

            return flights;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeOfDepature"></param>
        /// <param name="timeOfArrival"></param>
        /// <param name="travelTime"></param>
        /// <param name="price"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="airplaneID"></param>
        /// <param name="takenSeats"></param>
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

            db.Flights.InsertOnSubmit(flight);

            try
            {
                db.SubmitChanges();
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeOfDepature"></param>
        /// <param name="timeOfArrival"></param>
        /// <param name="travelTime"></param>
        /// <param name="price"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="airplaneID"></param>
        /// <param name="takenSeats"></param>
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
                    db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteFlight(int id)
        {
            bool returnValue = true;
            var flight = GetFlightByID(id);

            if (flight != null)
            {
                db.Flights.DeleteOnSubmit(flight);
                try
                {
                    db.SubmitChanges();
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
            BookingCtr bookingCtr = new BookingCtr(db);
            List<Flight> flights = new List<Flight>();
            bookingCtr.GetBookingFlights(bookingId).ToList().ForEach(bf => flights.Add(GetFlightByID(bf.flightID)));
            return flights;
        }

    }
}
