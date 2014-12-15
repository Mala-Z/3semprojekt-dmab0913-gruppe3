using System;
using System.Collections.Generic;
using System.ServiceModel;
using ControlLayer;
using DatabaseLayer;

namespace FlightService
{
    [ServiceBehavior(TransactionIsolationLevel = System.Transactions.IsolationLevel.Serializable, TransactionTimeout = "00:00:30")]
    public class FlightService : IFlightService
    {
        private static readonly MainCtr Main = new MainCtr();
        private static readonly AirplaneCtr AirplaneCtr = Main.AirplaneCtr;
        private static readonly AirportCtr AirportCtr = Main.AirportCtr;
        private static readonly BookingCtr BookingCtr = Main.BookingCtr;
        private static readonly FlightCtr FlightCtr = Main.FlightCtr;
        private static readonly PersonCtr PersonCtr = Main.PersonCtr;
        private static readonly DijkstraCtr Dijkstra = new DijkstraCtr();
        private static readonly DijkstraCtr Dijkstra2 = new DijkstraCtr();

        #region Airplane OperationContracts
        public List<Airplane> GetAllAirplanes()
        {
            return AirplaneCtr.GetAllAirplanes();
        }


        public bool CreateNewAirplane(int seats)
        {
            return AirplaneCtr.CreateNewAirplane(seats);
        }

        public Airplane GetAirplaneByID(int id)
        {
            return AirplaneCtr.GetAirplaneByID(id);
        }

        public bool UpdateAirplane(int id, int seats)
        {
            return AirplaneCtr.UpdateAirplane(id, seats);
        }

        public bool DeleteAirplane(int id)
        {
            return AirplaneCtr.DeleteAirplane(id);
        }
        #endregion

        #region Airport OperationContracts

        public List<Airport> GetAllAirports()
        {
            return AirportCtr.GetAllAirports();
        }

        public Airport GetAirportByID(int id)
        {
            return AirportCtr.GetAirportByID(id);
        }

        public bool CreateNewAirport(string name, string location)
        {
            return AirportCtr.CreateNewAirport(name, location);
        }

        public bool UpdateAirport(int id, string name, string location)
        {
            return AirportCtr.UpdateAirport(id, name, location);
        }

        public bool DeleteAirport(int id)
        {
            return AirportCtr.DeleteAirport(id);
        }
        #endregion 

        #region Booking OperationContracts

        public List<Booking> GetAllBookings()
        {
            return BookingCtr.GetAllBookings();
        }

        public Booking GetBookingByID(int id)
        {
            return BookingCtr.GetBookingByID(id);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public bool CreateNewBooking(int[] flightIDs, int[] personIDs, string totalTime, double totalPrice)
        {
            return BookingCtr.CreateNewBooking(flightIDs, personIDs, totalTime, totalPrice);
        }

        public bool UpdateBooking(int id, string totalTime, double totalPrice)
        {
            return BookingCtr.UpdateBooking(id, totalTime, totalPrice);
        }

        public bool DeleteBooking(int id)
        {
            return BookingCtr.DeleteBooking(id);
        }

        public IEnumerable<BookingPassenger> GetBookingPassengers(int bookingId)
        {
            return BookingCtr.GetBookingPassenger(bookingId);
        }

        public IEnumerable<BookingFlight> GetBookingFlights(int bookingId)
        {
            return BookingCtr.GetBookingFlights(bookingId);
        }

        public IEnumerable<Person> GetPersonsFromBooking(int bookingId)
        {
            return PersonCtr.GetPersonsFromBooking(bookingId);
        }

        public IEnumerable<Flight> GetFlightsFromBooking(int bookingId)
        {
            return FlightCtr.GetFlightsFromBooking(bookingId);
        } 
        #endregion

        #region Dijkstra OperationContracts

        public List<Flight> RunDijkstra(Airport from, Airport to, string date, bool usePrice)
        {
            return Dijkstra.Run(from, to, date, usePrice); 
        }

        public List<Flight> RunDijkstraCheapest(Airport from, Airport to, string date)
        {
            return Dijkstra.RunDikjstraCheapest(from, to, date);
        }

        public List<Flight> RunDijkstraFastest(Airport from, Airport to, string date)
        {
            return Dijkstra2.RunDikjstraFastest(from, to, date);
        }
         
        #endregion

        #region Flight OperationContracts
        public List<Flight> GetAllFlights()
        {
            return FlightCtr.GetAllFlights();
        }

        public List<Flight> GetAllFlightsByDate(DateTime fromDate)
        {
            return FlightCtr.GetAllFlightsByDate(fromDate);
        }

        public Flight GetFlightByID(int id)
        {
            return FlightCtr.GetFlightByID(id);
        }

        public List<Flight> GetFlightsByDate(string date)
        {
            return FlightCtr.GetFlightsByDate(date);
        }

        public List<Flight> GetFlightsFrom(Airport start, string date)
        {
            return FlightCtr.GetFlightsFrom(start, date);
        }

        public bool CreateNewFlight(string timeOfDepature, string timeOfArrival, double travelTime, double price, int from, int to, int airplaneID, int takenSeats)
        {
            return FlightCtr.CreateNewFlight(timeOfDepature, timeOfArrival, travelTime, price, from, to, airplaneID, takenSeats);
        }

        public bool UpdateFlight(int id, string timeOfDepature, string timeOfArrival, double travelTime, double price, int from, int to, int airplaneID, int takenSeats)
        {
            return FlightCtr.UpdateFlight(id, timeOfDepature, timeOfArrival, travelTime, price, from, to, airplaneID, takenSeats);
        }

        public bool DeleteFlight(int id)
        {
            return FlightCtr.DeleteFlight(id);
        }
        #endregion

        #region Person OperationContracts
        public List<Person> GetAllPersons()
        {
            return PersonCtr.GetAllPersons();
        }

        public Person GetPersonByID(int id)
        {
            return PersonCtr.GetPersonByID(id);
        }

        public bool CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo, string email, string birthdate)
        {
            return PersonCtr.CreateNewPerson(fName, lName, gender, address, phoneNo, email, birthdate);
        }

        public Person CreateNewPersonBooking(string fName, string lName)
        {
            return PersonCtr.CreateNewPersonBooking(fName, lName);
        }

        public Person CreateNewPersonBookingFull(string fName, string lName, string gender, string address,
            string phoneNo, string email)
        {
            return PersonCtr.CreateNewPersonBookingFull(fName, lName, gender, address, phoneNo, email);
        }

        public bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo, string email, string birthdate)
        {
            return PersonCtr.UpdatePerson(id, fName, lName, gender, address, phoneNo, email, birthdate);
        }

        public bool DeletePerson(int id)
        {
            return PersonCtr.DeletePerson(id);
        }
        #endregion
    }
}
