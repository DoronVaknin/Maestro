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
            ModalHeader.InnerHtml = PageHeader.InnerHtml = "הזמנות עבור הפרויקט " + sProjectName;
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
        CreateOrder(Convert.ToInt32(ValimCount.Text), Convert.ToString(ValimProvider.SelectedValue), 2);
        CreateOrder(Convert.ToInt32(ShoeingCount.Text), Convert.ToString(ShoeingProvider.SelectedValue), 3);
        CreateOrder(Convert.ToInt32(ProtectedSpaceCount.Text), Convert.ToString(ProtectedSpaceProvider.SelectedValue), 4);
        CreateOrder(Convert.ToInt32(BoxesCount.Text), Convert.ToString(BoxesProvider.SelectedValue), 5);
        CreateOrder(Convert.ToInt32(CollectedCount.Text), Convert.ToString(CollectedProvider.SelectedValue), 6);
        CreateOrder(Convert.ToInt32(UCount.Text), Convert.ToString(UProvider.SelectedValue), 7);
        CreateOrder(Convert.ToInt32(EngineCount.Text), Convert.ToString(EngineProvider.SelectedValue), 8);
        CreateOrder(Convert.ToInt32(GlassCount.Text), Convert.ToString(GlassProvider.SelectedValue), 9);
    }

    public void CreateOrder(int Count, string Supplier, int RawMeterialID)
    {
        if (Count > 0)
        {
            string ProjectID = Session["ProjectIDForProjectOrders"].ToString();
            DBservices db = new DBservices();
            Order o = new Order(Convert.ToInt32(ProjectID), Supplier, RawMeterialID, Count);

            OrdersGV.DataBind();
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
}