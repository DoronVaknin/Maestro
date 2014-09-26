using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ProjectNameForProjectHatches"] != null)
        {
            string sProjectName = Session["ProjectNameForProjectHatches"].ToString();
            PageHeader.InnerHtml = "פתחים עבור הפרויקט " + sProjectName;
        }
        DisableAllFields();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ToggleFailureTypeDDL", "ToggleFailureTypeDDL();", true);
        if (Page.IsPostBack)
            SetupQuickSearch(null, null);
    }

    public void DisableAllFields()
    {
        HatchID.Attributes.Add("disabled", "disabled");
        HatchID2.Attributes.Add("disabled", "disabled");
        HatchProjectName.Attributes.Add("disabled", "disabled");
        HatchStatus.Attributes.Add("disabled", "disabled");
        HatchComments.Attributes.Add("disabled", "disabled");
        HatchStatusLastModified.Attributes.Add("disabled", "disabled");
        HatchEmployeeName.Attributes.Add("disabled", "disabled");
        HatchType.Attributes.Add("disabled", "disabled");
        HatchFailureType.Attributes.Add("disabled", "disabled");
        //HatchActive.Attributes.Add("disabled", "disabled");
    }

    protected void SaveHatchDetailsBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectNameForProjectHatches"] != null)
        {
            Hatch h = new Hatch();
            int hID = Convert.ToInt32(ProjectHatchesGV.SelectedRow.Cells[1].Text);
            int hID2 = Convert.ToInt32(HatchID2.Text);
            int hsID = Convert.ToInt32(HatchStatus.SelectedValue);
            string Comments = HatchComments.Text;
            DateTime StatusLastModified = DateTime.Now;
            string Username = Page.User.Identity.Name;
            int eID = h.GetUsernameID(Username);
            int htID = Convert.ToInt32(HatchType.SelectedValue);
            int ftID = 0;
            if (hsID == 2)
                ftID = Convert.ToInt32(HatchFailureType.SelectedValue);
            h = new Hatch(hID, hID2, hsID, ftID, eID, StatusLastModified, Comments, htID);
            int RowAffected = h.UpdateHatchDetails();

            if (RowAffected > 0 && hsID == 2) //Notification to Technical Manager
            {
                string ProjectName = Session["ProjectNameForProjectHatches"].ToString();
                string Message = String.Format("דווחה תקלה עבור פתח מס' {0} בפרויקט {1} ", hID, ProjectName);
                Notification n = new Notification(Message, DateTime.Now.Date, 302042267, 0);
                RowAffected = n.InsertNewNotification();
            }
        }
    }

    protected void ProjectHatchesGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalServiceCalls", "ActivateModal('EditHatchModal');", true);
        HatchFailureType.Visible = true;
        HatchID.Text = ProjectHatchesGV.SelectedRow.Cells[1].Text;
        HatchID2.Text = ProjectHatchesGV.SelectedRow.Cells[2].Text;
        if (Session["ProjectNameForProjectHatches"] != null)
            HatchProjectName.Text = Session["ProjectNameForProjectHatches"].ToString();
        ListItem li1 = HatchType.Items.FindByText(ProjectHatchesGV.SelectedRow.Cells[3].Text);
        HatchType.SelectedValue = li1.Value;
        string Status = ProjectHatchesGV.SelectedRow.Cells[4].Text;
        ListItem li2 = HatchStatus.Items.FindByText(Status);
        HatchStatus.SelectedValue = li2.Value;

        string FailureType = ProjectHatchesGV.SelectedRow.Cells[7].Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ToggleFailureTypeDDL", "ToggleFailureTypeDDL();", true);
        if (FailureType == "&nbsp;")
            HatchFailureType.SelectedValue = "1";
        else if (FailureType.Contains("&quot;"))
            FailureType = FailureType.Replace("&quot;", "\"");
        else
        {
            ListItem li3 = HatchFailureType.Items.FindByText(FailureType);
            HatchFailureType.SelectedValue = li3.Value;
        }

        HatchStatusLastModified.Text = ProjectHatchesGV.SelectedRow.Cells[5].Text;
        HatchEmployeeName.Text = ProjectHatchesGV.SelectedRow.Cells[6].Text;
        HatchComments.Text = ProjectHatchesGV.SelectedRow.Cells[8].Text;
        //HatchActive.Checked = true;

        if (HatchComments.Text == "&nbsp;")
            HatchComments.Text = "";
        if (HatchStatusLastModified.Text == "&nbsp;")
            HatchStatusLastModified.Text = "";
        if (HatchEmployeeName.Text == "&nbsp;")
            HatchEmployeeName.Text = "";
    }

    protected void CreateHatchForProject_Click(object sender, EventArgs e)
    {
        if (Session["ProjectIDForProjectHatches"] != null)
        {
            int ProjectID = Convert.ToInt32(Session["ProjectIDForProjectHatches"]);
            Project p = new Project();
            p.CreateHatches(ProjectID);
            ProjectHatchesGV.DataBind();
        }
    }

    protected void DisableHatchHiddenBTN_Click(object sender, EventArgs e)
    {
        int iHatchID = Convert.ToInt32(ProjectHatchesGV.SelectedRow.Cells[1].Text);
        Hatch h = new Hatch();
        h.DisableHatch(iHatchID);
        ProjectHatchesGV.DataBind();
    }

    //protected void ProjectHatchesGV_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //        e.Row.Cells[8].Text = e.Row.Cells[8].Text == "True" ? "כן" : "לא";
    //}

    protected void SetupQuickSearch(object sender, EventArgs e)
    {
        if (ProjectHatchesGV.Rows.Count > 0)
        {
            GridViewRow row = ProjectHatchesGV.Rows[0];
            for (int i = 0; i < ProjectHatchesGV.Columns.Count; i++)
            {
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = ProjectHatchesGV.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox form-control";
                ProjectHatchesGV.HeaderRow.Cells[i].Controls.Add(txtSearch);
            }
        }
    }
}