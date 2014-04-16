<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="NewServiceCallExistingProject.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="cntr">
        <h1>
            קריאת שירות - פרויקט קיים
        </h1>
    </div>
    <br />
    <table id="NewSCExistingProjectTBL" class="table">
        <tr>
            <td>
                <asp:SqlDataSource ID="SCExisting_CustomerNameDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
                    SelectCommand="spGetRegion" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <div class="cntr">
                    <asp:DropDownList ID="SCExisting_CustomerName" runat="server" DataSourceID="SCExisting_CustomerNameDataSource"
                        CssClass="btn btn-default" DataTextField="RegionName" DataValueField="RegionID">
                    </asp:DropDownList>
                </div>
            </td>
            <td>
                <div class="input-group">
                    <input id="SCExisting_DateOpened" type="text" class="form-control datepicker" runat="server">
                    <span class="input-group-addon">תאריך פתיחה</span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="TextAreaHolder">
                    <textarea id="SCExisting_ProblemDescription" runat="server" class="form-control"
                        rows="5" cols="5" placeholder="תיאור התקלה"></textarea>
                </div>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <div class="cntr">
        <input type="button" value="צור קריאה" class="btn btn-default" />
        <asp:Button ID="CreateServiceCallBTN" runat="server" Text="צור קריאה" CssClass="btn btn-default HiddenButtons"
            Font-Bold="true" />
        <br />
        <br />
        <span class="ErrorLabel"></span>
    </div>
</asp:Content>
