<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ServiceCallsArchive.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            ארכיון קריאות שירות</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="ServiceCallsArchiveDS" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetServiceCallsArchive" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <asp:GridView ID="ServiceCallsArchiveGV" CssClass="DataTables" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="scID" DataSourceID="ServiceCallsArchiveDS"
        OnDataBound="SetupQuickSearch" OnSelectedIndexChanged="ServiceCallsGridView_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:BoundField DataField="scID" HeaderText="מס' קריאה" InsertVisible="False" ReadOnly="True"
                SortExpression="scID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="DateClosed" HeaderText="תאריך סגירה" SortExpression="DateClosed"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="ProblemDesc" HeaderText="התקלה" SortExpression="ProblemDesc" />
            <asp:CheckBoxField DataField="Urgent" HeaderText="דחוף" SortExpression="Urgent" />
        </Columns>
    </asp:GridView>
    <div class="modal fade" id="ModalServiceCalls" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="CloseBTN">
                        &times;</button>
                    <div class="cntr">
                        <h4 class="modal-title">
                            פרטי הקריאה</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <table id="ServiceCallTBL" class="table">
                        <tr>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallID" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">מס' קריאה</span>
                                </div>
                            </td>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallPhone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <span class="input-group-addon">טלפון</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallFirstName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    <span class="input-group-addon">שם פרטי</span>
                                </div>
                            </td>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallMobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                    <span class="input-group-addon">טלפון נייד</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallLastName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                    <span class="input-group-addon">שם משפחה</span>
                                </div>
                            </td>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallDateOpened" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                    <span class="input-group-addon">תאריך פתיחה</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">כתובת</span>
                                </div>
                            </td>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="ServiceCallExpirationDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">תפוגת אחריות</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="TextAreaHolder">
                                    <span style="float: right">* תיאור התקלה:</span>
                                    <asp:TextBox ID="ServiceCallProblemDesc" runat="server" CssClass="form-control" TextMode="multiline"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <label>
                                    <input id="ServiceCallUrgent" runat="server" type="checkbox">
                                    קריאה דחופה
                                </label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="modal-footer">
                    <%--<div class="cntr">
                        <button id="EditServiceCallDetailsBTN" runat="server" type="button" class="btn btn-default"
                            onclick="EnableServiceCallDetails()">
                            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
                        </button>
                        <button id="SaveServiceCallDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="ValidateServiceCallDetails()">
                            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelServiceCallDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="RestoreServiceCallDetails()">
                            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                        </button>
                        &nbsp;&nbsp;
                        <button id="Button1" runat="server" type="button" class="btn btn-default" onclick="ActivateCloseServiceCallDialog()">
                            סגור קריאת שירות&nbsp;&nbsp;<span class="glyphicon glyphicon-folder-close"></span>
                        </button>
                        <br />
                        <br />
                        <span id="ServiceCallDetailsErrorLabel" class="ErrorLabel"></span>
                        <asp:Button ID="SaveServiceCallDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
                            OnClick="SaveServiceCallDetailsBTN_Click" Font-Bold="true" />
                        <asp:Button ID="CloseServiceCallHiddenBTN" runat="server" Text="סגור קריאת שירות"
                            CssClass="btn btn-default HiddenButtons" OnClick="CloseServiceCallHiddenBTN_Click"
                            Font-Bold="true" />
                    </div>--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
