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
        SelectCommand="spGetOrders" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:GridView ID="OrdersGV" runat="server" AllowPaging="True" AllowSorting="True" CssClass = "DataTables"
        AutoGenerateColumns="False" DataKeyNames="oID" DataSourceID="SqlDataSource2">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:BoundField DataField="oID" HeaderText="מס' הזמנה" InsertVisible="False" ReadOnly="True"
                SortExpression="oID" />
            <asp:BoundField DataField="pName" HeaderText="שם הפרויקט" SortExpression="pName" />
            <asp:BoundField DataField="rName" HeaderText="שם הפריט" SortExpression="rName" />
            <asp:BoundField DataField="Quantity" HeaderText="כמות" SortExpression="Quantity" />
            <asp:BoundField DataField="osName" HeaderText="סטטוס" SortExpression="osName" />
            <asp:BoundField DataField="sName" HeaderText="שם הספק" SortExpression="sName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="EstimatedDateOfArrival" HeaderText="תאריך הגעה משוער"
                SortExpression="EstimatedDateOfArrival" DataFormatString="{0:dd/MM/yyyy}" />
        </Columns>
    </asp:GridView>
</asp:Content>
