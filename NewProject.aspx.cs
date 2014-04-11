﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Notification about customer that was added to database
        string CustomerFirstName = Request.QueryString["CustomerFirstName"];
        string CustomerLastName = Request.QueryString["CustomerLastName"];
        string CustomerWasAdded = "הלקוח" + " " + CustomerFirstName + " " + CustomerLastName + " " + "נוסף בהצלחה";
        CustomerLabel.Text = CustomerWasAdded;
        ProjectDateOpened.Value = (DateTime.Today).ToString("MM/dd/yyyy");
        ProjectExpirationDate.Value = (DateTime.Now.AddYears(7)).ToString("MM/dd/yyyy");
    }

    protected void CreateProject_Click(object sender, EventArgs e)
    {
        string filename = "";
        if (ProjectFiles.HasFile)
        {
            filename = Path.GetFileName(ProjectFiles.FileName);
            ProjectFiles.SaveAs(Server.MapPath("~/files/") + filename);
        }
        Project p = new Project(DateTime.ParseExact(ProjectDateOpened.Value, "MM/dd/yyyy", null), DateTime.ParseExact(ProjectExpirationDate.Value, "MM/dd/yyyy", null), ProjectComments.Value, ProjectPrice.Value, ProjectHatches.Value, ProjectArchitectName.Value, ProjectArchitectPhone.Value, ProjectContractorName.Value, ProjectContractorPhone.Value, ProjectSupervisorName.Value, ProjectSupervisorPhone.Value);
        //שמירת מספר תעודת זהות של הלקוח  במשתנה
        Int32 CustomerID = 0;
        if (Session["CustomerID"] != null)
            CustomerID = Convert.ToInt32(Session["CustomerID"]);
        p.InsertNewProject(p, CustomerID);
        Response.Redirect("~/Projects.aspx");
    }
}