<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true" CodeFile="NewCustomer.aspx.cs" Inherits="Default2" %>

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
                    <asp:TextBox ID="CustomerAddress" runat="server"></asp:TextBox>
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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>" 
                        SelectCommand="spGetRegion" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="CustomerArea" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="RegionName" 
                        DataValueField="RegionID">
                    </asp:DropDownList>
                </td>
            </tr>
            </table>
        <br /><br />

        <table class="style1">
            <tr>
                <td align="left" class="style6">
                    <asp:Button ID="Button1" runat="server" Text="שמור" onclick="Button1_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="ביטול" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>

