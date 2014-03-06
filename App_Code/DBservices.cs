using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //--------------------------------------------------------------------
    // Read from the DB into a table
    //--------------------------------------------------------------------
    public DBservices ReadFromDataBase(string conString, string tableName)
    {

        DBservices dbS = new DBservices(); // create a helper class
        SqlConnection con = null;

        try
        {
            con = dbS.connect(conString); // open the connection to the database/

            String selectStr = "SELECT * FROM " + tableName; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the data adapter to the dbS helper class in order to be able to save it to a Session Object
            dbS.dt = dt;
            dbS.da = da;

            return dbS;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method checks if the name already exists 
    //--------------------------------------------------------------------------------------------------
    //public bool NameExists(Product product, SqlConnection con)
    //{
    //    string str = "SELECT COUNT(*) FROM Products p WHERE Name = '" + product.Name + "'";
    //    SqlCommand cmd = CreateCommand(str, con);

    //    try
    //    {
    //        if ( (int)cmd.ExecuteScalar() == 0)
    //            return false;
    //        else
    //            return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        return true;
    //        // write to log
    //        throw (ex);
    //    }

    //}

    //--------------------------------------------------------------------------------------------------
    // This method inserts a car to the cars table 
    //--------------------------------------------------------------------------------------------------
    public void insert(Customer customer)
    {

        SqlConnection con;
        SqlCommand cmd;
        string comand = "INSERT INTO Customer (Name) VALUES ('"+customer.Name+"')";

        try
        {
            con = connect("igroup9_test1ConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        //bool Name_exists = NameExists(product, con);   // method that checks whether same name already exists
        //if (Name_exists == true)
        //    return 0;

        //String pStr = BuildInsertCommand(customer);      // helper method to build the insert string


        cmd = CreateCommand(comand, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //return numEffected;
        }
        catch (Exception ex)
        {
            
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert a car command String
    //--------------------------------------------------------------------
    //private void BuildInsertCommand(Customer customer)
    //{
    //    SqlCommand  cmd= new SqlCommand("Insert into Customer(customer.name)");

    //    //StringBuilder sb = new StringBuilder();
    //    //// use a string builder to create the dynamic string
    //    //sb.AppendFormat("Values('{0}', {1} ,'{2}', {3}, '{4}')", product.Name, product.Price.ToString(), product.ImageUrl, product.Amount.ToString(), product.Discount);
    //    //String prefix = "INSERT INTO Products " + "(Name, Price, imageUrl, Amount, Discount) ";
    //    //command = prefix + sb.ToString();
    //}

    //---------------------------------------------------------------------------------
    // update the dataset into the database
    //---------------------------------------------------------------------------------
    public void Update()
    {
        // the command build will automatically create insert/update/delete commands according to the select command
        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        
        da.Update(dt);
    }
     

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

}
