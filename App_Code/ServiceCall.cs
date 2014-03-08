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

    string CustomerName;

    public string CustomerName1
    {
        get { return CustomerName; }
        set { CustomerName = value; }
    }
    Int32 CellPhone;

    public Int32 CellPhone1
    {
        get { return CellPhone; }
        set { CellPhone = value; }
    }
    string Adress;

    public string Adress1
    {
        get { return Adress; }
        set { Adress = value; }
    }
    string Description;

    public string Description1
    {
        get { return Description; }
        set { Description = value; }
    }
    int Area;

    public int Area1
    {
        get { return Area; }
        set { Area = value; }
    }
    bool IsUrgent;

    public bool IsUrgent1
    {
        get { return IsUrgent; }
        set { IsUrgent = value; }
    }
    bool Warranty;

    public bool Warranty1
    {
        get { return Warranty; }
        set { Warranty = value; }
    }

    public void insert(ServiceCall sc)
    {
        DBservices dbs = new DBservices();

    }

}