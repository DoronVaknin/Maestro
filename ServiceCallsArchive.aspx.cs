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

    protected void OnDataBound(object sender, EventArgs e)
    {
        if (ServiceCallsArchiveGV.Rows.Count > 0)
        {
            GridViewRow row = ServiceCallsArchiveGV.Rows[0];
            for (int i = 0; i < ServiceCallsArchiveGV.Columns.Count - 1; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = ServiceCallsArchiveGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                ServiceCallsArchiveGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}