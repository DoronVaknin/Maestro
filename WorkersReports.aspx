<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="WorkersReports.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    <asp:SqlDataSource ID="FailureDataSource" runat="server"></asp:SqlDataSource>
    
    <asp:GridView ID="FailureGridView" runat="server">
    </asp:GridView>
    
    <br />
</asp:Content>

