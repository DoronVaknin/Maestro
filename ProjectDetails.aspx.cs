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
        string StatusNum = Convert.ToString(row.Cells[5].Text);
        int StatusNumber = p.GetStatusNumber(StatusNum);
        ProjectInfoStatus.SelectedIndex = (StatusNumber - 1);
    }

    public void SetPageDetails(DataTable dt)
    {
        ProjectInfoID.Text = dt.Rows[0].ItemArray[0].ToString();
        ProjectInfoFirstName.Text = dt.Rows[0].ItemArray[1].ToString();
        ProjectInfoLastName.Text = dt.Rows[0].ItemArray[2].ToString();
        ProjectInfoPhone.Text = dt.Rows[0].ItemArray[3].ToString();
        ProjectInfoMobile.Text = dt.Rows[0].ItemArray[4].ToString();
        ProjectInfoFax.Text = dt.Rows[0].ItemArray[5].ToString();
        ProjectInfoAddress.Text = dt.Rows[0].ItemArray[6].ToString();
        ProjectInfoCity.Text = dt.Rows[0].ItemArray[7].ToString();
        ProjectInfoEmail.Text = dt.Rows[0].ItemArray[8].ToString();
        ProjectInfoArchitectName.Text = dt.Rows[0].ItemArray[9].ToString();
        ProjectInfoArchitectMobile.Text = dt.Rows[0].ItemArray[10].ToString();
        ProjectInfoContractorName.Text = dt.Rows[0].ItemArray[11].ToString();
        ProjectInfoContractorMobile.Text = dt.Rows[0].ItemArray[12].ToString();
        ProjectInfoSupervisorName.Text = dt.Rows[0].ItemArray[13].ToString();
        ProjectInfoSupervisorMobile.Text = dt.Rows[0].ItemArray[14].ToString();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        ProjectInfoHatches.Text = row.Cells[8].Text;
        ProjectInfoCost.Text = row.Cells[7].Text;
        ProjectInfoComments.Text = row.Cells[6].Text;
    }

    protected void DropDownDataBound(object sender, EventArgs e)
    {

    }

    protected void SaveCustomerDetailsBTN_Click1(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.SaveCustomerNewDetails(ProjectInfoFirstName.Text, ProjectInfoLastName.Text, ProjectInfoPhone.Text, ProjectInfoMobile.Text, ProjectInfoFax.Text, ProjectInfoAddress.Text, ProjectInfoCity.Text, ProjectInfoEmail.Text, Convert.ToInt32(ProjectInfoID.Text));
        SaveCustomerDetailsBTN.Style.Add("display", "none");
        EditCustomerDetailsBTN.Style.Add("display", "inline-block");
    }

    protected void SaveProjectDetailsBTN_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        Project p = new Project();
        p.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectInfoStatus.SelectedIndex + 1);
        p.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToDouble(ProjectInfoCost.Text), ProjectInfoComments.Text, ProjectInfoArchitectName.Text, ProjectInfoArchitectMobile.Text, ProjectInfoContractorName.Text, ProjectInfoContractorMobile.Text, ProjectInfoSupervisorName.Text, ProjectInfoSupervisorMobile.Text);
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



