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
        if (!Page.IsPostBack)
        {
            if (Session["ProjectNameForProjectOrders"] != null)
            {
                string sProjectName = Session["ProjectNameForProjectOrders"].ToString();
                ModalHeader.InnerHtml = PageHeader.InnerHtml = "הזמנות עבור הפרויקט " + sProjectName;
            }
            LoadSuppliers();
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ResetCreateOrderForm()", true);
        //SetOrdersGrid();
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
        if (Count > 0 && Session["ProjectIDForProjectOrders"] != null)
        {
            string ProjectID = Session["ProjectIDForProjectOrders"].ToString();
            DBservices db = new DBservices();
            Order o = new Order(Convert.ToInt32(ProjectID), Convert.ToInt32(SupplierID), RawMeterialID, Count, DateTime.ParseExact(EstArrDate, "MM/dd/yyyy", null));
            o.CreateNewOrder(o);

            //OrdersGV.DataBind();
            //SetOrdersGrid();
        }
    }

    //public void SetOrdersGrid()
    //{
    //    string ProjectID = Session["ProjectIDForProjectOrders"].ToString();
    //    Project p = new Project();
    //    DataTable OrdersDT = p.GetOrderDetails(Convert.ToInt32(ProjectID));
    //    //DataTable StatusTable = p.GetOrderStatus(); // להחליט מה לעשות
    //    OrdersGV.DataSource = OrdersDT;
    //    OrdersGV.DataBind();
    //}
    protected void OrdersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('ModalOrderDetails')", true);
        GridViewRow row = OrdersGV.SelectedRow;
        //OrderStausDDL.SelectedItem = 2;
    }
}