using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisableSupplierDetailsFields();
        if (Page.IsPostBack)
            SetupQuickSearch(null, null);
    }

    public void DisableSupplierDetailsFields()
    {
        SupplierName.Attributes.Add("disabled", "disabled");
        SupplierAddress.Attributes.Add("disabled", "disabled");
        SupplierEmail.Attributes.Add("disabled", "disabled");
        SupplierPhone.Attributes.Add("disabled", "disabled");
        SupplierCellPhone.Attributes.Add("disabled", "disabled");
        SupplierFax.Attributes.Add("disabled", "disabled");
        //SupplierStatus.Attributes.Add("disabled", "disabled");
    }

    protected void SaveSupplierDetailsBTN_Click(object sender, EventArgs e)
    {
        int SupplierID = Convert.ToInt32(SuppliersGV.SelectedRow.Cells[1].Text);
        Supplier s = new Supplier(SupplierID, SupplierName.Value, SupplierAddress.Value, SupplierPhone.Value, SupplierCellPhone.Value, SupplierFax.Value, SupplierEmail.Value, true);
        int RowAffected = s.UpdateSupplierDetails();
    }
    protected void SuppliersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('EditSupplierModal');", true);
        SupplierName.Value = SuppliersGV.SelectedRow.Cells[2].Text;
        SupplierAddress.Value = SuppliersGV.SelectedRow.Cells[3].Text;
        SupplierPhone.Value = SuppliersGV.SelectedRow.Cells[4].Text;
        SupplierCellPhone.Value = SuppliersGV.SelectedRow.Cells[5].Text;
        SupplierFax.Value = SuppliersGV.SelectedRow.Cells[6].Text;
        SupplierEmail.Value = SuppliersGV.SelectedRow.Cells[7].Text;
        //SupplierStatus.Checked = true;
    }

    protected void SetupQuickSearch(object sender, EventArgs e)
    {
        if (SuppliersGV.Rows.Count > 0)
        {
            GridViewRow row = SuppliersGV.Rows[0];
            for (int i = 0; i < SuppliersGV.Columns.Count; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = SuppliersGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                SuppliersGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }

    protected void EnableSupplierHiddenBTN_Click(object sender, EventArgs e)
    {
        int SupplierID = Convert.ToInt32(SuppliersGV.SelectedRow.Cells[1].Text);
        Supplier s = new Supplier();
        s.DisableSupplier(SupplierID, true);
        SuppliersGV.DataBind();
    }
}