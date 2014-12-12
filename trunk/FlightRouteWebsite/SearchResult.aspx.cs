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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["fromA"] != null && Request.QueryString["toA"] != null && Request.QueryString["date"] != null
                && Request.QueryString["noOfPass"] != null)
            {
                CheapestRoute();
                FastestRoute();
            }
            else
            {
                //Gået direkte til siden uden QueryString
            }          
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

        var fPrice = fservice.RunDijkstraCheapest(airportFrom, airportTo, date.Substring(0, 10)).ToList();
        List<Flight> fListPrice = new List<Flight>();
        fListPrice = fPrice;

        repCheapest.DataSource = fListPrice;
        repCheapest.DataBind();

    }

    public void FastestRoute()
    {
        int fromID = Convert.ToInt32(Request.QueryString["fromA"]);
        int toID = Convert.ToInt32(Request.QueryString["toA"]);
        string date = Request.QueryString["date"];
        int noOfPassengers = Convert.ToInt32(Request.QueryString["noOfPass"]);
        Airport airportFrom = fservice.GetAirportByID(fromID);
        Airport airportTo = fservice.GetAirportByID(toID);

        var fFast = fservice.RunDijkstraFastest(airportFrom, airportTo, date.Substring(0, 10)).ToList();
        List<Flight> fListFast = new List<Flight>();
        fListFast = fFast;

        repFastest.DataSource = fListFast;
        repFastest.DataBind();

    }
}