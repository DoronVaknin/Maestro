<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ServiceCalls.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <br />
    <br />
    <asp:SqlDataSource ID="ServiceCallsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetAllServiceCalls" SelectCommandType="StoredProcedure" UpdateCommand="update ServiceCall set Urgent=@Urgent , ProblemDesc=@ProblemDesc where scID=@scID">
    </asp:SqlDataSource>
    <asp:GridView runat="server" ID="ServiceCallsGridView" AutoGenerateColumns="False"
        DataKeyNames="scID" DataSourceID="ServiceCallsDataSource" CssClass="DataTables"
        AllowPaging="True" AllowSorting="True">
        <Columns>
            <asp:CommandField ShowSelectButton="true" SelectText="בחור" />
            <asp:CommandField ShowEditButton="true" EditText="ערוך" />
            <asp:BoundField DataField="scID" HeaderText="מספר קריאה" InsertVisible="False" ReadOnly="True"
                SortExpression="scID" />
            <asp:BoundField DataField="fName" HeaderText="שם פרטי" SortExpression="fName" ReadOnly="True" />
            <asp:BoundField DataField="lName" HeaderText="שם משפחה" SortExpression="lName" ReadOnly="True" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                ReadOnly="True" />
            <asp:BoundField DataField="DateClosed" HeaderText="תאריך סגירה" SortExpression="DateClosed"
                ReadOnly="True" />
            <asp:BoundField DataField="ProblemDesc" HeaderText="תיאור" SortExpression="ProblemDesc" />
            <asp:CheckBoxField DataField="Urgent" HeaderText="דחוף" SortExpression="Urgent" />
        </Columns>
    </asp:GridView>
</asp:Content>
