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
/// Summary description for Project
/// </summary>
public class Project
{
    public Project()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Project(DateTime _OpenedDate, DateTime _ExpirationDate, DateTime _InstallationDate, string _Name, string _Comments, string _Cost, string _Hatches, string _ArchitectName, string _ArchitectPhone, string _ContractorName, string _ContractorPhone, string _SupervisorName, string _SupervisorPhone)
    {
        Name = _Name;
        DateOpened = _OpenedDate;
        ExpirationDate = _ExpirationDate;
        InstallationDate = _InstallationDate;
        Comments = _Comments;
        Cost = Convert.ToDouble(_Cost);
        NumOfHatches = Convert.ToInt32(_Hatches);
        ArchitectName = _ArchitectName;
        ArchitectPhone = _ArchitectPhone;
        ContractorName = _ContractorName;
        ContractorPhone = _ContractorPhone;
        SupervisorName = _SupervisorName;
        SupervisorPhone = _SupervisorPhone;
    }

    string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    string comments;
    public string Comments
    {
        get { return comments; }
        set { comments = value; }
    }

    DateTime dateOpened;
    public DateTime DateOpened
    {
        get { return dateOpened; }
        set { dateOpened = value; }
    }

    DateTime expirationDate;
    public DateTime ExpirationDate
    {
        get { return expirationDate; }
        set { expirationDate = value; }
    }

    DateTime installationDate;
    public DateTime InstallationDate
    {
        get { return installationDate; }
        set { installationDate = value; }
    }

    string contractorName;
    public string ContractorName
    {
        get { return contractorName; }
        set { contractorName = value; }
    }

    string contractorPhone;
    public string ContractorPhone
    {
        get { return contractorPhone; }
        set { contractorPhone = value; }
    }

    string architectName;
    public string ArchitectName
    {
        get { return architectName; }
        set { architectName = value; }
    }

    string architectPhone;
    public string ArchitectPhone
    {
        get { return architectPhone; }
        set { architectPhone = value; }
    }

    string supervisorName;
    public string SupervisorName
    {
        get { return supervisorName; }
        set { supervisorName = value; }
    }

    string supervisorPhone;
    public string SupervisorPhone
    {
        get { return supervisorPhone; }
        set { supervisorPhone = value; }
    }

    int cid;
    public int cID
    {
        get { return cid; }
        set { cid = value; }
    }

    int pid;
    public int pID
    {
        get { return pid; }
        set { pid = value; }
    }

    double cost;
    public double Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    int numOfHatches;
    public int NumOfHatches
    {
        get { return numOfHatches; }
        set { numOfHatches = value; }
    }

    string hatchesImageURL;
    public string HatchesImageURL
    {
        get { return hatchesImageURL; }
        set { hatchesImageURL = value; }
    }

    public void InsertNewProject(Project p, int CustomerID, int psID)
    {
        DBservices dbs = new DBservices();
        int ProjectID = dbs.InsertNewProject(this, CustomerID, psID);
        dbs.CreateHatches(this, ProjectID);
    }

    public DataTable GetCustomerInformation(int ProjectID)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetCustomerInformation(ProjectID);
        return dt;
    }

    public DataTable GetOrderDetails(int ProjectId)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetOrdersDetails(ProjectId);
        return dt;
    }

    public DataTable GetOrderStatus()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetOrderStatus();
        return dt;
    }

    public int GetStatusNumber(string StatusName)
    {
        DBservices db = new DBservices();
        int StatusNum = db.StatusNumber(StatusName);
        return StatusNum;
    }

    public void UpdateProjectDetails(int _ProjectID, double _Cost, string _Name, string _Comments, string _ArchitectName, string _ArchitectPhone, string _ContractorName, string _ContractorPhone, string _SupervisorName, string _SupervisorPhone, DateTime _ExpirationDate, DateTime _InstallationDate, int _ProjectStatusID)
    {
        DBservices db = new DBservices();
        db.UpdateProjectDetails(_ProjectID, _Cost, _Name, _Comments, _ArchitectName, _ArchitectPhone, _ContractorName, _ContractorPhone, _SupervisorName, _SupervisorPhone, _ExpirationDate, _InstallationDate, _ProjectStatusID);
    }

    public void LoadSuppliers(DropDownList ShutterProvider, DropDownList CollectedProvider, DropDownList AluminiumProvider, DropDownList ValimProvider, DropDownList UProvider, DropDownList ShoeingProvider, DropDownList EngineProvider, DropDownList ProtectedSpaceProvider, DropDownList GlassProvider, DropDownList BoxesProvider)
    {
        DBservices db = new DBservices();
        db.LoadSuppliers(ShutterProvider, 1);
        db.LoadSuppliers(CollectedProvider, 2);
        db.LoadSuppliers(AluminiumProvider, 3);
        db.LoadSuppliers(ValimProvider, 4);
        db.LoadSuppliers(UProvider, 5);
        db.LoadSuppliers(ShoeingProvider, 6);
        db.LoadSuppliers(EngineProvider, 7);
        db.LoadSuppliers(ProtectedSpaceProvider, 8);
        db.LoadSuppliers(GlassProvider, 9);
        db.LoadSuppliers(BoxesProvider, 10);
    }

    public DataTable GetProjects()
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetProjects();
        return dt;
    }

    public DataTable GetProjectDetails()
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetProjectDetails();
        return dt;
    }

    public DataTable GetHatches()
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetHatches();
        return dt;
    }

    public DataTable GetHatchesForProdApp()
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetHatchesForProdApp();
        return dt;
    }

    public DataTable GetProjectHatches(int ProjectID)
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetProjectHatches(ProjectID);
        return dt;
    }

    public DataTable GetProjectListForProdApp()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetProjectListForProdApp();
        return dt;
    }

    public DataTable GetProjectsNames()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetProjectsNames();
        return dt;
    }

    public DataTable GetAllDetails(int ProjectID)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetAllDetails(ProjectID);
        return dt;
    }

    public DataTable GetProjectsIncome()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetProjectsIncome();
        return dt;
    }

    public DataTable GetUndecidedCustomers()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetUndecidedCustomers();
        return dt;
    }

    public int UpdateUndecidedCustomerDetails(Project p, int ProjectStatusID, int CustomerID, string CustomerMobilePhone)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.UpdateUndecidedCustomerDetails(p, ProjectStatusID, CustomerID, CustomerMobilePhone);
        return RowAffected;
    }

    public int SetStatusProduction(int ProjectID)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.SetStatusProduction(ProjectID);
        return RowAffected;
    }

    public int SetExpirationDate(int ProjectID, DateTime ExpirationDate)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.SetExpirationDate(ProjectID, ExpirationDate);
        return RowAffected;
    }

}