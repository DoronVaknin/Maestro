<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectOrders.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1 id = "PageHeader" runat = "server">
            הזמנות עבור פרויקט
        </h1>
        <br />
    </div>
    <table id="ProjectOrdersTBL" class="DataTables">
        <tr>
            <td>
                שם הפריט
            </td>
            <td>
                כמות להזמנה
            </td>
            <td>
                שם הספק
            </td>
        </tr>
        <tr>
            <td>
                תריסים
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="ShutterCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="ShutterProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                וואלים
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="ValimCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="ValimProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                פרזול
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="ShoeingCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="ShoeingProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ממ"ד
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="ProtectedSpaceCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="ProtectedSpaceProvider" runat="server" CssClass="form-control"
                    Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ארגזים
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="BoxCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="BoxesProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                נאספים
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="CollectedCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="CollectedProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                יואים
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="UCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="UProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                מנועים
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="EngineCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="EngineProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                זכוכית
            </td>
            <td>
                <div class="input-group">
                    <span class="input-group-addon minus">-</span>
                    <asp:TextBox ID="GlassCount" runat="server" CssClass="form-control cntr">0</asp:TextBox>
                    <span class="input-group-addon plus">+</span>
                </div>
            </td>
            <td>
                <asp:DropDownList ID="GlassProvider" runat="server" CssClass="form-control" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
