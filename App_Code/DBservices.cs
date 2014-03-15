using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;


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

    public void insertcustomer(Customer c)
    {
        con = connect("igroup9_test1ConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertNewCustomer]", con))
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

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
                sqlComm.ExecuteNonQuery();
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
}

    



