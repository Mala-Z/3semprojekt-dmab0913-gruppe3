using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BookingHelper
/// </summary>
[Serializable]
public class BookingHelper
{
    public FlightServiceReference.Airport fromA { get; set; }
    public FlightServiceReference.Airport toA { get; set; }
    public string date { get; set; }
    public int noOfPass { get; set; }
    public List<FlightServiceReference.Flight> route { get; set; }

	public BookingHelper()
	{
        route = new List<FlightServiceReference.Flight>();
	}
}