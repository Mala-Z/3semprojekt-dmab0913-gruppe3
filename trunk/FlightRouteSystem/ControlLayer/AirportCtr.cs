using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;


namespace ControlLayer
{
    public class AirportCtr
    {
        private dmab0913_3DataContext db;

        public AirportCtr(dmab0913_3DataContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Get all Airports
        /// </summary>
        /// <returns>Returns a list of all Airport objects</returns>
        public List<Airport> GetAllAirports()
        {
            var airports = db.Airports.OrderBy(x => x.airportID).ToList();

            return airports;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Airport GetAirportByID(int id)
        {
            var airport = db.Airports.SingleOrDefault(a => a.airportID == id);

            return airport;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        public bool CreateNewAirport(string name, string location)
        {
            bool returnValue = true;
            var airport = new Airport {name = name, location = location};

            db.Airports.InsertOnSubmit(airport);
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
        /// <param name="name"></param>
        /// <param name="location"></param>
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
        public bool DeleteAirport(int id)
        {
            bool returnValue = true;
            var airport = GetAirportByID(id);
            if (airport != null)
            {
                db.Airports.DeleteOnSubmit(airport);
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
    }
}
