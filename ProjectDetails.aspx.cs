using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.WebControls;


public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        int ProjectID = Convert.ToInt32(row.Cells[1].Text);

        DBservices db = new DBservices();
        DataTable dt = db.GetCustomerInformation(ProjectID);
        SetOrdersGrid();

        if (!Page.IsPostBack)
        {
            SetPageDetails(dt);
            SetProjCurrentStatus();
            LoadSuppliers();
        }
    }

    public void SetOrdersGrid()
    {
        
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        DBservices db = new DBservices();
        DataTable dt2 = db.GetOrdersDetails(Convert.ToInt32(row.Cells[1].Text));
        OrdersGrid.DataSource = dt2;
        OrdersGrid.DataBind();
        DataTable statustable = new DataTable();
        statustable=db.GetOrderStatus();
        

    }


    public void SetProjCurrentStatus()
    {
        DBservices db = new DBservices();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        string status = Convert.ToString(row.Cells[5].Text);
        int statusnumber = db.StatusNumber(status);
        ProjectInfoStatus.SelectedIndex = (statusnumber - 1);
    }

    public void SetPageDetails(DataTable dt)
    {
        ProjectInfoID.Value = Convert.ToString(dt.Rows[0].ItemArray[0]);
        ProjectInfoFirstName.Value = Convert.ToString(dt.Rows[0].ItemArray[1]);
        ProjectInfoLastName.Value = Convert.ToString(dt.Rows[0].ItemArray[2]);
        ProjectInfoPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[3]);
        ProjectInfoMobile.Value = Convert.ToString(dt.Rows[0].ItemArray[4]);
        ProjectInfoFax.Value = Convert.ToString(dt.Rows[0].ItemArray[5]);
        ProjectInfoAddress.Value = Convert.ToString(dt.Rows[0].ItemArray[6]);
        ProjectInfoCity.Value = Convert.ToString(dt.Rows[0].ItemArray[7]);
        ProjectInfoEmail.Value = Convert.ToString(dt.Rows[0].ItemArray[8]);
        ProjectInfoArchitectName.Value = Convert.ToString(dt.Rows[0].ItemArray[9]);
        ProjectInfoArchitectMobile.Value = Convert.ToString(dt.Rows[0].ItemArray[10]);
        ProjectInfoContractorName.Value = Convert.ToString(dt.Rows[0].ItemArray[11]);
        ProjectInfoContractorPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[12]);
        ProjectInfoSupervisorName.Value = Convert.ToString(dt.Rows[0].ItemArray[13]);
        ProjectInfoSupervisorPhone.Value = Convert.ToString(dt.Rows[0].ItemArray[14]);
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        ProjectInfoHatches.Value = row.Cells[8].Text;
        ProjectInfoPrice.Value = row.Cells[7].Text;
        ProjectInfoComments.Value = row.Cells[6].Text;
    }

    protected void DropDownDataBound(object sender, EventArgs e)
    {

    }

    protected void SaveCustomerDetailsBTN_Click1(object sender, EventArgs e)
    {
        int CustomerInitialID = Convert.ToInt32(ProjectInfoID.Value);
        DBservices db = new DBservices();
        Customer c = new Customer();
        c.Cid = Convert.ToInt32(ProjectInfoID.Value);
        c.Fname = ProjectInfoFirstName.Value;
        c.Lname = ProjectInfoLastName.Value;
        c.Phone = Convert.ToInt32(ProjectInfoPhone.Value);
        c.Mobile = Convert.ToInt32(ProjectInfoMobile.Value);
        c.Fax = Convert.ToInt32(ProjectInfoFax.Value);
        c.Address = ProjectInfoAddress.Value;
        c.City = ProjectInfoCity.Value;
        c.Email = ProjectInfoEmail.Value;
        db.UpdateCustomerInformation(c, CustomerInitialID);
        SaveCustomerDetailsBTN.Style.Add("display", "none");
        EditCustomerDetailsBTN.Style.Add("display", "inline-block");
    }

    protected void SaveProjectDetailsBTN_Click(object sender, EventArgs e)
    {
        DBservices db = new DBservices();
        GridViewRow row = (GridViewRow)Session["selectedrow"];
        db.UpdateProjectStatus(Convert.ToInt32(row.Cells[1].Text), ProjectInfoStatus.SelectedIndex + 1);
        db.UpdateProjectDetails(Convert.ToInt32(row.Cells[1].Text), Convert.ToInt32(ProjectInfoPrice.Value), ProjectInfoComments.Value);
        SaveProjectDetailsBTN.Style.Add("display", "none");
        EditProjectDetailsBTN.Style.Add("display", "inline-block");
    }

    public void LoadSuppliers()
    {
        DBservices db = new DBservices();
        db.LoadSuppliers(ShutterProvider, 1);
        db.LoadSuppliers(CollectedProvider, 2);
        db.LoadSuppliers(ValimProvider, 3);
        db.LoadSuppliers(UProvider, 4);
        db.LoadSuppliers(ShoeingProvider, 5);
        db.LoadSuppliers(EngineProvider, 6);
        db.LoadSuppliers(ProtectedSpaceProvider, 7);
        db.LoadSuppliers(GlassProvider, 8);
        db.LoadSuppliers(BoxesProvider, 9);
    }

    //public void GetOrderStatus()
    //{
    //    DBservices db = new DBservices();
    //    db.GetOrderStatus(ShuttersCount);
    //    db.GetOrderStatus(CollectedCount);
    //    db.GetOrderStatus(ValimCount);
    //    db.GetOrderStatus(UCount);
    //    db.GetOrderStatus(ShuttersCount);
    //    db.GetOrderStatus(EnginCount);
    //    db.GetOrderStatus(ProtectedSpaceCount);
    //    db.GetOrderStatus(GlassCount);
    //    db.GetOrderStatus(BoxesCount);
    //}


    protected void Button1_Click(object sender, EventArgs e)
    {

        CreateOrder(Convert.ToInt32(ShutterCount.Text), Convert.ToString(ShutterProvider.SelectedValue),1);
       
    }

    public void CreateOrder(int Count, string Supplier,int RawMeterialID)
    {
        if (Count > 0)
        {
            DBservices db = new DBservices();
            Order o = new Order();
            GridViewRow row = (GridViewRow)Session["selectedrow"];
            o.ProjectID1 = Convert.ToInt32(row.Cells[1].Text);
            o.DateOpened1 = DateTime.Now;
            o.EstimateDateOfArrival1 = o.DateOpened1.AddDays(14); // בשלב זה נניח שתאריך ההגעה של ההזמנה תגיע בתוך 14 ימים
            o.OrderStatus1 = 1;
            o.SupplierID1=db.GetSupplierID(Supplier);
            o.RawMeterialID1 = RawMeterialID;  //השורה הזאת קצת יכולה להוות בעיה כי טכנית אני שולח את מספר האיי די לפי הסדר בטבלה אבל אם זה משתנה אז זה לא תקין
            o.Quantity1 = Count;
            db.CreateNewOrder(o);
            SetOrdersGrid();
        }
    }
}


//Public void PageLoad (......) {
//    DataSet ordersSet = // fetch all orders, including StatusID column
//    DataSet statusesSet = // fetch all status ID's and Names
	
//    String[] statusesArray = new String[]();
	
//    for each status String in statusesSet {
//        statusesArray.push(status);
//    }
	
//    for each DataRow order in ordersSet {
//        // inject all order rows to a table/placeholder
//        // when getting to the statuses column you can do something like that:
//        DropDownList _statusesDD = new DropDownList();
//        // iterate on statusesArray and add its elements to a DropDownList
//        for (Int32 i = 0; i < statusesArray.length; i++) {
//            _statusesDD.AddItem(i, statusesArray[i]);
//        }
//        // then just inject the dropdown to the table
		
//    }
//}