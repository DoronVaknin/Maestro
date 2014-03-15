﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewCustomer.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            לקוח חדש
        </h1>
    </div>
    <table id="NewCustomerTBL" class="table">
        <tr>
            <td>
                <div class="input-group">
                    <input id="CustomerId" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">ת.ז</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="CustomerPhone" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">טלפון</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="CustomerFirstName" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">שם פרטי</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="CustomerCellPhone" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">טלפון נייד</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="CustomerLastName" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">שם משפחה</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="CustomerFaxNumber" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">פקס</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="CustomerAddress" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">כתובת</span>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="CustomerEmail" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">דוא"ל</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="CustomerCity" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">עיר</span>
                </div>
            </td>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>"
                    SelectCommand="spGetRegion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <div class="cntr">
                    <asp:DropDownList ID="CustomerArea" runat="server" DataSourceID="SqlDataSource1"
                        class="btn btn-default" DataTextField="RegionName" DataValueField="RegionID">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div class="cntr">
        <asp:Button ID="CreateCustomer" runat="server" Text="צור לקוח" class="btn btn-default" Font-Bold = "true"
            OnClick="CreateCustomer_Click" />
    </div>
</asp:Content>