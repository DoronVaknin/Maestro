<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ServiceCalls.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <br />
    <br />
    <asp:SqlDataSource ID="ServiceCallsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetAllServiceCalls" SelectCommandType="StoredProcedure" UpdateCommand="update ServiceCall set Urgent=@Urgent, ProblemDesc=@ProblemDesc where scID=@scID">
    </asp:SqlDataSource>
    <asp:GridView runat="server" ID="ServiceCallsGridView" AutoGenerateColumns="False"
        DataKeyNames="scID" DataSourceID="ServiceCallsDataSource" CssClass="DataTables"
        AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="ServiceCallsGridView_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="true" SelectText="בחר" />
            <asp:CommandField ShowEditButton="true" EditText="ערוך" />
            <asp:BoundField DataField="scID" HeaderText="מספר קריאה" InsertVisible="False" ReadOnly="True"
                SortExpression="scID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" ReadOnly="True" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" ReadOnly="True" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="DateClosed" HeaderText="תאריך סגירה" SortExpression="DateClosed"
                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="ProblemDesc" HeaderText="תיאור" SortExpression="ProblemDesc" />
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
                    <h4 class="modal-title">
                        פרטי הקריאה</h4>
                </div>
                <div class="modal-body">
                </div>
                <br />
                <div align="center">
                    <table class="nav-justified" >
                        <tr>
                            <td>
                                תאריך פתיחה:&nbsp;
                                <asp:Label ID="OpenDateLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                תאריך סגירה :
                                <asp:Label ID="CloseDateLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                שם פרטי:
                                <asp:Label ID="FNameLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                שם משפחה:
                                <asp:Label ID="LNameLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                נייד:
                                <asp:Label ID="PhoneLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                שם פרויקט:
                                <asp:Label ID="ProjectNameLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                פרטי הקריאה:
                                <asp:Label ID="ServiceCallDescriptionLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                אזור:
                                <asp:Label ID="RegionLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                כתובת:
                                <asp:Label ID="AdressLBL" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="ServiceCallBTN" runat="server" Text="סגור קריאה" CssClass="btn btn-default"
                        Font-Bold="true" OnClick="ServiceCallBTN_Click" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="margin-left: 50%">
                        סגור ללא שינויים</button>
                    <%--  <button type="button" class="btn btn-primary">
                        save changes</button>--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
