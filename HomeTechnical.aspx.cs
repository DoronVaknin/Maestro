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
        SetupWorstSuppliersTable();
        SetupBestSuppliersTable();
    }

    public void SetupWorstSuppliersTable()
    {
        Supplier s = new Supplier();
        DataTable dt = new DataTable();
        dt = s.GetSuppliersRankTable();
        int RowsNum = dt.Rows.Count;
        if (RowsNum == 0) return; // Prevent conversion error
        int TotalDaysLate = Convert.ToInt32(dt.Compute("SUM(DaysLate)", "DaysLate > 0"));
        for (int i = 0; i < RowsNum; i++)
        {
            int DaysLate = 0;
            if (!System.DBNull.Value.Equals(dt.Rows[0].ItemArray[1]))
            {
                DaysLate = Convert.ToInt32(dt.Rows[0].ItemArray[1]);
                if (DaysLate < 0 || dt.Rows.Count > 3) //Takes only 3 latest suppliers' orders
                {
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();
                }
            }
            else
            {
                dt.Rows[0].Delete();
                dt.AcceptChanges();
            }
        }
        DataColumn dc = new DataColumn();
        dt.Columns.Add(dc);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            double Percent = 0;
            Percent = Math.Round(100 * Convert.ToDouble(dr.ItemArray[1]) / TotalDaysLate, 2);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BuildLateProgressBar" + i, "BuildLateProgressBar(" + Percent + "," + i + ");", true);
        }

        if (dt.Rows.Count > 0)
        {
            dt.Columns[0].ColumnName = "שם הספק";
            dt.Columns[1].ColumnName = "מס' איחורים (ימים)";
            dt.Columns[2].ColumnName = "איחורים (%)";
            WorstSuppliersGV.DataSource = dt;
            WorstSuppliersGV.DataBind();
        }
    }

    public void SetupBestSuppliersTable()
    {
        Supplier s = new Supplier();
        DataTable dt = new DataTable();
        dt = s.GetSuppliersRankTable();
        int RowsNum = dt.Rows.Count;
        if (RowsNum == 0) return; // Prevent conversion error
        int TotalDaysEarly = Math.Abs(Convert.ToInt32(dt.Compute("SUM(DaysLate)", "DaysLate < 0")));
        while (System.DBNull.Value.Equals(dt.Rows[0].ItemArray[1]))
        {
            dt.Rows[0].Delete(); // Clean before sorting
            dt.AcceptChanges();
        }
        for (int i = 0; i < RowsNum; i++)
        {
            if (!System.DBNull.Value.Equals(dt.Rows[dt.Rows.Count - 1].ItemArray[1]))
            {
                int DaysLate = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1].ItemArray[1]);
                if (DaysLate > 0 || dt.Rows.Count > 3) //Takes only 3 earliest suppliers' orders
                {
                    dt.Rows[dt.Rows.Count - 1].Delete();
                    dt.AcceptChanges();
                }
            }
        }
        DataColumn dc = new DataColumn();
        dt.Columns.Add(dc);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            dr[1] = Math.Abs(Convert.ToDouble(dr.ItemArray[1]));
            double Percent = Math.Round(100 * Convert.ToDouble(dr.ItemArray[1]) / TotalDaysEarly, 2);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "BuildEarlyProgressBar" + i, "BuildEarlyProgressBar(" + Percent + "," + i + ");", true);
        }

        if (dt.Rows.Count > 0)
        {
            dt.Columns[0].ColumnName = "שם הספק";
            dt.Columns[1].ColumnName = "מס' הקדמות (ימים)";
            dt.Columns[2].ColumnName = "הקדמות (%)";
            BestSuppliersGV.DataSource = dt;
            BestSuppliersGV.DataBind();
        }
    }
}