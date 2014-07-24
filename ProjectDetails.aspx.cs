using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.WebControls;


public partial class Default : System.Web.UI.Page
{
    static DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            int ProjectID = Convert.ToInt32(Session["ProjectID"]);

            Project p = new Project();
            DataTable DetailsTable = p.GetAllDetails(ProjectID);

            if (!Page.IsPostBack)
            {
                SetPageDetails(DetailsTable);
                GetProjectFiles(ProjectID);
                GetProjectHatchesPicture(ProjectID);
            }
            else
                DisableAllFields();

            ProjectIDHolder.Value = Session["ProjectID"].ToString();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ActivatePopover", "$('#ProjectDetailsStatusIcon').popover({ html: true, content: GetProgressBarContent() });", true);
        }
    }

    public void DisableAllFields()
    {
        //Customer Fields
        ProjectInfoID.Attributes.Add("disabled", "disabled");
        ProjectInfoFirstName.Attributes.Add("disabled", "disabled");
        ProjectInfoLastName.Attributes.Add("disabled", "disabled");
        ProjectInfoPhone.Attributes.Add("disabled", "disabled");
        ProjectInfoMobile.Attributes.Add("disabled", "disabled");
        ProjectInfoAddress.Attributes.Add("disabled", "disabled");
        ProjectInfoEmail.Attributes.Add("disabled", "disabled");
        ProjectInfoFax.Attributes.Add("disabled", "disabled");
        ProjectInfoArea.Attributes.Add("disabled", "disabled");

        //Project Fields
        ProjectInfoStatus.Attributes.Add("disabled", "disabled");
        ProjectInfoName.Attributes.Add("disabled", "disabled");
        ProjectInfoHatches.Attributes.Add("disabled", "disabled");
        ProjectInfoCost.Attributes.Add("disabled", "disabled");
        ProjectInfoComments.Attributes.Add("disabled", "disabled");
        ProjectInfoDateOpened.Attributes.Add("disabled", "disabled");
        ProjectInfoExpirationDate.Attributes.Add("disabled", "disabled");
        ProjectInfoInstallationDate.Attributes.Add("disabled", "disabled");
        ProjectInfoArchitectName.Attributes.Add("disabled", "disabled");
        ProjectInfoArchitectMobile.Attributes.Add("disabled", "disabled");
        ProjectInfoContractorName.Attributes.Add("disabled", "disabled");
        ProjectInfoContractorMobile.Attributes.Add("disabled", "disabled");
        ProjectInfoSupervisorName.Attributes.Add("disabled", "disabled");
        ProjectInfoSupervisorMobile.Attributes.Add("disabled", "disabled");
    }

    public void SetPageDetails(DataTable DetailsTable)
    {
        //Populate customer details
        ProjectInfoID.Text = DetailsTable.Rows[0].ItemArray[0].ToString();
        ProjectInfoFirstName.Text = DetailsTable.Rows[0].ItemArray[1].ToString();
        ProjectInfoLastName.Text = DetailsTable.Rows[0].ItemArray[2].ToString();
        ProjectInfoAddress.Text = DetailsTable.Rows[0].ItemArray[3].ToString();
        ProjectInfoPhone.Text = DetailsTable.Rows[0].ItemArray[4].ToString();
        ProjectInfoMobile.Text = DetailsTable.Rows[0].ItemArray[5].ToString();
        ProjectInfoFax.Text = DetailsTable.Rows[0].ItemArray[6].ToString();
        ProjectInfoEmail.Text = DetailsTable.Rows[0].ItemArray[7].ToString();
        ProjectInfoArea.SelectedValue = DetailsTable.Rows[0].ItemArray[21].ToString();

        //Populate project details
        ProjectInfoName.Text = DetailsTable.Rows[0].ItemArray[8].ToString();
        ProjectInfoStatus.SelectedValue = DetailsTable.Rows[0].ItemArray[9].ToString();
        ProjectInfoArchitectName.Text = DetailsTable.Rows[0].ItemArray[11].ToString();
        ProjectInfoArchitectMobile.Text = DetailsTable.Rows[0].ItemArray[12].ToString();
        ProjectInfoContractorName.Text = DetailsTable.Rows[0].ItemArray[13].ToString();
        ProjectInfoContractorMobile.Text = DetailsTable.Rows[0].ItemArray[14].ToString();
        ProjectInfoSupervisorName.Text = DetailsTable.Rows[0].ItemArray[15].ToString();
        ProjectInfoSupervisorMobile.Text = DetailsTable.Rows[0].ItemArray[16].ToString();
        ProjectInfoHatches.Text = DetailsTable.Rows[0].ItemArray[22].ToString();
        ProjectInfoCost.Text = DetailsTable.Rows[0].ItemArray[10].ToString();
        ProjectInfoComments.Text = DetailsTable.Rows[0].ItemArray[20].ToString();
        ProjectInfoDateOpened.Value = ((DateTime)DetailsTable.Rows[0].ItemArray[17]).ToString("MM/dd/yyyy");
        ProjectInfoExpirationDate.Value = ((DateTime)DetailsTable.Rows[0].ItemArray[18]).ToString("MM/dd/yyyy");
        if (!DetailsTable.Rows[0].ItemArray[19].Equals(System.DBNull.Value))
            ProjectInfoInstallationDate.Value = ((DateTime)DetailsTable.Rows[0].ItemArray[19]).ToString("MM/dd/yyyy");
    }

    protected void SaveCustomerDetailsBTN_Click1(object sender, EventArgs e)
    {
        Customer c = new Customer();
        c.SaveCustomerNewDetails(ProjectInfoFirstName.Text, ProjectInfoLastName.Text, ProjectInfoPhone.Text, ProjectInfoMobile.Text, ProjectInfoFax.Text, ProjectInfoAddress.Text, ProjectInfoEmail.Text, Convert.ToInt32(ProjectInfoArea.SelectedValue), Convert.ToInt32(ProjectInfoID.Text));
        SaveCustomerDetailsBTN.Style.Add("display", "none");
        EditCustomerDetailsBTN.Style.Add("display", "inline-block");
    }

    protected void SaveProjectDetailsBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            int pID = Convert.ToInt32(Session["ProjectID"]);
            Project p = new Project();
            p.UpdateProjectDetails(pID, Convert.ToDouble(ProjectInfoCost.Text), ProjectInfoName.Text, ProjectInfoComments.Text, ProjectInfoArchitectName.Text, ProjectInfoArchitectMobile.Text, ProjectInfoContractorName.Text, ProjectInfoContractorMobile.Text, ProjectInfoSupervisorName.Text, ProjectInfoSupervisorMobile.Text, DateTime.ParseExact(ProjectInfoExpirationDate.Value, "MM/dd/yyyy", null), DateTime.ParseExact(ProjectInfoInstallationDate.Value, "MM/dd/yyyy", null), Convert.ToInt32(ProjectInfoStatus.SelectedValue));


            //Notifications management
            switch (ProjectInfoStatus.SelectedValue)
            {
                case "2": //הזמנת עבודה
                    InsertNewProjectNotification(ProjectInfoName.Text);
                    break;

                case "3": // משקוף עיוור
                    InsertBlindFrameNotification(ProjectInfoFirstName.Text, ProjectInfoLastName.Text, ProjectInfoEmail.Text);
                    break;

                case "4": //סגירת פרטים
                    InsertDetailsClosureNotification(pID);
                    break;

                case "7": //ייצור
                    p.SetStatusProduction(pID);
                    break;

                case "8": //התקנה
                    bool ProjectInTime = AreHatchesReadyForInstallation(pID) ? true : false;
                    InsertInstallationStatusNotification(ProjectInTime, ProjectInfoName.Text);
                    break;

                case "9": //סיום פרויקט
                    DateTime ExpirationDate = DateTime.Now.AddYears(2);
                    p.SetExpirationDate(pID, ExpirationDate);
                    break;


                default:
                    break;
            }

            SaveProjectDetailsBTN.Style.Add("display", "none");
            EditProjectDetailsBTN.Style.Add("display", "inline-block");
        }
    }

    protected void GotoProjectOrdersHiddenBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            Session["ProjectIDForProjectOrders"] = Session["ProjectID"];
            Session["ProjectNameForProjectOrders"] = ProjectInfoName.Text;
            Response.Redirect("~/ProjectOrders.aspx");
        }
    }
    protected void OpenServiceCallHiddenBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            Session["ProjectIDForServiceCall"] = Session["ProjectID"];
            Session["ProjectNameForServiceCall"] = ProjectInfoName.Text;
            Response.Redirect("~/NewServiceCall.aspx?Source=ExistingProject");
        }
    }

    public bool AreHatchesReadyForInstallation(int pID)
    {
        Project p = new Project();
        dt = p.GetProjectHatches(pID);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i].ItemArray[2].ToString() != "הועמס")
                return false;
        }
        return true;
    }

    public void InsertNewProjectNotification(string ProjectName)
    {
        string Message = String.Format("נפתח פרויקט חדש {0}, נא להיערך לקראת משקוף עיוור.", ProjectName);
        Notification n = new Notification(Message, DateTime.Now.Date, 302042267, 38124123);
        n.InsertNewNotification();
    }

    public void InsertInstallationStatusNotification(bool ProjectInTime, string ProjectName)
    {
        Notification n = new Notification();
        if (ProjectInTime)
        {
            string Message = "הפרויקט " + ProjectName + " מתבצע כמתוכנן, הפתחים מוכנים להתקנה בבית הלקוח.";
            n = new Notification(Message, DateTime.Now.Date, 302042267, 38124123);
        }
        else
        {
            string sMessage = String.Format("הפתחים הבאים טרם מוכנים להתקנה בבית הלקוח עבור הפרויקט {0}:{1}", ProjectInfoName.Text, "<br>");
            //need to build a loop through all hatches

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[2].ToString() != "הועמס")
                {
                    int hID = Convert.ToInt32(dt.Rows[i].ItemArray[0]);
                    string HatchStatus = dt.Rows[i].ItemArray[2].ToString();
                    string Comments = dt.Rows[i].ItemArray[6].ToString();
                    string FailureType = "";

                    if (HatchStatus == "תקלה")
                        FailureType = dt.Rows[i].ItemArray[5].ToString();
                    if (FailureType != "")
                        sMessage += String.Format("{0} בסטטוס {1}, {2}, {3}{4}", hID, HatchStatus, FailureType, Comments, "<br>");
                    else
                        sMessage += String.Format("{0} בסטטוס {1}, {2}, {3}", hID, HatchStatus, Comments, "<br>");
                }
            }
            n = new Notification(sMessage, DateTime.Now.Date, 302042267, 38124123);
        }
        n.InsertNewNotification();
    }

    public void InsertDetailsClosureNotification(int ProjectID)
    {
        DateTime NotificationDate = DateTime.Now.Date.AddDays(46);
        //string sMessage = String.Format("בעוד שבועיים יחלפו 60 יום ממועד הפגישה לסגירת פרטים והפתחים הנל לא מוכנים להתקנה בבית הלקוח עבור פרויקט {0}:{1}", ProjectName, "<br>", hID, HatchStatus, FailureType, Comments);
        SpecialNotification sn = new SpecialNotification(ProjectID.ToString(), NotificationDate, 302042267, 38124123, "סגירת פרטים");
        sn.InsertNewSpecialNotification();
    }

    public void InsertBlindFrameNotification(string FirstName, string LastName, string CustomerEmailAddress)
    {
        string EmailMessage = String.Format("<div dir='rtl'>{0} {1} שלום,<br><br>נא הבא לידיעתך כי עלינו לתאם פגישה לטובת סגירת פרטי הפרויקט.<br>אנא צור קשר טלפוני במס' 04-6221774 לתיאום הפגישה.<br><br>בתודה,<br>מאסטרו אלומיניום.</div>", FirstName, LastName);
        string EmailSubject = "תיאום פגישה לסגירת פרטים";

        SpecialNotification sn = new SpecialNotification("", DateTime.Now.Date.AddDays(45), 302042267, 38124123, "משקוף עיוור", EmailSubject, EmailMessage, CustomerEmailAddress);
        sn.InsertNewSpecialNotification();
    }

    public void GetProjectFiles(int ProjectID)
    {
        File f = new File();
        DataTable dt = f.GetProjectFiles(ProjectID);

        string sHTML = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sFileID = dt.Rows[i].ItemArray[0].ToString();
            string sUrl = dt.Rows[i].ItemArray[2].ToString();
            string sFileName = Path.GetFileName(sUrl);
            sHTML +=
                "<div id='File" + sFileID + "' class='FileBlock'>" +
                    "<a class='DeleteFile pointer glyphicon glyphicon-remove'></a>&nbsp;&nbsp;" +
                    "<a href='" + sUrl + "' target = '_blank'>" + sFileName + "</a>" +
                "</div>";
        }
        ProjectFiles.InnerHtml = sHTML;
    }

    public void GetProjectHatchesPicture(int ProjectID)
    {
        Project p = new Project();
        DataTable dt = p.GetHatchesImageURL(ProjectID);

        string sURL = dt.Rows[0].ItemArray[0].ToString();
        if (sURL != "")
        {
            string sHTML =
                "<div id='ProjectHatchesFileHolder' class='FileBlock'>" +
                    "<a class='DeleteFile pointer glyphicon glyphicon-remove'></a>&nbsp;&nbsp;" +
                    "<a href='" + sURL + "' target = '_blank'>תרשים פתחים</a>" +
                "</div>";
            ProjectHatchesPictureContainer.InnerHtml = sHTML;
        }
    }

    //public void UploadHatchesImage_Click(object sender, EventArgs e)
    //{
    //    Project p = new Project();
    //    string sURL = "";
    //    int ProjectID = Convert.ToInt32(ProjectIDHolder.Value);
    //    if (ProjectHatchesPicture.HasFile)
    //    {
    //        sURL = Path.GetFileName(ProjectHatchesPicture.FileName);
    //        sURL = Server.MapPath(".") + "/files/ProjectsFiles/" + sURL;
    //        ProjectHatchesPicture.SaveAs(sURL);
    //        p.UploadHatchesImage(ProjectID, sURL);
    //    }
    //    string sHTML =
    //        "<div id='ProjectHatchesFileHolder' class='FileBlock'>" +
    //            "<a class='DeleteFile pointer glyphicon glyphicon-remove'></a>&nbsp;&nbsp;" +
    //            "<a href='" + sURL + "' target = '_blank'>תרשים פתחים</a>" +
    //        "</div>";
    //    ProjectHatchesPictureContainer.InnerHtml = "";
    //}

    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        if (AsyncFileUpload1.HasFile)
        {
            string strPath = MapPath("~/files/ProjectsFiles/") + Path.GetFileName(e.FileName);
            AsyncFileUpload1.SaveAs(strPath);
        }
    }

}