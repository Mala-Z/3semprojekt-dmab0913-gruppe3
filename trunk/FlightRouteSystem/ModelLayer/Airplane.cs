using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Airplane
    {
        private int airplaneID;
        private string name;
        private int seats;

        public Airplane(string name, int seats)
        {
            this.Name = name;
            this.Seats = seats;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Seats
        {
            get { return seats; }
            set { seats = value; }
        }

        public int AirplaneID
        {
            get { return airplaneID; }
            set { airplaneID = value; }
        }
    }
}
