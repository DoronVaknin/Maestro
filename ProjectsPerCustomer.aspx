<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectsPerCustomer.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class = "cntr">
        <h1 id="Header" runat="server">
        </h1>
    </div><br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>"
        SelectCommand="spShowProjectsPerCustomer" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CustomerID" SessionField="ID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        CssClass="DataTables" DataKeyNames="pID" DataSourceID="SqlDataSource1" AllowSorting="True"
        onselectedindexchanged="GridView1_SelectedIndexChanged" >
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
            <asp:BoundField DataField="pID" HeaderText="מספר פרויקט" InsertVisible="False" ReadOnly="True"
                SortExpression="pID"  />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחת פרויקט" 
                SortExpression="DateOpened" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס" 
                SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" 
                SortExpression="Comments" />
            <asp:BoundField DataField="price" HeaderText="מחיר" SortExpression="price" />
            <asp:BoundField DataField="NumOfHatches" HeaderText="מספר פתחים" 
                ReadOnly="True" SortExpression="NumOfHatches" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>


<%--    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        CssClass="DataTables" DataKeyNames="pID" DataSourceID="SqlDataSource1" AllowSorting="True"
        onselectedindexchanged="GridView1_SelectedIndexChanged" >
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
            <asp:BoundField DataField="pID" HeaderText="מספר פרויקט" InsertVisible="False" ReadOnly="True"
                SortExpression="pID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס פרויקט" SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
            <asp:BoundField DataField="NumOfHatches" HeaderText="מספר פתחים" ReadOnly="True" SortExpression="NumOfHatches" />
        </Columns>
    </asp:GridView>--%>
