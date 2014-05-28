<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="HomeTechnical.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div id="HomeContainer">
        <div id="NewsBox" class="panel panel-default">
            <div class="panel-heading">
                <span class="glyphicon glyphicon-list-alt"></span>&nbsp;&nbsp;<b>חדשות</b></div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-12">
                        <ul class="News">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="panel-footer">
            </div>
        </div>
        <asp:GridView ID="WorstSuppliersGV" CssClass="DataTables" runat="server">
        </asp:GridView>
        <asp:GridView ID="BestSuppliersGV" CssClass="DataTables" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
