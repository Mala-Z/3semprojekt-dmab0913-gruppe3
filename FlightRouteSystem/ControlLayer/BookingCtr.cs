using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DatabaseLayer;


namespace ControlLayer
{
    public class BookingCtr
    {
        private readonly dmab0913_3DataContext _db;

        public BookingCtr(dmab0913_3DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GetAllBookings
        /// </summary>
        /// <returns>List of bookings/Booking></returns>
        public List<Booking> GetAllBookings()
        {
            var bookings = _db.Bookings.OrderBy(x => x.bookingID).ToList();
            return bookings;
        }

        public Booking GetBookingByID(int id)
        {
            var booking = _db.Bookings.SingleOrDefault(a => a.bookingID == id);

            return booking;
        }

        /// <summary>
        /// http://www.hanselman.com/blog/GettingLINQToSQLAndLINQToEntitiesToUseNOLOCK.aspx
        /// </summary>
        /// <param name="totalTime"></param>
        /// <param name="totalPrice"></param>
        public bool CreateNewBooking(List<Flight> flights, List<Person> passengers,  string totalTime, double totalPrice)
        {
            bool returnValue = true;
            
            try
            {
                MainCtr mainCtr = new MainCtr();
                    
                var booking = new Booking { totalTime = totalTime, totalPrice = totalPrice*passengers.Count };

                foreach (Flight f in flights)
                {
                    if (mainCtr.AirplaneCtr.GetAirplaneByID((int)f.airplaneID).seats >= f.takenSeats + passengers.Count)
                    {
                        var bookingFlights = new BookingFlight
                        {
                            Booking = booking,
                            Flight = f
                        };
                        _db.BookingFlights.InsertOnSubmit(bookingFlights);
                        f.takenSeats += passengers.Count;
                    }
                    else
                    {
                        returnValue = false;
                    } 
                }

                foreach (Person p in passengers)
                {
                    _db.Persons.InsertOnSubmit(p);
                    var bookingPassenger = new BookingPassenger
                    {
                        Booking = booking,
                        Person = p
                    };
                    _db.BookingPassengers.InsertOnSubmit(bookingPassenger);
                }

                _db.Bookings.InsertOnSubmit(booking);
 
            }
            catch (SqlException)
            {
                returnValue = false;
            }
            catch (Exception)
            {
                returnValue = false;
            }

            if (returnValue)
            {
                try
                {
                    _db.SubmitChanges();
                }
                catch (Exception)
                {
                    returnValue = false;
                }
                
            }

            return returnValue;
        }

        public bool UpdateBooking(int id, string totalTime, double totalPrice)
        {
            bool returnValue = true;
            var booking = GetBookingByID(id);

            if (booking != null)
            {
                booking.totalTime = totalTime;
                booking.totalPrice = totalPrice;

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

        public bool DeleteBooking(int id)
        {
            bool returnValue = false;
            var booking = GetBookingByID(id);

            if (booking != null)
            {
                _db.Bookings.DeleteOnSubmit(booking);

                try
                {
                    _db.SubmitChanges();
                    returnValue = true;
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public IEnumerable<BookingPassenger> GetBookingPassenger(int bookingId)
        {
            var result = _db.BookingPassengers.Where(bp => bp.bookingID == bookingId).OrderBy(bp => bp.personID);
            return result;
        }

        public IEnumerable<BookingFlight> GetBookingFlights(int bookingId)
        {
            var result = _db.BookingFlights.Where(bf => bf.bookingID == bookingId).OrderBy(bf => bf.flightID);
            return result;
        }
    }
}
