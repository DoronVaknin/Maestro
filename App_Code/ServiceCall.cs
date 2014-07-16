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
/// Summary description for ServiceCall
/// </summary>
public class ServiceCall
{
    public ServiceCall()
    {

    }
    public ServiceCall(DateTime _DateOpened, string _Description, bool _Urgent)
    {
        DateOpened = _DateOpened;
        Description = _Description;
        Urgent = _Urgent;
    }

    int scID;
    public int ScID
    {
        get { return scID; }
        set { scID = value; }
    }

    DateTime dateOpened;
    public DateTime DateOpened
    {
        get { return dateOpened; }
        set { dateOpened = value; }
    }

    DateTime dateClosed;
    public DateTime DateClosed
    {
        get { return dateClosed; }
        set { dateClosed = value; }
    }

    string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    bool urgent;
    public bool Urgent
    {
        get { return urgent; }
        set { urgent = value; }
    }

    public void InsertExternalServiceCall(ServiceCall sc, string CustomerID)
    {
        DBservices dbs = new DBservices();
        dbs.InsertExternalServiceCall(sc, CustomerID);
    }

    public int InsertServiceCallExistingProject(ServiceCall sc, string CustomerID, int ProjectID)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertServiceCallExistingProject(sc, CustomerID, ProjectID);
        return RowAffected;
    }

    public int CloseServiceCall(int ServiceCallID)
    {
        DBservices db = new DBservices();
        int RowAffected = db.CloseServiceCall(ServiceCallID);
        return RowAffected;
    }

    public DataTable GetServiceCallPopupMissingDetails(int ServiceCallID)
    {
        DBservices db = new DBservices();
        DataTable dt= db.GetServiceCallPopupMissingDetails(ServiceCallID);
        return dt;
    }

    public DataTable GetServiceCalls()
    {
        DBservices db = new DBservices();
        DataTable dt = db.GetServiceCalls();
        return dt;
    }

    public DataTable GetOpenedServiceCalls()
    {
        DBservices db = new DBservices();
        DataTable dt = db.GetOpenedServiceCalls();
        return dt;
    }

    public int UpdateServiceCallDetails(int scID, string ProblemDesc, bool Urgent)
    {
        DBservices db = new DBservices();
        int RowAffected = db.UpdateServiceCallDetails(scID, ProblemDesc, Urgent);
        return RowAffected;
    }
}