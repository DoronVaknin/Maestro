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
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Project p = new Project();
        p.OpenedDate1 = Calendar1.SelectedDate;
        p.ExpirationDate1 =p.OpenedDate1.AddDays(60);
        p.Comment1 = txtComments.Text;
        if (txtPrice.Text != "")
            p.Price = Convert.ToInt16(txtPrice.Text);
        if (txtHathes.Text != "")
            p.HatchesNum1 = Convert.ToInt32(txtHathes.Text);
        p.ArchitectName1 = txtArchitectName.Text;
        if (txtArchitectPhone.Text != "")
            p.ArchitectPhone1 = Convert.ToInt32(txtArchitectPhone.Text);
        p.ContractorName1 = txtContractorName.Text;
        if (txtContractorPhone.Text != "")
            p.ContractorPhone1 = Convert.ToInt32(txtContractorPhone.Text);
        p.SupervisorName1 = txtSupervisorName.Text;
        if (txtSupervisorName.Text != "")
            p.SupervisorPhone1 = Convert.ToInt32(txtSupervisorPhone.Text);
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