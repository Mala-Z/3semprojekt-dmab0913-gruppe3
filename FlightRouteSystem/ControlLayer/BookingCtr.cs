using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;


namespace ControlLayer
{
    public class BookingCtr
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of bookings/Booking></returns>
        public List<Booking> GetAllBookings()
        {
            var db = DBConnection.GetInstance().GetConnection();

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
            var db = DBConnection.GetInstance().GetConnection();

            var booking = db.Bookings.SingleOrDefault(a => a.bookingID == id);

            return booking;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalTime"></param>
        /// <param name="totalPrice"></param>
        public bool CreateNewBooking(string totalTime, double totalPrice)
        {
            bool returnValue = true;
            var db = DBConnection.GetInstance().GetConnection();
            var booking = new Booking { totalTime = totalTime, totalPrice = totalPrice };

            db.Bookings.InsertOnSubmit(booking);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="totalTime"></param>
        /// <param name="totalPrice"></param>
        public bool UpdateBooking(int id, string totalTime, double totalPrice)
        {
            bool returnValue = true;
            var db = DBConnection.GetInstance().GetConnection();
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
            var db = DBConnection.GetInstance().GetConnection();
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
                finally
                {
                    
                }
                
            }
            return returnValue;
        }

    }
}
