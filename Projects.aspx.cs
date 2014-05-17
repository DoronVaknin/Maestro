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
        int SelectedIndex = ProjectsGV.SelectedIndex;
        int ProjectID = Convert.ToInt32(ProjectsGV.Rows[SelectedIndex].Cells[1].Text);
        Session["ProjectID"] = ProjectID;
        Response.Redirect("ProjectDetails.aspx");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        if (ProjectsGV.Rows.Count > 0)
        {
            GridViewRow row = ProjectsGV.Rows[0];
            for (int i = 0; i < ProjectsGV.Columns.Count - 1; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = ProjectsGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                ProjectsGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}