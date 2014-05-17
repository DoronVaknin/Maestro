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
        GridViewRow row = ProjectsArchiveGV.Rows[SelectedIndex];
        Session["selectedrow"] = row;
        Response.Redirect("ProjectDetails.aspx");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        if (ProjectsArchiveGV.Rows.Count > 0)
        {
            GridViewRow row = ProjectsArchiveGV.Rows[0];
            for (int i = 0; i < ProjectsArchiveGV.Columns.Count - 1; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = ProjectsArchiveGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                ProjectsArchiveGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}