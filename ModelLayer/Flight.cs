using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Flight
    {
        private int flightID;
        private string timeOfDeparture;
        private string timeOfArrival;
        private double travelTime;
        private double price;
        private int takenSeats;
        private Airplane airplane;
        private Airport from;
        private Airport to;

        public Flight(string timeOfDeparture, string timeOfArrival, double travelTime, double price, int takenSeats, Airplane airplane, Airport from, Airport to)
        {
            this.TimeOfDeparture = timeOfDeparture;
            this.TimeOfArrival = timeOfArrival;
            this.TravelTime = travelTime;
            this.Price = price;
            this.TakenSeats = takenSeats;
            this.Airplane = airplane;
            this.From = from;
            this.To = to;
        }

        public string TimeOfDeparture
        {
            get { return timeOfDeparture; }
            set { timeOfDeparture = value; }
        }

        public string TimeOfArrival
        {
            get { return timeOfArrival; }
            set { timeOfArrival = value; }
        }

        public double TravelTime
        {
            get { return travelTime; }
            set { travelTime = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int TakenSeats
        {
            get { return takenSeats; }
            set { takenSeats = value; }
        }

        public Airplane Airplane
        {
            get { return airplane; }
            set { airplane = value; }
        }

        public Airport From
        {
            get { return @from; }
            set { @from = value; }
        }

        public Airport To
        {
            get { return to; }
            set { to = value; }
        }

        public int FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }
    }
}
