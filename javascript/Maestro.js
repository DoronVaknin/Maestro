/// <reference path="jquery-1.11.0.js" />

var aCustomerDetails = [];
var aProjectDetails = [];

$(document).ready(function () {
    ActivateTabsMarking();
    ActivateToolbarButton("ToolbarBtnCreateProject", "NewCustomer", "CreateProject");
    ActivateToolbarButton("ToolbarBtnCreateServiceCallExternalCustomer", "NewCustomer", "CreateServiceCall");
    ActivateDatepicker();
    ActivateQuickSearch();
    ActivateServiceCallExistingProjectModal();
    if (IsPage("ProjectDetails")) {
        DisableCustomerDetailsFields();
        DisableProjectDetailsFields();
        FixTextAreaIssue();
        $("#ProjectDetailsStatusIcon").popover({ html: true, content: GetProgressBarContent() });
        ActivateGoogleAutoCompletion("ContentPlaceHolder3_ProjectInfoAddress");
        ActivatePlusMinus();
    }
    else if (IsPage("ProjectOrders"))
        ActivatePlusMinus();
    else if (IsPage("NewProject", "NewCustomer"))
        ActivateModal("ModalCustomerCreated");
    else if (IsPage("NewCustomer"))
        ActivateGoogleAutoCompletion("ContentPlaceHolder3_CustomerAddress");
    else if (IsPage("NewCustomer", "CreateProject")) {
        $("#CustomerForServiceCallBTN").addClass("HiddenButtons");
        ActivateGoogleAutoCompletion("ContentPlaceHolder3_CustomerAddress");
    }
    else if (IsPage("NewCustomer", "CreateServiceCall")) {
        $("#CustomerForProjectBTN").addClass("HiddenButtons");
        ActivateGoogleAutoCompletion("ContentPlaceHolder3_CustomerAddress");
    }
    else if (IsPage("NewSupplier"))
        ActivateGoogleAutoCompletion("ContentPlaceHolder3_SupplierAddress");
});

//Mark current tabs
function ActivateTabsMarking() {
    var sFileName = location.pathname.split('/').pop();
    sFileName = sFileName.substring(0, sFileName.length - 5);
    $("#" + sFileName).addClass("current");
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
    $('.search_textbox').first().hide();
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
function ActivateModal(sID) {
    $("#" + sID).modal();
}

function ActivateServiceCallExistingProjectModal() {
    $(".ChooseProjectToolbarButtons").click(function () {
        var sButtonID = this.id;
        switch (sButtonID) {
            case "ToolbarBtnCreateServiceCallExistingProject":
                $("#ModalChooseProject .modal-title").html("קריאת שירות - פרויקט קיים");
                $("#ChooseProjectForProjectOrdersBTN").addClass("HiddenButtons");
                $("#ChooseProjectForProjectHatchesBTN").addClass("HiddenButtons");
                $("#ChooseProjectForServiceCallBTN").removeClass("HiddenButtons");
                break;

            case "ToolbarBtnProjectOrders":
                $("#ModalChooseProject .modal-title").html("הזמנות עבור פרויקט");
                $("#ChooseProjectForServiceCallBTN").addClass("HiddenButtons");
                $("#ChooseProjectForProjectHatchesBTN").addClass("HiddenButtons");
                $("#ChooseProjectForProjectOrdersBTN").removeClass("HiddenButtons");
                break;

            case "ToolbarBtnProjectHatches":
                $("#ModalChooseProject .modal-title").html("פתחים עבור פרויקט");
                $("#ChooseProjectForServiceCallBTN").addClass("HiddenButtons");
                $("#ChooseProjectForProjectOrdersBTN").addClass("HiddenButtons");
                $("#ChooseProjectForProjectHatchesBTN").removeClass("HiddenButtons");
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

function DisableServiceCallDetailsFields() {
    $("#ServiceCallTBL input, #ServiceCallTBL textarea").attr("disabled", "disabled");
}

function FixTextAreaIssue() {
    var sValue = $("#ProjectDetailsTBL textarea").val();
    if (sValue == "&nbsp;")
        $("#ProjectDetailsTBL textarea").val("");
}

function ClickLoginBTN() {
    $("#LoginBTN").click();
}

//Project Details edit buttons
function EnableCustomerDetails() {
    $("#CustomerDetailsTBL input, #CustomerDetailsTBL select").removeAttr("disabled");
    $("#ContentPlaceHolder3_ProjectInfoID").attr("disabled", "disabled");
    SwitchCustomerDetailsEditSaveButtons(false);
    BackupCustomerDetails();
}

function SwitchCustomerDetailsEditSaveButtons(bShowEditButton) {
    $("#ContentPlaceHolder3_EditCustomerDetailsBTN").toggle(bShowEditButton);
    $("#ContentPlaceHolder3_SaveCustomerDetailsBTN").toggle(!bShowEditButton);
    $("#ContentPlaceHolder3_CancelCustomerDetailsBTN").toggle(!bShowEditButton);
}

function EnableProjectDetails() {
    $("#ProjectDetailsTBL *").removeAttr("disabled");
    $("#ContentPlaceHolder3_ProjectInfoHatches").attr("disabled", "disabled");
    $("#ContentPlaceHolder3_ProjectInfoDateOpened").attr("disabled", "disabled");
    SwitchProjectDetailsEditSaveButtons(false);
    BackupProjectDetails();
}

function SwitchProjectDetailsEditSaveButtons(bShowEditButton) {
    $("#ContentPlaceHolder3_EditProjectDetailsBTN").toggle(bShowEditButton);
    $("#ContentPlaceHolder3_SaveProjectDetailsBTN").toggle(!bShowEditButton);
    $("#ContentPlaceHolder3_CancelProjectDetailsBTN").toggle(!bShowEditButton);
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
    SwitchCustomerDetailsEditSaveButtons(true);
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
    SwitchProjectDetailsEditSaveButtons(true);
    ClearInvalidFields("#ProjectDetailsTBL");
}

function EnableServiceCallDetails() {
    $("#ContentPlaceHolder3_ServiceCallProblemDesc, #ContentPlaceHolder3_ServiceCallUrgent").removeAttr("disabled");
    SwitchServiceCallEditSaveButtons(false);
    BackupServiceCallDetails();
}

function SwitchServiceCallEditSaveButtons(bShowEditButton) {
    $("#ContentPlaceHolder3_EditServiceCallDetailsBTN").toggle(bShowEditButton);
    $("#ContentPlaceHolder3_SaveServiceCallDetailsBTN").toggle(!bShowEditButton);
    $("#ContentPlaceHolder3_CancelServiceCallDetailsBTN").toggle(!bShowEditButton);
}

function ValidateServiceCallDetails() {

}

function RestoreServiceCallDetails() {
    $("#ContentPlaceHolder3_ServiceCallProblemDesc").val(aServiceCallDetails[0]);
    var bUrgent = aServiceCallDetails[1] == "on" ? false : true;
    $("#ContentPlaceHolder3_ServiceCallUrgent").prop('checked', bUrgent);
    DisableServiceCallDetailsFields();
    SwitchServiceCallEditSaveButtons(true);
    ClearInvalidFields("#ServiceCallTBL");
}

//Navigation
function Goto(sPage, sQuery) {
    window.location = sPage + ".aspx" + (!IsEmpty(sQuery) ? sQuery : "");
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

function ValidateNewSupplier(Button) {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierName", function (s) { return s.length < 2; }, false, "השם הפרטי קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierAddress", function (s) { return s.length < 2; }, false, "כתובת המגורים קצרה מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierPhone", function (s) { return !isValidPhoneNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierCellPhone", function (s) { return s.length > 0 && !isValidMobileNumber(s); }, false, "יש להזין מס' טלפון תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierFax", function (s) { return s.length > 0 && !isValidPhoneNumber(s); }, false, "יש להזין מס' פקס תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_SupplierEmail", function (s) { return s.length > 0 && !IsEmail(s); }, false, "יש להזין כתובת מייל חוקית");
    if (bIsValid) {
        $(".ErrorLabel").html("");
        $("#ContentPlaceHolder3_CreateSupplierHiddenBTN").click();
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
        SwitchCustomerDetailsEditSaveButtons(true);
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
        SwitchProjectDetailsEditSaveButtons(true);
        DisableProjectDetailsFields();
        $("#ContentPlaceHolder3_SaveProjectDetailsHiddenBTN").click();
    }
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
    $(sTableID + " input").removeClass("Invalid");
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

function IsPage(sPageName, sSource) {
    return location.pathname + location.search == "/Maestro/" + sPageName + ".aspx" + (!IsEmpty(sSource) ? "?Source=" + sSource : "");
}

//Misc
function IsEmpty(o) {
    return (o == "" || o == null);
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

//Cities AutoCompletion
$.ajax({
    url: "xmlFiles/IsraelCities.xml",
    type: "GET",
    dataType: "xml",
    success: function (xmlResponse) {
        var data = $("City", xmlResponse).map(function () {
            return {
                value: $(this).attr("Heb")
            };
        }).get();
        $(".City").autocomplete({
            source: function (req, response) {
                var re = $.ui.autocomplete.escapeRegex(req.term);
                var matcher = new RegExp("^" + re, "i");
                response($.grep(data, function (item) {
                    return matcher.test(item.value);
                }));
            },
            minLength: 1
        });
    }
});

//DRAG & DROP
function sendFileToServer(formData, status) {
    var uploadURL = window.location.href.substring(0, window.location.href.lastIndexOf("/") + 1) + "files/";   //Upload URL
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
$(document).ready(function () {
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

});

//Google Autocompletion
function ActivateGoogleAutoCompletion(sID) {
    // Create the autocomplete object, restricting the search
    // to geographical location types.
    autocomplete = new google.maps.places.Autocomplete(
    (document.getElementById(sID)),
      { types: ['geocode'] });
}