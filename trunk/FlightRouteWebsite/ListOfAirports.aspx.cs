using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightServiceReference;
public partial class ListOfAirports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FlightServiceClient fService = new FlightServiceClient();

        repAirport.DataSource = fService.GetAllAirports();
        repAirport.DataBind();
    }
}