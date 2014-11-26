﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ControlLayer;
using DatabaseLayer;

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

        public List<Flight> GetAllFlights()
        {
            return flightCtr.GetAllFlights();
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

        public List<Person> GetAllPersons()
        {
            return personCtr.GetAllPersons();
        }

        public Person GetPersonByID(int id)
        {
            return personCtr.GetPersonByID(id);
        }

        public void CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo, string email, string birthdate, string password, int type)
        {
            personCtr.CreateNewPerson(fName, lName, gender, address, phoneNo, email, birthdate, password, type);
        }

        public bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo, string email, string birthdate, string password, int type)
        {
            return personCtr.UpdatePerson(id, fName, lName, gender, address, phoneNo, email, birthdate, password, type);
        }

        public bool DeletePerson(int id)
        {
            return personCtr.DeletePerson(id);
        }
    }
}
