<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectInformation.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 68px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            פרטי הלקוח
        </h1>
    </div>
    <br />
        <table id="CustomerDetailsTBL" class="table">
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtID" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">ת.ז</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtAdress" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">כתובת</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtFirstName" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">שם פרטי</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtCity" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">עיר</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtLastName" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">שם משפחה</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtEmail" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">דוא"ל</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtCustomerPhone" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">טלפון</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtCustomerFax" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">פקס</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtCustomerMobile" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">טלפון נייד</span>
                    </div>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtArchitectName" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">שם האדריכל</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtArchitectMobile" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">טלפון אדריכל</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtContractorName" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">שם הקבלן</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtContractorPhone" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">טלפון קבלן</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="txtSupervisorName" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">שם המפקח</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="txtSupervisorPhone" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">טלפון מפקח</span>
                    </div>
                </td>
            </tr>
        </table>
    <div class="cntr">
        <button id="EditCustomerDetailsBTN" runat="server" type="button" class="btn btn-default"
            onclick="EnableTextBoxes()">
            ערוך&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <asp:Button ID="SaveCustomerNewInformation" runat="server" Text="שמור" class="btn btn-default"
            OnClick="SaveCustomerNewInformation_Click1" Font-Bold="true" />
    </div>
    <br />
    <div class="cntr">
        <h1>
            פרטי הפרויקט
        </h1>
    </div>
    <table id="ProjectDetailsTBL" class="table">
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>"
                    SelectCommand="spGetProjectStatusList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>



                סטטוס הפרויקט:
                <asp:DropDownList ID="ProjectStatusDDL" runat="server" CssClass="btn btn-default"
                    DataSourceID="SqlDataSource1" DataTextField="psName" DataValueField="psName"
                    OnDataBinding="DropDownDataBound" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td>
                <div class="input-group">
                    <input id="txtProjectPrice" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">סה"כ עלות ללקוח</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="txtHatchesNum" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">מס' פתחים</span>
                </div>
            </td>
            <td>
                <div id="CommentContainer">
                    <textarea id="txtProjectComment" runat="server" class="form-control" cols="10" rows="3"
                        placeholder="הערות"></textarea>
                </div>
            </td>
        </tr>
    </table>
    <div class="cntr">
        <button id="edit" runat="server" type="button" class="btn btn-default" onclick="EnableProjectStatus()">
            ערוך&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <asp:Button ID="btnSaveProjDetails" runat="server" Text="שמור" class="btn btn-default"
            OnClick="btnSaveProjDetails_Click" Font-Bold="true" />
        <br />
        <br />
        הזמנות עבור פרוייקט<br />
        <br />
        <table class="nav-justified" id="OrderTable">
            <tr>
                <td>
                    שם הפריט</td>
                <td>
                    כמות להזמנה</td>
                <td>
                    שם הספק</td>
                <td>
                    סטטוס</td>
            </tr>
            <tr>
                <td>
                    תריסים</td>
                <td>
                    <asp:DropDownList ID="ShuttersCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ShutterProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ShutterAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    נאספים</td>
                <td class="style1">
                    <asp:DropDownList ID="CollectedCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="CollectedProvider" runat="server" value="2" Width="150px">
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="CollectedAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    וואלים</td>
                <td>
                    <asp:DropDownList ID="ValimCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ValimProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ValimAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    U</td>
                <td>
                    <asp:DropDownList ID="UCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="UProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="Uamount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    פרזול</td>
                <td>
                    <asp:DropDownList ID="ShoeingCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ShoeingProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ShoeingAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    מנועים</td>
                <td>
                    <asp:DropDownList ID="EnginCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="EngineProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="EnginesAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    ממ&quot;ד</td>
                <td>
                    <asp:DropDownList ID="ProtectedSpaceCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ProtectedSpaceProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ProtectedSpaceAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    זכוכית</td>
                <td>
                    <asp:DropDownList ID="GlassCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="GlassProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="GlassAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    ארגזים</td>
                <td>
                    <asp:DropDownList ID="BoxesCount" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="BoxesProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="BoxAmount" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="Button1" runat="server" Text="שמור" />
    <br />
</asp:Content>
