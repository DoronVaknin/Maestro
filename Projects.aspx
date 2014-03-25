﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="Projects.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            פרויקטים</h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>"
        SelectCommand="spShowAllProjects" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:GridView ID="ProjectsTBL" runat="server" AllowPaging="True" AllowSorting="True"
        OnSelectedIndexChanged="ProjectsTBL_SelectedIndexChanged" CssClass="DataTables"
        DataSourceID="SqlDataSource1" AutoGenerateColumns="false">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="True" />
            <asp:BoundField DataField="fname" HeaderText="שם פרטי" />
            <asp:BoundField DataField="lname" HeaderText="שם משפחה" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך פתיחה" />
            <asp:BoundField DataField="psName" HeaderText="סטטוס" />
            <asp:BoundField DataField="comments" HeaderText="הערות" />
            <asp:BoundField DataField="HatchesNum" HeaderText="מספר פתחים" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
