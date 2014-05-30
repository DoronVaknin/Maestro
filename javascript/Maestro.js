/// <reference path="jquery-1.11.0.js" />

//Used for Forms backup
var aCustomerDetails = [];
var aProjectDetails = [];

//Used for google maps
var Projects = {};
var ServiceCallsList = {};  //ServiceCallsList[scID][0] - Service call details, ServiceCallsList[scID][1] - Customer details, ServiceCallsList[scID][2] - Project details
var iTimeoutMin = 90;

//Used for Pie Chart
var ProjectsIncome = {};

//Used for Notifications
var Notifications = {};

$(document).ready(function () {
    WireHomeButtons();
    ActivateTabsMarking();
    ActivateToolbarButton("ToolbarBtnCreateProject", "NewCustomer", "CreateProject");
    ActivateToolbarButton("ToolbarBtnCreateServiceCallExternalCustomer", "NewCustomer", "CreateServiceCall");
    ActivateDatepicker();
    ActivateQuickSearch();
    ActivateServiceCallExistingProjectModal();
    var sPageName = GetPageName();
    switch (sPageName) {

        case "ProjectDetails".toLowerCase():
            DisableCustomerDetailsFields();
            DisableProjectDetailsFields();
            FixTextAreaIssue("ProjectDetailsTBL");
            ModifyInstallationDateField();
            $("#ProjectDetailsStatusIcon").popover({ html: true, content: GetProgressBarContent() });
            ActivateGoogleAutoCompletion("ContentPlaceHolder3_ProjectInfoAddress");
            break;

        case "ProjectOrders".toLowerCase():
            ActivatePlusMinus();
            break;

        case "NewProject.aspx?Source=NewCustomer".toLowerCase():
            ActivateModal("ModalCustomerCreated");
            ActivateDragAndDrop();
            break;

        case "NewProject.aspx?Source=ProjectsPerCustomer".toLowerCase():
            ActivateDragAndDrop();
            break;

        case "NewCustomer".toLowerCase():
            ActivateGoogleAutoCompletion("ContentPlaceHolder3_CustomerAddress");
            break;

        case "NewCustomer.aspx?Source=CreateProject".toLowerCase():
            $("#CustomerForServiceCallBTN").addClass("HiddenButtons");
            ActivateGoogleAutoCompletion("ContentPlaceHolder3_CustomerAddress");
            break;

        case "NewCustomer.aspx?Source=CreateServiceCall".toLowerCase():
            $("#CustomerForProjectBTN").addClass("HiddenButtons");
            ActivateGoogleAutoCompletion("ContentPlaceHolder3_CustomerAddress");
            break;

        case "NewSupplier".toLowerCase():
            ActivateGoogleAutoCompletion("ContentPlaceHolder3_SupplierAddress");
            break;

        case "HomeSales".toLowerCase():
            ResizeHomeContainer();
            ResizePriceOfferTable();
            GetProjectsIncome();
            SetupPieChart();
            ActivateNewsBox();
            break;

        case "HomeInstallations".toLowerCase():
            ResizeHomeContainer();
            InitializeGoogleMap();
            GetProjects();
            GetOpenedServiceCalls();
            PopulateGoogleMap();
            GetNotifications(38124123);
            ActivateNewsBox();
            break;

        case "HomeTechnical".toLowerCase():
            ResizeHomeContainer();
            GetNotifications(302042267);
            ActivateNewsBox();
            break;

        default: break;
    }
});

$(window).resize(function () {
    ResizeHomeContainer();
    ResizeNewsBox();
});

function WireHomeButtons() {
    var sUserName = $("#UserNameHolder").val();

    var bAdmin = sUserName.toUpperCase() === "Admin".toUpperCase();
    var bInstallationsManager = sUserName.toUpperCase() === "ShimonY".toUpperCase();
    var bSalesManager = sUserName.toUpperCase() === "MaliY".toUpperCase();
    var bTechnicalManager = sUserName.toUpperCase() === "BettiY".toUpperCase();

    if (bAdmin || bInstallationsManager)
        $("#Home a, #LogoWrapper").attr("href", "HomeInstallations.aspx");
    else if (bSalesManager)
        $("#Home a, #LogoWrapper").attr("href", "HomeSales.aspx");
    else if (bTechnicalManager)
        $("#Home a, #LogoWrapper").attr("href", "HomeTechnical.aspx");
    else
        $("#Home a, #LogoWrapper").attr("href", "HomeInstallations.aspx");
}

//Pie Chart
function SetupPieChart() {
    function GetRandomColor() {
        var letters = '0123456789ABCDEF'.split('');
        var sColor = '#';
        for (var i = 0; i < 6; i++) {
            sColor += letters[Math.floor(Math.random() * 16)];
        }
        return sColor;
    }
    var dTotalIncome = 0;
    for (var i in ProjectsIncome) {
        dTotalIncome += ProjectsIncome[i].Cost;
    }
    var aData = [];
    var aColors = [];
    for (var j in ProjectsIncome) {
        var Project = {};
        Project.label = ProjectsIncome[j].Name;
        Project.value = (100 * ProjectsIncome[j].Cost / dTotalIncome).toFixed(2);
        aData.push(Project);
        var sColor = GetRandomColor();
        aColors.push(sColor);
    }

    Morris.Donut({
        element: 'PieChart',
        data: aData,
        colors: aColors
    });
}

//News Box
function ActivateNewsBox() {
    $(".News").bootstrapNews({
        newsPerPage: 4,
        navigation: true,
        autoplay: true,
        direction: 'up', // up or down
        animationSpeed: 'normal',
        newsTickerInterval: 4000, //4 secs
        pauseOnHover: true,
        onStop: null,
        onPause: null,
        onReset: null,
        onPrev: null,
        onNext: null,
        onToDo: null
    });
    ResizeNewsBox();
}

function ResizeNewsBox() {
    $(".News").height("inherit");
}

//Undecided customers gridview
function ResizePriceOfferTable() {
    $("#PriceOfferContainer").width("70%");
}

//Suppliers ranks
function BuildLateProgressBar(dPercent, iIndex) {
    function GetRelevantDesc() {
        return dPercent >= 50 ? 'danger' : 'warning';
    }
    var sHTML = "";
    sHTML += '<div class="progress">';
    sHTML += '<div class="progress-bar progress-bar-' + GetRelevantDesc() + '" role="progressbar" aria-valuenow="' + dPercent + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + dPercent + '%">';
    //    sHTML += '<span class="sr-only">' + dPercent + '% Complete</span>';
    sHTML += dPercent > 15 ? dPercent + "%" : "";
    sHTML += '</div>';
    var Cells = $("#ContentPlaceHolder3_WorstSuppliersGV td:nth-child(3)");
    var Cell = Cells[iIndex];
    $(Cell).html(sHTML);
}

function BuildEarlyProgressBar(dPercent, iIndex) {
    function GetRelevantDesc() {
        return dPercent >= 50 ? 'success' : 'info';
    }
    var sHTML = "";
    sHTML += '<div class="progress">';
    sHTML += '<div class="progress-bar progress-bar-' + GetRelevantDesc() + '" role="progressbar" aria-valuenow="' + dPercent + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + dPercent + '%">';
    //    sHTML += '<span class="sr-only">' + dPercent + '% Complete</span>';
    sHTML += dPercent > 15 ? dPercent + "%" : "";
    sHTML += '</div>';
    var Cells = $("#ContentPlaceHolder3_BestSuppliersGV td:nth-child(3)");
    var Cell = Cells[iIndex];
    $(Cell).html(sHTML);
}

//Mark current tabs
function ActivateTabsMarking() {
    var sPageName = GetPageName();
    switch (sPageName) {
        case "HomeSales".toLowerCase():
        case "HomeTechnical".toLowerCase():
        case "HomeInstallations".toLowerCase():
            $("#Home").addClass("current");
            break;

        case "NewCustomer".toLowerCase():
        case "Customers".toLowerCase():
            $("#Customers").addClass("current");
            break;

        case "NewCustomer.aspx?Source=CreateProject".toLowerCase():
        case "NewProject.aspx?Source=NewCustomer".toLowerCase():
        case "Projects".toLowerCase():
        case "ProjectsArchive".toLowerCase():
            $("#Projects").addClass("current");
            break;

        case "ProjectOrders".toLowerCase():
        case "SupplierOrders".toLowerCase():
        case "NewSupplier".toLowerCase():
        case "Suppliers".toLowerCase():
        case "ProjectHatches".toLowerCase():
            $("#Technical").addClass("current");
            break;

        case "NewServiceCall".toLowerCase():
        case "NewCustomer.aspx?Source=CreateServiceCall".toLowerCase():
        case "NewServiceCall.aspx?Source=ExistingProject".toLowerCase():
        case "ServiceCalls".toLowerCase():
        case "ServiceCallsArchive".toLowerCase():
            $("#ServiceCalls").addClass("current");
            break;

        default: break;
    }
}

//Toolbar buttons
function ActivateToolbarButton(sID, sPage, sSource) {
    $("#" + sID).click(function () {
        window.location.replace(sPage + ".aspx?Source=" + sSource);
    });
}

//Datepicker activation
function ActivateDatepicker() {
    $(".datepicker").datepicker();
}

//Quick search activation
function ActivateQuickSearch() {
    $('.search_textbox').each(function (i) {
        $(this).quicksearch("[class*=DataTables] tr:not(:has(th))", {
            'testQuery': function (query, txt, row) {
                return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
            }
        });
    });
}

//Progress Bar
function GetProgressBarContent() {
    var iProjectStatusID = $("#ContentPlaceHolder3_ProjectInfoStatus")[0].selectedIndex + 1;
    var iPercent = parseInt(100 * iProjectStatusID / 9);
    var str = "";
    str += '<div class="progress">';
    str += '<div class="progress-bar" role="progressbar" aria-valuenow="' + iPercent + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + iPercent + '%; min-width:30px;">';
    str += '<span>' + iPercent + '%' + '</span>';
    str += '</div>';
    str += '</div>';
    return str;
}

//Modals Activation
function ActivateModal(sID, sContent, sModalToHide) {
    if (!IsEmpty(sModalToHide)) {
        $("#" + sModalToHide).modal('hide');
        setTimeout($("#" + sID).modal(), 1500);
    }
    else $("#" + sID).modal();
    if (!IsEmpty(sContent))
        $("#" + sID + " .modal-body").html(sContent);
}

function ActivateCloseServiceCallDialog() {
    var sFirstName = $("#ContentPlaceHolder3_ServiceCallFirstName").val();
    var sLastName = $("#ContentPlaceHolder3_ServiceCallLastName").val();
    $("#CloseServiceCallDialogModal .modal-body .QuestionContainer").html("האם אתה בטוח שברצונך לסגור את קריאת השירות של הלקוח " + sFirstName + " " + sLastName + "?");
    ActivateModal('CloseServiceCallDialogModal');
}

function CloseServiceCall() {
    $("#ContentPlaceHolder3_CloseServiceCallHiddenBTN").click();
}

function ActivateServiceCallExistingProjectModal() {
    $(".ChooseProjectToolbarButtons").click(function () {
        var sButtonID = this.id;
        switch (sButtonID) {
            case "ToolbarBtnCreateServiceCallExistingProject":
                $("#ModalChooseProject .modal-title").html("קריאת שירות - פרויקט קיים");
                $("#ModalChooseProject .modal-body input[type=submit], #SupplierNamesDDL").hide();
                $("#ChooseProjectForServiceCallBTN, #ProjectNamesDDL").show();
                break;

            case "ToolbarBtnProjectOrders":
                $("#ModalChooseProject .modal-title").html("הזמנות עבור פרויקט");
                $("#ModalChooseProject .modal-body input[type=submit], #SupplierNamesDDL").hide();
                $("#ChooseProjectForProjectOrdersBTN, #ProjectNamesDDL").show();
                break;

            case "ToolbarBtnProjectHatches":
                $("#ModalChooseProject .modal-title").html("פתחים עבור פרויקט");
                $("#ModalChooseProject .modal-body input[type=submit], #SupplierNamesDDL").hide();
                $("#ChooseProjectForProjectHatchesBTN, #ProjectNamesDDL").show();
                break;

            case "ToolbarBtnSupplierOrders":
                $("#ModalChooseProject .modal-title").html("הזמנות עבור ספק");
                $("#ModalChooseProject .modal-body input[type=submit], #ProjectNamesDDL").hide();
                $("#ChooseSupplierForSupplierOrdersBTN, #SupplierNamesDDL").show();
                break;
        }
        ActivateModal("ModalChooseProject");
    });
}

function ActivatePlusMinus() {
    $("#ProjectOrdersTBL .plus").click(function () {
        var TextBox = $(this).prev();
        var iValue = parseInt(TextBox.val());
        TextBox.val(iValue + 1);
    });
    $("#ProjectOrdersTBL .minus").click(function () {
        var TextBox = $(this).next();
        var iValue = parseInt(TextBox.val());
        if (iValue == "0")
            return;
        else if (iValue - 1 < 0)
            TextBox.val(0);
        else
            TextBox.val(iValue - 1);
    });
}

function ResetCreateOrderForm() {
    $("#ProjectOrdersTBL input.Count").val("0");
    $("#ProjectOrdersTBL input.datepicker").val("");
}

//Default state for project fields is disabled
function DisableCustomerDetailsFields() {
    $("#CustomerDetailsTBL input, #CustomerDetailsTBL select").attr("disabled", "disabled");
}

function DisableProjectDetailsFields() {
    $("#ProjectDetailsTBL input, #ProjectDetailsTBL textarea, #ProjectDetailsTBL select").attr("disabled", "disabled");
}

function DisableUndecidedCustomerDetailsFields() {
    $("#EditUndecidedCustomerTBL input, #EditUndecidedCustomerTBL textarea, #EditUndecidedCustomerTBL select").attr("disabled", "disabled");
}

function DisableServiceCallDetailsFields() {
    $("#ServiceCallTBL input, #ServiceCallTBL textarea").attr("disabled", "disabled");
}

function DisableOrderDetailsFields(sName) {
    $("#Edit" + sName + "OrderTBL input, #Edit" + sName + "OrderTBL select").attr("disabled", "disabled");
}

function DisableSupplierDetailsFields() {
    $("#EditSupplierTBL input").attr("disabled", "disabled");
}

function DisableHatchDetailsFields() {
    $("#EditHatchTBL select, #EditHatchTBL textarea").attr("disabled", "disabled");
}

function FixTextAreaIssue(sTableID) {
    var sValue = $("#" + sTableID + " textarea").val();
    if (sValue == "&nbsp;")
        $("#" + sTableID + " textarea").val("");
}

function ModifyInstallationDateField() {
    var psID = $("#ContentPlaceHolder3_ProjectInfoStatus").val();
    if (psID == "1")
        $("#ContentPlaceHolder3_ProjectInfoInstallationDate").next().text("תאריך חזרה ללקוח");
    else if (parseInt(psID) < 6) // between 2 to 5 - installation field in not relevant
        $("#ContentPlaceHolder3_ProjectInfoInstallationDate").parent().hide();
}

function ClickLoginBTN() {
    $("#LoginBTN").click();
}

//Edit, Save & Cancel buttons
function EnableCustomerDetails() {
    $("#CustomerDetailsTBL input, #CustomerDetailsTBL select").removeAttr("disabled");
    $("#ContentPlaceHolder3_ProjectInfoID").attr("disabled", "disabled");
    SwitchEditSaveButtons(false, "Customer");
    BackupCustomerDetails();
}

function SwitchEditSaveButtons(bShowEditButton, sName) {
    $("#ContentPlaceHolder3_Edit" + sName + "DetailsBTN").toggle(bShowEditButton);
    $("#ContentPlaceHolder3_Save" + sName + "DetailsBTN, #ContentPlaceHolder3_Cancel" + sName + "DetailsBTN").toggle(!bShowEditButton);
}

function EnableProjectDetails() {
    $("#ProjectDetailsTBL *").removeAttr("disabled");
    $("#ContentPlaceHolder3_ProjectInfoHatches").attr("disabled", "disabled");
    $("#ContentPlaceHolder3_ProjectInfoDateOpened").attr("disabled", "disabled");
    SwitchEditSaveButtons(false, "Project");
    BackupProjectDetails();
}

function RestoreCustomerDetails() {
    $("#ContentPlaceHolder3_ProjectInfoFirstName").val(aCustomerDetails[0]);
    $("#ContentPlaceHolder3_ProjectInfoLastName").val(aCustomerDetails[1]);
    $("#ContentPlaceHolder3_ProjectInfoPhone").val(aCustomerDetails[2]);
    $("#ContentPlaceHolder3_ProjectInfoMobile").val(aCustomerDetails[3]);
    $("#ContentPlaceHolder3_ProjectInfoAddress").val(aCustomerDetails[4]);
    $("#ContentPlaceHolder3_ProjectInfoEmail").val(aCustomerDetails[5]);
    $("#ContentPlaceHolder3_ProjectInfoFax").val(aCustomerDetails[6]);
    $("#ContentPlaceHolder3_ProjectInfoArea").val(aCustomerDetails[7]);

    DisableCustomerDetailsFields();
    SwitchEditSaveButtons(true, "Customer");
    ClearInvalidFields("#CustomerDetailsTBL");
}

function RestoreProjectDetails() {
    $("#ContentPlaceHolder3_ProjectInfoStatus").val(aProjectDetails[0]);
    $("#ContentPlaceHolder3_ProjectInfoName").val(aProjectDetails[1]);
    $("#ContentPlaceHolder3_ProjectInfoCost").val(aProjectDetails[2]);
    $("#ContentPlaceHolder3_ProjectInfoExpirationDate").val(aProjectDetails[3]);
    $("#ContentPlaceHolder3_ProjectInfoInstallationDate").val(aProjectDetails[4]);
    $("#ContentPlaceHolder3_ProjectInfoComments").val(aProjectDetails[5]);
    $("#ContentPlaceHolder3_ProjectInfoArchitectName").val(aProjectDetails[6]);
    $("#ContentPlaceHolder3_ProjectInfoContractorName").val(aProjectDetails[7]);
    $("#ContentPlaceHolder3_ProjectInfoSupervisorName").val(aProjectDetails[8]);
    $("#ContentPlaceHolder3_ProjectInfoArchitectMobile").val(aProjectDetails[9]);
    $("#ContentPlaceHolder3_ProjectInfoContractorMobile").val(aProjectDetails[10]);
    $("#ContentPlaceHolder3_ProjectInfoSupervisorMobile").val(aProjectDetails[11]);

    DisableProjectDetailsFields();
    SwitchEditSaveButtons(true, "Project");
    ClearInvalidFields("#ProjectDetailsTBL");
}

function EnableUndecidedCustomerDetails() {
    $("#EditUndecidedCustomerTBL input, #EditUndecidedCustomerTBL textarea, #EditUndecidedCustomerTBL select").removeAttr("disabled");
    SwitchEditSaveButtons(false, "UndecidedCustomer");
    BackupUndecidedCustomerDetails();
}

function RestoreUndecidedCustomerDetails() {
    $("#ContentPlaceHolder3_ProjectName").val(aUndecidedCustomerDetails[0]);
    $("#ContentPlaceHolder3_CustomerMobilePhone").val(aUndecidedCustomerDetails[1]);
    $("#ContentPlaceHolder3_ProjectComments").val(aUndecidedCustomerDetails[2]);
    $("#ContentPlaceHolder3_CustomerBackDate").val(aUndecidedCustomerDetails[3]);
    $("#ContentPlaceHolder3_ProjectStatus").val(aUndecidedCustomerDetails[4]);

    DisableUndecidedCustomerDetailsFields();
    SwitchEditSaveButtons(true, "UndecidedCustomer");
    ClearInvalidFields("#EditUndecidedCustomerTBL");
}

function EnableServiceCallDetails() {
    $("#ContentPlaceHolder3_ServiceCallProblemDesc, #ContentPlaceHolder3_ServiceCallUrgent").removeAttr("disabled");
    SwitchEditSaveButtons(false, "ServiceCall");
    BackupServiceCallDetails();
}

function RestoreServiceCallDetails() {
    $("#ContentPlaceHolder3_ServiceCallProblemDesc").val(aServiceCallDetails[0]);
    var bUrgent = aServiceCallDetails[1] == "on" ? false : true;
    $("#ContentPlaceHolder3_ServiceCallUrgent").prop('checked', bUrgent);
    DisableServiceCallDetailsFields();
    SwitchEditSaveButtons(true, "ServiceCall");
    ClearInvalidFields("#ServiceCallTBL");
}

function EnableOrderDetails(sName) {
    $("#ContentPlaceHolder3_" + sName + "OrderQuantity, #ContentPlaceHolder3_" + sName + "OrderStatus, #ContentPlaceHolder3_" + sName + "OrderEstimatedDOA").removeAttr("disabled");
    SwitchEditSaveButtons(false, "Order");
    BackupOrderDetails(sName);
}

function RestoreOrderDetails(sName) {
    $("#ContentPlaceHolder3_" + sName + "OrderQuantity").val(aOrderDetails[0]);
    $("#ContentPlaceHolder3_" + sName + "OrderStatus").val(aOrderDetails[1]);
    $("#ContentPlaceHolder3_" + sName + "OrderEstimatedDOA").val(aOrderDetails[2]);
    DisableOrderDetailsFields(sName);
    SwitchEditSaveButtons(true, "Order");
    ClearInvalidFields("#Edit" + sName + "OrderTBL");
}

function EnableSupplierDetails() {
    $("#EditSupplierTBL input").removeAttr("disabled");
    SwitchEditSaveButtons(false, "Supplier");
    BackupSupplierDetails();
}

function RestoreSupplierDetails() {
    $("#ContentPlaceHolder3_SupplierName").val(aSupplierDetails[0]);
    $("#ContentPlaceHolder3_SupplierAddress").val(aSupplierDetails[1]);
    $("#ContentPlaceHolder3_SupplierEmail").val(aSupplierDetails[2]);
    $("#ContentPlaceHolder3_SupplierPhone").val(aSupplierDetails[3]);
    $("#ContentPlaceHolder3_SupplierCellPhone").val(aSupplierDetails[4]);
    $("#ContentPlaceHolder3_SupplierFax").val(aSupplierDetails[5]);
    DisableSupplierDetailsFields();
    SwitchEditSaveButtons(true, "Supplier");
    ClearInvalidFields("#EditSupplierTBL");
}

function EnableHatchDetails() {
    $("#EditHatchTBL select, #EditHatchTBL textarea").removeAttr("disabled");
    SwitchEditSaveButtons(false, "Hatch");
    BackupHatchDetails();
}

function RestoreHatchDetails() {
    $("#ContentPlaceHolder3_HatchType").val(aHatchDetails[0]);
    $("#ContentPlaceHolder3_HatchStatus").val(aHatchDetails[1]);
    $("#ContentPlaceHolder3_HatchFailureType").val(aHatchDetails[2]);
    $("#ContentPlaceHolder3_HatchComments").val(aHatchDetails[3]);
    DisableHatchDetailsFields();
    SwitchEditSaveButtons(true, "Hatch");
    ClearInvalidFields("#EditHatchTBL");
}

//Gridview Update
function UpdateSupplierOrder(iRowIndex, sEstimatedDOA, sQuantity, sOrderStatus) {
    $('#ContentPlaceHolder3_SupplierOrdersGV tr:nth(' + iRowIndex + ') td:nth(4)').text(sEstimatedDOA);
    $('#ContentPlaceHolder3_SupplierOrdersGV tr:nth(' + iRowIndex + ') td:nth(5)').text(sQuantity);
    $('#ContentPlaceHolder3_SupplierOrdersGV tr:nth(' + iRowIndex + ') td:nth(7)').text(sOrderStatus);
}

//Navigation
function Goto(sPage, sQuery) {
    window.location = sPage + ".aspx" + (!IsEmpty(sQuery) ? sQuery : "");
}

function GotoProjectOrders() {
    $("#ContentPlaceHolder3_GotoProjectOrdersHiddenBTN").click();
}

function GotoOpenServiceCall() {
    $("#ContentPlaceHolder3_OpenServiceCallHiddenBTN").click();
}

function CreateOrder() {
    $("#ContentPlaceHolder3_CreateOrderHiddenBTN").click();
}

//Validators
function ValidateNewCustomer(Button) {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerId", function (s) { return s.length < 8 || !isInteger(s); }, false, "יש להזין מספר ת.ז תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerFirstName", function (s) { return s.length < 2; }, false, "השם הפרטי קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerLastName", function (s) { return s.length < 2; }, false, "שם המשפחה קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerAddress", function (s) { return s.length < 2; }, false, "כתובת המגורים קצרה מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerPhone", function (s) { return s.length > 0 && !isValidPhoneNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerCellPhone", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerFaxNumber", function (s) { return s.length > 0 && !isValidPhoneNumber(s); }, false, "יש להזין מס' פקס תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerEmail", function (s) { return !IsEmail(s); }, false, "יש להזין כתובת מייל חוקית");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        if (Button.id == "CustomerForProjectBTN")
            $("#ContentPlaceHolder3_CreateCustomerForProject").click();
        else
            $("#ContentPlaceHolder3_CreateCustomerForServiceCall").click();
    }
}

function ValidateNewProject() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectName", function (s) { return s.length < 2; }, false, "שם הפרויקט קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectDateOpened", function (s) { return !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectCost", function (s) { return !isNumber(s); }, false, "יש להזין מחיר חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectHatches", function (s) { return !isInteger(s); }, false, "יש להזין מספר פתחים שלם");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectContractorMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectArchitectMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectSupervisorMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        $("#ContentPlaceHolder3_CreateProject").click();
    }
}

function ValidateSupplierDetails(button) {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierName", function (s) { return s.length < 2; }, false, "השם הפרטי קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierAddress", function (s) { return s.length < 2; }, false, "כתובת המגורים קצרה מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierPhone", function (s) { return !isValidPhoneNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierCellPhone", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierFax", function (s) { return s.length > 0 && !isValidPhoneNumber(s); }, false, "יש להזין מס' פקס תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierEmail", function (s) { return s.length > 0 && !IsEmail(s); }, false, "יש להזין כתובת מייל חוקית");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        if (button.id == "ContentPlaceHolder3_CreateSupplierBTN")
            $("#ContentPlaceHolder3_CreateSupplierHiddenBTN").click();
        else {
            $("#SupplierDetailsErrorLabel").html("");
            SwitchEditSaveButtons(true, "Supplier");
            DisableSupplierDetailsFields();
            $("#ContentPlaceHolder3_SaveSupplierDetailsHiddenBTN").click();
        }
    }
}

function ValidateUndecidedCustomerDetails() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectName", function (s) { return s.length < 2; }, false, "שם הפרויקט קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerMobilePhone", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerBackDate", function (s) { return s.length > 0 && !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        ClearInvalidFields("#EditUndecidedCustomerTBL");
        SwitchEditSaveButtons(true, "UndecidedCustomer");
        DisableUndecidedCustomerDetailsFields();
        $("#ContentPlaceHolder3_SaveUndecidedCustomerDetailsHiddenBTN").click();
    }
}

function ValidateServiceCallExternal() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ServiceCallDateOpened", function (s) { return !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ServiceCallProblemDesc", function (s) { return s.length < 5; }, false, "תיאור התקלה קצר מדי");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        $("#ContentPlaceHolder3_CreateServiceCallExternal").click();
    }
}

function ValidateCustomerDetails() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoFirstName", function (s) { return s.length < 2; }, false, "השם הפרטי קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoLastName", function (s) { return s.length < 2; }, false, "שם המשפחה קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoAddress", function (s) { return s.length < 2; }, false, "כתובת המגורים קצרה מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoPhone", function (s) { return s.length > 0 && !isValidPhoneNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoFax", function (s) { return s.length > 0 && !isValidPhoneNumber(s); }, false, "יש להזין מס' פקס תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoEmail", function (s) { return !IsEmail(s); }, false, "יש להזין כתובת מייל חוקית");
    if (bIsValid) {
        $("#CustomerDetailsErrorLabel").html("");
        SwitchEditSaveButtons(true, "Customer");
        DisableCustomerDetailsFields();
        $("#ContentPlaceHolder3_SaveCustomerDetailsHiddenBTN").click();
    }
}

function ValidateProjectDetails() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoName", function (s) { return s.length < 2; }, false, "שם הפרויקט קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoExpirationDate", function (s) { return s.length > 0 && !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoInstallationDate", function (s) { return s.length > 0 && !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoCost", function (s) { return !isNumber(s); }, false, "יש להזין מחיר חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoHatches", function (s) { return !isInteger(s); }, false, "יש להזין מספר פתחים שלם");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoContractorMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoArchitectMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectInfoSupervisorMobile", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    if (bIsValid) {
        $("#ProjectDetailsErrorLabel").html("");
        SwitchEditSaveButtons(true, "Project");
        DisableProjectDetailsFields();
        $("#ContentPlaceHolder3_SaveProjectDetailsHiddenBTN").click();
    }
}

function ValidateServiceCallDetails() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ServiceCallProblemDesc", function (s) { return s.length == 0; }, false, "יש להזין תיאור תקלה");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        SwitchEditSaveButtons(true, "ServiceCall");
        DisableServiceCallDetailsFields();
        $("#ContentPlaceHolder3_SaveServiceCallDetailsHiddenBTN").click();
    }
}

function ValidateOrderDetails(sName) {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_" + sName + "OrderQuantity", function (s) { return s <= 0 || isNaN(s); }, false, "יש להזין כמות חיובית");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_" + sName + "OrderEstimatedDOA", function (s) { return s.length > 0 && !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        SwitchEditSaveButtons(true, "Order");
        DisableOrderDetailsFields(sName);
        $("#ContentPlaceHolder3_SaveOrderDetailsHiddenBTN").click();
    }
}

function ValidateHatchDetails() {
    $(".ErrorLabel").html("");
    SwitchEditSaveButtons(true, "Hatch");
    DisableHatchDetailsFields();
    $("#ContentPlaceHolder3_SaveHatchDetailsHiddenBTN").click();
}

function MarkInvalid(id, cb, bSelector, sMessage) {
    var sValue = $.trim($(id).val());
    var bInvalid = cb(sValue);
    var sFunctionCalledName = arguments.callee.caller.name;
    if (bSelector) {
        $(id).toggleClass("Invalid", bInvalid);
        //$(id).prev().find(".InvalidText").toggle(bInvalid);
    } else {
        $(id).toggleClass("Invalid", bInvalid);
        if (bInvalid && sMessage != "") {
            //$(id).parent().prev().find(".InvalidText").toggle(bInvalid);
            switch (sFunctionCalledName) {
                case "ValidateNewCustomer":
                case "ValidateNewProject":
                    $(".ErrorLabel").html(sMessage);
                    break;

                case "ValidateCustomerDetails":
                    $("#CustomerDetailsErrorLabel").html(sMessage);
                    break;

                case "ValidateProjectDetails":
                    $("#ProjectDetailsErrorLabel").html(sMessage);
                    break;

                default:
                    $(".ErrorLabel").html(sMessage);
                    break;
            }
        }
    }
    return !bInvalid;
}

function ClearInvalidFields(sTableID) {
    $(sTableID + " input, " + sTableID + " textarea").removeClass("Invalid");
    $(".ErrorLabel").html("");
}

//Backup details in case user choose to cancel changes
function BackupCustomerDetails() {
    aCustomerDetails = [];
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoFirstName").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoLastName").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoPhone").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoMobile").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoAddress").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoEmail").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoFax").val());
    aCustomerDetails.push($("#ContentPlaceHolder3_ProjectInfoArea").val());
}

function BackupUndecidedCustomerDetails() {
    aUndecidedCustomerDetails = [];
    aUndecidedCustomerDetails.push($("#ContentPlaceHolder3_ProjectName").val());
    aUndecidedCustomerDetails.push($("#ContentPlaceHolder3_CustomerMobilePhone").val());
    aUndecidedCustomerDetails.push($("#ContentPlaceHolder3_ProjectComments").val());
    aUndecidedCustomerDetails.push($("#ContentPlaceHolder3_CustomerBackDate").val());
    aUndecidedCustomerDetails.push($("#ContentPlaceHolder3_ProjectStatus").val());
}

function BackupProjectDetails() {
    aProjectDetails = [];
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoStatus").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoName").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoCost").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoExpirationDate").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoInstallationDate").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoComments").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoArchitectName").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoContractorName").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoSupervisorName").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoArchitectMobile").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoContractorMobile").val());
    aProjectDetails.push($("#ContentPlaceHolder3_ProjectInfoSupervisorMobile").val());
}

function BackupServiceCallDetails() {
    aServiceCallDetails = [];
    aServiceCallDetails.push($("#ContentPlaceHolder3_ServiceCallProblemDesc").val());
    aServiceCallDetails.push($("#ContentPlaceHolder3_ServiceCallUrgent").val());
}

function BackupOrderDetails(sName) {
    aOrderDetails = [];
    aOrderDetails.push($("#ContentPlaceHolder3_" + sName + "OrderQuantity").val());
    aOrderDetails.push($("#ContentPlaceHolder3_" + sName + "OrderStatus").val());
    aOrderDetails.push($("#ContentPlaceHolder3_" + sName + "OrderEstimatedDOA").val());
}

function BackupSupplierDetails() {
    aSupplierDetails = [];
    aSupplierDetails.push($("#ContentPlaceHolder3_SupplierName").val());
    aSupplierDetails.push($("#ContentPlaceHolder3_SupplierAddress").val());
    aSupplierDetails.push($("#ContentPlaceHolder3_SupplierEmail").val());
    aSupplierDetails.push($("#ContentPlaceHolder3_SupplierPhone").val());
    aSupplierDetails.push($("#ContentPlaceHolder3_SupplierCellPhone").val());
    aSupplierDetails.push($("#ContentPlaceHolder3_SupplierFax").val());
}

function BackupHatchDetails() {
    aHatchDetails = [];
    aHatchDetails.push($("#ContentPlaceHolder3_HatchType").val());
    aHatchDetails.push($("#ContentPlaceHolder3_HatchStatus").val());
    aHatchDetails.push($("#ContentPlaceHolder3_HatchFailureType").val());
    aHatchDetails.push($("#ContentPlaceHolder3_HatchComments").val());
}

//function IsPage(sPageName, sSource) {
//    return GetPageName() + location.search == sPageName + ".aspx" + (!IsEmpty(sSource) ? "?Source=" + sSource : "");
//}

function GetPageName() {
    var sFullPath = window.location.href;
    var iIndex = sFullPath.lastIndexOf("/");
    var sPageName = sFullPath.substr(iIndex + 1);
    if (!Contains(sPageName, "?")) {
        var iLength = sPageName.length;
        sPageName = sPageName.substring(0, iLength - 5).toLowerCase();
    }
    return sPageName;
}

//Misc
function IsEmpty(o) {
    return (o == "" || o == null);
}

function Contains(S, c) {
    return S.indexOf(c) > -1;
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function isValidDate(date) {
    if (date == "") return false;
    var matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(date);
    if (matches == null) return false;
    var d = matches[2];
    var m = matches[1] - 1;
    var y = matches[3];
    var composedDate = new Date(y, m, d);
    return composedDate.getDate() == d &&
			composedDate.getMonth() == m &&
			composedDate.getFullYear() == y;
}

function isNumber(n) {
    if (n == "") return false;
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function isInteger(number) {
    if (number == "") return false;
    var intRegex = /^\d+$/;
    return intRegex.test(number);
}

function isValidPhoneNumber(s) {
    return s.indexOf("0") == 0 && s.length >= 9;
}

function isValidMobileNumber(s) {
    return s.indexOf("05") == 0 && s.length == 10;
}

function ConvertToDate(sDate) {
    //Remove all non-numeric (except the plus)
    sDate = sDate.replace(/[^0-9 +]/g, '');
    //Create date
    var date = new Date(parseInt(sDate));
    var sFixedDate = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
    return sFixedDate;
}

//Cities AutoCompletion
//$.ajax({
//    url: "xmlFiles/IsraelCities.xml",
//    type: "GET",
//    dataType: "xml",
//    success: function (xmlResponse) {
//        var data = $("City", xmlResponse).map(function () {
//            return {
//                value: $(this).attr("Heb")
//            };
//        }).get();
//        $(".City").autocomplete({
//            source: function (req, response) {
//                var re = $.ui.autocomplete.escapeRegex(req.term);
//                var matcher = new RegExp("^" + re, "i");
//                response($.grep(data, function (item) {
//                    return matcher.test(item.value);
//                }));
//            },
//            minLength: 1
//        });
//    }
//});

//DRAG & DROP
function sendFileToServer(formData, status) {
    var uploadURL = window.location.href.substring(0, window.location.href.lastIndexOf("/") + 1) + "files/ReturnValue.ashx";   //Upload URL
    var extraData = {}; //Extra Data.
    var jqXHR = $.ajax({
        xhr: function () {
            var xhrobj = $.ajaxSettings.xhr();
            if (xhrobj.upload) {
                xhrobj.upload.addEventListener('progress', function (event) {
                    var percent = 0;
                    var position = event.loaded || event.position;
                    var total = event.total;
                    if (event.lengthComputable) {
                        percent = Math.ceil(position / total * 100);
                    }
                    //Set progress
                    status.setProgress(percent);
                }, false);
            }
            return xhrobj;
        },
        url: uploadURL,
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: formData,
        success: function (data) {
            status.setProgress(100);
            //$("#status1").append("File upload Done<br>");
        },
        error: function (e) {
            alert("Failed to upload files: " + e.responseText);
        }
    });

    status.setAbort(jqXHR);
}

var rowCount = 0;
function createStatusbar(obj) {
    rowCount++;
    var row = "odd";
    if (rowCount % 2 == 0) row = "even";
    this.statusbar = $("<div class='statusbar " + row + "'></div>");
    this.filename = $("<div class='filename'></div>").appendTo(this.statusbar);
    this.size = $("<div class='filesize'></div>").appendTo(this.statusbar);
    this.progressBar = $("<div class='progressBar'><div></div></div>").appendTo(this.statusbar);
    this.abort = $("<div class='abort'>Abort</div>").appendTo(this.statusbar);
    obj.after(this.statusbar);

    this.setFileNameSize = function (name, size) {
        var sizeStr = "";
        var sizeKB = size / 1024;
        if (parseInt(sizeKB) > 1024) {
            var sizeMB = sizeKB / 1024;
            sizeStr = sizeMB.toFixed(2) + " MB";
        }
        else {
            sizeStr = sizeKB.toFixed(2) + " KB";
        }

        this.filename.html(name);
        this.size.html(sizeStr);
    }
    this.setProgress = function (progress) {
        var progressBarWidth = progress * this.progressBar.width() / 100;
        this.progressBar.find('div').animate({ width: progressBarWidth }, 10).html(progress + "% ");
        if (parseInt(progress) >= 100) {
            this.abort.hide();
        }
    }
    this.setAbort = function (jqxhr) {
        var sb = this.statusbar;
        this.abort.click(function () {
            jqxhr.abort();
            sb.hide();
        });
    }
}
function handleFileUpload(files, obj) {
    for (var i = 0; i < files.length; i++) {
        var fd = new FormData();
        fd.append('file', files[i]);

        var status = new createStatusbar(obj); //Using this we can set progress.
        status.setFileNameSize(files[i].name, files[i].size);
        sendFileToServer(fd, status);

    }
}
function ActivateDragAndDrop() {
    var obj = $("#dragandrophandler");
    obj.on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).css('border', '2px solid #0B85A1');
    });
    obj.on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
    obj.on('drop', function (e) {

        $(this).css('border', '2px dotted #0B85A1');
        e.preventDefault();
        var files = e.originalEvent.dataTransfer.files;

        //We need to send dropped files to Server
        handleFileUpload(files, obj);
    });
    $(document).on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
    $(document).on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
        obj.css('border', '2px dotted #0B85A1');
    });
    $(document).on('drop', function (e) {
        e.stopPropagation();
        e.preventDefault();
    });
}

//Google Autocompletion
function ActivateGoogleAutoCompletion(sID) {
    // Create the autocomplete object, restricting the search
    // to geographical location types.
    autocomplete = new google.maps.places.Autocomplete(
	(document.getElementById(sID)),
	  { types: ['geocode'] });
}

/** Google Maps **/
var Map;
//var InfoWindow;
var oPosition = {};

function InitializeGoogleMap() {
    var mapOptions = {
        center: new google.maps.LatLng(32.321458, 34.853196),
        zoom: 10
    };
    Map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
}

function PopulateGoogleMap() {
    for (var pID in Projects) {
        GetCoordinatesByAddress(Projects[pID].Address);
        ShowProjectPin(oPosition, pID);
    }
    for (var scID in ServiceCallsList) {
        GetCoordinatesByAddress(ServiceCallsList[scID][1].Address);
        ShowServiceCallPin(oPosition, scID);
    }
}

function ShowServiceCallPin(oPosition, sID) {
    var Position = new google.maps.LatLng(oPosition.lat, oPosition.lng);
    var Image = "images/icons/red-pin.png";
    var Marker = new google.maps.Marker({
        position: Position,
        map: Map,
        title: "קריאת שירות",
        icon: Image
    });

    var sContent = '<div id="content">' +
				'<h3 class="firstHeading">' + ServiceCallsList[sID][1].Fname + " " + ServiceCallsList[sID][1].Lname + '</h3>' +
				'<div class="bodyContent">' +
				'<p><b>כתובת: </b>' + ServiceCallsList[sID][1].Address + '</p>' +
				'<p><b>טלפון נייד: </b>' + ServiceCallsList[sID][1].Mobile + '</p>' +
				'<p><b>תיאור התקלה: </b>' + ServiceCallsList[sID][0].Description + '</p>' +
    //                "<img src='" + poiPoint.ImageUrl + "' style = 'height:50px;' />" +
				'</div>' +
				'</div>';

    var InfoWindow = new google.maps.InfoWindow({
        content: sContent
    });

    google.maps.event.addListener(Marker, 'click', function () {
        InfoWindow.open(Map, Marker);
    });

    google.maps.event.trigger(Map, "resize");
}

function ShowProjectPin(oPosition, pID) {
    var Position = new google.maps.LatLng(oPosition.lat, oPosition.lng);
    var Image = "images/icons/blue-pin.png";
    var Marker = new google.maps.Marker({
        position: Position,
        map: Map,
        title: "פרויקט",
        icon: Image
    });

    var sContent = '<div id="content">' +
				'<h3 class="firstHeading">' + Projects[pID].Name + '</h3>' +
				'<div class="bodyContent">' +
				'<p><b>טלפון נייד: </b>' + Projects[pID].Mobile + '</p>' +
				'<p><b>כתובת: </b>' + Projects[pID].Address + '</p>' +
    //                "<img src='" + poiPoint.ImageUrl + "' style = 'height:50px;' />" +
				'</div>' +
				'</div>';

    var InfoWindow = new google.maps.InfoWindow({
        content: sContent
    });

    google.maps.event.addListener(Marker, 'click', function () {
        InfoWindow.open(Map, Marker);
    });
}

//----------------------------------------------------------------------------
// build the Projects page
// ProjectsList contains all the names of the projects
//----------------------------------------------------------------------------
function GetProjects() {
    dataString = "";
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetProjects',
        data: dataString, // Send value of the project id
        dataType: 'json', // Choosing a JSON datatype for the data sent
        type: 'POST',
        async: false, // this is a synchronous call
        contentType: 'application/json; charset = utf-8', // for the data received
        success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
        {
            p = $.parseJSON(data.d); // parse the data as json
            p = MergeInsideArrays(p);
            for (var i in p)
                Projects[p[i].pID] = p[i];
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project details: " + e.responseText);
        } // end of error
    });             // end of ajax call
}

function GetOpenedServiceCalls() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetOpenedServiceCalls',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            sc = $.parseJSON(data.d);
            for (var i in sc)
                ServiceCallsList[sc[i][0].ScID] = sc[i];
        }, // end of success
        error: function (e) {
            alert("failed to load Service calls" + e.responseText);
        } // end of error
    });               // end of ajax call
}

function GetCoordinatesByAddress(sAddress) {
    $.ajax({ // ajax call starts
        url: 'http://maps.googleapis.com/maps/api/geocode/json?address=' + sAddress + '&sensor=false',   // JQuery call to the server side method
        type: 'GET',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            oPosition = data.results[0].geometry.location;
        }, // end of success
        error: function (e) {
            alert("failed to get coordinates by address" + e.responseText);
        } // end of error
    });
}

function MergeInsideArrays(Arr) {
    var UniArr = [];
    for (var i = 0; i < Arr.length; i++) {
        var TempArr = [];
        for (var j = 0; j < Arr[i].length; j++) {
            for (var k in Arr[i][j]) {
                TempArr[k] = eval("Arr[i][j]." + k);
            }
        }
        UniArr.push(TempArr);
    }
    return UniArr;
}

function ResizeHomeContainer() {
    var iWindowHeight = $(window).height();
    $("#HomeContainer").height(0.68 * iWindowHeight);
}

function ActivateCountdown() {
    setTimeout('ActivateModal("ModalSessionTimeout")', (iTimeoutMin - 1) * 60 * 1000); //Timer till warning
    setTimeout(Logout, iTimeoutMin * 60 * 1000); //Timer till logout
}

function Logout() {
    __doPostBack('ctl00$LogoutBTN', '');
}

//Pie Chart
function GetProjectsIncome() {
    dataString = "";
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetProjectsIncome',
        data: dataString, // Send value of the project id
        dataType: 'json', // Choosing a JSON datatype for the data sent
        type: 'POST',
        async: false, // this is a synchronous call
        contentType: 'application/json; charset = utf-8', // for the data received
        success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
        {
            ProjectsIncome = $.parseJSON(data.d); // parse the data as json
            ProjectsIncome = MergeInsideArrays(ProjectsIncome);
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get projects income: " + e.responseText);
        } // end of error
    });               // end of ajax call
}

//Notifications
function GetNotifications(iEmployeeID) {
    dataString = JSON.stringify({ eID: iEmployeeID });
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetNotifications',
        data: dataString, // Send value of the project id
        dataType: 'json', // Choosing a JSON datatype for the data sent
        type: 'POST',
        async: false, // this is a synchronous call
        contentType: 'application/json; charset = utf-8', // for the data received
        success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
        {
            Notifications = $.parseJSON(data.d); // parse the data as json
            Notifications = MergeInsideArrays(Notifications);
            BuildNewsBox();
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project details: " + e.responseText);
        } // end of error
    });                // end of ajax call
}

function BuildNewsBox() {
    var sHTML = "";
    for (var i in Notifications) {
        sHTML += '<li class="news-item">';
        sHTML += '<table cellpadding="4">';
        sHTML += '<tr>';
        sHTML += '<td>';
        //        sHTML += '<img src="images/1.png" width="60" class="img-circle" />';
        sHTML += '</td>';
        sHTML += ConvertToDate(Notifications[i].MessageDate) + ": " + Notifications[i].Message;
        sHTML += '<td>';
        sHTML += '</td>';
        sHTML += '</tr>';
        sHTML += '</table>';
        sHTML += '</li>';
    }
    $("#NewsBox ul.News").html(sHTML);
}