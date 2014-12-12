using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_AddPassenger : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public string GetFName()
    {
        return txtFName.Text;
    }

    public string GetLName()
    {
        return txtLName.Text;
    }
}