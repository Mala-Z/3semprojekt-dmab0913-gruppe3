using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControlLayer;
using DatabaseLayer;
using GraphLayer;

namespace FlightService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class FlightService : IFlightService
    {
        private static MainCtr main = new MainCtr();
        private static AirplaneCtr airplaneCtr = main.AirplaneCtr;
        private static AirportCtr airportCtr = main.AirportCtr;
        private static BookingCtr bookingCtr = main.BookingCtr;
        private static FlightCtr flightCtr = main.FlightCtr;
        private static PersonCtr personCtr = main.PersonCtr;
        private static DijkstraCtr dijkstra = new DijkstraCtr();

        #region Airplane OperationContracts
        public List<Airplane> GetAllAirplanes()
        {
            return airplaneCtr.GetAllAirplanes();
        }


        public bool CreateNewAirplane(int seats)
        {
            return airplaneCtr.CreateNewAirplane(seats);
        }

        public Airplane GetAirplaneByID(int id)
        {
            return airplaneCtr.GetAirplaneByID(id);
        }

        public bool UpdateAirplane(int id, int seats)
        {
            return airplaneCtr.UpdateAirplane(id, seats);
        }

        public bool DeleteAirplane(int id)
        {
            return airplaneCtr.DeleteAirplane(id);
        }
        #endregion

        #region Airport OperationContracts

        public List<Airport> GetAllAirports()
        {
            return airportCtr.GetAllAirports();
        }

        public Airport GetAirportByID(int id)
        {
            return airportCtr.GetAirportByID(id);
        }

        public void CreateNewAirport(string name, string location)
        {
            airportCtr.CreateNewAirport(name, location);
        }

        public void UpdateAirport(int id, string name, string location)
        {
            airportCtr.UpdateAirport(id, name, location);
        }

        public void DeleteAirport(int id)
        {
            airportCtr.DeleteAirport(id);
        }
        #endregion 

        #region Booking OperationContracts

        public List<Booking> GetAllBookings()
        {
            return bookingCtr.GetAllBookings();
        }

        public Booking GetBookingByID(int id)
        {
            return bookingCtr.GetBookingByID(id);
        }

        public bool CreateNewBooking(List<Flight> flights, List<Person> passengers, string totalTime, double totalPrice)
        {
            return bookingCtr.CreateNewBooking(flights, passengers, totalTime, totalPrice);
        }

        public bool UpdateBooking(int id, string totalTime, double totalPrice)
        {
            return bookingCtr.UpdateBooking(id, totalTime, totalPrice);
        }

        public bool DeleteBooking(int id)
        {
            return bookingCtr.DeleteBooking(id);
        }
        #endregion

        #region Dijkstra OperationContracts

        public List<Vertex> RunDijkstra(Airport from, Airport to, string date, bool usePrice)
        {
            return dijkstra.runDikjstra(from, to, date, usePrice); 
        }
         
        #endregion

        #region Flight OperationContracts
        public List<Flight> GetAllFlights()
        {
            return flightCtr.GetAllFlights();
        }

        public List<Flight> GetAllFlightsByDate(DateTime fromDate)
        {
            return flightCtr.GetAllFlightsByDate(fromDate);
        }

        public Flight GetFlightByID(int id)
        {
            return flightCtr.GetFlightByID(id);
        }

        public List<Flight> GetFlightsByDate(string date)
        {
            return flightCtr.GetFlightsByDate(date);
        }

        public List<Flight> GetFlightsFrom(Airport start, string date)
        {
            return flightCtr.GetFlightsFrom(start, date);
        }

        public void CreateNewFlight(string timeOfDepature, string timeOfArrival, double travelTime, double price, int from, int to, int airplaneID, int takenSeats)
        {
            flightCtr.CreateNewFlight(timeOfDepature, timeOfArrival, travelTime, price, from, to, airplaneID, takenSeats);
        }

        public void UpdateFlight(int id, string timeOfDepature, string timeOfArrival, double travelTime, double price, int from, int to, int airplaneID, int takenSeats)
        {
            flightCtr.UpdateFlight(id, timeOfDepature, timeOfArrival, travelTime, price, from, to, airplaneID, takenSeats);
        }

        public void DeleteFlight(int id)
        {
            flightCtr.DeleteFlight(id);
        }
        #endregion

        #region Person OperationContracts
        public List<Person> GetAllPersons()
        {
            return personCtr.GetAllPersons();
        }

        public Person GetPersonByID(int id)
        {
            return personCtr.GetPersonByID(id);
        }

        public void CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo, string email, string birthdate)
        {
            personCtr.CreateNewPerson(fName, lName, gender, address, phoneNo, email, birthdate);
        }

        public bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo, string email, string birthdate)
        {
            return personCtr.UpdatePerson(id, fName, lName, gender, address, phoneNo, email, birthdate);
        }

        public bool DeletePerson(int id)
        {
            return personCtr.DeletePerson(id);
        }
        #endregion
    }
}
