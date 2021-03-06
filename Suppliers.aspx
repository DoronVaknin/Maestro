﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="Suppliers.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            ספקים
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetAllSuppliers" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
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
    <br />
    <div class="cntr">
        <button type="button" value="צור ספק חדש" class="btn btn-default" onclick="Goto('NewSupplier')">
            צור ספק חדש&nbsp;&nbsp;<span class="glyphicon glyphicon-plus"></span>
        </button>
    </div>
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
                        <button runat="server" type="button" class="btn btn-default" onclick="ActivateDisableSupplierDialog()">
                            השבת ספק&nbsp;&nbsp;<span class="glyphicon glyphicon-folder-close"></span>
                        </button>
                        <br />
                        <br />
                        <span id="SupplierDetailsErrorLabel" class="ErrorLabel"></span>
                        <asp:Button ID="SaveSupplierDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
                            OnClick="SaveSupplierDetailsBTN_Click" Font-Bold="true" />
                        <asp:Button ID="DisableSupplierHiddenBTN" runat="server" Text="השבת ספק"
                            CssClass="btn btn-default HiddenButtons" OnClick="DisableSupplierHiddenBTN_Click"
                            Font-Bold="true" />
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /Modal -->
    <div class="modal fade" id="DisableSupplierDialogModal" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                        &times;</button>
                    <div class="cntr">
                        <h4 class="modal-title">
                            השבתת ספק</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="QuestionContainer">
                    </div>
                    <br />
                    <div class="cntr">
                        <button id="DisableSupplierBTN" runat="server" type="button" class="btn btn-danger"
                            onclick="DisableSupplier()">
                            השבת ספק&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelDisableSupplierBTN" runat="server" data-dismiss="modal" type="button"
                            class="btn btn-default">
                            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                        </button>
                    </div>
                </div>
                <%--                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <button type="button" class="btn btn-primary">
                        Save changes</button>
                </div>--%>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /Modal -->
</asp:Content>
