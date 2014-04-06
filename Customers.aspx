<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="Customers.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            לקוחות</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="SELECT * FROM [Customer]"></asp:SqlDataSource>
    <asp:GridView ID="CustomersTBL" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="DataTables" DataKeyNames="cID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="Customers_SelectedIndexChanged"
        OnDataBound="OnDataBound">
        <Columns>
            <asp:CommandField SelectText="<b>בחר</b>" ShowSelectButton="True" />
            <asp:BoundField DataField="cID" HeaderText="תעודת זהות" ReadOnly="True" SortExpression="cID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="City" HeaderText="יישוב" SortExpression="City" />
            <asp:BoundField DataField="cAddress" HeaderText="כתובת" SortExpression="cAddress" />
            <asp:BoundField DataField="Phone" HeaderText="טלפון" SortExpression="Phone" />
            <asp:BoundField DataField="Mobile" HeaderText="נייד" SortExpression="Mobile" />
            <asp:BoundField DataField="Email" HeaderText="דוא&quot;ל" SortExpression="Email"
                Visible="false" />
            <asp:BoundField DataField="Fax" HeaderText="פקס" SortExpression="Fax" Visible="False" />
            <asp:BoundField DataField="RegionID" HeaderText="אזור" SortExpression="RegionID"
                Visible="False" />
        </Columns>
    </asp:GridView>
</asp:Content>
