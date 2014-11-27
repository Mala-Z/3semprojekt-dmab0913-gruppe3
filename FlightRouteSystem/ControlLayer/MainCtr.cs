using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

namespace ControlLayer
{
    public class MainCtr
    {
        private dmab0913_3DataContext db;
        public AirplaneCtr AirplaneCtr { get; set; }
        public AirportCtr AirportCtr { get; set; }
        public BookingCtr BookingCtr { get; set; }
        public FlightCtr FlightCtr { get; set; }
        public PersonCtr PersonCtr { get; set; }

        public MainCtr()
        {
            this.db = new dmab0913_3DataContext();
            this.AirplaneCtr = new AirplaneCtr(db);
            this.AirportCtr = new AirportCtr(db);
            this.BookingCtr = new BookingCtr(db);
            this.FlightCtr = new FlightCtr(db);
            this.PersonCtr = new PersonCtr(db);
        }

    }
}
