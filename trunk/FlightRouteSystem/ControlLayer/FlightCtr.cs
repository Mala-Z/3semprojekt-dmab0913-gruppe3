﻿ using System;
using System.Collections.Generic;
using System.Linq;
 using System.Net.Sockets;
 using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
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

        public List<Flight> GetAllFlightsByDate(DateTime fromDate, DateTime toDate)
        {
            //string fromD = fromDate.ToString("dd/MM/yy");
            //string toD = toDate.ToString("dd/MM/yy");

            var flights = from f in db.Flights
                          orderby f.airplaneID descending 
                //where (f.timeOfArrival >= fromD && f.timeOfArrival <= toD)
                         // where (DateTime.Parse(f.timeOfArrival) >= fromDate && DateTime.Parse(f.timeOfArrival) <= toDate)
                        select f;
            return flights.Take(50).ToList();
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
        public void CreateNewFlight(string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats)
        {
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
            db.SubmitChanges();

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
        public void UpdateFlight(int id, string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats)
        {
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

                db.Flights.InsertOnSubmit(flight);
                db.SubmitChanges();
            }  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFlight(int id)
        {
            var flight = GetFlightByID(id);

            if (flight != null)
            {
                db.Flights.DeleteOnSubmit(flight);
                db.SubmitChanges();
            }
        }

    }
}
