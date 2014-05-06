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
    protected void OrdersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('OrdersModal')", true);
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
            int value = OrderStatusDDL.SelectedIndex+1;
            Order o = new Order();
            o.UpdateOrderStatus(value,Convert.ToInt32(OrdersGV.SelectedRow.Cells[3].Text));
    }
}