﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    static DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Store cID & pID in DataTable for later use
        Project p = new Project();
        dt = p.GetUndecidedCustomers();

        DisableAllFields();
    }

    public void DisableAllFields()
    {
        ProjectName.Attributes.Add("disabled", "disabled");
        CustomerMobilePhone.Attributes.Add("disabled", "disabled");
        ProjectComments.Attributes.Add("disabled", "disabled");
        CustomerBackDate.Attributes.Add("disabled", "disabled");
        ProjectStatus.Attributes.Add("disabled", "disabled");
    }

    protected void PriceOfferGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalCustomerDetails", "ActivateModal('ModalEditUndecidedCustomer');", true);

        ProjectName.Value = PriceOfferGV.SelectedRow.Cells[1].Text;
        CustomerMobilePhone.Value = PriceOfferGV.SelectedRow.Cells[2].Text;
        ProjectComments.Text = PriceOfferGV.SelectedRow.Cells[3].Text;
        CustomerBackDate.Value = (Convert.ToDateTime(PriceOfferGV.SelectedRow.Cells[4].Text)).ToString("MM/dd/yyyy");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "FixTextAreaIssue", "FixTextAreaIssue('EditUndecidedCustomerTBL');", true);
    }

    protected void SaveUndecidedCustomerDetailsBTN_Click(object sender, EventArgs e)
    {
        Project p = new Project();
        int index = PriceOfferGV.SelectedIndex;
        int CustomerID = Convert.ToInt32(dt.Rows[index].ItemArray[0]);
        p.pID = Convert.ToInt32(dt.Rows[index].ItemArray[1]);
        p.Name = ProjectName.Value;
        p.Comments = ProjectComments.Text;
        p.InstallationDate = DateTime.ParseExact(CustomerBackDate.Value, "MM/dd/yyyy", null);
        int ProjectStatusID = Convert.ToInt32(ProjectStatus.SelectedValue);
        string MobilePhone = CustomerMobilePhone.Value;
        p.UpdateUndecidedCustomerDetails(p, ProjectStatusID, CustomerID, MobilePhone);
    }
}