using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c = (Customer)Session["Customer"];
        string fNname = c.Fname;
        string lName = c.Lname;
        PageHeader.InnerHtml = "קריאת שירות עבור הלקוח " + fNname + " " + lName;
        ServiceCallDateOpened.Value = (DateTime.Today).ToString("MM/dd/yyyy");
    }

    protected void CreateServiceCallExternal_Click(object sender, EventArgs e)
    {
        ServiceCall sc = new ServiceCall(DateTime.ParseExact(ServiceCallDateOpened.Value, "MM/dd/yyyy", null), ServiceCallProblemDesc.Text, ServiceCallUrgentCall.Checked);
        Customer c = new Customer();
        c = (Customer)Session["Customer"];
        sc.InsertExternalServiceCall(sc, c.cID);
    }
}

