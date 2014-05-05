using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Server.ClearError();
        Response.Redirect("Error.aspx");
    }
    protected void CreateCustomerForProject_Click(object sender, EventArgs e)
    {
        Customer c = new Customer(Convert.ToInt32(CustomerId.Value), CustomerFirstName.Value, CustomerLastName.Value, CustomerAddress.Value, CustomerEmail.Value, Convert.ToInt32(CustomerArea.Text));
        c.SetPhones(CustomerPhone.Value, CustomerCellPhone.Value, CustomerFaxNumber.Value);
        Session["Customer"] = c;
        int RowsAffected = c.InsertNewCustomer();
        if (RowsAffected > 0)
            Response.Redirect("NewProject.aspx?Source=NewCustomer");
        else
            Response.Write("לא ניתן לשמור את הלקוח");
    }

    protected void CreateCustomerForServiceCall_Click(object sender, EventArgs e)
    {
        Customer c = new Customer(Convert.ToInt32(CustomerId.Value), CustomerFirstName.Value, CustomerLastName.Value, CustomerAddress.Value, CustomerEmail.Value, Convert.ToInt32(CustomerArea.Text));
        c.SetPhones(CustomerPhone.Value, CustomerCellPhone.Value, CustomerFaxNumber.Value);
        Session["Customer"] = c;
        int RowsAffected = c.InsertNewCustomer();
        if (RowsAffected > 0)
            Response.Redirect("NewServiceCall.aspx?Source=NewCustomer");
        else
            Response.Write("לא ניתן לשמור את הלקוח");
    }
}