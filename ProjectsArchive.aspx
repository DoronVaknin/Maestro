<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectsArchive.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            ארכיון פרויקטים</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="ProjectsArchiveDS" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetProjectsArchive" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:GridView ID="ProjectsArchiveGV" CssClass="DataTables" runat="server" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="pID" DataSourceID="ProjectsArchiveDS"
        OnSelectedIndexChanged="ProjectsArchiveGV_SelectedIndexChanged" OnDataBound="OnDataBound">
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
            <asp:BoundField DataField="pID" HeaderText="מס' פרויקט" InsertVisible="False" ReadOnly="True"
                SortExpression="pID" />
            <asp:BoundField DataField="pName" HeaderText="שם פרויקט" SortExpression="pName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס" SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
            <asp:BoundField DataField="Cost" HeaderText="עלות" SortExpression="Cost" />
            <asp:BoundField DataField="NumOfHatches" HeaderText="מס' פתחים" ReadOnly="True" SortExpression="NumOfHatches" Visible = "false" />
        </Columns>
    </asp:GridView>
</asp:Content>
