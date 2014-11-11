using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Airport
    {
        private int airportID;
        private string name;
        private string location;

        public Airport(string name, string location)
        {
            this.Name = name;
            this.Location = location;
        }

        public Airport()
        {

        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public int AirportID
        {
            get { return airportID; }
            set { airportID = value; }
        }
    }
}
