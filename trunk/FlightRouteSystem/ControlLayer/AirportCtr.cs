using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DatabaseLayer;


namespace ControlLayer
{
    public class AirportCtr
    {
        private readonly dmab0913_3DataContext _db;

        public AirportCtr(dmab0913_3DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get all Airports
        /// </summary>
        /// <returns>Returns a list of all Airport objects</returns>
        public List<Airport> GetAllAirports()
        {
            var airports = _db.Airports.OrderBy(x => x.airportID).ToList();

            return airports;
        }

        public Airport GetAirportByID(int id)
        {
            var airport = _db.Airports.SingleOrDefault(a => a.airportID == id);

            return airport;
        }

        public bool CreateNewAirport(string name, string location)
        {
            bool returnValue = true;
            var airport = new Airport {name = name, location = location};

            _db.Airports.InsertOnSubmit(airport);
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

        public bool UpdateAirport(int id, string name, string location)
        {
            bool returnValue = true;
            var airport = GetAirportByID(id);

            if (airport != null)
            {
                airport.name = name;
                airport.location = location;

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

        public bool DeleteAirport(int id)
        {
            bool returnValue = true;
            var airport = GetAirportByID(id);
            if (airport != null)
            {
                _db.Airports.DeleteOnSubmit(airport);
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
    }
}
