<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectOrders.aspx.cs" Inherits="Default2" %>

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
        SelectCommand="spGetOrdersListForProject" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="ProjectID" SessionField="ProjectIDForProjectOrders" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView runat="server" CssClass="DataTables" ID="OrdersGV" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="oID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:CommandField EditText="ערוך" ShowEditButton="true" />
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
    <div class="cntr">
        <input id="OpenModalCreateOrdersBTN" type="button" value="צור הזמנה חדשה" class="btn btn-default"
            onclick="ActivateModal('ModalCreateOrders')" />
        <div class="modal fade" id="ModalCreateOrders" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                            &times;</button>
                        <h4 class="modal-title cntr">
                            הזמנות עבור פרויקט</h4>
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
                            </tr>
                            <tr>
                                <td>
                                    תריסים
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="ShutterCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ShutterProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    וואלים
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="ValimCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ValimProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    פרזול
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="ShoeingCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ShoeingProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ממ"ד
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="ProtectedSpaceCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ProtectedSpaceProvider" runat="server" CssClass="form-control"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ארגזים
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="BoxCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="BoxesProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    נאספים
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="CollectedCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="CollectedProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    יואים
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="UCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="UProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    מנועים
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="EngineCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="EngineProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    זכוכית
                                </td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon minus">-</span>
                                        <asp:TextBox ID="GlassCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                                        <span class="input-group-addon plus">+</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:DropDownList ID="GlassProvider" runat="server" CssClass="form-control" Width="150px">
                                    </asp:DropDownList>
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
</asp:Content>
