<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectsPerCustomer.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1 id="PageHeader" runat="server">
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spShowProjectsPerCustomer" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CustomerID" SessionField="CustomerID"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="DataTables"
        DataKeyNames="pID" DataSourceID="SqlDataSource1" AllowSorting="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:BoundField DataField="pID" HeaderText="מס' פרויקט" InsertVisible="False" ReadOnly="True"
                SortExpression="pID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="psName" HeaderText="סטטוס" SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
            <asp:BoundField DataField="Cost" HeaderText="עלות" SortExpression="Cost" />
            <asp:BoundField DataField="NumOfHatches" HeaderText="מס' פתחים" ReadOnly="True" SortExpression="NumOfHatches" />
        </Columns>
    </asp:GridView>
    <br />
    <div class="cntr">
        <asp:Button ID="AddProjectForCustomer" runat="server" Text="הוסף פרויקט" CssClass="btn btn-default"
            Font-Bold="true" OnClick="AddProjectForCustomer_Click" />
    </div>
</asp:Content>
