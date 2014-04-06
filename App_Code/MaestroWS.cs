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
    public string LoadProjectsList()
    {
        Project p = new Project();
        DataTable dt = p.GetProjects();
        Project[] Projects = new Project[dt.Rows.Count];
        for (int i = 0; i < Projects.Length; i++)
            Projects[i] = new Project();

        for (int i = 0; i < Projects.Length; i++)
            Projects[i].Pid1 = Convert.ToInt32(dt.Rows[i].ItemArray[0]);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(Projects);
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

        p.Pid1 = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
        ps.Statusname = dt.Rows[0].ItemArray[24].ToString();
        p.Comment1 = dt.Rows[0].ItemArray[1].ToString();
        p.OpenedDate1 = Convert.ToDateTime(dt.Rows[0].ItemArray[2]);
        p.ExpirationDate1 = Convert.ToDateTime(dt.Rows[0].ItemArray[3]);
        p.Price = Convert.ToInt32(dt.Rows[0].ItemArray[4]);

        c.Fname = dt.Rows[0].ItemArray[14].ToString();
        c.Lname = dt.Rows[0].ItemArray[15].ToString();
        c.Phone = Convert.ToInt32(dt.Rows[0].ItemArray[18]);
        c.Mobile = Convert.ToInt32(dt.Rows[0].ItemArray[19]);
        c.Fax = Convert.ToInt32(dt.Rows[0].ItemArray[20]);
        c.Email = dt.Rows[0].ItemArray[21].ToString();

        p.ContractorName1 = dt.Rows[0].ItemArray[5].ToString();
        p.ContractorPhone1 = Convert.ToInt32(dt.Rows[0].ItemArray[6]);
        p.ArchitectName1 = dt.Rows[0].ItemArray[7].ToString();
        p.ArchitectPhone1 = Convert.ToInt32(dt.Rows[0].ItemArray[8]);
        p.SupervisorName1 = dt.Rows[0].ItemArray[9].ToString();
        p.SupervisorPhone1 = Convert.ToInt32(dt.Rows[0].ItemArray[10]);

        myAL.Add(c);
        myAL.Add(p);
        myAL.Add(ps);

        // create a json serializer object
        JavaScriptSerializer js = new JavaScriptSerializer();
        // serialize to string
        string jsonString = js.Serialize(myAL);
        return jsonString;
    }
}
