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

    public Project(DateTime _OpenedDate, DateTime _ExpirationDate, string _Comments, string _Cost, string _Hatches, string _ArchitectName, string _ArchitectPhone, string _ContractorName, string _ContractorPhone, string _SupervisorName, string _SupervisorPhone)
    {
        DateOpened = _OpenedDate;
        ExpirationDate = _ExpirationDate;
        Comments = _Comments;
        if (_Cost != "")
            Cost = Convert.ToInt32(_Cost);
        if (_Hatches != "")
            NumOfHatches = Convert.ToInt32(_Hatches);
        ArchitectName = _ArchitectName;
        if (_ArchitectPhone != "")
            ArchitectPhone = _ArchitectPhone;
        else
            ArchitectPhone = "0";
        ContractorName = _ContractorName;
        if (_ContractorPhone != "")
            ContractorPhone = _ContractorPhone;
        else
            ContractorPhone = "0";
        SupervisorName = _SupervisorName;
        if (_SupervisorPhone != "")
            SupervisorPhone = _SupervisorPhone;
        else
            SupervisorPhone = "0";
    }

    string comments;
    public string Comments
    {
        get { return comments; }
        set { comments = value; }
    }

    DateTime expirationDate;
    public DateTime ExpirationDate
    {
        get { return expirationDate; }
        set { expirationDate = value; }
    }

    DateTime dateOpened;
    public DateTime DateOpened
    {
        get { return dateOpened; }
        set { dateOpened = value; }
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

    int cost;
    public int Cost
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

    public void InsertNewProject(Project p, int CustomerID)
    {
        DBservices db = new DBservices();
        int ProjectID = db.InsertProjectInfo(this, CustomerID);
        db.CreateHatches(this, ProjectID);
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

    public void UpdateProjectStatus(int ProjectID, int StatusNum)
    {
        DBservices db = new DBservices();
        db.UpdateProjectStatus(ProjectID, StatusNum);
    }

    public void UpdateProjectDetails(int ProjectID, int Cost, string Comments)
    {
        DBservices db = new DBservices();
        db.UpdateProjectDetails(ProjectID, Cost, Comments);
    }

    public void LoadSuppliers(DropDownList ShutterProvider, DropDownList CollectedProvider, DropDownList ValimProvider, DropDownList UProvider, DropDownList ShoeingProvider, DropDownList EngineProvider, DropDownList ProtectedSpaceProvider, DropDownList GlassProvider, DropDownList BoxesProvider)
    {
        DBservices db = new DBservices();
        db.LoadSuppliers(ShutterProvider, 1);
        db.LoadSuppliers(CollectedProvider, 2);
        db.LoadSuppliers(ValimProvider, 3);
        db.LoadSuppliers(UProvider, 4);
        db.LoadSuppliers(ShoeingProvider, 5);
        db.LoadSuppliers(EngineProvider, 6);
        db.LoadSuppliers(ProtectedSpaceProvider, 7);
        db.LoadSuppliers(GlassProvider, 8);
        db.LoadSuppliers(BoxesProvider, 9);
    }

    public DataTable GetProjects()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetProjects();
        return dt;
    }

    public DataTable GetProjectDetails(int ProjectID)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetProjectDetails(ProjectID);
        return dt;
    }

    public DataTable GetHatches(int ProjectID)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetHatches(ProjectID);
        return dt;
    }
}