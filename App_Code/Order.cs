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

    public Order(int _ProjectId, int _SupplierID, int _RawMaterialID, int _Quantity, DateTime _EstimatedDateOfArrival)
    {
        ProjectID = _ProjectId;
        DateOpened = DateTime.Now;
        EstimatedDateOfArrival = _EstimatedDateOfArrival;
        OrderStatus = 1;
        SupplierID = _SupplierID;
        RawMaterialID = _RawMaterialID;
        Quantity = _Quantity;
    }

    public void UpdateOrderStatus(int statusNUM, int OrderID)
    {
        DBservices db = new DBservices();
        db.UpdateOrderStatus(statusNUM, OrderID);
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

    public void CreateNewOrder(Order o)
    {
        DBservices db = new DBservices();
        db.CreateNewOrder(o);
    }

    public int UpdateOrderDetails(int oID, int Quantity, int OrderStatus, DateTime EstimatedDOA)
    {
        DBservices db = new DBservices();
        int RowAffected = db.UpdateOrderDetails(oID, Quantity, OrderStatus, EstimatedDOA);
        return RowAffected;
    }
}