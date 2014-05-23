using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier s = new Supplier();
        DataTable dt = new DataTable();
        dt = s.GetSuppliersRankTable();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int DaysLate = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
            if (DaysLate < 0 || dt.Rows.Count > 5) //Takes only 5 latest suppliers' orders
            {
                dt.Rows[i].Delete();
                dt.AcceptChanges();
            }
        }
        DataColumn dc = new DataColumn();
        dt.Columns.Add(dc);

        int TotalDaysLate = Convert.ToInt32(dt.Compute("SUM(DaysLate)", ""));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            double Percent = Math.Round(100 * Convert.ToDouble(dr.ItemArray[1]) / TotalDaysLate, 2);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BuildProgressBar" + i, "BuildProgressBar(" + Percent + "," + i + ");", true);
        }

        if (dt.Rows.Count > 0)
        {
            dt.Columns[0].ColumnName = "שם הספק";
            dt.Columns[1].ColumnName = "איחור מצטבר (ימים)";
            dt.Columns[2].ColumnName = "איחורים (%)";
            SuppliersRankGV.DataSource = dt;
            SuppliersRankGV.DataBind();
        }
    }
}