using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
	public Order()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int OrderID;

    public int OrderID1
    {
        get { return OrderID; }
        set { OrderID = value; }
    }
    DateTime DateOpened;

    public DateTime DateOpened1
    {
        get { return DateOpened; }
        set { DateOpened = value; }
    }
    DateTime EstimateDateOfArrival;

    public DateTime EstimateDateOfArrival1
    {
        get { return EstimateDateOfArrival; }
        set { EstimateDateOfArrival = value; }
    }
    DateTime DateOfArrival;

    public DateTime DateOfArrival1
    {
        get { return DateOfArrival; }
        set { DateOfArrival = value; }
    }
    int OrderStatus;

    public int OrderStatus1
    {
        get { return OrderStatus; }
        set { OrderStatus = value; }
    }
    int ProjectID;

    public int ProjectID1
    {
        get { return ProjectID; }
        set { ProjectID = value; }
    }
    int SupplierID;

    public int SupplierID1
    {
        get { return SupplierID; }
        set { SupplierID = value; }
    }
    int RawMeterialID;

    public int RawMeterialID1
    {
        get { return RawMeterialID; }
        set { RawMeterialID = value; }
    }
}