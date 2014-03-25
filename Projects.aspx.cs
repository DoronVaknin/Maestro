using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ProjectsTBL_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Need to add conditions in ProjectDetails.aspx.cs because of the rows difference (Project vs ProjectsPerCustomer)

        //Session["selectedrow"] = ProjectsTBL.SelectedRow;
        //Response.Redirect("ProjectDetails.aspx");
    }
   
}