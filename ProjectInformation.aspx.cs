﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ProjectID = Convert.ToInt16(Session["ProjectID"]);
        DBservices db = new DBservices();
        DataTable dt = db.GetCustomerInformation(ProjectID);

        if (!Page.IsPostBack)
        {
            SetAsReadOnly();
            SetCustomerValues(dt);
        }

    }

    public void SetAsReadOnly()
    {
        txtAdress.Attributes.Add("disabled" , "disabled");
        txtArchitectMobile.Attributes.Add("disabled", "disabled");
        txtArchitectName.Attributes.Add("disabled", "disabled");
        txtCity.Attributes.Add("disabled", "disabled");
        txtContractorMobile.Attributes.Add("disabled", "disabled");
        txtContractorName.Attributes.Add("disabled", "disabled");
        txtCustomerFax.Attributes.Add("disabled", "disabled");
        txtCustomerMobile.Attributes.Add("disabled", "disabled");
        txtCustomerPhone.Attributes.Add("disabled", "disabled");
        txtEmail.Attributes.Add("disabled", "disabled");
        txtFirstName.Attributes.Add("disabled", "disabled");
        txtID.ReadOnly = true;
        txtLastName.Attributes.Add("disabled", "disabled");
    }

    public void SetCustomerValues(DataTable dt)
    {
        txtID.Text = Convert.ToString(dt.Rows[0].ItemArray[0]);
        txtFirstName.Text = Convert.ToString(dt.Rows[0].ItemArray[1]);
        txtLastName.Text = Convert.ToString(dt.Rows[0].ItemArray[2]);
        txtCustomerPhone.Text = Convert.ToString(dt.Rows[0].ItemArray[3]);
        txtCustomerMobile.Text = Convert.ToString(dt.Rows[0].ItemArray[4]);
        txtCustomerFax.Text = Convert.ToString(dt.Rows[0].ItemArray[5]);
        txtAdress.Text = Convert.ToString(dt.Rows[0].ItemArray[6]);
        txtCity.Text = Convert.ToString(dt.Rows[0].ItemArray[7]);
        txtEmail.Text = Convert.ToString(dt.Rows[0].ItemArray[8]);
        txtArchitectName.Text = Convert.ToString(dt.Rows[0].ItemArray[9]);
        txtArchitectMobile.Text = Convert.ToString(dt.Rows[0].ItemArray[10]);
        txtContractorName.Text = Convert.ToString(dt.Rows[0].ItemArray[11]);
        txtContractorMobile.Text = Convert.ToString(dt.Rows[0].ItemArray[12]);

    }


    protected void SaveCustomerNewInformation_Click1(object sender, EventArgs e)
    {
        int CustomerInitialID = Convert.ToInt32(txtID.Text);
        DBservices db = new DBservices();
        Customer c = new Customer();
        c.Cid = Convert.ToInt32(txtID.Text);
        c.Fname = txtFirstName.Text;
        c.Lname = txtLastName.Text;
        c.Phone = Convert.ToInt32(txtCustomerPhone.Text);
        c.Mobile = Convert.ToInt32(txtCustomerMobile.Text);
        c.Fax = Convert.ToInt32(txtCustomerFax.Text);
        c.Address = txtAdress.Text;
        c.City = txtCity.Text;
        c.Email = txtEmail.Text;
        db.UpdateCustomerInformation(c, CustomerInitialID);
    }

    protected void DropDownDataBound(object sender, EventArgs e)
    {
        DBservices db = new DBservices();
        string status = Convert.ToString(Session["ProjectStatus"]);
        int statusnumber = db.StatusNumber(status);
        ProjectStatusDDL.SelectedIndex = (statusnumber - 1);
    }

    protected void ProjectStatusDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        int x;
    }
}

