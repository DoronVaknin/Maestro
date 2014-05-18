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
        if (!Page.IsPostBack)
        {
            if (Session["SupplierNameForSupplierOrders"] != null)
            {
                string SupplierName = Session["SupplierNameForSupplierOrders"].ToString();
                ModalHeader.InnerHtml = PageHeader.InnerHtml = "הזמנות עבור הספק " + SupplierName;
            }
        }
        DisableOrderDetailsFields();
        if (Page.IsPostBack)
            OnDataBound(null, null);
    }

    protected void SupplierOrdersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('EditSupplierOrderModal');", true);
        SupplierOrderID.Text = SupplierOrdersGV.SelectedRow.Cells[1].Text;
        SupplierOrderDateOpened.Text = SupplierOrdersGV.SelectedRow.Cells[2].Text;
        SupplierOrderItemName.Text = SupplierOrdersGV.SelectedRow.Cells[3].Text;
        SupplierOrderEstimatedDOA.Text = (Convert.ToDateTime(SupplierOrdersGV.SelectedRow.Cells[4].Text)).ToString("MM/dd/yyyy");
        SupplierOrderQuantity.Text = SupplierOrdersGV.SelectedRow.Cells[5].Text;
        SupplierOrderProject.Text = SupplierOrdersGV.SelectedRow.Cells[6].Text;
        ListItem li = SupplierOrderStatus.Items.FindByText(SupplierOrdersGV.SelectedRow.Cells[7].Text);
        SupplierOrderStatus.SelectedValue = li.Value;
    }

    protected void SaveOrderDetailsBTN_Click(object sender, EventArgs e)
    {
        Order o = new Order();
        int oID = Convert.ToInt32(SupplierOrderID.Text);
        int Quantity = Convert.ToInt32(SupplierOrderQuantity.Text);
        int OrderStatus = Convert.ToInt32(SupplierOrderStatus.SelectedValue);
        DateTime EstimatedDOA = DateTime.ParseExact(SupplierOrderEstimatedDOA.Text, "MM/dd/yyyy", null);
        int RowAffected = o.UpdateOrderDetails(oID, Quantity, OrderStatus, EstimatedDOA);
    }

    public void DisableOrderDetailsFields()
    {
        SupplierOrderID.Attributes.Add("disabled", "disabled");
        SupplierOrderDateOpened.Attributes.Add("disabled", "disabled");
        SupplierOrderItemName.Attributes.Add("disabled", "disabled");
        SupplierOrderEstimatedDOA.Attributes.Add("disabled", "disabled");
        SupplierOrderQuantity.Attributes.Add("disabled", "disabled");
        SupplierOrderProject.Attributes.Add("disabled", "disabled");
        SupplierOrderStatus.Attributes.Add("disabled", "disabled");
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        if (SupplierOrdersGV.Rows.Count > 0)
        {
            GridViewRow row = SupplierOrdersGV.Rows[0];
            for (int i = 0; i < SupplierOrdersGV.Columns.Count; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = SupplierOrdersGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                SupplierOrdersGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}