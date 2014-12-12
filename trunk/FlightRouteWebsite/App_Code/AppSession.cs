using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightServiceReference;

/// <summary>
/// Summary description for AppSession
/// </summary>
public static class AppSession
{
    # region Private Constants
    private const string _currentBookingHelper = "flight_currentbookinghelper";
    # endregion

    public static void ResetState()
    {
        BHelper = null;
    }

    public static BookingHelper BHelper
    {
        get
        {
            if (HttpContext.Current.Session[_currentBookingHelper] == null)
            {
                HttpContext.Current.Session[_currentBookingHelper] = new BookingHelper();
                return (BookingHelper)HttpContext.Current.Session[_currentBookingHelper];
            }
            else
            {
                return (BookingHelper)HttpContext.Current.Session[_currentBookingHelper];
            }
        }
        set
        {
            HttpContext.Current.Session[_currentBookingHelper] = value;
        }
    }
}