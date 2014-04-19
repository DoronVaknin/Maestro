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
        DataTable CustomerDT = p.GetCustomerInformation(ProjectID);
        DataTable ProjectDT = p.GetProjectDetails(ProjectID);
        //הצבה של כל פרטי הלקוח בשדות המתאימים
        SetOrdersGrid();
        ProjectIDHolder.Value = Convert.ToString(row.Cells[1].Text);

        if (!Page.IsPostBack)
        {
            SetPageDetails(CustomerDT, ProjectDT);
            SetProjCurrentStatus();
            LoadSuppliers();
        }
        else
            DisableAllFields();
    }

    public void DisableAllFields()
    {
        //Customer Fields
        ProjectInfoID.Attributes.Add("disabled", "disabled");
        ProjectInfoFirstName.Attributes.Add("disabled", "disabled");
        ProjectInfoLastName.Attributes.Add("disabled", "disabled");
        ProjectInfoPhone.Attributes.Add("disabled", "disabled");
        ProjectInfoMobile.Attributes.Add("disabled", "disabled");
        ProjectInfoAddress.Attributes.Add("disabled", "disabled");
        ProjectInfoCity.Attributes.Add("disabled", "disabled");
        ProjectInfoEmail.Attributes.Add("disabled", "disabled");
        ProjectInfoFax.Attributes.Add("disabled", "disabled");
        ProjectInfoArea.Attributes.Add("disabled", "disabled");

        //Project Fields
        ProjectInfoStatus.Attributes.Add("disabled", "disabled");
        ProjectInfoName.Attributes.Add("disabled", "disabled");
        ProjectInfoHatches.Attributes.Add("disabled", "disabled");
        ProjectInfoCost.Attributes.Add("disabled", "disabled");
        ProjectInfoComments.Attributes.Add("disabled", "disabled");
        ProjectInfoDateOpened.Attributes.Add("disabled", "disabled");
        ProjectInfoExpirationDate.Attributes.Add("disabled", "disabled");
        ProjectInfoInstallationDate.Attributes.Add("disabled", "disabled");
        ProjectInfoArchitectName.Attributes.Add("disabled", "disabled");
        ProjectInfoArchitectMobile.Attributes.Add("disabled", "disabled");
        ProjectInfoContractorName.Attributes.Add("disabled", "disabled");
        ProjectInfoContractorMobile.Attributes.Add("disabled", "disabled");
        ProjectInfoSupervisorName.Attributes.Add("disabled", "disabled");
        ProjectInfoSupervisorMobile.Attributes.Add("disabled", "disabled");
    }

    public void SetOrdersGrid()
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        Project p = new Project();
        DataTable OrdersDataTable = p.GetOrderDetails(Convert.ToInt32(row.Cells[1].Text));
        DataTable statustable = p.GetOrderStatus(); // להחליט מה לעשות
        OrderGrid.DataSource = OrdersDataTable;
        OrderGrid.DataBind();
    }

    public void SetProjCurrentStatus()
    {
        Project p = new Project();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        string StatusNum = Convert.ToString(row.Cells[5].Text);
        int StatusNumber = p.GetStatusNumber(StatusNum);
        ProjectInfoStatus.SelectedIndex = (StatusNumber - 1);
    }

    public void SetPageDetails(DataTable C_dt, DataTable P_dt)
    {
        //Populate customer details
        ProjectInfoID.Text = C_dt.Rows[0].ItemArray[0].ToString();
        ProjectInfoFirstName.Text = C_dt.Rows[0].ItemArray[1].ToString();
        ProjectInfoLastName.Text = C_dt.Rows[0].ItemArray[2].ToString();
        ProjectInfoPhone.Text = C_dt.Rows[0].ItemArray[3].ToString();
        ProjectInfoMobile.Text = C_dt.Rows[0].ItemArray[4].ToString();
        ProjectInfoFax.Text = C_dt.Rows[0].ItemArray[5].ToString();
        ProjectInfoAddress.Text = C_dt.Rows[0].ItemArray[6].ToString();
        ProjectInfoCity.Text = C_dt.Rows[0].ItemArray[7].ToString();
        ProjectInfoEmail.Text = C_dt.Rows[0].ItemArray[8].ToString();
        ProjectInfoArchitectName.Text = C_dt.Rows[0].ItemArray[9].ToString();
        ProjectInfoArchitectMobile.Text = C_dt.Rows[0].ItemArray[10].ToString();
        ProjectInfoContractorName.Text = C_dt.Rows[0].ItemArray[11].ToString();
        ProjectInfoContractorMobile.Text = C_dt.Rows[0].ItemArray[12].ToString();
        ProjectInfoSupervisorName.Text = C_dt.Rows[0].ItemArray[13].ToString();
        ProjectInfoSupervisorMobile.Text = C_dt.Rows[0].ItemArray[14].ToString();
        ProjectInfoArea.SelectedValue = C_dt.Rows[0].ItemArray[15].ToString();

        //Populate project details
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        ProjectInfoStatus.Text = P_dt.Rows[0].ItemArray[27].ToString();
        ProjectInfoName.Text = P_dt.Rows[0].ItemArray[1].ToString();
        ProjectInfoHatches.Text = row.Cells[8].Text;
        ProjectInfoCost.Text = P_dt.Rows[0].ItemArray[6].ToString();
        ProjectInfoComments.Text = P_dt.Rows[0].ItemArray[2].ToString();
        ProjectInfoDateOpened.Value = ((DateTime)P_dt.Rows[0].ItemArray[3]).ToString("MM/dd/yyyy");
        ProjectInfoExpirationDate.Value = ((DateTime)P_dt.Rows[0].ItemArray[4]).ToString("MM/dd/yyyy");
        ProjectInfoInstallationDate.Value = ((DateTime)P_dt.Rows[0].ItemArray[5]).ToString("MM/dd/yyyy");
        ProjectInfoContractorName.Text = P_dt.Rows[0].ItemArray[7].ToString();
        ProjectInfoContractorMobile.Text = P_dt.Rows[0].ItemArray[8].ToString();
        ProjectInfoArchitectName.Text = P_dt.Rows[0].ItemArray[9].ToString();
        ProjectInfoArchitectMobile.Text = P_dt.Rows[0].ItemArray[10].ToString();
        ProjectInfoSupervisorName.Text = P_dt.Rows[0].ItemArray[11].ToString();
        ProjectInfoSupervisorMobile.Text = P_dt.Rows[0].ItemArray[12].ToString();
        DateOpened.Text = ProjectInfoDateOpened.Value;
        warranty.Text = ProjectInfoExpirationDate.Value;
    }

    protected void SaveCustomerDetailsBTN_Click1(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.SaveCustomerNewDetails(ProjectInfoFirstName.Text, ProjectInfoLastName.Text, ProjectInfoPhone.Text, ProjectInfoMobile.Text, ProjectInfoFax.Text, ProjectInfoAddress.Text, ProjectInfoCity.Text, ProjectInfoEmail.Text, Convert.ToInt32(ProjectInfoArea.SelectedValue), Convert.ToInt32(ProjectInfoID.Text));
        SaveCustomerDetailsBTN.Style.Add("display", "none");
        EditCustomerDetailsBTN.Style.Add("display", "inline-block");
    }

    protected void SaveProjectDetailsBTN_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        Project p = new Project();
        p.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectInfoStatus.SelectedIndex + 1);
        p.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToDouble(ProjectInfoCost.Text), ProjectInfoName.Text, ProjectInfoComments.Text, ProjectInfoArchitectName.Text, ProjectInfoArchitectMobile.Text, ProjectInfoContractorName.Text, ProjectInfoContractorMobile.Text, ProjectInfoSupervisorName.Text, ProjectInfoSupervisorMobile.Text, DateTime.ParseExact(ProjectInfoExpirationDate.Value, "MM/dd/yyyy", null), DateTime.ParseExact(ProjectInfoInstallationDate.Value, "MM/dd/yyyy", null));
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
}



