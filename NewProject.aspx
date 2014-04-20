<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewProject.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <br />
        <h1>
            פרויקט חדש
        </h1>
    </div>
    <br />
    <table id="NewProjectTBL" class="table">
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectName" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">שם הפרויקט *</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectDateOpened" type="text" class="form-control datepicker" runat="server">
                    <span class="input-group-addon">תאריך פתיחה *</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectHatches" type="text" class="form-control" runat="server" maxlength="2">
                    <span class="input-group-addon">מס' פתחים *</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectExpirationDate" type="text" class="form-control datepicker" runat="server">
                    <span class="input-group-addon">תאריך תפוגה</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectCost" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">סה"כ עלות *</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectInstallationDate" type="text" class="form-control datepicker" runat="server">
                    <span class="input-group-addon">תאריך התקנה</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="TextAreaHolder">
                    <textarea id="ProjectComments" runat="server" placeholder="הערות" class="form-control"
                        rows="5" cols="10"></textarea></div>
            </td>
            <td>
                <div class="TextAreaHolder">
                    <asp:FileUpload ID="ProjectFiles" runat="server" /></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectContractorName" type="text" class="form-control" runat="server"
                        maxlength="20">
                    <span class="input-group-addon">שם הקבלן</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectContractorMobile" type="text" class="form-control" runat="server"
                        maxlength="10">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectArchitectName" type="text" class="form-control" runat="server"
                        maxlength="20">
                    <span class="input-group-addon">שם האדריכל</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectArchitectMobile" type="text" class="form-control" runat="server"
                        maxlength="10">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectSupervisorName" type="text" class="form-control" runat="server"
                        maxlength="20">
                    <span class="input-group-addon">שם המפקח</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectSupervisorMobile" type="text" class="form-control" runat="server"
                        maxlength="10">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
    </table>
    <div class="cntr">
        <div id="dragandrophandler">
            גרור קבצים לכאן</div>
        <br />
        <div id="status1">
        </div>
        <br />
        <input type="button" value="צור פרויקט" class="btn btn-default" onclick="ValidateNewProject()" />
        <asp:Button ID="CreateProject" runat="server" Text="צור פרויקט" CssClass="HiddenButtons"
            OnClick="CreateProject_Click" />
        <br />
        <br />
        <span class="ErrorLabel"></span>
    </div>
    <br />
    <!-- Modal -->
    <div class="modal fade" id="ModalCustomerCreated" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                        &times;</button>
                    <h4 class="modal-title">
                        הודעת מערכת</h4>
                </div>
                <div class="modal-body">
                    הלקוח נוצר בהצלחה
                </div>
                <%--                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <button type="button" class="btn btn-primary">
                        Save changes</button>
                </div>--%>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
