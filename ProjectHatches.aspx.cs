using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ProjectNameForProjectHatches"] != null)
        {
            string sProjectName = Session["ProjectNameForProjectHatches"].ToString();
            PageHeader.InnerHtml = "פתחים עבור הפרויקט " + sProjectName;
        }
        DisableAllFields();
    }

    public void DisableAllFields()
    {
        HatchID.Attributes.Add("disabled", "disabled");
        HatchProjectName.Attributes.Add("disabled", "disabled");
        HatchStatus.Attributes.Add("disabled", "disabled");
        HatchComments.Attributes.Add("disabled", "disabled");
        HatchStatusLastModified.Attributes.Add("disabled", "disabled");
        HatchEmployeeName.Attributes.Add("disabled", "disabled");
        HatchType.Attributes.Add("disabled", "disabled");
        HatchFailureType.Attributes.Add("disabled", "disabled");
    }

    protected void SaveHatchDetailsBTN_Click(object sender, EventArgs e)
    {
        if (Session["ProjectNameForProjectHatches"] != null)
        {
            Hatch h = new Hatch();
            int hID = Convert.ToInt32(ProjectHatchesGV.SelectedRow.Cells[2].Text);
            int hsID = Convert.ToInt32(HatchStatus.SelectedValue);
            string Comments = HatchComments.Text;
            DateTime StatusLastModified = DateTime.Now;
            string Username = GetUsername();
            int eID = h.GetUsernameID(Username);
            int htID = Convert.ToInt32(HatchType.SelectedValue);
            int ftID = 0;
            if (HatchFailureType.Visible)
                ftID = Convert.ToInt32(HatchFailureType.SelectedValue);
            h = new Hatch(hID, hsID, ftID, eID, StatusLastModified, Comments, htID);
            int RowAffected = h.UpdateHatchDetails();
        }
    }

    protected void ProjectHatchesGV_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallModalServiceCalls", "ActivateModal('EditHatchModal');", true);
        HatchFailureType.Visible = true;
        HatchID.Text = ProjectHatchesGV.SelectedRow.Cells[2].Text;
        if (Session["ProjectNameForProjectHatches"] != null)
            HatchProjectName.Text = Session["ProjectNameForProjectHatches"].ToString();
        ListItem li1 = HatchType.Items.FindByText(ProjectHatchesGV.SelectedRow.Cells[3].Text);
        HatchType.SelectedValue = li1.Value;
        string Status = ProjectHatchesGV.SelectedRow.Cells[4].Text;
        ListItem li2 = HatchStatus.Items.FindByText(Status);
        HatchStatus.SelectedValue = li2.Value;

        string FailureType = ProjectHatchesGV.SelectedRow.Cells[7].Text;
        if (Status != "תקלה")
            HatchFailureType.Visible = false;
        else
        {
            if (FailureType.Contains("&quot;"))
                FailureType = FailureType.Replace("&quot;", "\"");
            ListItem li3 = HatchFailureType.Items.FindByText(FailureType);
            HatchFailureType.SelectedValue = li3.Value;
        }

        HatchStatusLastModified.Text = ProjectHatchesGV.SelectedRow.Cells[5].Text;
        HatchEmployeeName.Text = ProjectHatchesGV.SelectedRow.Cells[6].Text;
        HatchComments.Text = ProjectHatchesGV.SelectedRow.Cells[8].Text;
        if (HatchComments.Text == "&nbsp;")
            HatchComments.Text = "";
    }

    public string GetUsername()
    {
        string EmployeeName = "";
        switch (Page.User.Identity.Name)
        {
            case "Admin": EmployeeName = "אדמין"; break;
            case "ShimonY": EmployeeName = "שמעון ימין"; break;
            case "MaliY": EmployeeName = "מלי ימין"; break;
            case "BettiY": EmployeeName = "בטי ימין"; break;
            default: break;
        }
        return EmployeeName;
    }
}