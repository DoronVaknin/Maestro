using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["ProjectNameForProjectOrders"] != null)
            {
                string ProjectName = Session["ProjectNameForProjectOrders"].ToString();
                ModalHeader.InnerHtml = PageHeader.InnerHtml = "הזמנות עבור הפרויקט " + ProjectName;
            }
            LoadSuppliers();
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ResetCreateOrderForm();", true);
        DisableOrderDetailsFields();
        if (Page.IsPostBack)
            SetupQuickSearch(null, null);
        foreach (GridViewRow GVR in ProjectOrdersGV.Rows)
        {
            if (GVR.Cells[8].Text == "מתעכב")
                GVR.Cells[8].Attributes.Add("class", "Late");
        }
    }

    protected void LoadSuppliers()
    {
        Project p = new Project();
        p.LoadSuppliers(ShutterProvider, CollectedProvider, AluminiumProvider, ValimProvider, UProvider, ShoeingProvider, EngineProvider, ProtectedSpaceProvider, GlassProvider, BoxesProvider);
    }

    protected void CreateOrderHiddenBTN_Click(object sender, EventArgs e)
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
        ProjectOrdersGV.DataBind();
    }

    protected void CreateOrder(int Count, string SupplierID, int RawMeterialID, string EstArrDate)
    {
        if (Count > 0 && Session["ProjectIDForProjectOrders"] != null)
        {
            string ProjectID = Session["ProjectIDForProjectOrders"].ToString();
            DateTime InstallationDate;
            InstallationDate = EstArrDate == "" ?
                               InstallationDate = DateTime.MinValue : DateTime.ParseExact(EstArrDate, "MM/dd/yyyy", null);
            Order o = new Order(Convert.ToInt32(ProjectID), Convert.ToInt32(SupplierID), RawMeterialID, Count, InstallationDate);
            o.CreateNewOrder(o);
        }
    }

    protected void ProjectOrdersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalOrderDetails", "ActivateModal('ModalOrderDetails');", true);
        ProjectOrderID.Text = ProjectOrdersGV.SelectedRow.Cells[2].Text;
        ProjectOrderDateOpened.Text = ProjectOrdersGV.SelectedRow.Cells[3].Text;
        ProjectOrderItemName.Text = ProjectOrdersGV.SelectedRow.Cells[4].Text;
        ProjectOrderEstimatedDOA.Text = DateTime.ParseExact(ProjectOrdersGV.SelectedRow.Cells[5].Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        ProjectOrderQuantity.Text = ProjectOrdersGV.SelectedRow.Cells[6].Text;
        ProjectOrderSupplier.Text = ProjectOrdersGV.SelectedRow.Cells[7].Text;
        ListItem li = ProjectOrderStatus.Items.FindByText(ProjectOrdersGV.SelectedRow.Cells[8].Text);
        ProjectOrderStatus.SelectedValue = li.Value;
        ProjectOrdersGV.SelectedIndex = -1;
    }

    protected void SaveOrderDetailsBTN_Click(object sender, EventArgs e)
    {
        Order o = new Order();
        int oID = Convert.ToInt32(ProjectOrderID.Text);
        int Quantity = Convert.ToInt32(ProjectOrderQuantity.Text);
        int OrderStatus = Convert.ToInt32(ProjectOrderStatus.SelectedValue);
        DateTime EstimatedDOA = DateTime.ParseExact(ProjectOrderEstimatedDOA.Text, "MM/dd/yyyy", null);
        int RowAffected = o.UpdateOrderDetails(oID, Quantity, OrderStatus, EstimatedDOA);
        ProjectOrdersGV.SelectedIndex = -1;
    }

    public void DisableOrderDetailsFields()
    {
        ProjectOrderID.Attributes.Add("disabled", "disabled");
        ProjectOrderDateOpened.Attributes.Add("disabled", "disabled");
        ProjectOrderItemName.Attributes.Add("disabled", "disabled");
        ProjectOrderEstimatedDOA.Attributes.Add("disabled", "disabled");
        ProjectOrderQuantity.Attributes.Add("disabled", "disabled");
        ProjectOrderSupplier.Attributes.Add("disabled", "disabled");
        ProjectOrderStatus.Attributes.Add("disabled", "disabled");
    }

    protected void SetupQuickSearch(object sender, EventArgs e)
    {
        if (ProjectOrdersGV.Rows.Count > 0)
        {
            GridViewRow row = ProjectOrdersGV.Rows[0];
            for (int i = 0; i < ProjectOrdersGV.Columns.Count; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = ProjectOrdersGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                ProjectOrdersGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}