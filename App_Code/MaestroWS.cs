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
using System.Net.Mail;

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
    public string GetProjects()
    {
        Project p = new Project();
        DataTable dt = p.GetProjectDetails();

        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < myAL.Length; i++)
        {
            p = new Project();
            Customer c = new Customer();
            ProjectStatus ps = new ProjectStatus();

            p.pID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            p.Name = dt.Rows[i].ItemArray[1].ToString();
            p.Cost = Convert.ToInt32(dt.Rows[i].ItemArray[6]);
            p.HatchesImageURL = dt.Rows[i].ItemArray[13].ToString();

            c.Fname = dt.Rows[i].ItemArray[17].ToString();
            c.Lname = dt.Rows[i].ItemArray[18].ToString();
            c.Address = dt.Rows[i].ItemArray[19].ToString();
            c.Phone = dt.Rows[i].ItemArray[20].ToString();
            c.Mobile = dt.Rows[i].ItemArray[21].ToString();
            c.Fax = dt.Rows[i].ItemArray[22].ToString();
            c.Email = dt.Rows[i].ItemArray[23].ToString();

            p.ContractorName = dt.Rows[i].ItemArray[7].ToString();
            p.ContractorPhone = dt.Rows[i].ItemArray[8].ToString();
            p.ArchitectName = dt.Rows[i].ItemArray[9].ToString();
            p.ArchitectPhone = dt.Rows[i].ItemArray[10].ToString();
            p.SupervisorName = dt.Rows[i].ItemArray[11].ToString();
            p.SupervisorPhone = dt.Rows[i].ItemArray[12].ToString();

            ps.StatusNum = Convert.ToInt32(dt.Rows[i].ItemArray[25]);
            ps.StatusName = dt.Rows[i].ItemArray[26].ToString();

            myAL[i].Add(c);
            myAL[i].Add(p);
            myAL[i].Add(ps);
        }

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetHatches()
    {
        Project p = new Project();

        DataTable dt = p.GetHatches();
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

            p.HatchesImageURL = dt.Rows[i].ItemArray[7].ToString();
            p.pID = Convert.ToInt32(dt.Rows[i].ItemArray[8]);

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
    public string GetPicsAndPins(int HatchID)
    {
        Hatch h = new Hatch();

        DataTable dt = h.GetPicsAndPins(HatchID);
        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < myAL.Length; i++)
        {
            Picture pic = new Picture();
            Pin pin = new Pin();

            pic.HatchID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            pic.PictureDescription = dt.Rows[i].ItemArray[2].ToString();
            pic.DateTaken = Convert.ToDateTime(dt.Rows[i].ItemArray[3]);
            pic.ImageURL = dt.Rows[i].ItemArray[4].ToString();

            if (!System.DBNull.Value.Equals(dt.Rows[i].ItemArray[5])) // show pictures without pins
            {
                pin.PinID = Convert.ToInt32(dt.Rows[i].ItemArray[5]);
                pin.PictureID = Convert.ToInt32(dt.Rows[i].ItemArray[1]);
                pin.CoordinateX = Convert.ToDouble(dt.Rows[i].ItemArray[6]);
                pin.CoordinateY = Convert.ToDouble(dt.Rows[i].ItemArray[7]);
                pin.Comment = dt.Rows[i].ItemArray[8].ToString();
                pin.AudioURL = dt.Rows[i].ItemArray[9].ToString();
                pin.VideoURL = dt.Rows[i].ItemArray[10].ToString();
            }

            myAL[i].Add(pic);
            myAL[i].Add(pin);
        }
        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

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
    public string GetHatchesForProdApp()
    {
        Project p = new Project();
        DataTable dt = p.GetHatchesForProdApp();

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
        int eID = h.GetUsernameID(User.Identity.Name);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(eID);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string UpdateHatchDetails(int HatchID, int HatchTypeID, int HatchStatusID, int FailureTypeID, int EmployeeID, string Date, string Comments)
    {
        Hatch h = new Hatch(HatchID, HatchStatusID, FailureTypeID, EmployeeID, Convert.ToDateTime(Date), Comments, HatchTypeID);
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string UploadPicture(int HatchID, string PictureDesc, string ImageURL)
    {
        Picture pic = new Picture(HatchID, PictureDesc, DateTime.Today.Date, ImageURL);
        int RowAffected = pic.UploadPicture();

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectsIncome()
    {
        Project p = new Project();
        DataTable dt = p.GetProjectsIncome();

        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            p = new Project();
            p.Name = dt.Rows[i].ItemArray[0].ToString();
            p.Cost = Convert.ToDouble(dt.Rows[i].ItemArray[1]);

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
    public string GetNotifications(int eID)
    {
        Notification n = new Notification();
        DataTable dt = n.GetNotifications(eID);

        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            n = new Notification();
            n.nNotification = dt.Rows[i].ItemArray[0].ToString();
            n.nDate = Convert.ToDateTime(dt.Rows[i].ItemArray[1]).Date;

            myAL[i].Add(n);
        }

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string InsertNewNotification(string Message, string MessageDate, int eID)
    {
        Notification n = new Notification(Message, DateTime.ParseExact(MessageDate, "dd/MM/yyyy", null), eID, 0);
        int RowAffected = n.InsertNewNotification();

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetSpecialNotifications(int eID)
    {
        SpecialNotification sn = new SpecialNotification();
        DataTable dt = sn.GetSpecialNotifications(eID);

        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sn = new SpecialNotification();

            sn.nID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            sn.nNotification = dt.Rows[i].ItemArray[1].ToString();
            sn.nDate = Convert.ToDateTime(dt.Rows[i].ItemArray[2]);
            sn.nType = dt.Rows[i].ItemArray[3].ToString();
            sn.EmailSubject = dt.Rows[i].ItemArray[4].ToString();
            sn.EmailMessage = dt.Rows[i].ItemArray[5].ToString();
            sn.EmailAddress = dt.Rows[i].ItemArray[6].ToString();

            myAL[i].Add(sn);
        }

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SendEmail(string TargetEmailAddress, string Subject, string sHTMLBody)
    {
        try
        {
            // encode back to original char
            sHTMLBody = sHTMLBody.Replace("~", "<");
            sHTMLBody = sHTMLBody.Replace("|", ">");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("doron9787@gmail.com", "מאסטרו אלומיניום");
            mailMessage.To.Add(TargetEmailAddress);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sHTMLBody;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("doron9787@gmail.com", "YourPassword");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteSpecialNotification(int NotificationID)
    {
        SpecialNotification sn = new SpecialNotification();
        int RowAffected = sn.DeleteSpecialNotification(NotificationID);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectStatus(int ProjectID)
    {
        Project p = new Project();
        DataTable dt = p.GetProjectStatus(ProjectID);

        string ProjectStatus = dt.Rows[0].ItemArray[0].ToString();

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(ProjectStatus);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProjectHatches(int ProjectID)
    {
        Project p = new Project();
        DataTable dt = p.GetProjectHatches(ProjectID);

        ArrayList[] myAL = new ArrayList[dt.Rows.Count];

        for (int i = 0; i < myAL.Length; i++)
            myAL[i] = new ArrayList();

        Hatch h = new Hatch();
        for (int i = 0; i < myAL.Length; i++)
        {
            h = new Hatch();

            h.HatchID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
            h.HatchStatus = dt.Rows[i].ItemArray[2].ToString();
            h.FtName = dt.Rows[i].ItemArray[5].ToString();
            h.Comments = dt.Rows[i].ItemArray[6].ToString();

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
    public string InsertNewPin(int pinID, double x, double y, string message, string audioPath, string videoPath, int PictureID)
    {
        Pin p = new Pin(pinID, x, y, message, audioPath, videoPath, PictureID);
        int RowAffected = p.InsertNewPin();

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeletePin(int pinID)
    {
        Pin p = new Pin();
        int RowAffected = p.DeletePin(pinID);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetTableCurrentIdentity(string TableName)
    {
        Picture p = new Picture();
        int Identity = p.GetTableCurrentIdentity(TableName);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(Identity);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string InsertNewFile(string Description, string Url, int ProjectID)
    {
        File f = new File(Description, Url, ProjectID);
        int RowAffected = f.InsertNewFile();

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DeleteFile(int FileID)
    {
        File f = new File();
        int RowAffected = f.DeleteFile(FileID);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(RowAffected);
        return jsonString;
    }
}