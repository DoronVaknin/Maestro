using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;

public partial class MaestroMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //bool isAuthenticted = HttpContext.Current.User.Identity.IsAuthenticated;
        //bool isWorker = HttpContext.Current.User.IsInRole("Worker");
        //if (!isAuthenticted) // Used to block attempts to get in to the system without authentication
        //    Response.Redirect("~/Default.aspx?Reason=UserNotAuthenticated");
        //else if (isWorker) // Used to block worker attempts from login to the system
        //    Response.Redirect("~/Default.aspx?Reason=UserNotAllowed");

        UserNameHolder.Value = Page.User.Identity.Name; //Used to wire Home & Logo to the right homepage
        switch (Page.User.Identity.Name)
        {
            case "Admin": Welcome.InnerText = "שלום, אדמין"; break;
            case "ShimonY": Welcome.InnerText = "שלום, שמעון"; break;
            case "MaliY": Welcome.InnerText = "שלום, מלי"; break;
            case "BettiY": Welcome.InnerText = "שלום, בטי"; break;
            default: break;
        }
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "expirescript", "ActivateCountdown();", true);
    }

    protected void LogoutBTN_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Default.aspx");
    }

    protected void ChooseProjectForServiceCallBTN_Click(object sender, EventArgs e)
    {
        Session["ProjectIDForServiceCall"] = ProjectNamesDDL.SelectedValue;
        Session["ProjectNameForServiceCall"] = ProjectNamesDDL.SelectedItem;
        Response.Redirect("~/NewServiceCall.aspx?Source=ExistingProject");
    }

    protected void ChooseProjectForProjectOrdersBTN_Click(object sender, EventArgs e)
    {
        Session["ProjectIDForProjectOrders"] = ProjectNamesDDL.SelectedValue;
        Session["ProjectNameForProjectOrders"] = ProjectNamesDDL.SelectedItem;
        Response.Redirect("~/ProjectOrders.aspx");
    }

    protected void ChooseProjectForProjectHatchesBTN_Click(object sender, EventArgs e)
    {
        Session["ProjectIDForProjectHatches"] = ProjectNamesDDL.SelectedValue;
        Session["ProjectNameForProjectHatches"] = ProjectNamesDDL.SelectedItem;
        Response.Redirect("~/ProjectHatches.aspx");
    }

    protected void ChooseSupplierForSupplierOrdersBTN_Click(object sender, EventArgs e)
    {
        Session["SupplierIDForSupplierOrders"] = SupplierNamesDDL.SelectedValue;
        Session["SupplierNameForSupplierOrders"] = SupplierNamesDDL.SelectedItem;
        Response.Redirect("~/SupplierOrders.aspx");
    }

}