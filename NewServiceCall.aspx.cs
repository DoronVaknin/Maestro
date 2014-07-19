using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Customer"] != null)
        {
            Customer c = new Customer();
            c = (Customer)Session["Customer"];
            string fNname = c.Fname;
            string lName = c.Lname;
            PageHeader.InnerHtml = "קריאת שירות עבור הלקוח " + fNname + " " + lName;
        }
        else if (Session["ProjectNameForServiceCall"] != null)
        {
            string pName = Session["ProjectNameForServiceCall"].ToString();
            PageHeader.InnerHtml = "קריאת שירות עבור הפרויקט " + pName;
        }
        ServiceCallDateOpened.Value = (DateTime.Today).ToString("MM/dd/yyyy");
        ServiceCallProblemDesc.Focus();
    }

    protected void CreateServiceCallExternal_Click(object sender, EventArgs e)
    {
        if (Request.Url.Query == "?Source=NewCustomer")
        {
            ServiceCall sc = new ServiceCall(DateTime.ParseExact(ServiceCallDateOpened.Value, "MM/dd/yyyy", null), ServiceCallProblemDesc.Text, ServiceCallUrgentCall.Checked);
            Customer c = new Customer();
            c = (Customer)Session["Customer"];
            sc.InsertExternalServiceCall(sc, c.cID);
        }
        else
        {
            if (Session["ProjectIDForServiceCall"] != null)
            {
                ServiceCall sc = new ServiceCall(DateTime.ParseExact(ServiceCallDateOpened.Value, "MM/dd/yyyy", null), ServiceCallProblemDesc.Text, ServiceCallUrgentCall.Checked);
                int pID = Convert.ToInt32(Session["ProjectIDForServiceCall"]);
                Project p = new Project();
                DataTable dt = p.GetCustomerInformation(pID);
                string cID = dt.Rows[0].ItemArray[0].ToString();
                sc.InsertServiceCallExistingProject(sc, cID, pID);
            }
        }
        Response.Redirect("~/ServiceCalls.aspx");
    }
}

