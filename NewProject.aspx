<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewProject.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            פרויקט חדש
        </h1>
    </div>
    <br />
    <table id="NewProjectTBL" class="table">
        <tr>
            <td>
                <div class="input-group">
                    <input id="DateTB" type="text" class="form-control datepicker" runat="server">
                    <span class="input-group-addon">תאריך</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="txtHatches" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">מספר פתחים</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div class="input-group">
                    <input id="txtPrice" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">סה"כ עלות ללקוח</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="TextAreaHolder">
                    <textarea id="txtComments" runat="server" placeholder="הערות" class="form-control"
                        rows="5" cols="10"></textarea></div>
            </td>
            <td>
                העלה קבצים
                <br />
                <div id="dragandrophandler">
                    Drag & Drop Files Here</div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="txtContractorName" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">שם הקבלן</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="txtContractorPhone" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="txtArchitectName" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">שם האדריכל</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="txtArchitectPhone" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="txtSupervisorName" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">שם המפקח</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="txtSupervisorPhone" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div class="cntr">
        <input type="button" value="צור פרויקט" class="btn btn-default" onclick="ValidateNewProject()" />
        <asp:Button ID="CreateProject" runat="server" Text="צור פרויקט" class="btn btn-default"
            CssClass="HiddenButtons" Font-Bold="true" OnClick="CreateProject_Click" />
    </div>
</asp:Content>
