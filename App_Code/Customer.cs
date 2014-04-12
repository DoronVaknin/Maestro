using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

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

    public Customer(int _cID, string _fName, string _lName, string _City, string _Address, string _Email, int _Region)
    {
        cID = _cID;
        Fname = _fName;
        Lname = _lName;
        City = _City;
        Address = _Address;
        Email = _Email;
        Region = _Region;
    }

    int cid;
    public int cID
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

    int region;
    public int Region
    {
        get { return region; }
        set { region = value; }
    }

    public void SetPhones(string _Phone, string _Mobile, string _Fax)
    {
        if (_Phone != "")
            phone = _Phone;
        else
            phone = "0";

        if (_Mobile != "")
            Mobile = _Mobile;
        else
            Mobile = "0";

        if (_Fax != "")
            Fax = _Fax;
        else
            Fax = "0";
    }

    public int InsertNewCustomer()
    {
        DBservices db = new DBservices();
        try
        {
            int RowsAffected = db.InsertNewCustomer(this);
            return RowsAffected;
        }

        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public void SaveCustomerNewDetails(string ProjectInfoFirstName, string ProjectInfoLastName, string ProjectInfoPhone, string ProjectInfoMobile, string ProjectInfoFax, string ProjectInfoAddress, string ProjectInfoCity, string ProjectInfoEmail, int ProjectInfoID)
    {
        Fname = ProjectInfoFirstName;
        Lname = ProjectInfoLastName;
        Phone = ProjectInfoPhone;
        Mobile = ProjectInfoMobile;
        Fax = ProjectInfoFax;
        Address = ProjectInfoAddress;
        City = ProjectInfoCity;
        Email = ProjectInfoEmail;
        DBservices db = new DBservices();
        db.UpdateCustomerInformation(this, ProjectInfoID);
    }
}