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

	public Notification(string _Message, DateTime _MessageDate, int _eID)
	{
       Message = _Message;
       MessageDate = _MessageDate;
       eID = _eID;
	}

    int nid;
    public int nID
    {
        get { return nid; }
        set { nid = value; }
    }

    string message;
    public string Message
    {
        get { return message; }
        set { message = value; }
    }

    DateTime messageDate;
    public DateTime MessageDate
    {
        get { return messageDate; }
        set { messageDate = value; }
    }

    int eid;
    public int eID
    {
        get { return eid; }
        set { eid = value; }
    }

    public int InsertNewNotification()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertNewNotification(this);
        return RowAffected;
    }

    public DataTable GetNotifications(int eID)
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetNotifications(eID);
        return dt;
    }
}