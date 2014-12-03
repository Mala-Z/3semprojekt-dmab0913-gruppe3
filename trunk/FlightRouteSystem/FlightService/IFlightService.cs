using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseLayer;
using GraphLayer;

namespace FlightService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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
        void CreateNewAirport(string name, string location);

        [OperationContract]
        void UpdateAirport(int id, string name, string location);

        [OperationContract]
        void DeleteAirport(int id);
        #endregion

        #region Booking OperationContracts

        [OperationContract]
        List<Booking> GetAllBookings();

        [OperationContract]
        Booking GetBookingByID(int id);

        [OperationContract]
        bool CreateNewBooking(List<Flight> flights, List<Person> passengers, string totalTime, double totalPrice);

        [OperationContract]
        bool UpdateBooking(int id, string totalTime, double totalPrice);

        [OperationContract]
        bool DeleteBooking(int id);


        #endregion

        #region Dijkstra OperationContracts
        /*
        [OperationContract]
        List<Vertex> RunDijkstra(Airport from, Airport to, string date, bool usePrice);
         * */
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
        void CreateNewFlight(string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats);

        [OperationContract]
        void UpdateFlight(int id, string timeOfDepature, string timeOfArrival, double travelTime, double price, int from,
            int to, int airplaneID, int takenSeats);

        [OperationContract]
        void DeleteFlight(int id);
        #endregion

        #region Person OperationContracts
        [OperationContract]
        List<Person> GetAllPersons();

        [OperationContract]
        Person GetPersonByID(int id);

        [OperationContract]
        void CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo,
            string email, string birthdate);

        [OperationContract]
        bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo,
            string email, string birthdate);

        [OperationContract]
        bool DeletePerson(int id);
        #endregion
    }
}
