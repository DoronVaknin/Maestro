﻿using System;
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

    public Notification(string _Message, DateTime _MessageDate, int _eID1)
    {
        Message = _Message;
        MessageDate = _MessageDate;
        eID1 = _eID1;
    }

    public Notification(string _Message, DateTime _MessageDate, int _eID1, int _eID2)
	{
       Message = _Message;
       MessageDate = _MessageDate;
       eID1 = _eID1;
       eID2 = _eID2;
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

    int eid1;
    public int eID1
    {
        get { return eid1; }
        set { eid1 = value; }
    }

    int eid2;
    public int eID2
    {
        get { return eid2; }
        set { eid2 = value; }
    }

    public int InsertHatchFailureNotification()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertHatchFailureNotification(this);
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