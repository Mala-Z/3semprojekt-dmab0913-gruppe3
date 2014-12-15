using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
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

        public bool CreateNewBooking(int[] flightIDs, List<Person> persons, string totalTime, double totalPrice)
        {
            bool returnValue = true;
            AirplaneCtr airplaneCtr = new AirplaneCtr(_db);
            
            using (var transScope = new TransactionScope())
            {
                //Lav lister med personer og flights fra arrays med id
                var flights = flightIDs.Select(id => _db.Flights.SingleOrDefault(flight => flight.flightID == id)).ToList();
                //Opret booking. Skal submittes til db så den får ID!
                var booking = new Booking { totalPrice = totalPrice * persons.Count, totalTime = totalTime };
                _db.Bookings.InsertOnSubmit(booking);
                

                try
                {
                    _db.SubmitChanges();

                    foreach (Person p in persons)
                    {
                        //Hvis personID er 0 er det en person der ikke er oprettet i db og derfor ikke har et ID endnu
                        if (p.personID == 0)
                        {
                            _db.Persons.InsertOnSubmit(p);
                            _db.SubmitChanges();
                        }

                        var bookingPassenger = new BookingPassenger
                        {
                            bookingID = booking.bookingID,
                            personID = p.personID
                        };
                        _db.BookingPassengers.InsertOnSubmit(bookingPassenger);
                    }


                    foreach (Flight f in flights)
                    {
                        //Hvis der er plads på flyet
                        if (airplaneCtr.GetAirplaneByID(Convert.ToInt32(f.airplaneID)).seats >= f.takenSeats + persons.Count)
                        {
                            //Opret ny BookingFlight
                            var bookingFlights = new BookingFlight
                            {
                                bookingID = booking.bookingID,
                                flightID = f.flightID
                            };
                            _db.BookingFlights.InsertOnSubmit(bookingFlights);
                            f.takenSeats += persons.Count;
                        }
                        else
                        {
                            returnValue = false;
                        }
                    }

                    if (returnValue)
                    {
                        _db.SubmitChanges();
                        transScope.Complete();
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                        
                }
                catch (SqlException)
                {
                    returnValue = false;
                }
                catch (Exception)
                {
                    returnValue = false;
                } 

            }

            //if (returnValue)
            //{
            //    try
            //    {
            //        _db.SubmitChanges();
            //    }
            //    catch (Exception)
            //    {
            //        returnValue = false;
            //    }
                
            //}
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
