<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectDetails.aspx.cs" Inherits="Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 68px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="cntr">
        <h1>
            פרטי הלקוח
        </h1>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SaveCustomerDetailsHiddenBTN" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table id="CustomerDetailsTBL" class="table">
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoID" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
                            <span class="input-group-addon">ת.ז</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoAddress" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            <span class="input-group-addon">כתובת</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoFirstName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            <span class="input-group-addon">שם פרטי</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoCity" runat="server" CssClass="form-control City"></asp:TextBox>
                            <span class="input-group-addon">עיר</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoLastName" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                            <span class="input-group-addon">שם משפחה</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoEmail" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            <span class="input-group-addon">דוא"ל</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoPhone" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            <span class="input-group-addon">טלפון</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoFax" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            <span class="input-group-addon">פקס</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoMobile" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            <span class="input-group-addon">טלפון נייד</span>
                        </div>
                    </td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                            SelectCommand="spGetRegion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <div class="cntr">
                            אזור: &nbsp;
                            <asp:DropDownList ID="ProjectInfoArea" runat="server" DataSourceID="SqlDataSource2"
                                CssClass="btn btn-default" DataTextField="RegionName" DataValueField="RegionID">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="cntr">
        <button id="EditCustomerDetailsBTN" runat="server" type="button" class="btn btn-default"
            onclick="EnableCustomerDetails()">
            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <button id="SaveCustomerDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="ValidateCustomerDetails()">
            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
        </button>
        <button id="CancelCustomerDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="RestoreCustomerDetails()">
            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
        </button>
        <br />
        <br />
        <span id="CustomerDetailsErrorLabel" class="ErrorLabel"></span>
        <asp:Button ID="SaveCustomerDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
            OnClick="SaveCustomerDetailsBTN_Click1" Font-Bold="true" />
    </div>
    <asp:HiddenField ID="ProjectIDHolder" runat="server" />
    <br />
    <div class="cntr">
        <h1>
            פרטי הפרויקט
        </h1>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SaveProjectDetailsHiddenBTN" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table id="ProjectDetailsTBL" class="table">
                <tr>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                            SelectCommand="spGetProjectStatusList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        סטטוס הפרויקט:
                        <asp:DropDownList ID="ProjectInfoStatus" runat="server" CssClass="btn btn-default"
                            DataSourceID="SqlDataSource1" DataTextField="psName" DataValueField="psName"
                            AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div class="input-group">
                            <input id="ProjectInfoDateOpened" type="text" class="form-control datepicker" runat="server">
                            <span class="input-group-addon">תאריך פתיחה</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoName" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                            <span class="input-group-addon">שם הפרויקט</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <input id="ProjectInfoExpirationDate" type="text" class="form-control datepicker"
                                runat="server">
                            <span class="input-group-addon">תאריך תפוגה</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoHatches" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                            <span class="input-group-addon">מס' פתחים</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <input id="ProjectInfoInstallationDate" type="text" class="form-control datepicker"
                                runat="server">
                            <span class="input-group-addon">תאריך התקנה</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoCost" runat="server" CssClass="form-control"></asp:TextBox>
                            <span class="input-group-addon">סה"כ עלות</span>
                        </div>
                    </td>
                    <td>
                        <div class="TextAreaHolder">
                            <asp:TextBox ID="ProjectInfoComments" runat="server" CssClass="form-control" TextMode="multiline"
                                placeholder="הערות"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoArchitectName" runat="server" CssClass="form-control"
                                MaxLength="15"></asp:TextBox>
                            <span class="input-group-addon">שם האדריכל</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoArchitectMobile" runat="server" CssClass="form-control"
                                MaxLength="10"></asp:TextBox>
                            <span class="input-group-addon">טלפון אדריכל</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoContractorName" runat="server" CssClass="form-control"
                                MaxLength="15"></asp:TextBox>
                            <span class="input-group-addon">שם הקבלן</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoContractorMobile" runat="server" CssClass="form-control"
                                MaxLength="10"></asp:TextBox>
                            <span class="input-group-addon">טלפון קבלן</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoSupervisorName" runat="server" CssClass="form-control"
                                MaxLength="15"></asp:TextBox>
                            <span class="input-group-addon">שם המפקח</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="ProjectInfoSupervisorMobile" runat="server" CssClass="form-control"
                                MaxLength="10"></asp:TextBox>
                            <span class="input-group-addon">טלפון מפקח</span>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="cntr">
        <button id="EditProjectDetailsBTN" runat="server" type="button" class="btn btn-default"
            onclick="EnableProjectDetails()">
            ערוך&nbsp;&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <button id="SaveProjectDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="ValidateProjectDetails()">
            שמור&nbsp;&nbsp;<span class="glyphicon glyphicon-ok"></span>
        </button>
        <button id="CancelProjectDetailsBTN" runat="server" type="button" class="btn btn-default HiddenButtons"
            onclick="RestoreProjectDetails()">
            בטל&nbsp;&nbsp;<span class="glyphicon glyphicon-remove"></span>
        </button>
        <asp:Button ID="SaveProjectDetailsHiddenBTN" runat="server" Text="שמור" CssClass="btn btn-default HiddenButtons"
            OnClick="SaveProjectDetailsBTN_Click" Font-Bold="true" />
        <br />
        <br />
        <span id="ProjectDetailsErrorLabel" class="ErrorLabel"></span>
        <br />
        <h1>
            הזמנות עבור הפרויקט</h1>
    </div>
    <br />
    <table class="nav-justified" id="OrderTable">
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
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                תריסים
            </td>
            <td>
                <asp:TextBox ID="ShutterCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ShutterProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                נאספים
            </td>
            <td>
                <asp:TextBox ID="CollectedCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="CollectedProvider" runat="server" value="2" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                וואלים
            </td>
            <td class="style1">
                <asp:TextBox ID="ValimCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:DropDownList ID="ValimProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td class="style1">
                U
            </td>
            <td class="style1">
                <asp:TextBox ID="UCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:DropDownList ID="UProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                פרזול
            </td>
            <td>
                <asp:TextBox ID="ShoeingCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ShoeingProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                מנועים
            </td>
            <td>
                <asp:TextBox ID="EngineCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="EngineProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ממ&quot;ד
            </td>
            <td>
                <asp:TextBox ID="ProtectedSpaceCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ProtectedSpaceProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                זכוכית
            </td>
            <td>
                <asp:TextBox ID="GlassCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="GlassProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ארגזים
            </td>
            <td>
                <asp:TextBox ID="BoxCount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="BoxesProvider" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="cntr">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="צור הזמנה"
            CssClass="btn btn-default" Font-Bold="true" /></div>
    <br />
    <br />
    <asp:GridView runat="server" AutoGenerateColumns="False" DataSourceID="OrderDataSource"
        CssClass="DataTables">
        <Columns>
            <asp:CommandField ShowEditButton="true" />
            <asp:BoundField DataField="oID" HeaderText="oId" SortExpression="oId" ReadOnly="true" />
            <asp:BoundField DataField="DateOpened" HeaderText="DateOpened" SortExpression="DateOpened"
                ReadOnly="true" />
            <asp:BoundField DataField="rName" HeaderText="rName" SortExpression="rName" ReadOnly="true" />
            <asp:BoundField DataField="DateOfArrival" HeaderText="DateOfArrival" SortExpression="DateOfArrival"
                ReadOnly="true" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity"
                ReadOnly="true" />
            <asp:BoundField DataField="sName" HeaderText="sName" SortExpression="sName" ReadOnly="true" />
            <asp:TemplateField HeaderText="osName" SortExpression="osName">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="StatusDataSource"
                        DataTextField="osName" DataValueField="osName" SelectedValue='<%# Bind("osName") %>'>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("osName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="OrderDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrdersListForProject" SelectCommandType="StoredProcedure"
        UpdateCommand="Update[Orders] set [osID] =@osID where [oID]=@oID">
        <SelectParameters>
            <asp:ControlParameter ControlID="ProjectIDHolder" DefaultValue="" Name="ProjectID"
                PropertyName="Value" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="osID" Type="Int32" />
            <asp:Parameter Name="oID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="StatusDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
        SelectCommand="spGetOrderStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <br />
    <br />
</asp:Content>
