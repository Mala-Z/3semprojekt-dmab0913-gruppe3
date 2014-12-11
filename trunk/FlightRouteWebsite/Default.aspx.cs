using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
}