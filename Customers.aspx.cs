﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Customers_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ID"] = CustomersTBL.SelectedRow.Cells[1].Text;
        Session["FirstName"] = CustomersTBL.SelectedRow.Cells[2].Text;
        Session["LastName"] = CustomersTBL.SelectedRow.Cells[3].Text;
        Response.Redirect("ProjectsPerCustomer.aspx");
    }
}