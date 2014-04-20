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

    protected void ServiceCallsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('ModalServiceCalls')", true);
        GridViewRow row = ServiceCallsGridView.SelectedRow;
        ServiceCall sc = new ServiceCall();
        dt=sc.GetServiceCallPopupMissingDetails(Convert.ToInt32(row.Cells[2].Text));
        //Set the Popup details
        OpenDateLBL.Text = row.Cells[5].Text;
        if (row.Cells[6].Text == "&nbsp;")
            CloseDateLBL.Visible = false;
        else
            CloseDateLBL.Text = row.Cells[6].Text;
        FNameLBL.Text = row.Cells[3].Text;
        LNameLBL.Text = row.Cells[4].Text;
        PhoneLBL.Text = dt.Rows[0]["Mobile"].ToString();
        ProjectNameLBL.Text = dt.Rows[0]["pName"].ToString();
        if (ProjectNameLBL.Text == "&nbsp;")
            ProjectNameLBL.Visible = false;
        RegionLBL.Text = dt.Rows[0]["RegionName"].ToString();
        AdressLBL.Text = dt.Rows[0]["cAddress"].ToString();
        ServiceCallDescriptionLBL.Text = row.Cells[7].Text;
    }

    protected void ServiceCallBTN_Click(object sender, EventArgs e)
    {
        GridViewRow row = ServiceCallsGridView.SelectedRow;
        ServiceCall sc = new ServiceCall();
        sc.CloseServiceCall(Convert.ToInt32(row.Cells[2].Text));
    }
}