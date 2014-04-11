using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectStatus
/// </summary>
public class ProjectStatus
{
    public ProjectStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    int statusNum;
    public int StatusNum
    {
        get { return statusNum; }
        set { statusNum = value; }
    }

    string statusName;
    public string StatusName
    {
        get { return statusName; }
        set { statusName = value; }
    }
}