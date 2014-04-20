<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectHatches.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1 id = "PageHeader" runat = "server">
            רשימת פתחים
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetHatches" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="ProjectID" SessionField="ProjectIDForProjectHatches"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="ProjectHatchesGV" CssClass="DataTables" runat="server" AllowPaging="True"
        AllowSorting="True" DataSourceID="SqlDataSource1" AutoGenerateColumns="False">
        <Columns>
            <asp:CommandField EditText="ערוך" ShowEditButton="true" />
            <asp:CommandField DeleteText="מחק" ShowDeleteButton="true" />
            <asp:BoundField DataField="hID" HeaderText="מס' פתח" InsertVisible="False" ReadOnly="True"
                SortExpression="hID" />
            <asp:BoundField DataField="htName" HeaderText="סוג הפתח" SortExpression="htName" />
            <asp:BoundField DataField="hsName" HeaderText="סטטוס" SortExpression="hsName" />
        </Columns>
    </asp:GridView>
</asp:Content>
