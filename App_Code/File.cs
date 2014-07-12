using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for File
/// </summary>
public class File
{
    public File()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public File(string _Description, string _Url, int _pID)
    {
        Description = _Description;
        Url = _Url;
        pID = _pID;
    }

    string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    string url;
    public string Url
    {
        get { return url; }
        set { url = value; }
    }

    int pid;
    public int pID
    {
        get { return pid; }
        set { pid = value; }
    }

    public int InsertNewFile()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertNewFile(this);
        return RowAffected;
    }

    public DataTable GetProjectFiles(int ProjectID)
    {
        DBservices dbs = new DBservices();
        DataTable dt = dbs.GetProjectFiles(ProjectID);
        return dt;
    }
}