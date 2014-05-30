<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewCustomer.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="form-background">
        <div class="cntr">
            <h1>
                לקוח חדש
            </h1>
        </div>
        <br />
        <table id="NewCustomerTBL" class="table">
            <tr>
                <td>
                    <div class="input-group">
                        <input id="CustomerId" type="text" class="form-control" runat="server" maxlength="9">
                        <span class="input-group-addon">ת.ז *</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="CustomerPhone" type="text" class="form-control" runat="server" maxlength="10">
                        <span class="input-group-addon">טלפון</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="CustomerFirstName" type="text" class="form-control" runat="server" maxlength="20">
                        <span class="input-group-addon">שם פרטי *</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="CustomerCellPhone" type="text" class="form-control" runat="server" maxlength="10">
                        <span class="input-group-addon">טלפון נייד</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="CustomerLastName" type="text" class="form-control" runat="server" maxlength="20">
                        <span class="input-group-addon">שם משפחה *</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="CustomerFaxNumber" type="text" class="form-control" runat="server" maxlength="10">
                        <span class="input-group-addon">פקס</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="CustomerAddress" runat="server" type="text" class="form-control Address"
                            placeholder="">
                        <span class="input-group-addon">כתובת *</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="CustomerEmail" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">דוא"ל *</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                        SelectCommand="spGetRegion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <div class="cntr">
                        אזור: &nbsp;
                        <asp:DropDownList ID="CustomerArea" runat="server" DataSourceID="SqlDataSource1"
                            CssClass="btn btn-default" DataTextField="RegionName" DataValueField="RegionID">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="cntr">
        <button id="CustomerForProjectBTN" class="btn btn-default" type="button" onclick="ValidateNewCustomer(this)">
            המשך ליצירת פרויקט&nbsp;&nbsp;<span class="glyphicon glyphicon-circle-arrow-left"></span>
        </button>
        <button id="CustomerForServiceCallBTN" class="btn btn-default" type="button" onclick="ValidateNewCustomer(this)">
            המשך ליצירת קריאת שירות&nbsp;&nbsp;<span class="glyphicon glyphicon-circle-arrow-left"></span>
        </button>
        <asp:Button ID="CreateCustomerForProject" runat="server" Text="המשך לפרויקט" CssClass="btn btn-default HiddenButtons"
            OnClick="CreateCustomerForProject_Click" />
        <asp:Button ID="CreateCustomerForServiceCall" runat="server" Text="המשך לקריאת שירות"
            CssClass="btn btn-default HiddenButtons" OnClick="CreateCustomerForServiceCall_Click" />
        <br />
        <br />
        <span class="ErrorLabel"></span>
    </div>
    <div class="modal fade" id="ModalCustomerError" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl">
                        &times;</button>
                    <h4 class="modal-title">
                        הודעת מערכת</h4>
                </div>
                <div class="modal-body">
                    לא ניתן לשמור את הלקוח, אנא נסה מאוחר יותר.
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
</asp:Content>
