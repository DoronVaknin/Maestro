using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustomerRow"] != null)
        {
            GridViewRow CustomerRow = (GridViewRow)Session["CustomerRow"];
            string Fname = CustomerRow.Cells[2].Text;
            string Lname = CustomerRow.Cells[3].Text;
            PageHeader.InnerHtml = "פרויקטים עבור הלקוח " + Fname + " " + Lname;
        }
    }
    protected void ProjectsPerCustomerGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ProjectID"] = ProjectsPerCustomerGV.SelectedRow.Cells[1].Text;
        Response.Redirect("~/ProjectDetails.aspx");
    }

    public void AddProjectBTN_Click(object sender, EventArgs e) 
    { 
        if (Session["CustomerRow"] != null)
        {
            GridViewRow CustomerRow = (GridViewRow)Session["CustomerRow"];
            string Fname = CustomerRow.Cells[2].Text;
            string Lname = CustomerRow.Cells[3].Text;
            Session["Customer"] = Fname + " " + Lname;
        }
        Response.Redirect("~/NewProject.aspx?Source=ProjectsPerCustomer");
    }
}