using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Booking
    {
        private double totalPrice;
        private double totalTime;
        private List<Person> passengerList;
        private List<Flight> flightList;
 

        public Booking(double totalPrice, double totalTime)
        {
            this.totalPrice = totalPrice;
            this.totalTime = totalTime;
            this.flightList = new List<Flight>();
            this.passengerList = new List<Person>();
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public double TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        public List<Flight> FlightList
        {
            get { return flightList; }
        }

        public void AddFlight(Flight flight)
        {
            this.flightList.Add(flight);
        }

        public void RemoveFlight(Flight flight)
        {
            this.flightList.Remove(flight);
        }

        public List<Person> PassengerList
        {
            get { return passengerList; }
        }

        public void AddPassenger(Person person)
        {
            this.passengerList.Add(person);
        }

        public void RemovePassenger(Person person)
        {
            this.passengerList.Remove(person);
        }
    }
}
