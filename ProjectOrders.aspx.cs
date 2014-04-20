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
        LoadSuppliers();
    }

    public void LoadSuppliers()
    {
        Project p = new Project();
        p.LoadSuppliers(ShutterProvider, CollectedProvider, ValimProvider, UProvider, ShoeingProvider, EngineProvider, ProtectedSpaceProvider, GlassProvider, BoxesProvider);
    }
}