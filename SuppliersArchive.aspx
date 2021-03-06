﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="SuppliersArchive.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            ארכיון ספקים
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetSuppliersArchive" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:GridView ID="SuppliersGV" CssClass="DataTables" runat="server" AllowSorting="True"
        DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="SupplierID"
        OnSelectedIndexChanged="SuppliersGV_SelectedIndexChanged" OnDataBound="SetupQuickSearch">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="true" />
            <asp:BoundField DataField="SupplierID" HeaderText="מס' ספק" InsertVisible="False"
                ReadOnly="True" SortExpression="SupplierID" />
            <asp:BoundField DataField="sName" HeaderText="שם הספק" SortExpression="sName" />
            <asp:BoundField DataField="sAddress" HeaderText="כתובת" SortExpression="sAddress" />
            <asp:BoundField DataField="Phone" HeaderText="טלפון" SortExpression="Phone" />
            <asp:BoundField DataField="Mobile" HeaderText="נייד" SortExpression="Mobile" />
            <asp:BoundField DataField="Fax" HeaderText="פקס" SortExpression="Fax" />
            <asp:BoundField DataField="Email" HeaderText="דוא&quot;ל" SortExpression="Email" />
            <%--<asp:BoundField DataField="IsActive" HeaderText="פעיל" SortExpression="IsActive" />--%>
        </Columns>
    </asp:GridView>
    <div class="modal fade" id="EditSupplierModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="CloseBTN">
                        &times;</button>
                    <div class="cntr">
                        <h4 id="ModalHeader" runat="server" class="modal-title">
                            פרטי הספק</h4>
                    </div>
                </div>
                <div id="SupplierModal" class="modal-body" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SaveSupplierDetailsHiddenBTN" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table id="EditSupplierTBL" class="table">
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <input id="SupplierName" type="text" class="form-control" runat="server" maxlength="30">
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
                                            <input id="SupplierAddress" type="text" class="form-control" runat="server">
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
                                        <div class="input-group">
                                            <input id="SupplierEmail" type="text" class="form-control" runat="server">
                                            <span class="input-group-addon">דוא"ל</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <input id="SupplierFax" type="text" class="form-control" runat="server" maxlength="10">
                                            <span class="input-group-addon">פקס</span>
                                        </div>
                                    </td>
                                </tr>
                                <%--                                <tr>
                                    <td>
                                        <label>
                                            <input id="SupplierStatus" runat="server" type="checkbox">
                                            פעיל
                                        </label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <div class="cntr">
                        <button id="EditSupplierDetailsBTN" runat="server" type="button" class="btn btn-default"
                            onclick="EnableSupplierDetails()">
                            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
                        </button>
                        <button id="SaveSupplierDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="ValidateSupplierDetails(this)">
                            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelSupplierDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="RestoreSupplierDetails()">
                            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                        </button>
                        &nbsp;&nbsp;
                        <button runat="server" type="button" class="btn btn-success" onclick="EnableSupplier()">
                            הפעל ספק&nbsp;&nbsp;<span class="glyphicon glyphicon-repeat"></span>
                        </button>
                        <br />
                        <br />
                        <span id="SupplierDetailsErrorLabel" class="ErrorLabel"></span>
                        <asp:Button ID="SaveSupplierDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
                            OnClick="SaveSupplierDetailsBTN_Click" Font-Bold="true" />
                        <asp:Button ID="EnableSupplierHiddenBTN" runat="server" Text="הפעל ספק"
                            CssClass="btn btn-default HiddenButtons" OnClick="EnableSupplierHiddenBTN_Click"
                            Font-Bold="true" />
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /Modal -->
</asp:Content>
