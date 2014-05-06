using System;
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
        if (Session["Customer"] != null)
        {
            Customer c = new Customer();
            c = (Customer)Session["Customer"];
            ProjectName.Value = c.Fname + " " + c.Lname;
        }
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
        Project p = new Project(DateTime.ParseExact(ProjectDateOpened.Value, "MM/dd/yyyy", null), DateTime.ParseExact(ProjectExpirationDate.Value, "MM/dd/yyyy", null), DateTime.ParseExact(ProjectInstallationDate.Value, "MM/dd/yyyy", null), ProjectName.Value, ProjectComments.Value, ProjectCost.Value, ProjectHatches.Value, ProjectArchitectName.Value, ProjectArchitectMobile.Value, ProjectContractorName.Value, ProjectContractorMobile.Value, ProjectSupervisorName.Value, ProjectSupervisorMobile.Value);
        //שמירת מספר תעודת זהות של הלקוח  במשתנה
        Int32 CustomerID = 0;
        if (Session["Customer"] != null)
        {
            Customer c = new Customer();
            c = (Customer)Session["Customer"];
            CustomerID = c.cID;
            p.InsertNewProject(p, CustomerID);
        }
        Response.Redirect("~/Projects.aspx");
    }
}