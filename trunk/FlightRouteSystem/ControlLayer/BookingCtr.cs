using System;
using System.Collections.Generic;
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
        public void CreateNewBooking(string totalTime, double totalPrice)
        {
            var db = DBConnection.GetInstance().GetConnection();

            var booking = new Booking { totalTime = totalTime, totalPrice = totalPrice };

            db.Bookings.InsertOnSubmit(booking);
            db.SubmitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="totalTime"></param>
        /// <param name="totalPrice"></param>
        public void UpdateBooking(int id, string totalTime, double totalPrice)
        {
            var db = DBConnection.GetInstance().GetConnection();

            var booking = GetBookingByID(id);

            if (booking != null)
            {
                booking.totalTime = totalTime;
                booking.totalPrice = totalPrice;

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBooking(int id)
        {
            var db = DBConnection.GetInstance().GetConnection();
            var booking = GetBookingByID(id);
            if (booking != null)
            {
                db.Bookings.DeleteOnSubmit(booking);
                db.SubmitChanges();
            }
        }

    }
}
