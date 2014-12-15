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
    //private int noOfPassengers;
    private Airport airportFrom;
    private Airport airportTo;
    private String date;
    List<Flight> fListFast = new List<Flight>();
    List<Flight> fListPrice = new List<Flight>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["fromA"] != null && Request.QueryString["toA"] != null &&
                Request.QueryString["date"] != null)
            {
                int fromID = Convert.ToInt32(Request.QueryString["fromA"]);
                int toID = Convert.ToInt32(Request.QueryString["toA"]);
                date = Request.QueryString["date"].Substring(0, 10);
                //noOfPassengers = Convert.ToInt32(Request.QueryString["noOfPass"]);
                airportFrom = fservice.GetAirportByID(fromID);
                airportTo = fservice.GetAirportByID(toID);

                if (airportFrom != null && airportTo != null)
                {
                    CheapestRoute(airportFrom, airportTo, date);
                    FastestRoute(airportFrom, airportTo, date);
                }

            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    public void CheapestRoute(FlightServiceReference.Airport airportFrom, FlightServiceReference.Airport airportTo, string date)
    {
        var fPrice = fservice.RunDijkstraCheapest(airportFrom, airportTo, date.Substring(0, 10)).ToList();
       
        fListPrice = fPrice;
        
       

        var cTotalCost = (from f in fListPrice
                          select f.price).Sum();
        lblCTotalCost.Text = cTotalCost.ToString();
        var cTotalTime = (from f in fListPrice
                          select f.traveltime).Sum();
        lblCTotalTime.Text = cTotalTime.ToString();

        repCheapest.DataSource = fListPrice;
        repCheapest.DataBind();
    }

    public void FastestRoute(FlightServiceReference.Airport airportFrom, FlightServiceReference.Airport airportTo, string date)
    {
        var fFast = fservice.RunDijkstraFastest(airportFrom, airportTo, date.Substring(0, 10)).ToList();
        
        fListFast = fFast;

        

        var fTotalCost = (from f in fListFast
                          select f.price).Sum();
        lblFTotalCost.Text = fTotalCost.ToString();
        var fTotalTime = (from f in fListFast
                          select f.traveltime).Sum();
        lblFTotalTime.Text = fTotalTime.ToString();

        repFastest.DataSource = fListFast;
        repFastest.DataBind();
    }

    public string getAirportName(int id)
    {
        return new FlightServiceClient().GetAirportByID(id).name;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int fromID = Convert.ToInt32(Request.QueryString["fromA"]);
        int toID = Convert.ToInt32(Request.QueryString["toA"]);
        date = Request.QueryString["date"];
        //noOfPassengers = Convert.ToInt32(Request.QueryString["noOfPass"]);
        airportFrom = fservice.GetAirportByID(fromID);
        airportTo = fservice.GetAirportByID(toID);
        var fFast = fservice.RunDijkstraFastest(airportFrom, airportTo, date.Substring(0, 10)).ToList();
        AppSession.BHelper.fromA = airportFrom;
        AppSession.BHelper.toA = airportTo;
        AppSession.BHelper.date = date.Substring(0,10);
        //AppSession.BHelper.noOfPass = noOfPassengers;
        AppSession.BHelper.route = fFast;
        Response.Redirect("~/Booking.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int fromID = Convert.ToInt32(Request.QueryString["fromA"]);
        int toID = Convert.ToInt32(Request.QueryString["toA"]);
        date = Request.QueryString["date"];
        //noOfPassengers = Convert.ToInt32(Request.QueryString["noOfPass"]);
        airportFrom = fservice.GetAirportByID(fromID);
        airportTo = fservice.GetAirportByID(toID);
        var fPrice = fservice.RunDijkstraCheapest(airportFrom, airportTo, date.Substring(0, 10)).ToList();

        AppSession.BHelper.fromA = airportFrom;
        AppSession.BHelper.toA = airportTo;
        AppSession.BHelper.date = date.Substring(0,10);
        //AppSession.BHelper.noOfPass = noOfPassengers;
        AppSession.BHelper.route = fPrice;
        Response.Redirect("~/Booking.aspx");
    }
}