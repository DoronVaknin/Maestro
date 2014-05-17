<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectDetails.aspx.cs" Inherits="Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="form-background">
        <div class="cntr">
            <h1>
                פרטי הלקוח
            </h1>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="SaveCustomerDetailsHiddenBTN" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table id="CustomerDetailsTBL" class="table">
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoID" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                <span class="input-group-addon">ת.ז</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoPhone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                <span class="input-group-addon">טלפון</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoFirstName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                <span class="input-group-addon">שם פרטי *</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoMobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                <span class="input-group-addon">טלפון נייד</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoLastName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                <span class="input-group-addon">שם משפחה *</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoFax" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                <span class="input-group-addon">פקס</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoAddress" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                <span class="input-group-addon">כתובת *</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoEmail" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                <span class="input-group-addon">דוא"ל *</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                SelectCommand="spGetRegion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            <div class="cntr">
                                אזור: &nbsp;
                                <asp:DropDownList ID="ProjectInfoArea" runat="server" DataSourceID="SqlDataSource2"
                                    CssClass="btn btn-default" DataTextField="RegionName" DataValueField="RegionID">
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
    <div class="cntr">
        <button id="EditCustomerDetailsBTN" runat="server" type="button" class="btn btn-default"
            onclick="EnableCustomerDetails()">
            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <button id="SaveCustomerDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="ValidateCustomerDetails()">
            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
        </button>
        <button id="CancelCustomerDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="RestoreCustomerDetails()">
            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
        </button>
        <br />
        <br />
        <span id="CustomerDetailsErrorLabel" class="ErrorLabel"></span>
        <asp:Button ID="SaveCustomerDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
            OnClick="SaveCustomerDetailsBTN_Click1" Font-Bold="true" />
    </div>
    <asp:HiddenField ID="ProjectIDHolder" runat="server" />
    <br />
    <div class="form-background">
        <div class="cntr">
            <h1>
                פרטי הפרויקט
            </h1>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="SaveProjectDetailsHiddenBTN" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table id="ProjectDetailsTBL" class="table">
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                SelectCommand="spGetProjectStatusList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                            <span id="ProjectDetailsStatusIcon" class="glyphicon glyphicon-info-sign" data-placement="top"
                                data-trigger="hover" data-title="התקדמות הפרויקט"></span>&nbsp;&nbsp;סטטוס הפרויקט:
                            <asp:DropDownList ID="ProjectInfoStatus" runat="server" CssClass="btn btn-default"
                                DataSourceID="SqlDataSource1" DataTextField="psName" DataValueField="psID"
                                AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <div class="input-group">
                                <input id="ProjectInfoDateOpened" type="text" class="form-control datepicker" runat="server">
                                <span class="input-group-addon">תאריך פתיחה</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoName" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                <span class="input-group-addon">שם הפרויקט *</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <input id="ProjectInfoExpirationDate" type="text" class="form-control datepicker"
                                    runat="server">
                                <span class="input-group-addon">תאריך תפוגה</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoHatches" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                <span class="input-group-addon">מס' פתחים</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <input id="ProjectInfoInstallationDate" type="text" class="form-control datepicker"
                                    runat="server">
                                <span class="input-group-addon">תאריך התקנה</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoCost" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">סה"כ עלות *</span>
                            </div>
                        </td>
                        <td>
                            <div class="TextAreaHolder">
                                <asp:TextBox ID="ProjectInfoComments" runat="server" CssClass="form-control" TextMode="multiline"
                                    placeholder="הערות"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoArchitectName" runat="server" CssClass="form-control"
                                    MaxLength="15"></asp:TextBox>
                                <span class="input-group-addon">שם האדריכל</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoArchitectMobile" runat="server" CssClass="form-control"
                                    MaxLength="10"></asp:TextBox>
                                <span class="input-group-addon">טלפון אדריכל</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoContractorName" runat="server" CssClass="form-control"
                                    MaxLength="15"></asp:TextBox>
                                <span class="input-group-addon">שם הקבלן</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoContractorMobile" runat="server" CssClass="form-control"
                                    MaxLength="10"></asp:TextBox>
                                <span class="input-group-addon">טלפון קבלן</span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoSupervisorName" runat="server" CssClass="form-control"
                                    MaxLength="15"></asp:TextBox>
                                <span class="input-group-addon">שם המפקח</span>
                            </div>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox ID="ProjectInfoSupervisorMobile" runat="server" CssClass="form-control"
                                    MaxLength="10"></asp:TextBox>
                                <span class="input-group-addon">טלפון מפקח</span>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="cntr">
        <button id="EditProjectDetailsBTN" runat="server" type="button" class="btn btn-default"
            onclick="EnableProjectDetails()">
            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <button id="SaveProjectDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="ValidateProjectDetails()">
            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
        </button>
        <button id="CancelProjectDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="RestoreProjectDetails()">
            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
        </button>
        <asp:Button ID="SaveProjectDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
            OnClick="SaveProjectDetailsBTN_Click" Font-Bold="true" />
        <br />
        <br />
        <span id="ProjectDetailsErrorLabel" class="ErrorLabel"></span>
    </div>
    <br />
    <div class="cntr">
        <h1 id="PageHeader" runat="server">
            הזמנות עבור הפרויקט
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrdersListForProject" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProjectIDHolder" Name="ProjectID" PropertyName="Value"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView runat="server" CssClass="DataTables" ID="OrdersGV" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="oID" DataSourceID="SqlDataSource3"
        OnSelectedIndexChanged="OrdersGV_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:CommandField DeleteText="מחק" ShowDeleteButton="true" />
            <asp:BoundField DataField="oID" HeaderText="מס' הזמנה" InsertVisible="False" ReadOnly="True"
                SortExpression="oID" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="rName" HeaderText="שם הפריט" SortExpression="rName" />
            <asp:BoundField DataField="DateOfArrival" HeaderText="תאריך הגעה" SortExpression="DateOfArrival"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Quantity" HeaderText="כמות" SortExpression="Quantity" />
            <asp:BoundField DataField="sName" HeaderText="שם הספק" SortExpression="sName" />
            <asp:BoundField DataField="osName" HeaderText="סטטוס" SortExpression="osName" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <div class="cntr">
        <button id="OpenModalCreateOrdersBTN" type="button" class="btn btn-default" onclick="ActivateModal('ModalCreateOrders')">
            צור הזמנה חדשה&nbsp;&nbsp;<span class="glyphicon glyphicon-plus"></span>
        </button>
    </div>
    <br />
    <div class="modal fade" id="ModalCreateOrders" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                        &times;</button>
                    <h4 id="ModalHeader" runat="server" class="modal-title cntr">
                    </h4>
                </div>
                <div class="modal-body">
                    <table id="ProjectOrdersTBL" class="DataTables">
                        <tr>
                            <td>
                                שם הפריט
                            </td>
                            <td>
                                כמות להזמנה
                            </td>
                            <td>
                                שם הספק
                            </td>
                            <td>
                                תאריך הגעה משוער
                            </td>
                        </tr>
                        <tr>
                            <td>
                                תריסים
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="ShutterCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="ShutterProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="ShutterEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                נאספים
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="CollectedCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="CollectedProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="CollectedEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                אלומיניום
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="AluminiumCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="AluminiumProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="AluminiumEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                וואלים
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="ValimCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="ValimProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="ValimEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                יואים
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="UCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="UProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="UEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                פרזול
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="ShoeingCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="ShoeingProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="ShoeingEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                מנועים
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="EngineCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="EngineProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="EngineEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ממ"ד
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="ProtectedSpaceCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="ProtectedSpaceProvider" runat="server" CssClass="form-control"
                                    Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="ProtectedSpaceEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                זכוכית
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="GlassCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="GlassProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="GlassEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ארגזים
                            </td>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon minus">-</span>
                                    <asp:TextBox ID="BoxesCount" runat="server" CssClass="form-control cntr Count">0</asp:TextBox>
                                    <span class="input-group-addon plus">+</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="BoxesProvider" runat="server" CssClass="form-control" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="BoxesEstArrDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="cntr">
                        <asp:Button ID="CreateOrderBTN" runat="server" OnClick="CreateOrderBTN_Click" Text="צור הזמנה"
                            CssClass="btn btn-default" Font-Bold="true" />
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
    <asp:SqlDataSource runat="server" ID="StatusDataSource" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrderStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <div class="modal fade" id="ModalOrderDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="CloseBTN">
                        &times;</button>
                    <h4 class="modal-title">
                        פרטי ההזמנה</h4>
                </div>
                <div class="modal-body" style="text-align: right;">
                    <br />
                    <asp:Label runat="server" Text="מספר הזמנה:" ID="OrderNumLBL"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="תאריך ההזמנה:" ID="OrderDateLBL"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="תאריך הגעה משוער:" ID="EstimateDateLBL"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="שם הפריט:" ID="RawMeterialLBL"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="כמות שהוזמנה:" ID="QuantityLBL"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="שם הספק:" ID="SupplierLBL"></asp:Label>
                    <br />
                    סטטוס הזמנה: &nbsp &nbsp
                    <asp:DropDownList ID="OrderStausDDL" runat="server" DataSourceID="StatusDataSource"
                        DataTextField="osName" DataValueField="osID">
                    </asp:DropDownList>
                    <br />
                </div>
                <br />
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
