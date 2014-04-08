using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;
    SqlConnection con;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int insertcustomer(Customer c)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertNewCustomer]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@cid", c.Cid);
                sqlComm.Parameters.AddWithValue("@fname", c.Fname);
                sqlComm.Parameters.AddWithValue("@lname", c.Lname);
                sqlComm.Parameters.AddWithValue("@city", c.City);
                sqlComm.Parameters.AddWithValue("@address", c.Address);
                sqlComm.Parameters.AddWithValue("@phone", c.Phone);
                sqlComm.Parameters.AddWithValue("@mobile", c.Mobile);
                sqlComm.Parameters.AddWithValue("@fax", c.Fax);
                sqlComm.Parameters.AddWithValue("@email", c.Email);
                sqlComm.Parameters.AddWithValue("@region", c.Region);
                sqlComm.CommandTimeout = 600;
                int RowsAffected = sqlComm.ExecuteNonQuery();
                return RowsAffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int InsertProjectInfo(Project p, int CId)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertProjectInfo]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@id", CId);
                sqlComm.Parameters.AddWithValue("@date", p.OpenedDate1);
                sqlComm.Parameters.AddWithValue("@exdate", p.ExpirationDate1.AddDays(60));
                sqlComm.Parameters.AddWithValue("@price", p.Price);
                sqlComm.Parameters.AddWithValue("@comment", p.Comment1);
                sqlComm.Parameters.AddWithValue("@ContName", p.ContractorName1);
                sqlComm.Parameters.AddWithValue("@contPhone", p.ContractorPhone1);
                sqlComm.Parameters.AddWithValue("@ArchName", p.ArchitectName1);
                sqlComm.Parameters.AddWithValue("@ArchPhone", p.ArchitectPhone1);
                sqlComm.Parameters.AddWithValue("@superName", p.SupervisorName1);
                sqlComm.Parameters.AddWithValue("@superPhone", p.SupervisorPhone1);
                sqlComm.CommandTimeout = 600;
                int value = (int)sqlComm.ExecuteScalar();
                return value;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public void CreateHatches(Project p, int id)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spCreateHatches]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@NumOfHatches", p.HatchesNum1);
                sqlComm.Parameters.AddWithValue("@ProjectID", id);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int findid(int id)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spFindProjectID]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@customerid", id);
                sqlComm.CommandTimeout = 600;
                int value = (int)sqlComm.ExecuteScalar();
                return value;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public SqlConnection connect(String conString)
    {
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    public DataTable GetCustomerInformation(int id)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetCustomerInformation]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", id);
                da.SelectCommand = sqlComm;
                da.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public void UpdateCustomerInformation(Customer c, int OriginalCustomerID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateCustomerInformation]", con))
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@OriginalID", Convert.ToInt32(OriginalCustomerID));
                sqlComm.Parameters.AddWithValue("@FirstNAme", c.Fname);
                sqlComm.Parameters.AddWithValue("@LastName", c.Lname);
                sqlComm.Parameters.AddWithValue("@Phone", Convert.ToInt32(c.Phone));
                sqlComm.Parameters.AddWithValue("@Mobile", Convert.ToInt32(c.Mobile));
                sqlComm.Parameters.AddWithValue("@Fax", Convert.ToInt32(c.Fax));
                sqlComm.Parameters.AddWithValue("@Address", c.Address);
                sqlComm.Parameters.AddWithValue("@City", c.City);
                sqlComm.Parameters.AddWithValue("@Email", c.Email);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int StatusNumber(string status)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectStatusNumber]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@status", status);

                sqlComm.CommandTimeout = 600;
                int value = (int)sqlComm.ExecuteScalar();
                return value;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public void UpdateProjectStatus(int ProjectID, int ProjectStatusNum)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateProjectStatus]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
                sqlComm.Parameters.AddWithValue("@ProjectStatusNum", ProjectStatusNum);

                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public void UpdateProjectDetails(int ProjectID, int ProjPrice, string Comments)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateProjectDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
                sqlComm.Parameters.AddWithValue("@ProjPrice", ProjPrice);
                sqlComm.Parameters.AddWithValue("@ProjectComment", Comments);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public void LoadSuppliers(DropDownList ddl, int num)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetSuppliers]", con))
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@RawMaterialID ", num);
                sqlComm.CommandTimeout = 600;
                IDataReader dr = sqlComm.ExecuteReader();
                ddl.DataSource = dr;
                ddl.DataTextField = "sName";
                ddl.DataValueField = "sName";
                ddl.DataBind();
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public DataTable GetOrderStatus()
    {
        DataTable dt3 = new DataTable();
        con = connect("igroup9_prodConnectionString");
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlCommand sqlComm = new SqlCommand("[spGetOrderStatus]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 600;
                da.SelectCommand = sqlComm;
                da.Fill(dt3);
                return dt3;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }


    public int GetSupplierID(string SupplierName)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetSupplierID]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 600;
                sqlComm.Parameters.AddWithValue("@SupplierName", SupplierName);
                int SupplierID = (int)sqlComm.ExecuteScalar();
                return SupplierID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int GetRawMeterialID(string RM)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetRawMeterialID]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 600;
                sqlComm.Parameters.AddWithValue("@RawMetirial", RM);
                int ID = (int)sqlComm.ExecuteScalar();
                return ID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }

    public void CreateNewOrder(Order o)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spCreateNewOrder]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@DateOpened", o.DateOpened1);
                sqlComm.Parameters.AddWithValue("@EstimateDateOfArrival", o.EstimateDateOfArrival1);
                sqlComm.Parameters.AddWithValue("@Quantity", o.Quantity1);
                sqlComm.Parameters.AddWithValue("@osID", o.OrderStatus1);
                sqlComm.Parameters.AddWithValue("@pID", o.ProjectID1);
                sqlComm.Parameters.AddWithValue("@SupplierID", o.SupplierID1);
                sqlComm.Parameters.AddWithValue("@rmID", o.RawMeterialID1);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public DataTable GetOrdersDetails(int ProjectID)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetOrdersListForProject]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
                da.SelectCommand = sqlComm;
                da.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    //Mobile Application Methods

    public DataTable GetProjects()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spShowAllProjects]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = sqlComm;
                da.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public DataTable GetProjectDetails(int ProjectID)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
                da.SelectCommand = sqlComm;
                da.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public DataTable GetHatches(int ProjectID)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetHatches]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
                da.SelectCommand = sqlComm;
                da.Fill(dt);
                return dt;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}





