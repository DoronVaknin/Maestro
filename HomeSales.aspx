<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="HomeSales.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div id="HomeContainer">
        <%--<div id="NewsBox" class="panel panel-default">
            <div class="panel-heading">
                <span class="glyphicon glyphicon-list-alt"></span>&nbsp;&nbsp;<b>חדשות</b></div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-12">
                        <ul class="News">
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/1.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            לורם איפסום דולור סיט אמט, קונסקטורר אדיפיסינג אלית ושבעגט ליבם סולגק. בראיט ולחת
                                            צורק מונחף, בגורמי מגמש. תרבנך וסתעד לכנו סתשם השמה - לתכי מורגם בורק? לתיג ישבעס.
                                            <a href="#">קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/2.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            הועניב היושבב שערש שמחויט - שלושע ותלברו חשלו שעותלשך וחאית נובש ערששף. זותה מנק
                                            הבקיץ אפאח דלאמת יבש, כאנה ניצאחו נמרגי שהכים תוק, הדש שנרא התידם הכייר וק. <a href="#">
                                                קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/3.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            לפרומי בלוף קינץ תתיח לרעח. לת צשחמי צש בליא, מנסוטו צמלח לביקו ננבי, צמוקו בלוקריה
                                            שיצמה ברורק. קונדימנטום קורוס בליקרה, נונסטי קלובר בריקנה סטום, לפריקך תצטריק לרטי.
                                            <a href="#">קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/3.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            לורם איפסום דולור סיט אמט, קונסקטורר אדיפיסינג אלית. סת אלמנקום ניסי נון ניבאה.
                                            דס איאקוליס וולופטה דיאם. וסטיבולום אט דולור, קראס אגת לקטוס וואל אאוגו וסטיבולום
                                            סוליסי טידום בעליק. קונדימנטום קורוס בליקרה, נונסטי קלובר בריקנה סטום, לפריקך תצטריק
                                            לרטי. <a href="#">קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="panel-footer">
            </div>
        </div>--%>
        <asp:SqlDataSource ID="PriceOfferDS" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
            SelectCommand="spGetUndecidedCustomers" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <div id="PriceOfferContainer">
            <h2>
                לקוחות בהצעת מחיר
            </h2>
            <br />
            <asp:GridView ID="PriceOfferGV" CssClass="DataTables" runat="server" AutoGenerateColumns="False"
                DataSourceID="PriceOfferDS" AllowPaging="True" PageSize="7" OnSelectedIndexChanged="PriceOfferGV_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
                    <asp:BoundField DataField="pName" HeaderText="שם פרויקט" SortExpression="pName" />
                    <asp:BoundField DataField="Mobile" HeaderText="טלפון נייד" SortExpression="Mobile" />
                    <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
                    <asp:BoundField DataField="InstallationDate" HeaderText="תאריך חזרה ללקוח" SortExpression="InstallationDate"
                        DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="cID" HeaderText="ת.ז" SortExpression="cID" Visible="false" />
                    <asp:BoundField DataField="pID" HeaderText="מס' פרויקט" SortExpression="pID" Visible="false" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="PieChartContainer">
            <h2>
                הכנסות מפרויקטים
            </h2>
            <div id="PieChart" style="height: 250px;">
            </div>
        </div>
        <div class="modal fade" id="ModalEditUndecidedCustomer" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                            id="CloseBTN">
                            &times;</button>
                        <div class="cntr">
                            <h4 id="ModalHeader" runat="server" class="modal-title">
                                פרטי הלקוח</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="SaveUndecidedCustomerDetailsHiddenBTN" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <table id="EditUndecidedCustomerTBL" class="table">
                                    <tr>
                                        <td>
                                            <div class="input-group">
                                                <input id="ProjectName" type="text" class="form-control" runat="server">
                                                <span class="input-group-addon">שם הפרויקט *</span>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input id="CustomerMobilePhone" type="text" class="form-control" runat="server" maxlength="10">
                                                <span class="input-group-addon">טלפון נייד *</span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="TextAreaHolder">
                                                <asp:TextBox ID="ProjectComments" runat="server" CssClass="form-control" TextMode="multiline"
                                                    placeholder="הערות"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input id="CustomerBackDate" type="text" class="form-control datepicker" runat="server"
                                                    maxlength="10">
                                                <span class="input-group-addon">תאריך חזרה ללקוח</span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                                SelectCommand="spGetProjectStatusList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                            <div class="cntr">
                                                סטטוס הפרויקט: &nbsp;
                                                <asp:DropDownList ID="ProjectStatus" runat="server" DataSourceID="SqlDataSource1"
                                                    CssClass="btn btn-default" DataTextField="psName" DataValueField="psID">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <div class="cntr">
                            <button id="EditUndecidedCustomerDetailsBTN" runat="server" type="button" class="btn btn-default"
                                onclick="EnableUndecidedCustomerDetails()">
                                ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
                            </button>
                            <button id="SaveUndecidedCustomerDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                                onclick="ValidateUndecidedCustomerDetails()">
                                שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                            </button>
                            <button id="CancelUndecidedCustomerDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                                onclick="RestoreUndecidedCustomerDetails()">
                                בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                            </button>
                            <br />
                            <br />
                            <span id="CustomerDetailsErrorLabel" class="ErrorLabel"></span>
                            <asp:Button ID="SaveUndecidedCustomerDetailsHiddenBTN" runat="server" Text="שמור"
                                CssClass="btn btn-default HiddenButtons" OnClick="SaveUndecidedCustomerDetailsBTN_Click"
                                Font-Bold="true" />
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
</asp:Content>
