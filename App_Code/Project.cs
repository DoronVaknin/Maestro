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

    public Project(DateTime _OpenedDate1, DateTime _ExpirationDate1, string _comment, string price, string hatches, string ProjectArchitectName, string ProjectArchitectPhone, string ProjectContractorName, string ProjectContractorPhone, string ProjectSupervisorName, string ProjectSupervisorPhone)
    {
        OpenedDate = _OpenedDate1;
        ExpirationDate = _ExpirationDate1;
        Comment1 = _comment;
        if (price != "")
            Price = Convert.ToInt32(price);
        if (hatches != "")
            HatchesNum1 = Convert.ToInt32(hatches);
        ArchitectName = ProjectArchitectName;
        if (ProjectArchitectPhone != "")
            ArchitectPhone = Convert.ToInt32(ProjectArchitectPhone);
        else
            ArchitectPhone = 0;
        ContractorName = ProjectContractorName;
        if (ProjectContractorPhone != "")
            ContractorPhone1 = Convert.ToInt32(ProjectContractorPhone);
        else
            ContractorPhone1 = 0;
        SupervisorName = ProjectSupervisorName;
        if (ProjectSupervisorPhone != "")
            SupervisorPhone1 = Convert.ToInt32(ProjectSupervisorPhone);
        else
            SupervisorPhone1 = 0;
    }

    string Comment;
    public string Comment1
    {
        get { return Comment; }
        set { Comment = value; }
    }

    DateTime ExpirationDate;
    public DateTime ExpirationDate1
    {
        get { return ExpirationDate; }
        set { ExpirationDate = value; }
    }

    DateTime OpenedDate;
    public DateTime OpenedDate1
    {
        get { return OpenedDate; }
        set { OpenedDate = value; }
    }

    string ContractorName;
    public string ContractorName1
    {
        get { return ContractorName; }
        set { ContractorName = value; }
    }

    int ContractorPhone;
    public int ContractorPhone1
    {
        get { return ContractorPhone; }
        set { ContractorPhone = value; }
    }

    string ArchitectName;
    public string ArchitectName1
    {
        get { return ArchitectName; }
        set { ArchitectName = value; }
    }

    int ArchitectPhone;
    public int ArchitectPhone1
    {
        get { return ArchitectPhone; }
        set { ArchitectPhone = value; }
    }

    string SupervisorName;
    public string SupervisorName1
    {
        get { return SupervisorName; }
        set { SupervisorName = value; }
    }

    int SupervisorPhone;
    public int SupervisorPhone1
    {
        get { return SupervisorPhone; }
        set { SupervisorPhone = value; }
    }

    int Cid;
    public int Cid1
    {
        get { return Cid; }
        set { Cid = value; }
    }

    int Pid;
    public int Pid1
    {
        get { return Pid; }
        set { Pid = value; }
    }

    int price;
    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    int HatchesNum;
    public int HatchesNum1
    {
        get { return HatchesNum; }
        set { HatchesNum = value; }
    }

    public void InsertNewProject(Project p, int CustomerID)
    {
        DBservices db = new DBservices();
        int projectid = db.InsertProjectInfo(this, CustomerID);
        db.CreateHatches(this, projectid);
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

    public int GetStatusNumber(string status)
    {
        DBservices db = new DBservices();
        int StatusNumber = db.StatusNumber(status);
        return StatusNumber;
    }

    public void UpdateProjectStatus(int ProjectID, int StatusNum)
    {
        DBservices db = new DBservices();
        db.UpdateProjectStatus(ProjectID, StatusNum);
    }

    public void UpdateProjectDetails(int ProjectID, int price, string comments)
    {
        DBservices db = new DBservices();
        db.UpdateProjectDetails(ProjectID, price, comments);
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