using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Booking : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int noOfPass = 0;

            if (noOfPass == 0)
            {
                h2RestPass.Visible = false;
            }
            else
            {
                initializePassengers(noOfPass); 
            }
            
        }
    }

    private void initializePassengers(int noOfPass)
    {
        for (int i = 0; i < noOfPass-1; i++)
        {
            ASP.usercontrols_addpassenger_ascx addPass = (ASP.usercontrols_addpassenger_ascx)LoadControl("~/UserControls/AddPassenger.ascx");
            otherPassengers.Controls.Add(addPass);
        }
        
    }

    protected void btnBook_Click(object sender, EventArgs e)
    {
    }
}