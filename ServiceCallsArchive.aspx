<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ServiceCallsArchive.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            ארכיון קריאות שירות</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="ServiceCallsArchiveDS" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetServiceCallsArchive" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <asp:GridView ID="ServiceCallsArchiveGV" CssClass="DataTables" runat="server" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="scID" DataSourceID="ServiceCallsArchiveDS">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:BoundField DataField="scID" HeaderText="מס' קריאה" InsertVisible="False" ReadOnly="True"
                SortExpression="scID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="DateClosed" HeaderText="תאריך סגירה" SortExpression="DateClosed" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="ProblemDesc" HeaderText="התקלה" SortExpression="ProblemDesc" />
            <asp:CheckBoxField DataField="Urgent" HeaderText="דחוף" SortExpression="Urgent" />
        </Columns>
    </asp:GridView>
</asp:Content>
