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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.Cid = Convert.ToInt32(CustomerId.Text);
        c.Fname = CustomerFirstName.Text;
        c.Lname = CustomerLastName.Text;
        c.City = CustomerCity.Text;
        c.Address = CustomerAddress.Text;
        if (CustomerPhone.Text != "")
            c.Phone = Convert.ToInt32(CustomerPhone.Text);
        else
            c.Phone = 0;
        if (CustomerCellPhone.Text != "")
            c.Mobile = Convert.ToInt32(CustomerCellPhone.Text);
        else
            c.Mobile = 0;
        if (CustomerFaxNumber.Text != "")
            c.Fax = Convert.ToInt32(CustomerFaxNumber.Text);
        else
            c.Fax = 0;
        c.Email = CustomerEmail.Text;
        if (CustomerArea.Text != "")
            c.Region = Convert.ToInt32(CustomerArea.Text);
        else
            c.Region = 0;
        DBservices db = new DBservices();
        db.insertcustomer(c);



    }
}