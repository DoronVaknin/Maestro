using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Supplier
/// </summary>
public class Supplier
{
    public Supplier()
    {

    }

    public Supplier(string _Name, string _Address, string _Phone, string _Mobile, string _Fax, string _Email)
    {
        Name = _Name;
        Address = _Address;
        Phone = _Phone;
        Mobile = _Mobile;
        Fax = _Fax;
        Email = _Email;
    }

    public Supplier(int _SupplierID, string _Name, string _Address, string _Phone, string _Mobile, string _Fax, string _Email)
    {
        sID = _SupplierID;
        Name = _Name;
        Address = _Address;
        Phone = _Phone;
        Mobile = _Mobile;
        Fax = _Fax;
        Email = _Email;
    }

    int sid;
    public int sID
    {
        get { return sid; }
        set { sid = value; }
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
        DBservices dbs = new DBservices();
        try
        {
            int RowsAffected = dbs.InsertNewSupplier(this);
            return RowsAffected;
        }

        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public int UpdateSupplierDetails()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.UpdateSupplierDetails(this);
        return RowAffected;
    }

    public DataTable GetSuppliersRankTable() 
    {
        DataTable dt = new DataTable();
        DBservices dbs = new DBservices();
        dt = dbs.GetSuppliersRankTable();
        return dt;
    }
}