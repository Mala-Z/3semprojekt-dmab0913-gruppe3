using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightServiceReference;

public partial class _Default : Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        comboboxSource();
    }

    public void comboboxSource()
    {
        airportFrom.DataSource = ListItems.AirportItems();
        airportTo.DataSource = ListItems.AirportItems();
        airportFrom.DataBind();
        airportTo.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FlightServiceClient flightService = new FlightServiceClient();




        FlightServiceReference.Airport fromA =
            flightService.GetAirportByID(Int32.Parse(((ListItem)airportFrom.SelectedItem).Value));
        FlightServiceReference.Airport toA =
            flightService.GetAirportByID(Int32.Parse(((ListItem)airportTo.SelectedItem).Value));
        int noOfPass = Int32.Parse(txtNoOfPassengers.Text);

        if (txtNoOfPassengers.Text != "" && dateBox.SelectedDate != null)
        {
            if (noOfPass >= 1)
            {
                if (fromA.airportID != toA.airportID)
                {

                    hidAirportFromID.Value = fromA.airportID.ToString();
                    hidAirportToID.Value = toA.airportID.ToString();
                    hidDateString.Value = dateBox.SelectedDate.ToString();
                    hidNoOfPassengers.Value = noOfPass.ToString();
                    Response.Redirect(
                        "~/SearchResult.aspx?fromA=hidAirportFromID.Value&toA=hidAirportToID.Value&date=hidDateString.Value&noOfPass=hidNoOfPassengers.Value");
                }

            }


        }
    }
}
