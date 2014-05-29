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

    public int InsertNewCustomer(Customer c)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertNewCustomer]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@cID", c.cID);
                sqlComm.Parameters.AddWithValue("@fName", c.Fname);
                sqlComm.Parameters.AddWithValue("@lName", c.Lname);
                sqlComm.Parameters.AddWithValue("@Address", c.Address);
                sqlComm.Parameters.AddWithValue("@Phone", c.Phone);
                sqlComm.Parameters.AddWithValue("@Mobile", c.Mobile);
                sqlComm.Parameters.AddWithValue("@Fax", c.Fax);
                sqlComm.Parameters.AddWithValue("@Email", c.Email);
                sqlComm.Parameters.AddWithValue("@Region", c.Region);
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

    public int InsertNewProject(Project p, int cID, int psID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertNewProject]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@cID", cID);
                sqlComm.Parameters.AddWithValue("@psID", psID);
                sqlComm.Parameters.AddWithValue("@pName", p.Name);
                sqlComm.Parameters.AddWithValue("@Cost", p.Cost);
                sqlComm.Parameters.AddWithValue("@DateOpened", p.DateOpened);
                sqlComm.Parameters.AddWithValue("@ExpirationDate", p.ExpirationDate);
                sqlComm.Parameters.AddWithValue("@InstallationDate", p.InstallationDate);
                sqlComm.Parameters.AddWithValue("@Comments", p.Comments);
                sqlComm.Parameters.AddWithValue("@ContName", p.ContractorName);
                sqlComm.Parameters.AddWithValue("@ContPhone", p.ContractorPhone);
                sqlComm.Parameters.AddWithValue("@ArchName", p.ArchitectName);
                sqlComm.Parameters.AddWithValue("@ArchPhone", p.ArchitectPhone);
                sqlComm.Parameters.AddWithValue("@SuperName", p.SupervisorName);
                sqlComm.Parameters.AddWithValue("@SuperPhone", p.SupervisorPhone);
                sqlComm.CommandTimeout = 600;
                int ProjectID = (int)sqlComm.ExecuteScalar();
                return ProjectID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int InsertNewNotification(Notification n)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertNewNotification]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Message", n.Message);
                sqlComm.Parameters.AddWithValue("@MessageDate", n.MessageDate);
                sqlComm.Parameters.AddWithValue("@EmployeeID1", n.eID1);
                sqlComm.Parameters.AddWithValue("@EmployeeID2", n.eID2);
                sqlComm.Parameters.AddWithValue("@IsDetailsClosure", n.IsDetailsClosure);
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

    public void CreateHatches(Project p, int pID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spCreateHatches]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@NumOfHatches", p.NumOfHatches);
                sqlComm.Parameters.AddWithValue("@ProjectID", pID);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int GetProjectID(int cID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectID]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@CustomerID", cID);
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

    public DataTable GetCustomerInformation(int pID)
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
                sqlComm.Parameters.AddWithValue("@ProjectID", pID);
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

    public void UpdateCustomerInformation(Customer c, int cID)
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
                sqlComm.Parameters.AddWithValue("@cID", Convert.ToInt32(cID));
                sqlComm.Parameters.AddWithValue("@FirstName", c.Fname);
                sqlComm.Parameters.AddWithValue("@LastName", c.Lname);
                sqlComm.Parameters.AddWithValue("@Phone", c.Phone);
                sqlComm.Parameters.AddWithValue("@Mobile", c.Mobile);
                sqlComm.Parameters.AddWithValue("@Fax", c.Fax);
                sqlComm.Parameters.AddWithValue("@Address", c.Address);
                sqlComm.Parameters.AddWithValue("@Email", c.Email);
                sqlComm.Parameters.AddWithValue("@RegionID", c.Region);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int StatusNumber(string StatusName)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectStatusNumber]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@StatusName", StatusName);

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

    public void UpdateProjectDetails(int ProjectID, double Cost, string Name, string Comments, string ArchitectName, string ArchitectPhone, string ContractorName, string ContractorPhone, string SupervisorName, string SupervisorPhone, DateTime ExpirationDate, DateTime InstallationDate, int ProjectStatusID)
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
                sqlComm.Parameters.AddWithValue("@ProjectStatusID", ProjectStatusID);
                sqlComm.Parameters.AddWithValue("@pName", Name);
                sqlComm.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                sqlComm.Parameters.AddWithValue("@InstallationDate", InstallationDate);
                sqlComm.Parameters.AddWithValue("@Cost", Cost);
                sqlComm.Parameters.AddWithValue("@Comments", Comments);
                sqlComm.Parameters.AddWithValue("@ContractorName", ContractorName);
                sqlComm.Parameters.AddWithValue("@ContractorPhone", ContractorPhone);
                sqlComm.Parameters.AddWithValue("@ArchitectName", ArchitectName);
                sqlComm.Parameters.AddWithValue("@ArchitectPhone", ArchitectPhone);
                sqlComm.Parameters.AddWithValue("@SupervisorName", SupervisorName);
                sqlComm.Parameters.AddWithValue("@SupervisorPhone", SupervisorPhone);
                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public void LoadSuppliers(DropDownList ddl, int RawMaterialID)
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
                sqlComm.Parameters.AddWithValue("@RawMaterialID ", RawMaterialID);
                sqlComm.CommandTimeout = 600;
                IDataReader dr = sqlComm.ExecuteReader();
                ddl.DataSource = dr;
                ddl.DataTextField = "sName";
                ddl.DataValueField = "SupplierID";
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
        DataTable dt = new DataTable();
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
                da.Fill(dt);
                return dt;
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

    public int GetRawMaterialID(string RawMaterialName)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetRawMaterialID]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandTimeout = 600;
                sqlComm.Parameters.AddWithValue("@RawMaterialName", RawMaterialName);
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
                sqlComm.Parameters.AddWithValue("@DateOpened", o.DateOpened);
                sqlComm.Parameters.AddWithValue("@EstimateDateOfArrival", o.EstimatedDateOfArrival);
                sqlComm.Parameters.AddWithValue("@Quantity", o.Quantity);
                sqlComm.Parameters.AddWithValue("@osID", o.OrderStatus);
                sqlComm.Parameters.AddWithValue("@pID", o.ProjectID);
                sqlComm.Parameters.AddWithValue("@SupplierID", o.SupplierID);
                sqlComm.Parameters.AddWithValue("@rmID", o.RawMaterialID);
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

    public void InsertExternalServiceCall(ServiceCall sc, int CustomerID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertExternalServiceCall]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@DateOpened", sc.DateOpened);
                sqlComm.Parameters.AddWithValue("@ProblemDesc", sc.Description);
                sqlComm.Parameters.AddWithValue("@Urgent", sc.Urgent);
                sqlComm.Parameters.AddWithValue("@cID", CustomerID);

                sqlComm.CommandTimeout = 600;
                sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int InsertServiceCallExistingProject(ServiceCall sc, int CustomerID, int ProjectID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertServiceCallExistingProject]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@DateOpened", sc.DateOpened);
                sqlComm.Parameters.AddWithValue("@ProblemDesc", sc.Description);
                sqlComm.Parameters.AddWithValue("@Urgent", sc.Urgent);
                sqlComm.Parameters.AddWithValue("@cID", CustomerID);
                sqlComm.Parameters.AddWithValue("@pID", ProjectID);

                sqlComm.CommandTimeout = 600;
                int RowAffected = sqlComm.ExecuteNonQuery();
                return RowAffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public int InsertNewSupplier(Supplier s)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spInsertNewSupplier]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@sName", s.Name);
                sqlComm.Parameters.AddWithValue("@Address", s.Address);
                sqlComm.Parameters.AddWithValue("@Phone", s.Phone);
                sqlComm.Parameters.AddWithValue("@Mobile", s.Mobile);
                sqlComm.Parameters.AddWithValue("@Fax", s.Fax);
                sqlComm.Parameters.AddWithValue("@Email", s.Email);
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

    public int UpdateSupplierDetails(Supplier s)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateSupplierDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@SupplierID", s.sID);
                sqlComm.Parameters.AddWithValue("@sName", s.Name);
                sqlComm.Parameters.AddWithValue("@Address", s.Address);
                sqlComm.Parameters.AddWithValue("@Phone", s.Phone);
                sqlComm.Parameters.AddWithValue("@Mobile", s.Mobile);
                sqlComm.Parameters.AddWithValue("@Fax", s.Fax);
                sqlComm.Parameters.AddWithValue("@Email", s.Email);
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

    public int UpdateUndecidedCustomerDetails(Project p, int ProjectStatusID, int CustomerID, string CustomerMobilePhone)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateUndecidedCustomerDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", p.pID);
                sqlComm.Parameters.AddWithValue("@ProjectName", p.Name);
                sqlComm.Parameters.AddWithValue("@Comments", p.Comments);
                sqlComm.Parameters.AddWithValue("@BackToCustomerDate", p.InstallationDate);
                sqlComm.Parameters.AddWithValue("@ProjectStatusID", ProjectStatusID);
                sqlComm.Parameters.AddWithValue("@CustomerID", CustomerID);
                sqlComm.Parameters.AddWithValue("@CustomerMobilePhone", CustomerMobilePhone);
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

    public DataTable GetServiceCalls()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetServiceCalls]", con))
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

    public DataTable GetOpenedServiceCalls()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetOpenedServiceCalls]", con))
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

    public DataTable GetProjectsNames()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectsNames]", con))
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

    public DataTable GetProjectDetails()
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

    public DataTable GetHatches()
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

    public DataTable GetPicsAndPins(int HatchID)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetPicsAndPins]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@HatchID", HatchID);
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

    public int CloseServiceCall(int ServiceCallID)
    {
        DateTime date = DateTime.Now;
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("spCloseServiceCall", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@date", date);
                sqlComm.Parameters.AddWithValue("@ServiceCallID", ServiceCallID);
                int RowAffected = sqlComm.ExecuteNonQuery();
                return RowAffected;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

    public DataTable GetServiceCallPopupMissingDetails(int ServicCallID)
    {
        DataTable dt = new DataTable();
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("spGetServiceCallPopupMissingDetails", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ServiceCallID", ServicCallID);
                SqlDataAdapter da = new SqlDataAdapter();
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

    //Production App functions
    public DataTable GetHatchesForProdApp()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetHatchesForProdApp]", con))
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

    public DataTable GetProjectListForProdApp()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectListForProdApp]", con))
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

    public DataTable GetHatchStatusList()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetHatchStatusList]", con))
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

    public DataTable GetFailureTypeList()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetFailureTypeList]", con))
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

    public DataTable GetUsernameID(string Username)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetUsernameID]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Username", Username);
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

    public int UpdateHatchDetails(Hatch h)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateHatchDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@HatchID", h.HatchID);
                sqlComm.Parameters.AddWithValue("@HatchStatusID", h.HatchStatusID);
                sqlComm.Parameters.AddWithValue("@HatchTypeID", h.HatchTypeID);
                if (h.FtID == 0)
                    sqlComm.Parameters.AddWithValue("@FailureTypeID", null);
                else
                    sqlComm.Parameters.AddWithValue("@FailureTypeID", h.FtID);
                sqlComm.Parameters.AddWithValue("@EmployeeID", h.EmployeeID);
                sqlComm.Parameters.AddWithValue("@StatusLastModified", h.StatusLastModified);
                sqlComm.Parameters.AddWithValue("@Comments", h.Comments);
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

    public int UpdateServiceCallDetails(int scID, string ProblemDesc, bool Urgent)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateServiceCallDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@scID", scID);
                sqlComm.Parameters.AddWithValue("@ProblemDesc", ProblemDesc);
                sqlComm.Parameters.AddWithValue("@Urgent", Urgent);
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

    public int UpdateOrderDetails(int oID, int Quantity, int OrderStatus, DateTime EstimatedDOA)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUpdateOrderDetails]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@OrderID", oID);
                sqlComm.Parameters.AddWithValue("@Quantity", Quantity);
                sqlComm.Parameters.AddWithValue("@OrderStatus", OrderStatus);
                sqlComm.Parameters.AddWithValue("@EstimatedDateOfArrival", EstimatedDOA);
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

    public DataTable GetAllDetails(int ProjectID)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetAllProjectDetails]", con))
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

    public int UploadPicture(Picture pic)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spUploadPicture]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@HatchID", pic.HatchID);
                sqlComm.Parameters.AddWithValue("@PictureDesc", pic.PictureDescription);
                sqlComm.Parameters.AddWithValue("@DateTaken", pic.DateTaken);
                sqlComm.Parameters.AddWithValue("@ImageURL", pic.ImageURL);
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

    public DataTable GetSuppliersRankTable()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetSuppliersRankTable]", con))
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

    public DataTable GetProjectsIncome()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectsIncome]", con))
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

    public DataTable GetUndecidedCustomers()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetUndecidedCustomers]", con))
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

    public DataTable GetNotifications(int eID1)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetNotifications]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@EmployeeID1", eID1);
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

    public DataTable GetProjectHatches(int ProjectID)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spGetProjectHatches]", con))
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

    public int SetStatusProduction(int ProjectID)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spSetStatusProduction]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
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

    public int SetExpirationDate(int ProjectID, DateTime ExpirationDate)
    {
        con = connect("igroup9_prodConnectionString");
        using (SqlCommand sqlComm = new SqlCommand("[spSetExpirationDate]", con))
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            try
            {
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@ProjectID", ProjectID);
                sqlComm.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
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
}