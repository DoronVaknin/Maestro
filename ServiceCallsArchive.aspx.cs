using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisableAllFields();
    }

    protected void ServiceCallsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        int scID = Convert.ToInt32(ServiceCallsArchiveGV.SelectedRow.Cells[1].Text);
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

    protected void SetupQuickSearch(object sender, EventArgs e)
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