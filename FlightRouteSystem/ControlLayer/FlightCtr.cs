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
        //private DBConnection dbConn = DBConnection.GetInstance();
        

        public List<Flight> getFlights()
        {
            dmab0913_3DataContext db = DBConnection.GetInstance().GetConnection();

            var result = from p in db.Persons
                         where p.gender == "m"
                         orderby p.fname
                         select p;
        }

    }
}
