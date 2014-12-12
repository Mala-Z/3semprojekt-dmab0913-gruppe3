using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightServiceReference;

public partial class SearchResult : System.Web.UI.Page
{
    private FlightServiceClient fservice = new FlightServiceClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        {
           CheapestRoute();
            FastestRoute();
        }
    }

    public void CheapestRoute()
    {
        int fromID = Convert.ToInt32(Request.QueryString["fromA"]);
        int toID = Convert.ToInt32(Request.QueryString["toA"]);
        string date = Request.QueryString["date"];
        int noOfPassengers = Convert.ToInt32(Request.QueryString["noOfPass"]);
        Airport airportFrom = fservice.GetAirportByID(fromID);
        Airport airportTo = fservice.GetAirportByID(toID);

        List<Flight> fListPrice = new List<Flight>();
        fListPrice = fservice.RunDijkstraCheapest(airportFrom, airportTo, date);
    }
}