using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Data;
using System.Web.Security;


/// <summary>
/// Summary description for MaestroWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MaestroWS : System.Web.Services.WebService
{
    public MaestroWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Login(string Username, string Password)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string jsonString = "";
        bool ans = Membership.ValidateUser(Username, Password);
        if (ans)
        {
            FormsAuthentication.SetAuthCookie(Username, false);
            jsonString = js.Serialize(ans);
            return jsonString;
        }
        jsonString = js.Serialize(ans);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectsList()
    {
        Project p = new Project();
        DataTable dt = p.GetProjects();

        int[] ProjArr = new int[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
            ProjArr[i] = Convert.ToInt32(dt.Rows[i].ItemArray[0]);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(ProjArr);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectDetails(int pID)
    {
        Project p = new Project();
        DataTable dt = p.GetProjectDetails(pID);

        Customer c = new Customer();
        ProjectStatus ps = new ProjectStatus();
        ArrayList myAL = new ArrayList();

        p.pID = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        p.Name = dt.Rows[0].ItemArray[1].ToString();
        p.Cost = Convert.ToInt32(dt.Rows[0].ItemArray[6]);
        p.HatchesImageURL = dt.Rows[0].ItemArray[13].ToString();

        c.Fname = dt.Rows[0].ItemArray[17].ToString();
        c.Lname = dt.Rows[0].ItemArray[18].ToString();
        c.Address = dt.Rows[0].ItemArray[19].ToString();
        c.Phone = dt.Rows[0].ItemArray[20].ToString();
        c.Mobile = dt.Rows[0].ItemArray[21].ToString();
        c.Fax = dt.Rows[0].ItemArray[22].ToString();
        c.Email = dt.Rows[0].ItemArray[23].ToString();

        p.ContractorName = dt.Rows[0].ItemArray[7].ToString();
        p.ContractorPhone = dt.Rows[0].ItemArray[8].ToString();
        p.ArchitectName = dt.Rows[0].ItemArray[9].ToString();
        p.ArchitectPhone = dt.Rows[0].ItemArray[10].ToString();
        p.SupervisorName = dt.Rows[0].ItemArray[11].ToString();
        p.SupervisorPhone = dt.Rows[0].ItemArray[12].ToString();

        ps.StatusNum = Convert.ToInt32(dt.Rows[0].ItemArray[25]);
        ps.StatusName = dt.Rows[0].ItemArray[26].ToString();

        myAL.Add(c);
        myAL.Add(p);
        myAL.Add(ps);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectHatches(int pID)
    {
        Project p = new Project();

        DataTable dt = p.GetProjectHatches(pID);
        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < myAL.Length; i++)
        {
            p = new Project();
            Hatch h = new Hatch();

            h.HatchID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            h.HatchType = dt.Rows[i].ItemArray[1].ToString();
            h.HatchStatus = dt.Rows[i].ItemArray[2].ToString();
            h.StatusLastModified = Convert.ToDateTime(dt.Rows[i].ItemArray[3]);
            h.EmployeeName = dt.Rows[i].ItemArray[4].ToString();
            h.FtName = dt.Rows[i].ItemArray[5].ToString();
            h.Comments = dt.Rows[i].ItemArray[6].ToString();

            p.pID = pID;
            p.HatchesImageURL = dt.Rows[i].ItemArray[7].ToString();

            myAL[i].Add(p);
            myAL[i].Add(h);
        }
        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetPicsAndPins(int ProjectID)
    {
        Hatch h = new Hatch();

        DataTable dt = h.GetPicsAndPins(ProjectID);
        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < myAL.Length; i++)
        {
            h = new Hatch();
            Picture pic = new Picture();
            Pin pin = new Pin();

            h.ProjectID = ProjectID;
            h.HatchID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);

            pic.PictureID = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
            pic.PictureDescription = dt.Rows[i].ItemArray[2].ToString();
            pic.DateTaken = Convert.ToDateTime(dt.Rows[i].ItemArray[3]);
            pic.ImageURL = dt.Rows[i].ItemArray[4].ToString();

            pin.PinID = Convert.ToInt32(dt.Rows[i].ItemArray[5]);
            pin.CoordinateX = Convert.ToDouble(dt.Rows[i].ItemArray[6]);
            pin.CoordinateY = Convert.ToDouble(dt.Rows[i].ItemArray[7]);
            pin.Comment = dt.Rows[i].ItemArray[8].ToString();
            pin.AudioURL = dt.Rows[i].ItemArray[9].ToString();
            pin.VideoURL = dt.Rows[i].ItemArray[10].ToString();

            myAL[i].Add(h);
            myAL[i].Add(pic);
            myAL[i].Add(pin);
        }
        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    //Prod App functions
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectListForProdApp()
    {
        Project p = new Project();
        DataTable dt = p.GetProjectListForProdApp();
        int[] ProjArr = new int[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
            ProjArr[i] = Convert.ToInt32(dt.Rows[i].ItemArray[0]);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(ProjArr);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectHatchesForProdApp(int pID)
    {
        Project p = new Project();
        DataTable dt = p.GetProjectHatchesForProdApp(pID);

        Hatch h = new Hatch();
        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < myAL.Length; i++)
        {
            p = new Project();
            h = new Hatch();

            p.pID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            p.Name = dt.Rows[i].ItemArray[1].ToString();
            p.HatchesImageURL = dt.Rows[i].ItemArray[2].ToString();
            h.HatchID = Convert.ToInt32(dt.Rows[i].ItemArray[3]);
            h.HatchType = dt.Rows[i].ItemArray[4].ToString();
            h.HatchStatus = dt.Rows[i].ItemArray[5].ToString();
            h.EmployeeName = dt.Rows[i].ItemArray[6].ToString();
            h.StatusLastModified = Convert.ToDateTime(dt.Rows[i].ItemArray[7]);
            h.FtName = dt.Rows[i].ItemArray[8].ToString();
            h.Comments = dt.Rows[i].ItemArray[9].ToString();

            myAL[i].Add(p);
            myAL[i].Add(h);
        }

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetHatchStatusList()
    {
        Hatch h = new Hatch();
        DataTable dt = h.GetHatchStatusList();
        Dictionary<string, string> dic = new Dictionary<string, string>();

        for (int i = 0; i < dt.Rows.Count; i++)
            dic.Add(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString());

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(dic);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetFailureTypeList()
    {
        Hatch h = new Hatch();
        DataTable dt = h.GetFailureTypeList();
        Dictionary<string, string> dic = new Dictionary<string, string>();

        for (int i = 0; i < dt.Rows.Count; i++)
            dic.Add(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString());

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(dic);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUsernameID()
    {
        Hatch h = new Hatch();
        DataTable dt = h.GetUsernameID(User.Identity.Name);
        int eID = Convert.ToInt32(dt.Rows[0].ItemArray[0]);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(eID);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string UpdateHatchDetails(int HatchID, int HatchStatusID, int FailureTypeID, int EmployeeID, string Date, string Comments)
    {
        Hatch h = new Hatch(HatchID, HatchStatusID, FailureTypeID, EmployeeID, Convert.ToDateTime(Date), Comments);
        int RowAffected = h.UpdateHatchDetails();

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetOpenedServiceCalls()
    {
        ServiceCall sc = new ServiceCall();
        DataTable dt = sc.GetOpenedServiceCalls();

        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < myAL.Length; i++)
        {
            sc = new ServiceCall();
            Customer c = new Customer();
            Project p = new Project();

            sc.ScID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            sc.Description = dt.Rows[i].ItemArray[1].ToString();
            sc.Urgent = Convert.ToBoolean(dt.Rows[i].ItemArray[2]);
            sc.DateOpened = Convert.ToDateTime(dt.Rows[i].ItemArray[3]);
            if (!System.DBNull.Value.Equals(dt.Rows[i].ItemArray[4]))
                sc.DateClosed = Convert.ToDateTime(dt.Rows[i].ItemArray[4]);

            c.cID = Convert.ToInt32(dt.Rows[i].ItemArray[7]);
            c.Fname = dt.Rows[i].ItemArray[8].ToString();
            c.Lname = dt.Rows[i].ItemArray[9].ToString();
            c.Address = dt.Rows[i].ItemArray[10].ToString();
            c.Phone = dt.Rows[i].ItemArray[11].ToString();
            c.Mobile = dt.Rows[i].ItemArray[12].ToString();
            c.Fax = dt.Rows[i].ItemArray[13].ToString();
            c.Email = dt.Rows[i].ItemArray[14].ToString();

            if (!System.DBNull.Value.Equals(dt.Rows[i].ItemArray[16]))
                p.pID = Convert.ToInt32(dt.Rows[i].ItemArray[16]);
            if (!System.DBNull.Value.Equals(dt.Rows[i].ItemArray[17]))
                p.Name = dt.Rows[i].ItemArray[17].ToString();
            if (!System.DBNull.Value.Equals(dt.Rows[i].ItemArray[19]))
                p.DateOpened = Convert.ToDateTime(dt.Rows[i].ItemArray[19]);
            if (!System.DBNull.Value.Equals(dt.Rows[i].ItemArray[20]))
                p.ExpirationDate = Convert.ToDateTime(dt.Rows[i].ItemArray[20]);

            myAL[i].Add(sc);
            myAL[i].Add(c);
            myAL[i].Add(p);
        }

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectsNames()
    {
        Project p = new Project();
        DataTable dt = p.GetProjectsNames();
        Dictionary<string, string> dic = new Dictionary<string, string>();

        for (int i = 0; i < dt.Rows.Count; i++)
            dic.Add(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString());

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(dic);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string CreateServiceCall(int ProjectID, string ProblemDescription, string Date, bool Urgent)
    {
        ServiceCall sc = new ServiceCall(Convert.ToDateTime(Date), ProblemDescription, Urgent);
        DataTable dt = new DataTable();

        Project p = new Project();
        dt = p.GetCustomerInformation(ProjectID);
        int cID = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        int RowAffected = sc.InsertServiceCallExistingProject(sc, cID, ProjectID);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string CloseServiceCall(int scID)
    {
        ServiceCall sc = new ServiceCall();
        int RowAffected = sc.CloseServiceCall(scID);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

}