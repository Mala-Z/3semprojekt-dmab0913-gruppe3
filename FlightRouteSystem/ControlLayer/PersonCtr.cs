using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DatabaseLayer;


namespace ControlLayer
{
    public class PersonCtr
    {
        private readonly dmab0913_3DataContext _db;

        public PersonCtr(dmab0913_3DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get all Persons
        /// </summary>
        /// <returns>Returns a list of all Person objects</returns>
        public List<Person> GetAllPersons()
        {
            var persons = _db.Persons.OrderBy(x => x.personID).ToList();
            return persons;
        }

        public Person GetPersonByID(int id)
        {
            var person = _db.Persons.SingleOrDefault(a => a.personID == id);
            return person;
        }

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

            _db.Persons.InsertOnSubmit(person);

            try
            {
                _db.SubmitChanges();
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
           
            return person;
        }

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
                    _db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public bool DeletePerson(int id)
        {
            bool returnValue = true;
            var person = GetPersonByID(id);
            if (person != null)
            {
                _db.Persons.DeleteOnSubmit(person);
                try
                {
                    _db.SubmitChanges();
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public IEnumerable<Person> GetPersonsFromBooking(int bookingId)
        {
            MainCtr mainCtr = new MainCtr();
            List<Person> persons = new List<Person>();
            mainCtr.BookingCtr.GetBookingPassenger(bookingId).ToList().ForEach(bp => persons.Add(GetPersonByID(bp.personID)));
            return persons;
        } 
    }
}
