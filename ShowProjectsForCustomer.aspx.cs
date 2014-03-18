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
        int id = Convert.ToInt32(Session["ID"]);
        string fname = Convert.ToString(Session["FirstName"]);
        string lname = Convert.ToString(Session["LastName"]);
        Header.InnerHtml = "פרויקטים עבור הלקוח " + fname + " " + lname;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow NewRow = GridView1.SelectedRow;
        Session["ProjectID"] = NewRow.Cells[1].Text;
        Session["ProjectStatus"] = NewRow.Cells[5].Text;
        Response.Redirect("ProjectInformation.aspx");
    }

 
}