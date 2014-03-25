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

    int cid;
    public int Cid
    {
        get { return cid; }
        set { cid = value; }
    }

    string fname;
    public string Fname
    {
        get { return fname; }
        set { fname = value; }
    }

    string lname;
    public string Lname
    {
        get { return lname; }
        set { lname = value; }
    }

    string city;
    public string City
    {
        get { return city; }
        set { city = value; }
    }

    string address;
    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    int phone;
    public int Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    int mobile;
    public int Mobile
    {
        get { return mobile; }
        set { mobile = value; }
    }

    int fax;
    public int Fax
    {
        get { return fax; }
        set { fax = value; }
    }

    string email;
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    int region;
    public int Region
    {
        get { return region; }
        set { region = value; }
    }

    public void insert()
    {
        DBservices db = new DBservices();
        db.insertcustomer(this);
    }
}