using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ProjectID =Convert.ToInt16( Session["ProjectID"]);
        DBservices db = new DBservices();
        DataTable dt = db.GetCustomerInformation(ProjectID);
        
        
    }
}