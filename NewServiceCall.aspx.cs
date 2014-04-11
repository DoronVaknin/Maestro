using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ServiceCall sc = new ServiceCall();
        sc.CustomerName = CustomerName.Text;
        sc.Mobile = Convert.ToInt32(CustomerPhone.Text);
        sc.Address = CustomerAddress.Text;
        sc.Description = ErrorDescription.Text;
        sc.Area = Convert.ToInt32(AreaTB.SelectedItem.Value);
        if (IsUrgent.Checked)
            sc.Urgent = true;
        else
            sc.Urgent = false;
        if (IsWarranty.Checked)
            sc.Warranty = true;
        else
            sc.Warranty = false;

        //sc.insert(this);
    }
}

