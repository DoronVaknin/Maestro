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

    }
    protected void Customers_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = CustomersGV.SelectedIndex;
        GridViewRow row = CustomersGV.Rows[SelectedIndex];
        Session["CustomerRow"] = row;
        Session["CustomerID"] = row.Cells[1].Text;
        Response.Redirect("ProjectsPerCustomer.aspx");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        if (CustomersGV.Rows.Count > 0)
        {
            GridViewRow row = CustomersGV.Rows[0];
            for (int i = 0; i < CustomersGV.Columns.Count - 1; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = CustomersGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                CustomersGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}