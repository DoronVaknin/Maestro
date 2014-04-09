using System;
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

    int hatchID;
    public int HatchID
    {
        get { return hatchID; }
        set { hatchID = value; }
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

    public DataTable GetPicsAndPins(int ProjectID)
    {
        DataTable dt = new DataTable();
        DBservices db = new DBservices();
        dt = db.GetPicsAndPins(ProjectID);
        return dt;
    }
}