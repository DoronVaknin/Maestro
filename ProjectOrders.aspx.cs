using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ProjectNameForProjectOrders"] != null)
        {
            string sProjectName = Session["ProjectNameForProjectOrders"].ToString();
            PageHeader.InnerHtml = "הזמנות עבור הפרויקט " + sProjectName;
        }
        LoadSuppliers();
        //SetOrdersGrid();
    }

    public void LoadSuppliers()
    {
        Project p = new Project();
        p.LoadSuppliers(ShutterProvider, CollectedProvider, ValimProvider, UProvider, ShoeingProvider, EngineProvider, ProtectedSpaceProvider, GlassProvider, BoxesProvider);
    }

    public void CreateOrderBTN_Click(object sender, EventArgs e)
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

            //SetOrdersGrid();
        }
    }

    public void SetOrdersGrid()
    {
        string ProjectID = Session["ProjectIDForProjectOrders"].ToString();
        Project p = new Project();
        DataTable OrdersDT = p.GetOrderDetails(Convert.ToInt32(ProjectID));
        //DataTable StatusTable = p.GetOrderStatus(); // להחליט מה לעשות
        OrdersGV.DataSource = OrdersDT;
        OrdersGV.DataBind();
    }
    protected void OrdersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('ModalOrderDetails')", true);
        GridViewRow row = OrdersGV.SelectedRow;
        //OrderStausDDL.SelectedItem = 2;
    }
}