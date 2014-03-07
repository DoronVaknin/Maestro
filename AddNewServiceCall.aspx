<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="AddNewServiceCall.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        width: 136px;
    }
        .style3
        {
            width: 406px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div align="center">
        <h1>
            צור קריאת שירות</h1>
    </div>
    <br />
    <table class="style1">
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                שם הלקוח
            </td>
            <td>
                <asp:TextBox ID="CustomerName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                נייד
            </td>
            <td>
                <asp:TextBox ID="CustomerPhone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                כתובת
            </td>
            <td>
                <asp:TextBox ID="CustomerAdress" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                תיאור התקלה
            </td>
            <td>
                <asp:TextBox ID="ErrorDescription" TextMode="multiline" Columns="50" Rows="5" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                אזור
            </td>
            <td>
                <asp:DropDownList ID="AreaTB" runat="server">
                    <asp:ListItem Value="1">השרון</asp:ListItem>
                    <asp:ListItem Value="2">השפלה</asp:ListItem>
                    <asp:ListItem Value="3">צפון</asp:ListItem>
                    <asp:ListItem Value="4">מרכז</asp:ListItem>
                    <asp:ListItem Value="5">דרום</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                קריאה דחופה
            </td>
            <td>
                &nbsp;
                <asp:CheckBox ID="IsUrgent" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" class="style2" style="padding-right: 30px">
                המוצר באחריות
            </td>
            <td>
                &nbsp;
                <asp:CheckBox ID="IsWarranty" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr>
            <td align="left" class="style3">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    style="margin-left: 0px" Text="שמור" Width="66px" />
            </td>
            <td style="padding-right: 10px">
                <asp:Button ID="Button2" runat="server" Text="בטל" Width="64px" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
