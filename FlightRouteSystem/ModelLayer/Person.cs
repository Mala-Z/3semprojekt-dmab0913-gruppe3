using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Person
    {
        private int personID;
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
            this.FName = fName;
            this.LName = lName;
            this.Gender = gender;
            this.Address = address;
            this.PhoneNo = phoneNo;
            this.Birthdate = birthdate;
            this.Email = email;
            this.Password = password;
            this.IsEmployee = isEmployee;
        }

        /// <summary>
        /// Constructor for Person as a passenger
        /// </summary>
        public Person(string fName, string lName, string gender, string address, string phoneNo, string birthdate, string email)
        {
            this.FName = fName;
            this.LName = lName;
            this.Gender = gender;
            this.Address = address;
            this.PhoneNo = phoneNo;
            this.Birthdate = birthdate;
            this.Email = email;
            this.IsEmployee = false;
        }


        public string FName
        {
            get { return fName; }
            set { fName = value; }
        }

        public string LName
        {
            get { return lName; }
            set { lName = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        public string Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool IsEmployee
        {
            get { return isEmployee; }
            set { isEmployee = value; }
        }

        public int PersonID
        {
            get { return personID; }
            set { personID = value; }
        }
    }
}
