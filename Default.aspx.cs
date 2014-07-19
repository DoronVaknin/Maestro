using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Url.Query == "?Reason=UserNotAuthenticated")
            ErrorLBL.InnerText = "יש להתחבר לפני כניסה למערכת";
        else if (Request.Url.Query == "?Reason=UserNotAllowed")
            ErrorLBL.InnerText = "אינך מורשה להיכנס למערכת";
        UserNameTB.Focus();
    }

    protected void LoginBTN_Click(object sender, EventArgs e)
    {
        string username = UserNameTB.Value;
        string password = PasswordTB.Value;
        if (Membership.ValidateUser(username, password))
        {
            //bool isWorker = User.IsInRole("Worker");
            //if (isWorker) // Used to block worker attempts from login to the system
            //    Response.Redirect("~/Default.aspx?Reason=UserNotAllowed");
            //else
            //{
            FormsAuthentication.SetAuthCookie(username, false);
            RedirectToHomePage(username);
            //}
        }
        else
            ErrorLBL.InnerText = "שם משתמש או סיסמא לא נכונים";
    }

    public void RedirectToHomePage(string UserName)
    {
        bool Admin = UserName.Equals("Admin", StringComparison.InvariantCultureIgnoreCase);
        bool InstallationsManager = UserName.Equals("ShimonY", StringComparison.InvariantCultureIgnoreCase);
        bool SalesManager = UserName.Equals("MaliY", StringComparison.InvariantCultureIgnoreCase);
        bool TechnicalManager = UserName.Equals("BettiY", StringComparison.InvariantCultureIgnoreCase);

        if (Admin || InstallationsManager)
            Response.Redirect("~/HomeInstallations.aspx");
        else if (SalesManager)
            Response.Redirect("~/HomeSales.aspx");
        else if (TechnicalManager)
            Response.Redirect("~/HomeTechnical.aspx");
        else
            Response.Redirect("~/HomeInstallations.aspx");
    }
}

