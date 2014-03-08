<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="projectinfo.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 170px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">


    <table class="style1">
        <tr>
            <td class="style2" align="right" style="padding-right: 20px">
                תאריך</td>
            <td>
                <asp:Calendar ID="Calendar1" runat="server" Height="219px" Width="757px">
                </asp:Calendar>
            </td>
        </tr>
        <tr>
            <td class="style2" align="right" style="padding-right: 20px">
                הערות</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" align="right" style="padding-right: 20px">
                סה&quot;כ עלות ללקוח</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" align="right" style="padding-right: 20px">
                העלה קבצים</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2" align="right" style="padding-right: 20px">
                מספר פתחים
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                <br />
            </td>
        </tr>
    </table>
    <br /><br />

        <table class="style1">
        <tr>
            <td align="left" class="style3">
               
            </td>
            <asp:Button runat="server" Text="Button" />
            <td style="padding-right: 10px">
                <asp:Button ID="Button2" runat="server" Text="בטל" Width="64px" />
            </td>
        </tr>
    </table>

</asp:Content>

