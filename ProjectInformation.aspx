<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="ProjectInformation.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 130px
        }
        .style2
        {
            width: 113px
        }
        .style4
        {
            width: 186px
        }
        .style5
        {
            width: 166px
        }
        .style6
        {
            width: 58px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <p>
        <br />
        פרטי הלקוח:
    </p>
    <table class="nav-justified">
        <tr>
            <td class="style1">
                תעודת זהות:</td>
            <td class="style4">
                <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                טלפון</td>
            <td class="style5">
                <asp:TextBox ID="txtCustomerPhone" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                כתובת</td>
            <td dir="rtl">
                <asp:TextBox ID="txtAdress" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                שם פרטי:</td>
            <td class="style4">
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                טלפון נייד</td>
            <td class="style5">
                <asp:TextBox ID="txtCustomerMobile" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                עיר</td>
            <td dir="rtl">
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                שם משפחה:</td>
            <td class="style4">
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                פקס</td>
            <td class="style5">
                <asp:TextBox ID="txtCustomerFax" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                דוא&quot;ל</td>
            <td dir="rtl">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                שם האדריכל:</td>
            <td class="style4">
                <asp:TextBox ID="txtArchitectName" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                טלפון נייד</td>
            <td class="style5">
                <asp:TextBox ID="txtArchitectMobile" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                &nbsp;</td>
            <td dir="rtl">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                שם הקבלן:</td>
            <td class="style4">
                <asp:TextBox ID="txtContractorName" runat="server"></asp:TextBox>
            </td>
            <td class="style2">
                טלפון נייד</td>
            <td class="style5">
                <asp:TextBox ID="txtContractorMobile" runat="server"></asp:TextBox>
            </td>
            <td class="style6">
                &nbsp;</td>
            <td dir="rtl">
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:Button runat="server" Text="ערוך" ID="EditButton" 
        onclick="Unnamed1_Click" />

    <asp:Button ID="SaveCustomerNewInformation" runat="server" Text="שמור" 
        onclick="SaveCustomerNewInformation_Click1" />

</asp:Content>

