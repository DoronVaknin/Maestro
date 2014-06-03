using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for SpecialNotification
/// </summary>
public class SpecialNotification : Notification
{
    public SpecialNotification()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SpecialNotification(string _Notification, DateTime _nDate, int _eID1, int _eID2, string _nType)
        : base(_Notification, _nDate, _eID1, _eID2)
    {
        nType = _nType;
    }

    public SpecialNotification(string _Notification, DateTime _nDate, int _eID1, int _eID2, string _nType, string _EmailSubject, string _EmailMessage, string _EmailAddress)
        : base(_Notification, _nDate, _eID1, _eID2)
    {
        nType = _nType;
        EmailSubject = _EmailSubject;
        EmailMessage = _EmailMessage;
        EmailAddress = _EmailAddress;
    }

    string ntype;
    public string nType
    {
        get { return ntype; }
        set { ntype = value; }
    }

    string emailSubject;
    public string EmailSubject
    {
        get { return emailSubject; }
        set { emailSubject = value; }
    }

    string emailMessage;
    public string EmailMessage
    {
        get { return emailMessage; }
        set { emailMessage = value; }
    }

    string emailAddress;
    public string EmailAddress
    {
        get { return emailAddress; }
        set { emailAddress = value; }
    }

    public int InsertNewSpecialNotification()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertNewSpecialNotification(this);
        return RowAffected;
    }

    public DataTable GetSpecialNotifications(int eID)
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetSpecialNotifications(eID);
        return dt;
    }

    public int DeleteSpecialNotification(int nID)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.DeleteSpecialNotification(nID);
        return RowAffected;
    }
}