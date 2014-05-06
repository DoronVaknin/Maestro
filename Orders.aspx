<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="Orders.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            הזמנות
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrders" SelectCommandType="StoredProcedure" UpdateCommand="update Orders set Quantity=@Quantity, EstimatedDateOfArrival=@EstimatedDateOfArrival where oID=@oID">
        <UpdateParameters>
            <asp:Parameter Name="Quantity" />
            <asp:Parameter Name="EstimatedDateOfArrival" />
            <asp:Parameter Name="oID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="OrdersGV" runat="server" AllowPaging="True" AllowSorting="True"
        CssClass="DataTables" AutoGenerateColumns="False" DataKeyNames="oID" DataSourceID="SqlDataSource2"
        OnSelectedIndexChanged="OrdersGV_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="עדכן סטטוס" ShowSelectButton="True" />
            <asp:CommandField DeleteText="מחק" ShowDeleteButton="true" />
            <asp:BoundField DataField="oID" HeaderText="מס' הזמנה" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="pName" HeaderText="שם הפרויקט" SortExpression="pName"
                ReadOnly="True" />
            <asp:BoundField DataField="rName" HeaderText="שם הפריט" SortExpression="rName" ReadOnly="True" />
            <asp:BoundField DataField="Quantity" HeaderText="כמות" SortExpression="Quantity" />
            <asp:BoundField DataField="osName" HeaderText="סטטוס" SortExpression="osName" ReadOnly="True" />
            <asp:BoundField DataField="sName" HeaderText="שם הספק" SortExpression="sName" ReadOnly="True" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />
            <asp:BoundField DataField="EstimatedDateOfArrival" HeaderText="תאריך הגעה משוער"
                SortExpression="EstimatedDateOfArrival" DataFormatString="{0:dd/MM/yyyy}" />
        </Columns>
    </asp:GridView>
    <div class="modal fade" id="OrdersModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" align="center">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="CloseBTN">
                        &times;</button>
                    <h4 class="modal-title">
                        שינוי סטטוס הזמנה</h4>
                </div>
                <div class="modal-body" align="center">
                    <asp:DropDownList runat="server" ID="OrderStatusDDL" DataSourceID="OrderStatusDataSource"
                        DataTextField="osName" DataValueField="osID">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="OrderStatusDataSource" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                        SelectCommand="spGetOrderStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <asp:Button runat="server" Text="Button" OnClick="Unnamed1_Click" />
                </div>
                <br />
                <div align="center">
                </div>
                <div class="modal-footer" align="center">
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
