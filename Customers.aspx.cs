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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow NewRow = GridView1.SelectedRow;
        Session["ID"] = NewRow.Cells[1].Text;
        Response.Redirect("ShowProjectsForCustomer.aspx");
    }
}