using System;
using System.Collections.Generic;
using System.ServiceModel;
using DatabaseLayer;

namespace FlightService
{
    [ServiceContract]
    public interface IFlightService
    {

        #region Airplane OperationContracts
        [OperationContract]
        List<Airplane> GetAllAirplanes();

        [OperationContract]
        bool CreateNewAirplane(int seats);

        [OperationContract]
        Airplane GetAirplaneByID(int id);

        [OperationContract]
        bool UpdateAirplane(int id, int seats);

        [OperationContract]
        bool DeleteAirplane(int id);
        #endregion

        #region Airport OperationContracts

        [OperationContract]
        List<Airport> GetAllAirports();

        [OperationContract]
        Airport GetAirportByID(int id);

        [OperationContract]
        bool CreateNewAirport(string name, string location);

        [OperationContract]
        bool UpdateAirport(int id, string name, string location);

        [OperationContract]
        bool DeleteAirport(int id);
        #endregion

        #region Booking OperationContracts

        [OperationContract]
        List<Booking> GetAllBookings();

        [OperationContract]
        Booking GetBookingByID(int id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool CreateNewBooking(int[] flightIDs, int[] personIDs, string totalTime, double totalPrice);

        [OperationContract]
        bool UpdateBooking(int id, string totalTime, double totalPrice);

        [OperationContract]
        bool DeleteBooking(int id);

        [OperationContract]
        IEnumerable<BookingPassenger> GetBookingPassengers(int bookingId);

        [OperationContract]
        IEnumerable<BookingFlight> GetBookingFlights(int bookingId);

        [OperationContract]
        IEnumerable<Person> GetPersonsFromBooking(int bookingId);

        [OperationContract]
        IEnumerable<Flight> GetFlightsFromBooking(int bookingId);


            #endregion

        #region Dijkstra OperationContracts

        [OperationContract]
        List<Flight> RunDijkstra(Airport from, Airport to, string date, bool usePrice);

        [OperationContract]
        List<Flight> RunDijkstraCheapest(Airport from, Airport to, string date);

        [OperationContract]
        List<Flight> RunDijkstraFastest(Airport from, Airport to, string date);

        #endregion

        #region Flight OperationContracts
        [OperationContract]
        List<Flight> GetAllFlights();

        [OperationContract]
        Flight GetFlightByID(int id);

        [OperationContract]
        List<Flight> GetAllFlightsByDate(DateTime fromDate);

        [OperationContract]
        List<Flight> GetFlightsByDate(string date);

        [OperationContract]
        List<Flight> GetFlightsFrom(Airport start, string date);

        [OperationContract]
        bool CreateNewFlight(string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats);

        [OperationContract]
        bool UpdateFlight(int id, string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats);

        [OperationContract]
        bool DeleteFlight(int id);
        #endregion

        #region Person OperationContracts
        [OperationContract]
        List<Person> GetAllPersons();

        [OperationContract]
        Person GetPersonByID(int id);

        [OperationContract]
        bool CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo,
            string email, string birthdate);

        [OperationContract]
        Person CreateNewPersonBookingFull(string fName, string lName, string gender, string address,
            string phoneNo, string email);

        [OperationContract]
        Person CreateNewPersonBooking(string fName, string lName);

        [OperationContract]
        bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo,
            string email, string birthdate);

        [OperationContract]
        bool DeletePerson(int id);
        #endregion
    }
}
