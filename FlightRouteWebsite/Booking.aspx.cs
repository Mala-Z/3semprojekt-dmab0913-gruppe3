using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
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
            TableRoute();
            h2RestPass.Visible = false;

            var app = AppSession.BHelper;

            if (AppSession.BHelper.fromA != null && AppSession.BHelper.toA != null && AppSession.BHelper.date != null &&
                AppSession.BHelper.noOfPass != null
                && AppSession.BHelper.route.Count != 0)
            {
                _fromA = AppSession.BHelper.fromA;
                _toA = AppSession.BHelper.toA;
                _date = AppSession.BHelper.date;
                //_noOfPass = AppSession.BHelper.noOfPass;
                _route = AppSession.BHelper.route;

                _fromA1.InnerText = AppSession.BHelper.fromA.name;
                _toA1.InnerText = AppSession.BHelper.toA.name;
                _date1.InnerText = AppSession.BHelper.date;
                _noOfPass1.InnerText = AppSession.BHelper.noOfPass.ToString();


                //if (_noOfPass != 0)
                //{
                //    h2RestPass.Visible = true;
                //    initializePassengers(_noOfPass);
                //}
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

    private void TableRoute()
    {
        
        List<Flight> fRoute = AppSession.BHelper.route;



        var cTotalCost = (from f in fRoute
                          select f.price * AppSession.BHelper.noOfPass).Sum();
        lblCTotalCost.Text = cTotalCost.ToString();
        var cTotalTime = (from f in AppSession.BHelper.route
                          select f.traveltime).Sum();
        lblCTotalTime.Text = cTotalTime.ToString();
        repRoute.DataSource = AppSession.BHelper.route;
        repRoute.DataBind();
    }

    private void initializePassengers(int noOfPass)
    {
        for (int i = 0; i < noOfPass-1; i++)
        {
            var addPass = (ASP.usercontrols_addpassenger_ascx)LoadControl("~/UserControls/AddPassenger.ascx");
            otherPassengers.Controls.Add(addPass);
            _restPassList.Add(addPass);
        }
        
    }

    public string getAirportName(int id)
    {
        return new FlightServiceClient().GetAirportByID(id).name;
    }

    protected void btnBook_Click(object sender, EventArgs e)
    {
        bool extraPassResult = true;
        var passList = new List<FlightServiceReference.Person>();
        var fService = new FlightServiceClient();
        if (txtFName != null && txtLName != null && txtAddress != null && txtEmail != null && txtPhoneNo != null)
        {

            //foreach (Control ctl in otherPassengers.Controls)
            //{
            //    if (ctl is UserControls_AddPassenger)
            //    {
            //        UserControls_AddPassenger p = (UserControls_AddPassenger) ctl;
            //        if (p.GetFName() != null && p.GetLName() != null)
            //        {
            //            passList.Add(fService.CreateNewPersonBooking(p.GetFName(), p.GetLName()));
            //        }
            //        else
            //        {
            //            //TODO Fejl besked, det må ikke være null
            //            extraPassResult = false;
            //        }

            //    }
            //}

            if (extraPassResult)
            {
                passList.Add(fService.CreateNewPersonBookingFull(txtFName.Text, txtLName.Text, ddlGender.SelectedValue,
                    txtAddress.Text,
                    txtPhoneNo.Text, txtEmail.Text));
                var route = AppSession.BHelper.route;

                FlightServiceReference.Flight[] fl = route.ToArray();
                FlightServiceReference.Person[] pl = passList.ToArray();
                string totalTime = (from f in route
                    select f.price*AppSession.BHelper.noOfPass).Sum().ToString();
                double totalCost = Double.Parse((from f in AppSession.BHelper.route
                    select f.traveltime).Sum().ToString());

                if (fService.CreateNewBooking(fl, pl, totalTime, totalCost))
                {
                    Response.Redirect("~/BookingSuccess.aspx");
                }
                else
                {
                    //Fejl
                }
            }
            else
            {
                //fejl
            }

        }
    }

}