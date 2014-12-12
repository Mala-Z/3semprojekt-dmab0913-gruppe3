﻿using System;
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
    private int noOfPassengers;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["fromA"] != null && Request.QueryString["toA"] != null && Request.QueryString["date"] != null
                && Request.QueryString["noOfPass"] != null)
            {
                int fromID = Convert.ToInt32(Request.QueryString["fromA"]);
                int toID = Convert.ToInt32(Request.QueryString["toA"]);
                string date = Request.QueryString["date"];
                noOfPassengers = Convert.ToInt32(Request.QueryString["noOfPass"]);
                Airport airportFrom = fservice.GetAirportByID(fromID);
                Airport airportTo = fservice.GetAirportByID(toID);

                if (airportFrom != null && airportTo != null)
                {
                    CheapestRoute(airportFrom, airportTo, date);
                    FastestRoute(airportFrom, airportTo, date);
                }
                
            }
            else
            {
                //Gået direkte til siden uden QueryString
            }          
        }
    }

    public void CheapestRoute(FlightServiceReference.Airport airportFrom, FlightServiceReference.Airport airportTo, string date)
    {
        var fPrice = fservice.RunDijkstraCheapest(airportFrom, airportTo, date.Substring(0, 10)).ToList();
        List<Flight> fListPrice = new List<Flight>();
        fListPrice = fPrice;

        var cTotalCost = (from f in fListPrice
                          select f.price * noOfPassengers).Sum();
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
        List<Flight> fListFast = new List<Flight>();
        fListFast = fFast;

        var fTotalCost = (from f in fListFast
                          select f.price * noOfPassengers).Sum();
        lblFTotalCost.Text = fTotalCost.ToString();
        var fTotalTime = (from f in fListFast
                          select f.traveltime).Sum();
        lblFTotalTime.Text = fTotalTime.ToString();

        repFastest.DataSource = fListFast;
        repFastest.DataBind();
    }
}