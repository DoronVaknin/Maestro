﻿using System;
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
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('ModalServiceCalls')", true);
        //GridViewRow row = ServiceCallsGridView.SelectedRow;
        int scID = Convert.ToInt32(ServiceCallsGridView.SelectedRow.Cells[1].Text);
        ServiceCall sc = new ServiceCall();
        dt = sc.GetServiceCallPopupMissingDetails(scID);

        //Set the Popup details
        ServiceCallID.Text = scID.ToString();
        ServiceCallDateOpened.Text = ((DateTime)dt.Rows[0].ItemArray[1]).ToString("MM/dd/yyyy");
        ServiceCallExpirationDate.Text = ((DateTime)dt.Rows[0].ItemArray[2]).ToString("MM/dd/yyyy");
        ServiceCallProblemDesc.Text = dt.Rows[0].ItemArray[3].ToString();
        ServiceCallUrgent.Checked = Convert.ToBoolean(dt.Rows[0].ItemArray[4]);
        ServiceCallFirstName.Text = dt.Rows[0].ItemArray[5].ToString();
        ServiceCallLastName.Text = dt.Rows[0].ItemArray[6].ToString();
        ServiceCallPhone.Text = dt.Rows[0].ItemArray[7].ToString();
        ServiceCallMobile.Text = dt.Rows[0].ItemArray[8].ToString();
        ServiceCallAddress.Text = dt.Rows[0].ItemArray[9].ToString();
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

    protected void ServiceCallBTN_Click(object sender, EventArgs e)
    {
        GridViewRow row = ServiceCallsGridView.SelectedRow;
        ServiceCall sc = new ServiceCall();
        sc.CloseServiceCall(Convert.ToInt32(row.Cells[1].Text));
    }

    protected void SaveServiceCallDetailsBTN_Click(object sender, EventArgs e)
    {


    }
}