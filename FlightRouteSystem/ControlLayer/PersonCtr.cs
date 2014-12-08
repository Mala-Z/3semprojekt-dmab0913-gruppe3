using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;


namespace ControlLayer
{
    public class PersonCtr
    {
        private dmab0913_3DataContext db;

        public PersonCtr(dmab0913_3DataContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Get all Persons
        /// </summary>
        /// <returns>Returns a list of all Person objects</returns>
        public List<Person> GetAllPersons()
        {
            var persons = db.Persons.OrderBy(x => x.personID).ToList();

            return persons;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetPersonByID(int id)
        {
            var person = db.Persons.SingleOrDefault(a => a.personID == id);

            return person;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="gender"></param>
        /// <param name="address"></param>
        /// <param name="phoneNo"></param>
        /// <param name="email"></param>
        /// <param name="birthdate"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        public bool CreateNewPerson(string fName, string lName, string gender, string address, string phoneNo,
                                    string email, string birthdate)
        {
            bool returnValue = true;
            var person = new Person
            {
                fname = fName,
                lname = lName,
                gender = gender,
                address = address,
                phoneNo = phoneNo,
                email = email,
                birthdate = birthdate
            };

            db.Persons.InsertOnSubmit(person);
            try
            {
                db.SubmitChanges();
            }
            catch (SqlException)
            {
                returnValue = false;
            }

            return returnValue;
        }

        public Person CreateNewPersonBooking(string fName, string lName)
        {
            var person = new Person
            {
                fname = fName,
                lname = lName
            };

            db.Persons.InsertOnSubmit(person);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                
                Debug.Write(e.InnerException);
            }
            
            return person;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="gender"></param>
        /// <param name="address"></param>
        /// <param name="phoneNo"></param>
        /// <param name="email"></param>
        /// <param name="birthdate"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        public bool UpdatePerson(int id, string fName, string lName, string gender, string address, string phoneNo, 
                                 string email, string birthdate)
        {
            bool returnValue = true;

            var person = GetPersonByID(id);

            if (person != null)
            {
                person.fname = fName;
                person.lname = lName;
                person.gender = gender;
                person.address = address;
                person.phoneNo = phoneNo;
                person.email = email;
                person.birthdate = birthdate;

                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public bool DeletePerson(int id)
        {
            bool returnValue = true;
            var person = GetPersonByID(id);
            if (person != null)
            {
                db.Persons.DeleteOnSubmit(person);
                try
                {
                    db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
    }
}
