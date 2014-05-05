<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewSupplier.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
 <div class="form-background">
    <div class="cntr">
        <h1>
            ספק חדש
        </h1>
    </div>
    <br />
    <table id="NewSupplierTBL" class="table">
        <tr>
            <td>
                <div class="input-group">
                    <input id="SupplierName" type="text" class="form-control" runat="server" maxlength = "30">
                    <span class="input-group-addon">שם הספק *</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="SupplierPhone" type="text" class="form-control" runat="server" maxlength="10">
                    <span class="input-group-addon">טלפון *</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="SupplierAddress" type="text" class="form-control" runat="server" maxlength="30">
                    <span class="input-group-addon">כתובת *</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="SupplierCellPhone" type="text" class="form-control" runat="server" maxlength="10">
                    <span class="input-group-addon">טלפון נייד</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
<%--                <div class="input-group">
                    <input id="SupplierCity" type="text" class="form-control City" runat="server" maxlength="30">
                    <span class="input-group-addon">עיר *</span>
                </div>--%>
            </td>
            <td>
                <div class="input-group">
                    <input id="SupplierFax" type="text" class="form-control" runat="server" maxlength="10">
                    <span class="input-group-addon">פקס</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div class="input-group">
                    <input id="SupplierEmail" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">דוא"ל</span>
                </div>
            </td>
        </tr>
    </table>
    </div>
 
    <div class="cntr">
        <input id="CreateSupplierBTN" type="button" value="צור ספק" class="btn btn-default"
            onclick="ValidateNewSupplier(this)" />
        <asp:Button ID="CreateSupplierHiddenBTN" runat="server" Text="צור ספק" CssClass="btn btn-default HiddenButtons"
            OnClick="CreateSupplierHiddenBTN_Click" />
        <br />
        <br />
        <span class="ErrorLabel"></span>
    </div>
    <br />
    <br />
</asp:Content>
