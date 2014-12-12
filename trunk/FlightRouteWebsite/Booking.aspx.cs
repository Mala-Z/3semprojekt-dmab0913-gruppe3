using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightServiceReference;

public partial class Booking : System.Web.UI.Page
{

    private List<UserControls_AddPassenger> _restPassList = new List<UserControls_AddPassenger>();
    private FlightServiceReference.Airport _fromA;
    private FlightServiceReference.Airport _toA;
    private string _date;
    private int _noOfPass;
    private List<FlightServiceReference.Flight> _route = new List<FlightServiceReference.Flight>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["fromA"] != null && Session["toA"] != null && Session["date"] != null &&
                Session["noOfPass"] != null && Session["route"] != null)
            {
                _fromA = (FlightServiceReference.Airport)Session["fromA"];
                _toA = (FlightServiceReference.Airport)Session["toA"];
                _date = (string)Session["date"];
                _noOfPass = (int)Session["noOfPass"];
                _route = (List<FlightServiceReference.Flight>)Session["route"];

                if (_noOfPass == 0)
                {
                    h2RestPass.Visible = false;
                }
                else
                {
                    initializePassengers(_noOfPass);
                }
            }
            
            
        }
    }

    private void initializePassengers(int noOfPass)
    {
        for (int i = 0; i < noOfPass-1; i++)
        {
            ASP.usercontrols_addpassenger_ascx addPass = (ASP.usercontrols_addpassenger_ascx)LoadControl("~/UserControls/AddPassenger.ascx");
            otherPassengers.Controls.Add(addPass);
            _restPassList.Add(addPass);
        }
        
    }

    protected void btnBook_Click(object sender, EventArgs e)
    {
    }
}