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

    public Order(int _ProjectId, string _Supplier, int _RawMaterialID, int _Quantity)
    {
        DBservices db = new DBservices();
        ProjectID = _ProjectId;
        DateOpened = DateTime.Now;
        EstimatedDateOfArrival = DateOpened.AddDays(14);
        OrderStatus = 1;
        SupplierID = db.GetSupplierID(_Supplier);
        RawMaterialID = _RawMaterialID;
        Quantity = _Quantity;
        db.CreateNewOrder(this);
    }

    int orderID;
    public int OrderID
    {
        get { return orderID; }
        set { orderID = value; }
    }

    DateTime dateOpened;
    public DateTime DateOpened
    {
        get { return dateOpened; }
        set { dateOpened = value; }
    }

    DateTime estimatedDateOfArrival;
    public DateTime EstimatedDateOfArrival
    {
        get { return estimatedDateOfArrival; }
        set { estimatedDateOfArrival = value; }
    }

    DateTime dateOfArrival;
    public DateTime DateOfArrival
    {
        get { return dateOfArrival; }
        set { dateOfArrival = value; }
    }

    int orderStatus;
    public int OrderStatus
    {
        get { return orderStatus; }
        set { orderStatus = value; }
    }

    int projectID;
    public int ProjectID
    {
        get { return projectID; }
        set { projectID = value; }
    }

    int supplierID;
    public int SupplierID
    {
        get { return supplierID; }
        set { supplierID = value; }
    }

    int rawMaterialID;
    public int RawMaterialID
    {
        get { return rawMaterialID; }
        set { rawMaterialID = value; }
    }

    int quantity;
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
}