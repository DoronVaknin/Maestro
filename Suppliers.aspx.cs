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
        DisableSupplierDetailsFields();
    }

    public void DisableSupplierDetailsFields()
    {
        SupplierName.Attributes.Add("disabled", "disabled");
        SupplierAddress.Attributes.Add("disabled", "disabled");
        SupplierEmail.Attributes.Add("disabled", "disabled");
        SupplierPhone.Attributes.Add("disabled", "disabled");
        SupplierCellPhone.Attributes.Add("disabled", "disabled");
        SupplierFax.Attributes.Add("disabled", "disabled");
    }

    protected void SaveSupplierDetailsBTN_Click(object sender, EventArgs e)
    {
        int SupplierID = Convert.ToInt32(SuppliersGV.SelectedRow.Cells[2].Text);
        Supplier s = new Supplier(SupplierID, SupplierName.Value, SupplierAddress.Value, SupplierPhone.Value, SupplierCellPhone.Value, SupplierFax.Value, SupplierEmail.Value);
        int RowAffected = s.UpdateSupplierDetails();
    }
    protected void SuppliersGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ActivateModal('EditSupplierModal');", true);
        SupplierName.Value = SuppliersGV.SelectedRow.Cells[3].Text;
        SupplierAddress.Value = SuppliersGV.SelectedRow.Cells[4].Text;
        SupplierPhone.Value = SuppliersGV.SelectedRow.Cells[5].Text;
        SupplierCellPhone.Value = SuppliersGV.SelectedRow.Cells[6].Text;
        SupplierFax.Value = SuppliersGV.SelectedRow.Cells[7].Text;
        SupplierEmail.Value = SuppliersGV.SelectedRow.Cells[8].Text;
    }
}