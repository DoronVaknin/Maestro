﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="AddNewCustomer.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 372px;
        }
        .style5
        {
            width: 120px;
        }
        .style6
        {
            width: 469px;
        }
        .style7
        {
            width: 121px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <p>
        <br />
        <table class="style1">
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    תעודת זהות</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="CustomerId" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    טלפון</td>
                <td align="right">
                    <asp:TextBox ID="CustomerPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    שם פרטי</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="CustomerFirstName" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    טלפון נייד</td>
                <td align="right">
                    <asp:TextBox ID="CustomerCellPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    שם משפחה</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="CustomerLastName" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    פקס</td>
                <td align="right">
                    <asp:TextBox ID="CustomerFaxNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    כתובת</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="CustomerAdress" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    כתובת דוא&quot;ל</td>
                <td align="right">
                    <asp:TextBox ID="CustomerEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    עיר</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="CustomerCity" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    אזור</td>
                <td align="right">
                    <asp:TextBox ID="CustomerArea" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    שם הקבלן
                </td>
                <td align="right" class="style3">
                    <asp:TextBox ID="ContractorName" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    טלפון נייד</td>
                <td align="right">
                    <asp:TextBox ID="ContractorPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    שם האדריכל</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="ArchitectName" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    טלפון נייד</td>
                <td align="right">
                    <asp:TextBox ID="ArchitectPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style7">
                    &nbsp;&nbsp;&nbsp;
                    שם המפקח</td>
                <td align="right" class="style3">
                    <asp:TextBox ID="SupervisorName" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="style5">
                    טלפון נייד</td>
                <td align="right">
                    <asp:TextBox ID="SupervisorPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br /><br />

        <table class="style1">
            <tr>
                <td align="left" class="style6">
                    <asp:Button ID="Button1" runat="server" Text="שמור" />
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="ביטול" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>

