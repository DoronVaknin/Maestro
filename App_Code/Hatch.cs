﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Hatch
/// </summary>
public class Hatch
{
    public Hatch()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Hatch(int _HatchID, int _HatchStatusID, int _FailureTypeID, int _EmployeeID, DateTime _StatusLastModified, string _Comments)
    {
        HatchID = _HatchID;
        HatchStatusID = _HatchStatusID;
        if (_FailureTypeID != 0)
            ftID = _FailureTypeID;
        EmployeeID = _EmployeeID;
        StatusLastModified = _StatusLastModified;
        Comments = _Comments;
    }

    public Hatch(int _HatchID, int _HatchID2, int _HatchStatusID, int _FailureTypeID, int _EmployeeID, DateTime _StatusLastModified, string _Comments, int _HatchTypeID)
    {
        HatchID = _HatchID;
        HatchID2 = _HatchID2;
        HatchStatusID = _HatchStatusID;
        HatchTypeID = _HatchTypeID;
        if (_FailureTypeID != 0)
            ftID = _FailureTypeID;
        EmployeeID = _EmployeeID;
        StatusLastModified = _StatusLastModified;
        Comments = _Comments;
    }

    int hatchID;
    public int HatchID
    {
        get { return hatchID; }
        set { hatchID = value; }
    }

    int hatchID2;
    public int HatchID2
    {
        get { return hatchID2; }
        set { hatchID2 = value; }
    }

    int hatchStatusID;
    public int HatchStatusID
    {
        get { return hatchStatusID; }
        set { hatchStatusID = value; }
    }

    string hatchStatus;
    public string HatchStatus
    {
        get { return hatchStatus; }
        set { hatchStatus = value; }
    }

    DateTime statusLastModified;
    public DateTime StatusLastModified
    {
        get { return statusLastModified; }
        set { statusLastModified = value; }
    }

    int hatchTypeID;
    public int HatchTypeID
    {
        get { return hatchTypeID; }
        set { hatchTypeID = value; }
    }

    string hatchType;
    public string HatchType
    {
        get { return hatchType; }
        set { hatchType = value; }
    }

    int projectID;
    public int ProjectID
    {
        get { return projectID; }
        set { projectID = value; }
    }

    int employeeID;
    public int EmployeeID
    {
        get { return employeeID; }
        set { employeeID = value; }
    }

    string eName;
    public string EmployeeName
    {
        get { return eName; }
        set { eName = value; }
    }

    string username;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    int ftID;
    public int FtID
    {
        get { return ftID; }
        set { ftID = value; }
    }

    string ftName;
    public string FtName
    {
        get { return ftName; }
        set { ftName = value; }
    }

    string comments;
    public string Comments
    {
        get { return comments; }
        set { comments = value; }
    }

    bool isActive;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public DataTable GetPicsAndPins(int HatchID)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetPicsAndPins(HatchID);
        return dt;
    }

    public DataTable GetHatchStatusList()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetHatchStatusList();
        return dt;
    }

    public DataTable GetFailureTypeList()
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetFailureTypeList();
        return dt;
    }

    public int UpdateHatchDetails()
    {
        DBservices db = new DBservices();
        int RowAffected = db.UpdateHatchDetails(this);
        return RowAffected;
    }

    public int GetUsernameID(string Username)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetUsernameID(Username);
        int eID = 0;
        if (dt.Rows.Count > 0)
            eID = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        else
            eID = 300843637;
        return eID;
    }

    public int DisableHatch(int HatchID)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.DisableHatch(HatchID);
        return RowAffected;
    }

}