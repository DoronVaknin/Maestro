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
        DisableAllFields();
    }

    protected void ServiceCallsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        int scID = Convert.ToInt32(ServiceCallsGV.SelectedRow.Cells[1].Text);
        ServiceCall sc = new ServiceCall();
        dt = sc.GetServiceCallPopupMissingDetails(scID);

        //Set the Popup details
        if (dt.Rows.Count > 0)
        {
            ServiceCallID.Text = scID.ToString();
            ServiceCallDateOpened.Text = ((DateTime)dt.Rows[0].ItemArray[1]).ToString("MM/dd/yyyy");
            DateTime ExpirationDate = (DateTime)dt.Rows[0].ItemArray[2];
            ServiceCallExpirationDate.Text = ExpirationDate.ToString("MM/dd/yyyy");
            if (DateTime.Now > ExpirationDate) // Warranty expired
                ServiceCallExpirationDate.Style.Add("border", "2px solid #DB0F0F");
            else // Product is in warranty
                ServiceCallExpirationDate.Style.Add("border", "2px solid #00B800");

                ServiceCallProblemDesc.Text = dt.Rows[0].ItemArray[3].ToString();
            ServiceCallUrgent.Checked = Convert.ToBoolean(dt.Rows[0].ItemArray[4]);
            ServiceCallFirstName.Text = dt.Rows[0].ItemArray[5].ToString();
            ServiceCallLastName.Text = dt.Rows[0].ItemArray[6].ToString();
            ServiceCallPhone.Text = dt.Rows[0].ItemArray[7].ToString();
            ServiceCallMobile.Text = dt.Rows[0].ItemArray[8].ToString();
            ServiceCallAddress.Text = dt.Rows[0].ItemArray[9].ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalServiceCalls", "ActivateModal('ModalServiceCalls');", true);
        }
    }

    public void DisableAllFields()
    {
        //Service Call Fields
        ServiceCallID.Attributes.Add("disabled", "disabled");
        ServiceCallDateOpened.Attributes.Add("disabled", "disabled");
        ServiceCallExpirationDate.Attributes.Add("disabled", "disabled");
        ServiceCallProblemDesc.Attributes.Add("disabled", "disabled");
        ServiceCallUrgent.Attributes.Add("disabled", "disabled");
        ServiceCallFirstName.Attributes.Add("disabled", "disabled");
        ServiceCallLastName.Attributes.Add("disabled", "disabled");
        ServiceCallPhone.Attributes.Add("disabled", "disabled");
        ServiceCallMobile.Attributes.Add("disabled", "disabled");
        ServiceCallAddress.Attributes.Add("disabled", "disabled");
        ServiceCallID.Attributes.Add("disabled", "disabled");
        ServiceCallID.Attributes.Add("disabled", "disabled");
    }

    //protected void ServiceCallBTN_Click(object sender, EventArgs e)
    //{
    //    GridViewRow row = ServiceCallsGV.SelectedRow;
    //    ServiceCall sc = new ServiceCall();
    //    int RowAffected = sc.CloseServiceCall(Convert.ToInt32(row.Cells[1].Text));
    //}

    protected void SaveServiceCallDetailsBTN_Click(object sender, EventArgs e)
    {
        ServiceCall sc = new ServiceCall();
        int scID = Convert.ToInt32(ServiceCallsGV.SelectedRow.Cells[1].Text);
        string ProblemDesc = ServiceCallProblemDesc.Text;
        bool Urgent = ServiceCallUrgent.Checked;
        int RowAffected = sc.UpdateServiceCallDetails(scID, ProblemDesc, Urgent);
        //if (RowAffected > 0)
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalServiceCallUpdated", "ActivateModal('ModalServiceCallUpdated','קריאת השירות עודכנה בהצלחה','ModalServiceCalls');", true);
        //else
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalServiceCallUpdated", "ActivateModal('ModalServiceCallUpdated','אירעה שגיאה בשרת, אנא נסה מאוחר יותר','ModalServiceCalls');", true);
    }

    protected void CloseServiceCallHiddenBTN_Click(object sender, EventArgs e)
    {
        int scID = Convert.ToInt32(ServiceCallsGV.SelectedRow.Cells[1].Text);
        ServiceCall sc = new ServiceCall();
        int RowAffected = sc.CloseServiceCall(scID);
        if (RowAffected > 0)
            ServiceCallsGV.DataBind();
    }

    protected void SetupQuickSearch(object sender, EventArgs e)
    {
        if (ServiceCallsGV.Rows.Count > 0)
        {
            GridViewRow row = ServiceCallsGV.Rows[0];
            for (int i = 0; i < ServiceCallsGV.Columns.Count - 1; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = ServiceCallsGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                ServiceCallsGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}