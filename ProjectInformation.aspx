<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="ProjectInformation.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 149px
        }
        .style2
        {
            width: 113px
        }
        .style3
        {
            width: 147px
        }
        .style4
        {
            width: 186px
        }
        .style5
        {
            width: 166px
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
                <asp:TextBox ID="TextBox1" runat="server">txtID</asp:TextBox>
            </td>
            <td class="style2">
                טלפון</td>
            <td class="style5">
                <asp:TextBox ID="TextBox6" runat="server">txtCustomerPhone</asp:TextBox>
            </td>
            <td class="style3">
                כתובת</td>
            <td>
                <asp:TextBox ID="TextBox11" runat="server">txtAdress</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                שם פרטי:</td>
            <td class="style4">
                <asp:TextBox ID="TextBox2" runat="server">txtFirstName</asp:TextBox>
            </td>
            <td class="style2">
                טלפון נייד</td>
            <td class="style5">
                <asp:TextBox ID="TextBox7" runat="server">txtCustomerMobile</asp:TextBox>
            </td>
            <td class="style3">
                עיר</td>
            <td>
                <asp:TextBox ID="TextBox12" runat="server">txtCity</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                שם משפחה:</td>
            <td class="style4">
                <asp:TextBox ID="TextBox3" runat="server">txtLastName</asp:TextBox>
            </td>
            <td class="style2">
                פקס</td>
            <td class="style5">
                <asp:TextBox ID="TextBox8" runat="server">txtCustomerFax</asp:TextBox>
            </td>
            <td class="style3">
                דוא&quot;ל</td>
            <td>
                <asp:TextBox ID="TextBox13" runat="server">txtEmail</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                שם האדריכל:</td>
            <td class="style4">
                <asp:TextBox ID="TextBox4" runat="server">txtArchitectName</asp:TextBox>
            </td>
            <td class="style2">
                טלפון נייד</td>
            <td class="style5">
                <asp:TextBox ID="TextBox9" runat="server">txtArchitectMobile</asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                שם הקבלן:</td>
            <td class="style4">
                <asp:TextBox ID="TextBox5" runat="server">txtContractorName</asp:TextBox>
            </td>
            <td class="style2">
                טלפון נייד</td>
            <td class="style5">
                <asp:TextBox ID="TextBox10" runat="server">txtContractorMobile</asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

