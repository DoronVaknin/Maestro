<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ServiceCalls.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            קריאות שירות פתוחות</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="ServiceCallsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOpenedServiceCalls" SelectCommandType="StoredProcedure" UpdateCommand="update ServiceCall set Urgent=@Urgent, ProblemDesc=@ProblemDesc where scID=@scID">
    </asp:SqlDataSource>
    <asp:GridView runat="server" ID="ServiceCallsGV" AutoGenerateColumns="False"
        DataKeyNames="scID" DataSourceID="ServiceCallsDataSource" CssClass="DataTables"
        AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="ServiceCallsGridView_SelectedIndexChanged" OnDataBound = "OnDataBound">
        <Columns>
            <asp:CommandField ShowSelectButton="true" SelectText="בחר" />
            <asp:BoundField DataField="scID" HeaderText="מס' קריאה" InsertVisible="False" ReadOnly="True"
                SortExpression="scID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" ReadOnly="True" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" ReadOnly="True" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="DateClosed" HeaderText="תאריך סגירה" SortExpression="DateClosed"
                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" Visible="false" />
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
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SaveServiceCallDetailsHiddenBTN" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
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
                                            <asp:TextBox ID="ServiceCallAddress" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br />
                <div class="modal-footer">
                    <div class="cntr">
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
                        <button runat="server" type="button" class="btn btn-default" onclick="ActivateCloseServiceCallDialog()">
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
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- Modal -->
    <div class="modal fade" id="ModalServiceCallUpdated" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                        &times;</button>
                    <h4 class="modal-title">
                        הודעת מערכת</h4>
                </div>
                <div class="modal-body">
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
    <!-- Modal -->
    <div class="modal fade" id="CloseServiceCallDialogModal" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                        &times;</button>
                    <div class="cntr">
                        <h4 class="modal-title">
                            סגירת קריאת שירות</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="QuestionContainer">
                    </div>
                    <br />
                    <div class="cntr">
                        <button id="CloseServiceCallBTN" runat="server" type="button" class="btn btn-danger"
                            onclick="CloseServiceCall()">
                            סגור קריאה&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelCloseServiceCallBTN" runat="server" data-dismiss="modal" type="button"
                            class="btn btn-default">
                            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                        </button>
                    </div>
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
