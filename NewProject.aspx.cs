using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTB.Value = DateTime.Now.ToShortDateString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Project p = new Project();
        p.OpenedDate1 = Convert.ToDateTime(DateTB.Value);
        p.ExpirationDate1 = p.OpenedDate1.AddDays(60);
        p.Comment1 = txtComments.InnerText;
        if (txtPrice.Value != "")
            p.Price = Convert.ToInt16(txtPrice.Value);
        if (txtHathes.Value != "")
            p.HatchesNum1 = Convert.ToInt32(txtHathes.Value);
        p.ArchitectName1 = txtArchitectName.Value;
        if (txtArchitectPhone.Value != "")
            p.ArchitectPhone1 = Convert.ToInt32(txtArchitectPhone.Value);
        p.ContractorName1 = txtContractorName.Value;
        if (txtContractorPhone.Value != "")
            p.ContractorPhone1 = Convert.ToInt32(txtContractorPhone.Value);
        p.SupervisorName1 = txtSupervisorName.Value;
        if (txtSupervisorName.Value != "")
            p.SupervisorPhone1 = Convert.ToInt32(txtSupervisorPhone.Value);
        Customer temp = new Customer();
        if (Session["customer"] != null)
        {
            temp = (Customer)Session["customer"];
            int id = temp.Cid;
        }
        DBservices db = new DBservices();
        db.InsertProjectInfo(p, temp.Cid);
        int projectid = db.findid(temp.Cid);
        db.CreateHatches(p, projectid);
    }
}