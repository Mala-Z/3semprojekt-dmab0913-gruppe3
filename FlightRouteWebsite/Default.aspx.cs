using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FlightServiceReference;

public partial class _Default : Page
{
    private static readonly FlightServiceClient fService = new FlightServiceClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            comboboxSource();
        }
    }

    public void comboboxSource()
    {
        //airportFrom.DataSource = ListItems.AirportItems();
        //airportTo.DataSource = ListItems.AirportItems();


        foreach (var a in fService.GetAllAirports())
        {
            string text = a.name + " " + a.location;
            string value = a.airportID.ToString();

            airportFrom.Items.Add(new ListItem(text, value));
            airportTo.Items.Add(new ListItem(text, value));
        }
        airportFrom.DataBind();
        airportTo.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FlightServiceClient flightService = new FlightServiceClient();

            string fromAs = airportFrom.SelectedValue;
            string toAs = airportTo.SelectedValue;
            int noOfPass = Int32.Parse(txtNoOfPassengers.Text);

            if (txtNoOfPassengers.Text != "" && dateBox.SelectedDate != null)
            {
                if (noOfPass >= 1)
                {
                    if (fromAs != toAs)
                    {
                        string AirportFromID = fromAs;
                        string AirportToID = toAs;
                        string DateString = dateBox.SelectedDate.ToString().Substring(0, 10);
                        string NoOfPassengers = noOfPass.ToString();

                        Response.Redirect("~/SearchResult.aspx?fromA=" + AirportFromID + "&toA=" + AirportToID + "&date=" +
                                          DateString + "&noOfPass=" + NoOfPassengers);
                    }
                    else
                    {
                        CompareValidator1.Text = "Luftehavne må ikke være ens";

                    }
                }
            }
        }

        catch (NullReferenceException err)
        {

        }
        catch (FormatException err)
        {

        }
    }
}
