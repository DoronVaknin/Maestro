﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
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
    <asp:GridView ID="ProjectsGV" runat="server" AllowSorting="True" CssClass="DataTables"
        DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnDataBound="SetupQuickSearch"
        OnSelectedIndexChanged="ProjectsTBL_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
            <asp:BoundField DataField="pID" HeaderText="מס' פרויקט" InsertVisible="False" ReadOnly="True"
                SortExpression="pID" />
            <asp:BoundField DataField="pName" HeaderText="שם פרויקט" SortExpression="pName" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" SortExpression="DateOpened"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס" SortExpression="psName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
            <asp:BoundField DataField="Cost" HeaderText="עלות (ש&quot;ח)" SortExpression="Cost" />
            <asp:BoundField DataField="NumOfHatches" HeaderText="מס' פתחים" ReadOnly="True" SortExpression="NumOfHatches"
                Visible="false" />
        </Columns>
    </asp:GridView>
    <br />
    <div class="cntr">
        <button type="button" class="btn btn-default" onclick="Goto('NewCustomer','?Source=CreateProject')">
            צור פרויקט חדש&nbsp;&nbsp;<span class="glyphicon glyphicon-plus"></span>
        </button>
    </div>
    <br />
</asp:Content>
