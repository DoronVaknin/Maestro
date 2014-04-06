using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.WebControls;


public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        int ProjectID = Convert.ToInt32(row.Cells[1].Text);
        Project p = new Project();
        //שליפת נתוני הלקוח להצגה
        DataTable dt = p.GetCustomerInformation(ProjectID);
        //הצבה של כל פרטי הלקוח בשדות המתאימים
        SetOrdersGrid();
        ProjectIDHolder.Value = Convert.ToString(row.Cells[1].Text);

        if (!Page.IsPostBack)
        {
            SetPageDetails(dt);
            SetProjCurrentStatus();
            LoadSuppliers();
        }
    }

    public void SetOrdersGrid()
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        Project p = new Project();
        DataTable OrdersDataTable = p.GetOrderDetails(Convert.ToInt32(row.Cells[1].Text));
        DataTable statustable = p.GetOrderStatus();

    }


    public void SetProjCurrentStatus()
    {
        Project p = new Project();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        string status = Convert.ToString(row.Cells[5].Text);
        int statusnumber = p.GetStatusNumber(status);
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
        ProjectInfoSupervisorName.Value = Convert.ToString(dt.Rows[0].ItemArray[13]);
        ProjectInfoSupervisorPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[14]);
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        ProjectInfoHatches.Value = row.Cells[8].Text;
        ProjectInfoPrice.Value = row.Cells[7].Text;
        ProjectInfoComments.Value = row.Cells[6].Text;
    }

    protected void DropDownDataBound(object sender, EventArgs e)
    {

    }

    protected void SaveCustomerDetailsBTN_Click1(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.SaveCustomerNewDetails(ProjectInfoFirstName.Value, ProjectInfoLastName.Value, ProjectInfoPhone.Value, ProjectInfoMobile.Value, ProjectInfoFax.Value, ProjectInfoAddress.Value, ProjectInfoCity.Value, ProjectInfoEmail.Value, Convert.ToInt32(ProjectInfoID.Value));
        SaveCustomerDetailsBTN.Style.Add("display", "none");
        EditCustomerDetailsBTN.Style.Add("display", "inline-block");
    }

    protected void SaveProjectDetailsBTN_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        Project p = new Project();
        p.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectInfoStatus.SelectedIndex + 1);
        p.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToInt32(ProjectInfoPrice.Value), ProjectInfoComments.Value);
        SaveProjectDetailsBTN.Style.Add("display", "none");
        EditProjectDetailsBTN.Style.Add("display", "inline-block");
    }

    public void LoadSuppliers()
    {
        Project p = new Project();
        p.LoadSuppliers(ShutterProvider, CollectedProvider, ValimProvider, UProvider, ShoeingProvider, EngineProvider, ProtectedSpaceProvider, GlassProvider, BoxesProvider);

    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        CreateOrder(Convert.ToInt32(ShutterCount.Text), Convert.ToString(ShutterProvider.SelectedValue), 1);

    }

    public void CreateOrder(int Count, string Supplier, int RawMeterialID)
    {
        if (Count > 0)
        {
            GridViewRow row = (GridViewRow)Session["selectedrow"];
            DBservices db = new DBservices();
            Order o = new Order(Convert.ToInt32(row.Cells[1].Text), Supplier, RawMeterialID, Count);

            SetOrdersGrid();
        }
    }

    //בדיקה- למחוק
    protected void OrdersGridView_RowEdit(object sender, EventArgs e)
    {
        Response.Write("kuku");
    }







}



