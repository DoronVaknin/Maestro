<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewServiceCall.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="form-background">
        <div class="cntr">
            <h1 id="PageHeader" runat="server">
            </h1>
        </div>
        <br />
        <table id="NewServiceCallExternalTBL" class="table">
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ServiceCallDateOpened" type="text" class="form-control datepicker" runat="server">
                        <span class="input-group-addon">תאריך פתיחה *</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="TextAreaHolder">
                        <asp:TextBox ID="ServiceCallProblemDesc" runat="server" CssClass="form-control" TextMode="multiline"
                            placeholder="* תיאור התקלה"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:CheckBox ID="ServiceCallUrgentCall" runat="server" />
                        קריאה דחופה
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="cntr">
        <button id="CreateServiceCallExternalBTN" type="button" class="btn btn-default" onclick="ValidateServiceCallExternal()">
            צור קריאה&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
        </button>
        <asp:Button ID="CreateServiceCallExternal" runat="server" CssClass="btn btn-default HiddenButtons"
            OnClick="CreateServiceCallExternal_Click" />
        <br />
        <br />
        <span class="ErrorLabel"></span>
    </div>
</asp:Content>
