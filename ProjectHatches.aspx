<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectHatches.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1 id="PageHeader" runat="server">
            רשימת פתחים
        </h1>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetProjectHatches" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="ProjectID" SessionField="ProjectIDForProjectHatches"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="ProjectHatchesGV" CssClass="DataTables" runat="server"
        AllowSorting="True" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
        DataKeyNames="hID" 
        OnSelectedIndexChanged="ProjectHatchesGV_SelectedIndexChanged" 
        OnDataBound = "SetupQuickSearch">
        <Columns>
            <asp:CommandField SelectText="בחר" ShowSelectButton="true" />
            <asp:CommandField DeleteText="מחק" ShowDeleteButton="true" />
            <asp:BoundField DataField="hID" HeaderText="מס' פתח" InsertVisible="False" ReadOnly="True"
                SortExpression="hID" />
            <asp:BoundField DataField="htName" HeaderText="סוג הפתח" SortExpression="htName" />
            <asp:BoundField DataField="hsName" HeaderText="סטטוס" SortExpression="hsName" />
            <asp:BoundField DataField="StatusLastModified" HeaderText="תאריך דיווח" SortExpression="StatusLastModified"
                DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="eName" HeaderText="העובד המדווח" SortExpression="eName" />
            <asp:BoundField DataField="ftName" HeaderText="סוג התקלה" SortExpression="ftName" />
            <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
        </Columns>
    </asp:GridView>
    <div class="modal fade" id="EditHatchModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" dir="rtl"
                        id="CloseBTN">
                        &times;</button>
                    <div class="cntr">
                        <h4 id="ModalHeader" runat="server" class="modal-title">
                            פרטי הפתח</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SaveHatchDetailsHiddenBTN" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table id="EditHatchTBL" class="table">
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="HatchID" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">מס' פתח</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="HatchStatusLastModified" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">תאריך דיווח</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="HatchProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">שם הפרויקט</span>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="input-group">
                                            <asp:TextBox ID="HatchEmployeeName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">העובד המדווח</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                            SelectCommand="spGetHatchTypeList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <div class="cntr">
                                            סוג הפתח: &nbsp;
                                            <asp:DropDownList ID="HatchType" runat="server" DataSourceID="SqlDataSource2" CssClass="btn btn-default"
                                                DataTextField="htName" DataValueField="htID">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                            SelectCommand="spGetHatchStatusList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <div class="cntr">
                                            סטטוס: &nbsp;
                                            <asp:DropDownList ID="HatchStatus" runat="server" DataSourceID="SqlDataSource3" CssClass="btn btn-default"
                                                DataTextField="hsName" DataValueField="hsID">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="TextAreaHolder">
                                            <span style="float: right">הערות:</span>
                                            <asp:TextBox ID="HatchComments" runat="server" CssClass="form-control" TextMode="multiline"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                                            SelectCommand="spGetFailureTypeList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <div class="cntr">
                                            סוג התקלה: &nbsp;
                                            <asp:DropDownList ID="HatchFailureType" runat="server" DataSourceID="SqlDataSource4"
                                                CssClass="btn btn-default" DataTextField="ftName" DataValueField="ftID">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <div class="cntr">
                        <button id="EditHatchDetailsBTN" runat="server" type="button" class="btn btn-default"
                            onclick="EnableHatchDetails()">
                            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
                        </button>
                        <button id="SaveHatchDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="ValidateHatchDetails()">
                            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
                        </button>
                        <button id="CancelHatchDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
                            onclick="RestoreHatchDetails()">
                            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
                        </button>
                        <br />
                        <br />
                        <span id="HatchDetailsErrorLabel" class="ErrorLabel"></span>
                        <asp:Button ID="SaveHatchDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
                            OnClick="SaveHatchDetailsBTN_Click" Font-Bold="true" />
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
