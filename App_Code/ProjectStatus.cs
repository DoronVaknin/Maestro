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

    int statusnum;
    public int Statusnum
    {
        get { return statusnum; }
        set { statusnum = value; }
    }

    string statusname;
    public string Statusname
    {
        get { return statusname; }
        set { statusname = value; }
    }
}