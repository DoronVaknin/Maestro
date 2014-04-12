using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ServiceCall
/// </summary>
public class ServiceCall
{
    public ServiceCall()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    string customerName;
    public string CustomerName
    {
        get { return customerName; }
        set { customerName = value; }
    }

    string mobile;
    public string Mobile
    {
        get { return mobile; }
        set { mobile = value; }
    }

    string address;
    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    int area;
    public int Area
    {
        get { return area; }
        set { area = value; }
    }

    bool urgent;
    public bool Urgent
    {
        get { return urgent; }
        set { urgent = value; }
    }

    bool warranty;
    public bool Warranty
    {
        get { return warranty; }
        set { warranty = value; }
    }

    public void InsertServiceCall(ServiceCall sc)
    {
        DBservices dbs = new DBservices();
    }
}