using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Supplier
/// </summary>
public class Supplier
{
    public Supplier(string _Name, string _Address, string _City, string _Phone, string _Mobile, string _Fax, string _Email)
    {
        Name = _Name;
        Address = _Address;
        City = _City;
        Phone = _Phone;
        Mobile = _Mobile;
        Fax = _Fax;
        Email = _Email;
    }

    string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    string address;
    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    string city;
    public string City
    {
        get { return city; }
        set { city = value; }
    }

    string phone;
    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    string mobile;
    public string Mobile
    {
        get { return mobile; }
        set { mobile = value; }
    }

    string fax;
    public string Fax
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

    public int InsertNewSupplier()
    {
        DBservices db = new DBservices();
        try
        {
            int RowsAffected = db.InsertNewSupplier(this);
            return RowsAffected;
        }

        catch (Exception ex)
        {
            throw (ex);
        }
    }
}