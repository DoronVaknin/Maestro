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
    protected void Customers_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = CustomersTBL.SelectedIndex;
        SelectedIndex += 1;
        GridViewRow row = CustomersTBL.Rows[SelectedIndex];
        Session["Customer"] = row;
        Session["CustomerID"] = row.Cells[1].Text;
        //Session["FirstName"] = CustomersTBL.Rows[SelectedIndex].Cells[2].Text;
        //Session["LastName"] = CustomersTBL.Rows[SelectedIndex].Cells[3].Text;
        Response.Redirect("ProjectsPerCustomer.aspx");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        for (int i = 0; i < CustomersTBL.Columns.Count - 3; i++)
        {
                TableHeaderCell cell = new TableHeaderCell();
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = CustomersTBL.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                cell.Controls.Add(txtSearch);
                row.Controls.Add(cell);
        }
        CustomersTBL.HeaderRow.Parent.Controls.AddAt(1, row);
    }
}