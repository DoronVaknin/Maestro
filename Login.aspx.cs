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

    }

    protected void LoginBTN_Click(object sender, EventArgs e)
    {
        string username = UserNameTB.Value;
        string password = PasswordTB.Value;
        if (Membership.ValidateUser(username, password))
        {
            FormsAuthentication.SetAuthCookie(username, false);
            Response.Redirect("~/Default.aspx");
        }
        else
            ErrorLBL.InnerText = "שם משתמש או סיסמא לא נכונים";
    }
}

