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
        GridViewRow CustomerRow = (GridViewRow)Session["Customer"];
        string Fname = CustomerRow.Cells[2].Text;
        string Lname = CustomerRow.Cells[3].Text;
        Header.InnerHtml = "פרויקטים עבור הלקוח " + Fname + " " + Lname;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["selectedrow"] = GridView1.SelectedRow;
        Response.Redirect("ProjectDetails.aspx");
    }

    protected void AddProjectForCustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewProject.aspx");
    }
}