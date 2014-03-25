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
        ProjectDate.Value = DateTime.Now.ToShortDateString();
    }
    protected void CreateProject_Click(object sender, EventArgs e)
    {
        Project p = new Project();
        p.OpenedDate1 = Convert.ToDateTime(ProjectDate.Value);
        p.ExpirationDate1 = p.OpenedDate1.AddDays(60);
        p.Comment1 = ProjectComments.InnerText;
        if (ProjectPrice.Value != "")
            p.Price = Convert.ToInt16(ProjectPrice.Value);
        if (ProjectHatches.Value != "")
            p.HatchesNum1 = Convert.ToInt32(ProjectHatches.Value);
        p.ArchitectName1 = ProjectArchitectName.Value;
        if (ProjectArchitectPhone.Value != "")
            p.ArchitectPhone1 = Convert.ToInt32(ProjectArchitectPhone.Value);
        p.ContractorName1 = ProjectContractorName.Value;
        if (ProjectContractorPhone.Value != "")
            p.ContractorPhone1 = Convert.ToInt32(ProjectContractorPhone.Value);
        p.SupervisorName1 = ProjectSupervisorName.Value;
        if (ProjectSupervisorPhone.Value != "")
            p.SupervisorPhone1 = Convert.ToInt32(ProjectSupervisorPhone.Value);
        Customer TempCustomer = new Customer();
        if (Session["customer"] != null)
        {
            TempCustomer = (Customer)Session["customer"];
            int id = TempCustomer.Cid;
        }
        DBservices db = new DBservices();
        db.InsertProjectInfo(p, TempCustomer.Cid);
        int projectid = db.findid(TempCustomer.Cid);
        db.CreateHatches(p, projectid);
    }
}