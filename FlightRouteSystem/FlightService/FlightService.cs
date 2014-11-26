using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControlLayer;

namespace FlightService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class FlightService : IFlightService
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
