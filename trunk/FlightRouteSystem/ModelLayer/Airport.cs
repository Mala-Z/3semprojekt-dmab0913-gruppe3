﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Airport
    {
        private string name;
        private string location;

        public Airport(string name, string location)
        {
            this.Name = name;
            this.Location = location;
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
    }
}
