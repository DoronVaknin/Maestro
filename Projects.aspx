<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="Projects.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            פרויקטים</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spShowAllProjects" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:GridView ID="ProjectsTBL" runat="server" AllowSorting="True" CssClass="DataTables"
        DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnDataBound="OnDataBound"
        OnSelectedIndexChanged="ProjectsTBL_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
            <asp:BoundField DataField="pID" HeaderText="מספר פרויקט" InsertVisible="False" ReadOnly="True"
                SortExpression="pID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס" SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
            <asp:BoundField DataField="Cost" HeaderText="מחיר" SortExpression="Cost" />
            <asp:BoundField DataField="NumOfHatches" HeaderText="מספר פתחים" ReadOnly="True" SortExpression="NumOfHatches" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
