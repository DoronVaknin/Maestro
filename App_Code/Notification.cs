using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Notification
/// </summary>
public class Notification
{
    public Notification()
    {

    }

    public Notification(string _Notification, DateTime _nDate, string _eID1, string _eID2)
    {
        if (_Notification != "")
            nNotification = _Notification;
        nDate = _nDate;
        eID1 = _eID1;
        if (_eID2 != "0")
            eID2 = _eID2;
    }

    int nid;
    public int nID
    {
        get { return nid; }
        set { nid = value; }
    }

    string notification;
    public string nNotification
    {
        get { return notification; }
        set { notification = value; }
    }

    DateTime ndate;
    public DateTime nDate
    {
        get { return ndate; }
        set { ndate = value; }
    }

    string eid1;
    public string eID1
    {
        get { return eid1; }
        set { eid1 = value; }
    }

    string eid2;
    public string eID2
    {
        get { return eid2; }
        set { eid2 = value; }
    }

    public int InsertNewNotification()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertNewNotification(this);
        return RowAffected;
    }

    public DataTable GetNotifications(string eID)
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetNotifications(eID);
        return dt;
    }
}