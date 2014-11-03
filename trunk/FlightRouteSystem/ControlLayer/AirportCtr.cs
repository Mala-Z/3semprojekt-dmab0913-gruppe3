using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;


namespace ControlLayer
{
    public class AirportCtr
    {
        /// <summary>
        /// Get all Airports
        /// </summary>
        /// <returns>Returns a list of all Airport objects</returns>
        public List<Airport> GetAllAirports()
        {
            var db = DBConnection.GetInstance().GetConnection();

            var airports = db.Airports.OrderBy(x => x.airportID);

            return airports.ToList();
        }

    }
}
