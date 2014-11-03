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
        public List<Airport> GetAllAirports()
        {
            var db = DBConnection.GetInstance().GetConnection();

            var airports = db.Airports.OrderBy(x => x.airportID);

            return airports.ToList();
        }

    }
}
