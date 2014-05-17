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
        if (Session["ProjectID"] != null)
        {
            int ProjectID = Convert.ToInt32(Session["ProjectID"]);

            Project p = new Project();
            DataTable DetailsTable = p.GetAllDetails(ProjectID);

            if (!Page.IsPostBack)
            {
                SetPageDetails(DetailsTable);
                //SetProjCurrentStatus();
                LoadSuppliers();
            }
            else
                DisableAllFields();

            string ProjectName = ProjectInfoName.Text;
            ModalHeader.InnerHtml = PageHeader.InnerHtml = "הזמנות עבור הפרויקט " + ProjectName;
        }
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
        if (Session["ProjectID"] != null)
        {
            int ProjectID = Convert.ToInt32(Session["ProjectID"]);
            Project p = new Project();
            DataTable OrdersDataTable = p.GetOrderDetails(ProjectID);
            DataTable statustable = p.GetOrderStatus(); // להחליט מה לעשות
            OrdersGV.DataSource = OrdersDataTable;
            OrdersGV.DataBind();
        }
    }

    //public void SetProjCurrentStatus()
    //{
    //    Project p = new Project();
    //    GridViewRow row = (GridViewRow)Session["ProjectID"];
    //    string StatusNum = Convert.ToString(row.Cells[5].Text);
    //    int StatusNumber = p.GetStatusNumber(StatusNum);
    //    ProjectInfoStatus.SelectedIndex = (StatusNumber - 1);
    //}

    public void SetPageDetails(DataTable DetailsTable)
    {
        //Populate customer details
        ProjectInfoID.Text = DetailsTable.Rows[0].ItemArray[0].ToString();
        ProjectInfoFirstName.Text = DetailsTable.Rows[0].ItemArray[1].ToString();
        ProjectInfoLastName.Text = DetailsTable.Rows[0].ItemArray[2].ToString();
        ProjectInfoAddress.Text = DetailsTable.Rows[0].ItemArray[3].ToString();
        ProjectInfoPhone.Text = DetailsTable.Rows[0].ItemArray[4].ToString();
        ProjectInfoMobile.Text = DetailsTable.Rows[0].ItemArray[5].ToString();
        ProjectInfoFax.Text = DetailsTable.Rows[0].ItemArray[6].ToString();
        ProjectInfoEmail.Text = DetailsTable.Rows[0].ItemArray[7].ToString();
        ProjectInfoArea.SelectedValue = DetailsTable.Rows[0].ItemArray[21].ToString();

        //Populate project details
        ProjectInfoName.Text = DetailsTable.Rows[0].ItemArray[8].ToString();
        ProjectInfoStatus.SelectedValue = DetailsTable.Rows[0].ItemArray[9].ToString();
        ProjectInfoArchitectName.Text = DetailsTable.Rows[0].ItemArray[11].ToString();
        ProjectInfoArchitectMobile.Text = DetailsTable.Rows[0].ItemArray[12].ToString();
        ProjectInfoContractorName.Text = DetailsTable.Rows[0].ItemArray[13].ToString();
        ProjectInfoContractorMobile.Text = DetailsTable.Rows[0].ItemArray[14].ToString();
        ProjectInfoSupervisorName.Text = DetailsTable.Rows[0].ItemArray[15].ToString();
        ProjectInfoSupervisorMobile.Text = DetailsTable.Rows[0].ItemArray[16].ToString();
        ProjectInfoHatches.Text = DetailsTable.Rows[0].ItemArray[22].ToString();
        ProjectInfoCost.Text = DetailsTable.Rows[0].ItemArray[10].ToString();
        ProjectInfoComments.Text = DetailsTable.Rows[0].ItemArray[20].ToString();
        ProjectInfoDateOpened.Value = ((DateTime)DetailsTable.Rows[0].ItemArray[17]).ToString("MM/dd/yyyy");
        ProjectInfoExpirationDate.Value = ((DateTime)DetailsTable.Rows[0].ItemArray[18]).ToString("MM/dd/yyyy");
        ProjectInfoInstallationDate.Value = ((DateTime)DetailsTable.Rows[0].ItemArray[19]).ToString("MM/dd/yyyy");
    }

    protected void SaveCustomerDetailsBTN_Click1(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.SaveCustomerNewDetails(ProjectInfoFirstName.Text, ProjectInfoLastName.Text, ProjectInfoPhone.Text, ProjectInfoMobile.Text, ProjectInfoFax.Text, ProjectInfoAddress.Text, ProjectInfoEmail.Text, Convert.ToInt32(ProjectInfoArea.SelectedValue), Convert.ToInt32(ProjectInfoID.Text));
        SaveCustomerDetailsBTN.Style.Add("display", "none");
        EditCustomerDetailsBTN.Style.Add("display", "inline-block");
    }

    protected void SaveProjectDetailsBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            GridViewRow row = (GridViewRow)Session["ProjectID"];
            Project p = new Project();
            p.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectInfoStatus.SelectedIndex + 1);
            p.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToDouble(ProjectInfoCost.Text), ProjectInfoName.Text, ProjectInfoComments.Text, ProjectInfoArchitectName.Text, ProjectInfoArchitectMobile.Text, ProjectInfoContractorName.Text, ProjectInfoContractorMobile.Text, ProjectInfoSupervisorName.Text, ProjectInfoSupervisorMobile.Text, DateTime.ParseExact(ProjectInfoExpirationDate.Value, "MM/dd/yyyy", null), DateTime.ParseExact(ProjectInfoInstallationDate.Value, "MM/dd/yyyy", null));
            SaveProjectDetailsBTN.Style.Add("display", "none");
            EditProjectDetailsBTN.Style.Add("display", "inline-block");
        }
    }

    protected void LoadSuppliers()
    {
        Project p = new Project();
        p.LoadSuppliers(ShutterProvider, CollectedProvider, AluminiumProvider, ValimProvider, UProvider, ShoeingProvider, EngineProvider, ProtectedSpaceProvider, GlassProvider, BoxesProvider);
    }

    protected void CreateOrderBTN_Click(object sender, EventArgs e)
    {
        CreateOrder(Convert.ToInt32(ShutterCount.Text), ShutterProvider.SelectedValue, 1, ShutterEstArrDate.Text);
        CreateOrder(Convert.ToInt32(CollectedCount.Text), CollectedProvider.SelectedValue, 2, CollectedEstArrDate.Text);
        CreateOrder(Convert.ToInt32(AluminiumCount.Text), AluminiumProvider.SelectedValue, 3, AluminiumEstArrDate.Text);
        CreateOrder(Convert.ToInt32(ValimCount.Text), ValimProvider.SelectedValue, 4, ValimEstArrDate.Text);
        CreateOrder(Convert.ToInt32(UCount.Text), UProvider.SelectedValue, 5, UEstArrDate.Text);
        CreateOrder(Convert.ToInt32(ShoeingCount.Text), ShoeingProvider.SelectedValue, 6, ShoeingEstArrDate.Text);
        CreateOrder(Convert.ToInt32(EngineCount.Text), EngineProvider.SelectedValue, 7, EngineEstArrDate.Text);
        CreateOrder(Convert.ToInt32(ProtectedSpaceCount.Text), ProtectedSpaceProvider.SelectedValue, 8, ProtectedSpaceEstArrDate.Text);
        CreateOrder(Convert.ToInt32(GlassCount.Text), GlassProvider.SelectedValue, 9, GlassEstArrDate.Text);
        CreateOrder(Convert.ToInt32(BoxesCount.Text), BoxesProvider.SelectedValue, 10, BoxesEstArrDate.Text);
        OrdersGV.DataBind();
    }

    protected void CreateOrder(int Count, string SupplierID, int RawMeterialID, string EstArrDate)
    {
        if (Count > 0 && Session["ProjectID"] != null)
        {
            string ProjectID = Session["ProjectID"].ToString();
            DBservices db = new DBservices();
            Order o = new Order(Convert.ToInt32(ProjectID), Convert.ToInt32(SupplierID), RawMeterialID, Count, DateTime.ParseExact(EstArrDate, "MM/dd/yyyy", null));
            o.CreateNewOrder(o);

            //OrdersGV.DataBind();
            //SetOrdersGrid();
        }
    }

    protected void OrdersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('ModalOrderDetails')", true);
        GridViewRow row = OrdersGV.SelectedRow;
        //OrderStatusDDL.SelectedItem = 2;
    }
}



