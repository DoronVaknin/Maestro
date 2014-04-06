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

    public Customer(int _cid, string _fname, string _lname, string _city, string _address, string _email, int _region)
    {
        cid = _cid;
        fname = _fname;
        lname = _lname;
        city = _city;
        address = _address;
        email = _email;
        region = _region;
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

    public void SetPhones(string _phone, string _CustomerCellPhone, string _CustomerFaxNumber)
    {
        if (_phone != "")
            phone = Convert.ToInt32(_phone);
        else
            phone = 0;

        if (_CustomerCellPhone != "")
            Mobile = Convert.ToInt32(_CustomerCellPhone);
        else
            Mobile = 0;

        if (_CustomerFaxNumber != "")
            Fax = Convert.ToInt32(_CustomerFaxNumber);
        else
            Fax = 0;
    }

    public int InsertNewCustomer()
    {
        DBservices db = new DBservices();
        try
        {
            int RowsAffected = db.insertcustomer(this);
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
        Phone = Convert.ToInt32(ProjectInfoPhone);
        Mobile = Convert.ToInt32(ProjectInfoMobile);
        Fax = Convert.ToInt32(ProjectInfoFax);
        Address = ProjectInfoAddress;
        City = ProjectInfoCity;
        Email = ProjectInfoEmail;
        DBservices db = new DBservices();
        db.UpdateCustomerInformation(this, ProjectInfoID);
    }
}