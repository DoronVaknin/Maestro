<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="Suppliers.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            ספקים
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetAllSuppliers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:GridView ID="SuppliersGV" CssClass="DataTables" runat="server" AllowPaging="True"
        AllowSorting="True" DataSourceID="SqlDataSource2" AutoGenerateColumns="False"
        DataKeyNames="SupplierID">
        <Columns>
            <asp:CommandField SelectText = "בחר" ShowSelectButton = "true" />
            <asp:CommandField DeleteText="מחק" ShowDeleteButton="true" />
            <asp:BoundField DataField="SupplierID" HeaderText="מס' ספק" InsertVisible="False"
                ReadOnly="True" SortExpression="SupplierID" />
            <asp:BoundField DataField="sName" HeaderText="שם הספק" SortExpression="sName" />
            <asp:BoundField DataField="sAddress" HeaderText="כתובת" SortExpression="sAddress" />
            <asp:BoundField DataField="Phone" HeaderText="טלפון" SortExpression="Phone" />
            <asp:BoundField DataField="Mobile" HeaderText="נייד" SortExpression="Mobile" />
            <asp:BoundField DataField="Fax" HeaderText="פקס" SortExpression="Fax" />
            <asp:BoundField DataField="Email" HeaderText="דוא&quot;ל" SortExpression="Email" />
        </Columns>
    </asp:GridView>
    <br />
    <div class="cntr">
        <button type="button" value="צור ספק חדש" class="btn btn-default" onclick="Goto('NewSupplier')" >
            צור ספק חדש&nbsp;&nbsp;<span class="glyphicon glyphicon-plus"></span>
        </button>
    </div>
</asp:Content>