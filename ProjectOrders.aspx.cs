using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }

    public void LoadSuppliers()
    {
        Project p = new Project();
        p.LoadSuppliers(ShutterProvider, CollectedProvider, ValimProvider, UProvider, ShoeingProvider, EngineProvider, ProtectedSpaceProvider, GlassProvider, BoxesProvider);
    }
}