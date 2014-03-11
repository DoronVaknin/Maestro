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
        ServiceCall sc = new ServiceCall();
        sc.CustomerName1 = CustomerName.Text;
        sc.CellPhone1 = Convert.ToInt32( CustomerPhone.Text);
        sc.Adress1 = CustomerAdress.Text;
        sc.Description1 = ErrorDescription.Text;
        sc.Area1 =Convert.ToInt32( AreaTB.SelectedItem.Value);
        if (IsUrgent.Checked)
        {
            sc.IsUrgent1 = true;
        }
        else
            sc.IsUrgent1 = false;
        if (IsWarranty.Checked)
            sc.Warranty1 = true;
        else
            sc.Warranty1 = false;

        //sc.insert(this);
    }
}

