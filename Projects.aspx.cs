using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void ProjectsTBL_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = ProjectsTBL.SelectedIndex;
        SelectedIndex += 1;
        GridViewRow row = ProjectsTBL.Rows[SelectedIndex];
        Session["selectedrow"] = row;
        Response.Redirect("ProjectDetails.aspx");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        for (int i = 0; i < ProjectsTBL.Columns.Count; i++)
        {
            TableHeaderCell cell = new TableHeaderCell();
            TextBox txtSearch = new TextBox();
            txtSearch.Attributes["placeholder"] = ProjectsTBL.Columns[i].HeaderText;
            txtSearch.CssClass = "search_textbox form-control";
            cell.Controls.Add(txtSearch);
            row.Controls.Add(cell);
        }
        ProjectsTBL.HeaderRow.Parent.Controls.AddAt(1, row);
    }

 

}