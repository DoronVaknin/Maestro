<%@ Page Title="" Language="C#" MasterPageFile="~/MaestroMaster.master" AutoEventWireup="true"
    CodeFile="HomeBackup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <%--    <div class="cntr">
        <h1>
            מפת פרויקטים וקריאות שירות</h1>
    </div>--%>
    <div id="HomeContainer">
        <%--        <div id="map-canvas">
        </div>--%>
        <div id="NewsBox" class="panel panel-default">
            <div class="panel-heading">
                <span class="glyphicon glyphicon-list-alt"></span>&nbsp;&nbsp;<b>חדשות</b></div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-12">
                        <ul class="News">
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/1.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            לורם איפסום דולור סיט אמט, קונסקטורר אדיפיסינג אלית ושבעגט ליבם סולגק. בראיט ולחת
                                            צורק מונחף, בגורמי מגמש. תרבנך וסתעד לכנו סתשם השמה - לתכי מורגם בורק? לתיג ישבעס.
                                            <a href="#">קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/2.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            הועניב היושבב שערש שמחויט - שלושע ותלברו חשלו שעותלשך וחאית נובש ערששף. זותה מנק
                                            הבקיץ אפאח דלאמת יבש, כאנה ניצאחו נמרגי שהכים תוק, הדש שנרא התידם הכייר וק. <a href="#">
                                                קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/3.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            לפרומי בלוף קינץ תתיח לרעח. לת צשחמי צש בליא, מנסוטו צמלח לביקו ננבי, צמוקו בלוקריה
                                            שיצמה ברורק. קונדימנטום קורוס בליקרה, נונסטי קלובר בריקנה סטום, לפריקך תצטריק לרטי.
                                            <a href="#">קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li class="news-item">
                                <table cellpadding="4">
                                    <tr>
                                        <td>
                                            <img src="images/3.png" width="60" class="img-circle" />
                                        </td>
                                        <td>
                                            לורם איפסום דולור סיט אמט, קונסקטורר אדיפיסינג אלית. סת אלמנקום ניסי נון ניבאה.
                                            דס איאקוליס וולופטה דיאם. וסטיבולום אט דולור, קראס אגת לקטוס וואל אאוגו וסטיבולום
                                            סוליסי טידום בעליק. קונדימנטום קורוס בליקרה, נונסטי קלובר בריקנה סטום, לפריקך תצטריק
                                            לרטי. <a href="#">קרא עוד...</a>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="panel-footer">
            </div>
        </div>
        <asp:GridView ID="SuppliersRankGV" CssClass = "DataTables" runat="server">
        </asp:GridView>
        <%--        <asp:SqlDataSource ID="PriceOfferDS" runat="server" ConnectionString="<%$ ConnectionStrings:igroup9_prodConnectionString %>"
            SelectCommand="spGetUndecidedCustomers" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <asp:GridView ID="PriceOfferGV" CssClass="DataTables" runat="server" AutoGenerateColumns="False"
            DataSourceID="PriceOfferDS" AllowPaging="True" PageSize = "3">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText = "בחר" />
                <asp:BoundField DataField="pName" HeaderText="שם פרויקט" SortExpression="pName" />
                <asp:BoundField DataField="Mobile" HeaderText="טלפון נייד" SortExpression="Mobile" />
                <asp:BoundField DataField="Comments" HeaderText="הערות" SortExpression="Comments" />
                <asp:BoundField DataField="InstallationDate" HeaderText="תאריך חזרה ללקוח" SortExpression="InstallationDate"
                    DataFormatString="{0:dd/MM/yyyy}" />
            </Columns>
        </asp:GridView>
        <div id="PieChart" style="height: 250px;">
        </div>--%>
    </div>
</asp:Content>
