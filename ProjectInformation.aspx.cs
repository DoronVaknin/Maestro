using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        int ProjectID =Convert.ToInt32(row.Cells[1].Text);

        DBservices db = new DBservices();
        DataTable dt = db.GetCustomerInformation(ProjectID);

        if (!Page.IsPostBack)
        {
            SetPageDetails(dt);
            SetProjCurrentStatus();
            
        }

    }

    public void SetProjCurrentStatus()
    {
        DBservices db = new DBservices();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        string status = Convert.ToString(row.Cells[5].Text);
        int statusnumber = db.StatusNumber(status);
        ProjectStatusDDL.SelectedIndex = (statusnumber - 1);
    }

    public void SetPageDetails(DataTable dt)
    {
        txtID.Value = Convert.ToString(dt.Rows[0].ItemArray[0]);
        txtFirstName.Value = Convert.ToString(dt.Rows[0].ItemArray[1]);
        txtLastName.Value = Convert.ToString(dt.Rows[0].ItemArray[2]);
        txtCustomerPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[3]);
        txtCustomerMobile.Value = Convert.ToString(dt.Rows[0].ItemArray[4]);
        txtCustomerFax.Value = Convert.ToString(dt.Rows[0].ItemArray[5]);
        txtAdress.Value = Convert.ToString(dt.Rows[0].ItemArray[6]);
        txtCity.Value = Convert.ToString(dt.Rows[0].ItemArray[7]);
        txtEmail.Value = Convert.ToString(dt.Rows[0].ItemArray[8]);
        txtArchitectName.Value = Convert.ToString(dt.Rows[0].ItemArray[9]);
        txtArchitectMobile.Value = Convert.ToString(dt.Rows[0].ItemArray[10]);
        txtContractorName.Value = Convert.ToString(dt.Rows[0].ItemArray[11]);
        txtContractorPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[12]);
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        txtHatchesNum.Value = row.Cells[8].Text;
        txtProjectPrice.Value = row.Cells[7].Text;
        txtProjectComment.Value = row.Cells[6].Text;
        
        
    }

    protected void DropDownDataBound(object sender, EventArgs e)
    {

    }

    


    protected void SaveCustomerNewInformation_Click1(object sender, EventArgs e)
    {
        int CustomerInitialID = Convert.ToInt32(txtID.Value);
        DBservices db = new DBservices();
        Customer c = new Customer();
        c.Cid = Convert.ToInt32(txtID.Value);
        c.Fname = txtFirstName.Value;
        c.Lname = txtLastName.Value;
        c.Phone = Convert.ToInt32(txtCustomerPhone.Value);
        c.Mobile = Convert.ToInt32(txtCustomerMobile.Value);
        c.Fax = Convert.ToInt32(txtCustomerFax.Value);
        c.Address = txtAdress.Value;
        c.City = txtCity.Value;
        c.Email = txtEmail.Value;
        db.UpdateCustomerInformation(c, CustomerInitialID);
    }




    protected void btnSaveProjDetails_Click(object sender, EventArgs e)
    {
        DBservices db = new DBservices();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        db.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectStatusDDL.SelectedIndex + 1);
        db.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToInt32(txtProjectPrice.Value), txtProjectComment.Value);
    }

}

