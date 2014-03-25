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
        int ProjectID = Convert.ToInt32(row.Cells[1].Text);

        DBservices db = new DBservices();
        DataTable dt = db.GetCustomerInformation(ProjectID);

        if (!Page.IsPostBack)
        {
            SetPageDetails(dt);
            SetProjCurrentStatus();
        }
        LoadSuppliers();
        GetOrderStatus();
    }

    public void SetProjCurrentStatus()
    {
        DBservices db = new DBservices();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        string status = Convert.ToString(row.Cells[5].Text);
        int statusnumber = db.StatusNumber(status);
        ProjectInfoStatus.SelectedIndex = (statusnumber - 1);
    }

    public void SetPageDetails(DataTable dt)
    {
        ProjectInfoID.Value = Convert.ToString(dt.Rows[0].ItemArray[0]);
        ProjectInfoFirstName.Value = Convert.ToString(dt.Rows[0].ItemArray[1]);
        ProjectInfoLastName.Value = Convert.ToString(dt.Rows[0].ItemArray[2]);
        ProjectInfoPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[3]);
        ProjectInfoMobile.Value = Convert.ToString(dt.Rows[0].ItemArray[4]);
        ProjectInfoFax.Value = Convert.ToString(dt.Rows[0].ItemArray[5]);
        ProjectInfoAddress.Value = Convert.ToString(dt.Rows[0].ItemArray[6]);
        ProjectInfoCity.Value = Convert.ToString(dt.Rows[0].ItemArray[7]);
        ProjectInfoEmail.Value = Convert.ToString(dt.Rows[0].ItemArray[8]);
        ProjectInfoArchitectName.Value = Convert.ToString(dt.Rows[0].ItemArray[9]);
        ProjectInfoArchitectMobile.Value = Convert.ToString(dt.Rows[0].ItemArray[10]);
        ProjectInfoContractorName.Value = Convert.ToString(dt.Rows[0].ItemArray[11]);
        ProjectInfoContractorPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[12]);
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        ProjectInfoHatches.Value = row.Cells[8].Text;
        ProjectInfoPrice.Value = row.Cells[7].Text;
        ProjectInfoComments.Value = row.Cells[6].Text;
    }

    protected void DropDownDataBound(object sender, EventArgs e)
    {

    }

    protected void SaveCustomerDetails_Click1(object sender, EventArgs e)
    {
        int CustomerInitialID = Convert.ToInt32(ProjectInfoID.Value);
        DBservices db = new DBservices();
        Customer c = new Customer();
        c.Cid = Convert.ToInt32(ProjectInfoID.Value);
        c.Fname = ProjectInfoFirstName.Value;
        c.Lname = ProjectInfoLastName.Value;
        c.Phone = Convert.ToInt32(ProjectInfoPhone.Value);
        c.Mobile = Convert.ToInt32(ProjectInfoMobile.Value);
        c.Fax = Convert.ToInt32(ProjectInfoFax.Value);
        c.Address = ProjectInfoAddress.Value;
        c.City = ProjectInfoCity.Value;
        c.Email = ProjectInfoEmail.Value;
        db.UpdateCustomerInformation(c, CustomerInitialID);
    }

    protected void SaveProjectDetails_Click(object sender, EventArgs e)
    {
        DBservices db = new DBservices();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        db.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectInfoStatus.SelectedIndex + 1);
        db.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToInt32(ProjectInfoPrice.Value), ProjectInfoComments.Value);
    }

    public void LoadSuppliers()
    {
        DBservices db = new DBservices();
        db.LoadSuppliers(ShutterProvider, 1);
        db.LoadSuppliers(CollectedProvider, 2);
        db.LoadSuppliers(ValimProvider, 3);
        db.LoadSuppliers(UProvider, 4);
        db.LoadSuppliers(ShoeingProvider, 5);
        db.LoadSuppliers(EngineProvider, 6);
        db.LoadSuppliers(ProtectedSpaceProvider, 7);
        db.LoadSuppliers(GlassProvider, 8);
        db.LoadSuppliers(BoxesProvider, 9);
    }

    public void GetOrderStatus()
    {
        DBservices db = new DBservices();
        db.GetOrderStatus(ShutterAmount);
        db.GetOrderStatus(CollectedAmount);
        db.GetOrderStatus(ValimAmount);
        db.GetOrderStatus(Uamount);
        db.GetOrderStatus(ShoeingAmount);
        db.GetOrderStatus(EnginesAmount);
        db.GetOrderStatus(ProtectedSpaceAmount);
        db.GetOrderStatus(GlassAmount);
        db.GetOrderStatus(BoxAmount);
    }
}

