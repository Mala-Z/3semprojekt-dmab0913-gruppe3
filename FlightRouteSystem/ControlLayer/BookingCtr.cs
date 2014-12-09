﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using System.Transactions;


namespace ControlLayer
{
    public class BookingCtr
    {
        private dmab0913_3DataContext db;

        public BookingCtr(dmab0913_3DataContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of bookings/Booking></returns>
        public List<Booking> GetAllBookings()
        {
            var bookings = db.Bookings.OrderBy(x => x.bookingID).ToList();

            return bookings;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Booking GetBookingByID(int id)
        {
            var booking = db.Bookings.SingleOrDefault(a => a.bookingID == id);

            return booking;
        }

        /// <summary>
        /// http://www.hanselman.com/blog/GettingLINQToSQLAndLINQToEntitiesToUseNOLOCK.aspx
        /// </summary>
        /// <param name="totalTime"></param>
        /// <param name="totalPrice"></param>
        public bool CreateNewBooking(List<Flight> flights, List<Person> passengers,  string totalTime, double totalPrice)
        {
            bool returnValue = false;
            
            try
            {
                MainCtr main = new MainCtr();
                    
                var booking = new Booking { totalTime = totalTime, totalPrice = totalPrice*passengers.Count };

                foreach (Flight f in flights)
                {
                    if (main.AirplaneCtr.GetAirplaneByID((int)f.airplaneID).seats >= f.takenSeats + passengers.Count)
                    {
                        var BookingFlights = new BookingFlight
                        {
                            Booking = booking,
                            Flight = f
                        };
                        db.BookingFlights.InsertOnSubmit(BookingFlights);
                        f.takenSeats += passengers.Count;
                    }
                    else
                    {
                        returnValue = false;
                    } 
                }

                foreach (Person p in passengers)
                {
                    var BookingPassenger = new BookingPassenger
                    {
                        Booking = booking,
                        Person = p
                    };
                    db.BookingPassengers.InsertOnSubmit(BookingPassenger);
                }

                db.Bookings.InsertOnSubmit(booking);
                returnValue = true;
            }
            catch (SqlException)
            {
                returnValue = false;
            }
            catch (Exception)
            {
                //trans.Rollback();
            }

            if (returnValue)
            {
                db.SubmitChanges();
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="totalTime"></param>
        /// <param name="totalPrice"></param>
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
        public bool DeleteBooking(int id)
        {
            bool returnValue = false;
            
            var booking = GetBookingByID(id);

            if (booking != null)
            {
                db.Bookings.DeleteOnSubmit(booking);

                try
                {
                    db.SubmitChanges();
                    returnValue = true;
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
