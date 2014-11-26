using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ControlLayer;
using DatabaseLayer;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WCFService : IWCFService
    {
        private static AirplaneCtr airplaneCtr = new AirplaneCtr();
        private static AirportCtr airportCtr = new AirportCtr();
        private static BookingCtr bookingCtr = new BookingCtr();
        private static FlightCtr flightCtr = new FlightCtr();
        private static PersonCtr personCtr = new PersonCtr();

        public List<Airplane> GetAllAirplanes()
        {
            return airplaneCtr.GetAllAirplanes();
        }

    }
}
