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

    protected void CreateSupplierHiddenBTN_Click(object sender, EventArgs e)
    {
        Supplier s = new Supplier(SupplierName.Value, SupplierAddress.Value, SupplierPhone.Value, SupplierCellPhone.Value, SupplierFax.Value, SupplierEmail.Value);
        int RowAffected = s.InsertNewSupplier();
        if (RowAffected > 0)
            Response.Redirect("Suppliers.aspx");
        else
            Response.Write("לא ניתן לשמור את הספק");
    }
}