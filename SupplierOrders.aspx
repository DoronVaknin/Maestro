<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="SupplierOrders.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1 id="PageHeader" runat="server">
            הזמנות עבור ספק
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrdersListForSupplier" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="SupplierID" SessionField="SupplierIDForSupplierOrders"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="SupplierOrdersGV" runat="server" AllowPaging="True" AllowSorting="True"
        CssClass="DataTables" AutoGenerateColumns="False" DataKeyNames="oID" DataSourceID="SqlDataSource2"
        OnSelectedIndexChanged="SupplierOrdersGV_SelectedIndexChanged" OnDataBound = "OnDataBound">
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
            <asp:BoundField DataField="oID" HeaderText="מס' הזמנה" InsertVisible="False" ReadOnly="True"
                SortExpression="oID" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="rName" HeaderText="שם הפריט" SortExpression="rName" />
            <asp:BoundField DataField="EstimatedDateOfArrival" HeaderText="תאריך הגעה משוער"
                SortExpression="EstimatedDateOfArrival" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Quantity" HeaderText="כמות" SortExpression="Quantity" />
            <asp:BoundField DataField="pName" HeaderText="שם הפרויקט" SortExpression="pName" />
            <asp:BoundField DataField="osName" HeaderText="סטטוס" SortExpression="osName" />
        </Columns>
    </asp:GridView>
    <div class="modal fade" id="EditSupplierOrderModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="CloseBTN">
                        &times;</button>
                    <div class="cntr">
                        <h4 id="ModalHeader" runat="server" class="modal-title">
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
                            <table id="EditSupplierOrderTBL" class="table">
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="SupplierOrderID" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">מס' הזמנה</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="SupplierOrderProject" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">שם הפרויקט</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="SupplierOrderItemName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">שם הפריט</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="SupplierOrderDateOpened" runat="server" CssClass="form-control datepicker"
                                                MaxLength="10"></asp:TextBox>
                                            <span class="input-group-addon">תאריך פתיחה</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="SupplierOrderQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">כמות</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="SupplierOrderEstimatedDOA" runat="server" CssClass="form-control datepicker"
                                                MaxLength="10"></asp:TextBox>
                                            <span class="input-group-addon">הגעה משוערת</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                            SelectCommand="spGetOrderStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <div class="cntr">
                                            סטטוס: &nbsp;
                                            <asp:DropDownList ID="SupplierOrderStatus" runat="server" DataSourceID="SqlDataSource1"
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
                <div class="modal-footer">
                    <div class="cntr">
                        <button id="EditOrderDetailsBTN" runat="server" type="button" class="btn btn-default"
                            onclick="EnableOrderDetails('Supplier')">
                            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
                        </button>
                        <button id="SaveOrderDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="ValidateOrderDetails('Supplier')">
                            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelOrderDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="RestoreOrderDetails('Supplier')">
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
