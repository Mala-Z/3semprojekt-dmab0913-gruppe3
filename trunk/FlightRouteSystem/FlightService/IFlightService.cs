using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseLayer;

namespace FlightService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFlightService
    {
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

        //Flight
        [OperationContract]
        List<Flight> GetAllFlights();

        [OperationContract]
        Flight GetFlightByID(int id);

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

        //Airport
        [OperationContract]
        Airport GetAirportByID(int id);

        [OperationContract]
        void CreateNewAirport(string name, string location);

        [OperationContract]
        void UpdateAirport(int id, string name, string location);

        [OperationContract]
        void DeleteAirport(int id);

        //Person

        [OperationContract]
        List<Person> GetAllPersons();

        [OperationContract]
        Person GetPersonByID(int id);

        [OperationContract]
        void CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo,
            string email, string birthdate, string password, int type);

        [OperationContract]
        bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo,
            string email, string birthdate, string password, int type);

        [OperationContract]
        bool DeletePerson(int id);

    //// Use a data contract as illustrated in the sample below to add composite types to service operations.
    //// You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "FlightService.ContractType".
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    }
}
