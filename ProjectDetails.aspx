<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="ProjectDetails.aspx.cs" Inherits="Default2" EnableEventValidation="false" %>

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
                        <input id="ProjectInfoID" type="text" class="form-control" runat="server" maxlength = "9">
                        <span class="input-group-addon">ת.ז</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoAddress" type="text" class="form-control" runat="server" maxlength = "30">
                        <span class="input-group-addon">כתובת</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoFirstName" type="text" class="form-control" runat="server" maxlength = "15">
                        <span class="input-group-addon">שם פרטי</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoCity" type="text" class="form-control" runat="server" maxlength = "15">
                        <span class="input-group-addon">עיר</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoLastName" type="text" class="form-control" runat="server" maxlength = "15">
                        <span class="input-group-addon">שם משפחה</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoEmail" type="text" class="form-control" runat="server">
                        <span class="input-group-addon">דוא"ל</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoPhone" type="text" class="form-control" runat="server" maxlength = "10">
                        <span class="input-group-addon">טלפון</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoFax" type="text" class="form-control" runat="server" maxlength = "10">
                        <span class="input-group-addon">פקס</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoMobile" type="text" class="form-control" runat="server" maxlength = "10">
                        <span class="input-group-addon">טלפון נייד</span>
                    </div>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoArchitectName" type="text" class="form-control" runat="server" maxlength = "15">
                        <span class="input-group-addon">שם האדריכל</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoArchitectMobile" type="text" class="form-control" runat="server" maxlength = "10">
                        <span class="input-group-addon">טלפון אדריכל</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoContractorName" type="text" class="form-control" runat="server" maxlength = "15">
                        <span class="input-group-addon">שם הקבלן</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoContractorPhone" type="text" class="form-control" runat="server" maxlength = "10">
                        <span class="input-group-addon">טלפון קבלן</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoSupervisorName" type="text" class="form-control" runat="server" maxlength = "15">
                        <span class="input-group-addon">שם המפקח</span>
                    </div>
                </td>
                <td>
                    <div class="input-group">
                        <input id="ProjectInfoSupervisorPhone" type="text" class="form-control" runat="server" maxlength = "10">
                        <span class="input-group-addon">טלפון מפקח</span>
                    </div>
                </td>
            </tr>
        </table>
    <div class="cntr">
        <button id="EditCustomerDetailsBTN" runat="server" type="button" class="btn btn-default"
            onclick="EnableCustomerDetails()">
            ערוך&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <asp:Button ID="SaveCustomerDetails" runat="server" Text="שמור" class="btn btn-default"
            OnClick="SaveCustomerDetails_Click1" Font-Bold="true" />
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
                <asp:DropDownList ID="ProjectInfoStatus" runat="server" CssClass="btn btn-default"
                    DataSourceID="SqlDataSource1" DataTextField="psName" DataValueField="psName"
                    OnDataBinding="DropDownDataBound" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td>
                <div class="input-group">
                    <input id="ProjectInfoPrice" type="text" class="form-control" runat="server">
                    <span class="input-group-addon">סה"כ עלות</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <input id="ProjectInfoHatches" type="text" class="form-control" runat="server" maxlength = "2">
                    <span class="input-group-addon">מס' פתחים</span>
                </div>
            </td>
            <td>
                <div id="CommentContainer">
                    <textarea id="ProjectInfoComments" runat="server" class="form-control" cols="10" rows="3"
                        placeholder="הערות"></textarea>
                </div>
            </td>
        </tr>
    </table>
    <div class="cntr">
        <button id="EditProjectDetailsBTN" runat="server" type="button" class="btn btn-default" onclick="EnableProjectDetails()">
            ערוך&nbsp;<span class="glyphicon glyphicon-pencil"></span>
        </button>
        <asp:Button ID="SaveProjectDetails" runat="server" Text="שמור" class="btn btn-default"
            OnClick="SaveProjectDetails_Click" Font-Bold="true" />
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
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    תריסים</td>
                <td>
                    <asp:TextBox ID="ShutterCount" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ShutterProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    נאספים</td>
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
                    וואלים</td>
                <td class="style1">
                    <asp:TextBox ID="ValimCount" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ValimProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td class="style1">
                    U</td>
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
                    פרזול</td>
                <td>
                    <asp:TextBox ID="ShoeingCount" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ShoeingProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    מנועים</td>
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
                    ממ&quot;ד</td>
                <td>
                    <asp:TextBox ID="ProtectedSpaceCount" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ProtectedSpaceProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    זכוכית</td>
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
                    ארגזים</td>
                <td>
                    <asp:TextBox ID="BoxCount" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="BoxesProvider" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    </div>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="צור  הזמנה" />
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:igroup9_test1ConnectionString %>" 
        SelectCommand="spGetOrderStatus" SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <br />
    הזמנות עבור פרויקט זה:<br />
    <asp:GridView ID="OrdersGrid" runat="server" AutoGenerateColumns="False"  >
     <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="DateOpened" HeaderText="תאריך" 
                    InsertVisible="False" ReadOnly="True" SortExpression="DateOpened" />
            <asp:BoundField DataField="rName" HeaderText="חומר גלם" 
                    SortExpression="rName" />
            <asp:BoundField DataField="DateOfArrival" HeaderText="תאריך הגעה" 
                    SortExpression="DateOfArrival" />
            <asp:BoundField DataField="Quantity" HeaderText="כמות" 
                    SortExpression="Quantity" />

            <asp:BoundField DataField="sName" HeaderText="שם הספק" 
                    SortExpression="sName" />
            <asp:TemplateField HeaderText="Status" SortExpression="Status">
            <EditItemTemplate>
            
            <asp:DropDownList ID="DropDownList1" runat="server" 
                            DataSourceID="SqlDataSource2" DataTextField="osName" DataValueField="osName" 
                            SelectedValue='<%# Bind("osName") %>'>
           </asp:DropDownList>

            </EditItemTemplate>



            </asp:TemplateField>

                    </Columns>
    </asp:GridView>
    </asp:Content>
