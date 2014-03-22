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
                    <input id="DateTB" type="text" class="datepicker" runat="server">
                    <span class="input-group-addon">תאריך</span>
                </div>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                הערות
                <br />
                <textarea id="txtComments" runat="server" cols="40"></textarea>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="txtPrice" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">סה"כ עלות ללקוח</span>
                </div>
            </td>
            <td>
                <div id="status1">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                העלה קבצים
                <br />
                <div id="dragandrophandler">
                    Drag & Drop Files Here</div>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="txtHathes" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">מספר פתחים</span>
                </div>
            </td>
            <td>
                &nbsp;
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
        <asp:Button ID="Button1" runat="server" Text="צור פרויקט" class="btn btn-default"
            Font-Bold="true" OnClick="Button1_Click" />
    </div>
</asp:Content>
