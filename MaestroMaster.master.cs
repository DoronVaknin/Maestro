using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MaestroMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Page.User.Identity.Name)
        {
            case "Admin": Welcome.InnerText = "שלום, אדמין"; break;
            case "ShimonY": Welcome.InnerText = "שלום, שמעון"; break;
            case "MaliY": Welcome.InnerText = "שלום, מלי"; break;
            case "BettiY": Welcome.InnerText = "שלום, בטי"; break;
            default: break;
        }
    }

    protected void LogoutBTN_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Default.aspx");
    }
}
