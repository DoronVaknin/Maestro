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


public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            int ProjectID = Convert.ToInt32(Session["ProjectID"]);

            Project p = new Project();
            DataTable DetailsTable = p.GetAllDetails(ProjectID);

            if (!Page.IsPostBack)
                SetPageDetails(DetailsTable);
            else
                DisableAllFields();
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
            Response.Redirect("ProjectOrders.aspx");
        }
    }
    protected void OpenServiceCallHiddenBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectID"] != null)
        {
            Session["ProjectIDForServiceCall"] = Session["ProjectID"];
            Session["ProjectNameForServiceCall"] = ProjectInfoName.Text;
            Response.Redirect("NewServiceCall.aspx?Source=ExistingProject");
        }
    }
}



