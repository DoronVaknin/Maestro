<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="ShowProjectsForCustomer.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

<br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>" 
        SelectCommand="ShowProjectsForCustomer" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CustomerID" SessionField="ID" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="pID" DataSourceID="SqlDataSource1" AllowSorting="True" >
        <Columns>
            <asp:BoundField DataField="pID" HeaderText="מספר פרויקט" InsertVisible="False" 
                ReadOnly="True" SortExpression="pID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" 
                SortExpression="DateOpened" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס פרויקט" 
                SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" 
                SortExpression="Comments" />
            <asp:BoundField DataField="NumOfHathes" HeaderText="מספר פתחים" 
                ReadOnly="True" SortExpression="NumOfHathes" />
        </Columns>
    </asp:GridView>
<br />

</asp:Content>

