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

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateCustomer_Click(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.Cid = Convert.ToInt32(CustomerId.Value);
        c.Fname = CustomerFirstName.Value;
        c.Lname = CustomerLastName.Value;
        c.City = CustomerCity.Value;
        c.Address = CustomerAddress.Value;
        if (CustomerPhone.Value != "")
            c.Phone = Convert.ToInt32(CustomerPhone.Value);
        else
            c.Phone = 0;
        if (CustomerCellPhone.Value != "")
            c.Mobile = Convert.ToInt32(CustomerCellPhone.Value);
        else
            c.Mobile = 0;
        if (CustomerFaxNumber.Value != "")
            c.Fax = Convert.ToInt32(CustomerFaxNumber.Value);
        else
            c.Fax = 0;
        c.Email = CustomerEmail.Value;
        if (CustomerArea.Text != "")
            c.Region = Convert.ToInt32(CustomerArea.Text);
        else
            c.Region = 0;
        DBservices db = new DBservices();
        try
        {
            db.insertcustomer(c);
            Session["customer"] = c;
            Response.Redirect("NewProject.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}