using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Customer
/// </summary>
public class Customer
{
	public Customer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public void insert()
    {
        DBservices db = new DBservices();
        db.insert(this);
    }


}