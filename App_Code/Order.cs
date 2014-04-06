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

    public Order(int ProjectId, string Supplier, int RawMeterialID, int Quantity)
    {
        DBservices db = new DBservices();
        ProjectID1 = ProjectId;
        DateOpened1 = DateTime.Now;
        EstimateDateOfArrival1 = DateOpened1.AddDays(14);
        OrderStatus1 = 1;
        SupplierID1 = db.GetSupplierID(Supplier);
        RawMeterialID1 = RawMeterialID;
        Quantity1 = Quantity;
        db.CreateNewOrder(this);
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

    int Quantity;

    public int Quantity1
    {
        get { return Quantity; }
        set { Quantity = value; }
    }
}