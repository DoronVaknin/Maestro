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

    protected void ProjectsArchiveGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        int SelectedIndex = ProjectsArchiveGV.SelectedIndex;
        SelectedIndex += 1;
        GridViewRow row = ProjectsArchiveGV.Rows[SelectedIndex];
        Session["selectedrow"] = row;
        Response.Redirect("ProjectDetails.aspx");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        for (int i = 0; i < ProjectsArchiveGV.Columns.Count - 1; i++)
        {
            TableHeaderCell cell = new TableHeaderCell();
            TextBox txtSearch = new TextBox();
            txtSearch.Attributes["placeholder"] = ProjectsArchiveGV.Columns[i].HeaderText;
            txtSearch.CssClass = "search_textbox form-control";
            cell.Controls.Add(txtSearch);
            row.Controls.Add(cell);
        }
        ProjectsArchiveGV.HeaderRow.Parent.Controls.AddAt(1, row);
    }
}