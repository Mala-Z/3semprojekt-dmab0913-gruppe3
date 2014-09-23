using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Person
    {
        private string fName;
        private string lName;
        private string gender;
        private string address;
        private string phoneNo;
        private string birthdate;
        private string email;
        private string password;
        private bool isEmployee;

        /// <summary>
        /// Constructor for Person with all fields
        /// </summary>
        public Person(string fName, string lName, string gender, string address, string phoneNo, string birthdate, string email, string password, bool isEmployee)
        {
            this.fName = fName;
            this.lName = lName;
            this.gender = gender;
            this.address = address;
            this.phoneNo = phoneNo;
            this.birthdate = birthdate;
            this.email = email;
            this.password = password;
            this.isEmployee = isEmployee;
        }

        /// <summary>
        /// Constructor for Person as a passenger
        /// </summary>
        public Person(string fName, string lName, string gender, string address, string phoneNo, string birthdate, string email)
        {
            this.fName = fName;
            this.lName = lName;
            this.gender = gender;
            this.address = address;
            this.phoneNo = phoneNo;
            this.birthdate = birthdate;
            this.email = email;
            this.isEmployee = false;
        }

    }
}
