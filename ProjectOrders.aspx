<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectOrders.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1 id="PageHeader" runat="server">
            הזמנות עבור פרויקט
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrdersListForProject" SelectCommandType="StoredProcedure"
        DeleteCommand="DELETE FROM Orders WHERE oID=@oID" DeleteCommandType="Text">
        <SelectParameters>
            <asp:SessionParameter Name="ProjectID" SessionField="ProjectIDForProjectOrders" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView runat="server" CssClass="DataTables" ID="ProjectOrdersGV" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="oID" DataSourceID="SqlDataSource1"
        OnSelectedIndexChanged="ProjectOrdersGV_SelectedIndexChanged" OnDataBound="OnDataBound">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:CommandField DeleteText="מחק" ShowDeleteButton="true" />
            <asp:BoundField DataField="oID" HeaderText="מס' הזמנה" InsertVisible="False" ReadOnly="True"
                SortExpression="oID" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="rName" HeaderText="שם הפריט" SortExpression="rName" />
            <asp:BoundField DataField="EstimatedDateOfArrival" HeaderText="תאריך הגעה משוער"
                SortExpression="EstimatedDateOfArrival" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Quantity" HeaderText="כמות" SortExpression="Quantity" />
            <asp:BoundField DataField="sName" HeaderText="שם הספק" SortExpression="sName" />
            <asp:BoundField DataField="osName" HeaderText="סטטוס" SortExpression="osName" />
        </Columns>
    </asp:GridView>
    <br />
    <div class="cntr">
        <button id="OpenModalCreateOrdersBTN" type="button" class="btn btn-default" onclick="ActivateModal('ModalCreateOrders');">
            צור הזמנה חדשה&nbsp;&nbsp;<span class="glyphicon glyphicon-plus"></span>
        </button>
    </div>
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
                        <button type="button" class="btn btn-default" onclick="CreateOrder()">
                            צור הזמנה&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <asp:Button ID="CreateOrderHiddenBTN" runat="server" OnClick="CreateOrderHiddenBTN_Click"
                            Text="צור הזמנה" CssClass="btn btn-default HiddenButtons" Font-Bold="true" />
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
    <div class="modal fade" id="ModalOrderDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="Button1">
                        &times;</button>
                    <div class="cntr">
                        <h4 class="modal-title">
                            פרטי ההזמנה</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SaveOrderDetailsHiddenBTN" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table id="EditProjectOrderTBL" class="table">
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="ProjectOrderID" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">מס' הזמנה</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="ProjectOrderSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">שם הספק</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="ProjectOrderItemName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">שם הפריט</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="ProjectOrderDateOpened" runat="server" CssClass="form-control datepicker"
                                                MaxLength="10"></asp:TextBox>
                                            <span class="input-group-addon">תאריך פתיחה</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="ProjectOrderQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">כמות</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="ProjectOrderEstimatedDOA" runat="server" CssClass="form-control datepicker"
                                                MaxLength="10"></asp:TextBox>
                                            <span class="input-group-addon">הגעה משוערת</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                            SelectCommand="spGetOrderStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <div class="cntr">
                                            סטטוס: &nbsp;
                                            <asp:DropDownList ID="ProjectOrderStatus" runat="server" DataSourceID="SqlDataSource2"
                                                CssClass="btn btn-default" DataTextField="osName" DataValueField="osID">
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
                <br />
                <div class="modal-footer">
                    <div class="cntr">
                        <button id="EditOrderDetailsBTN" runat="server" type="button" class="btn btn-default"
                            onclick="EnableOrderDetails('Project')">
                            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
                        </button>
                        <button id="SaveOrderDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="ValidateOrderDetails('Project')">
                            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelOrderDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="RestoreOrderDetails('Project')">
                            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                        </button>
                        <br />
                        <br />
                        <span id="OrderDetailsErrorLabel" class="ErrorLabel"></span>
                        <asp:Button ID="SaveOrderDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
                            OnClick="SaveOrderDetailsBTN_Click" Font-Bold="true" />
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
