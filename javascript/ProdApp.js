/// <reference path="jquery-1.11.0.js" />

var Hatches = {};
var HatchStatusList = {};
var FailureTypeList = {};
var HatchDetails = {};
var sEmployeeID = "";
var CurrentPageData = [];
var bFirstLogin = true;


$(document).ready(function () {
    $("#LoginBTN").click(function () {
        Login();
    });

    //some css settings
    $("#LoginBTN").parent().css({ "width": "36%", "margin": "auto" });
});

$(document).on("mobileinit", function () {
    // Setting #Container div as a jQuery Mobile pageContainer
    $.mobile.pageContainer = $('#Container');
    // Setting default page transition to slide
    $.mobile.defaultPageTransition = 'slide';
});

$(document).on("change", ".HatchStatusDDL", function () {
    var sHatchStatus = $(this).find(":selected").text();
    var bShowFailureDDL = sHatchStatus == "תקלה";
    $.mobile.activePage.find(".FailureTypeParagraph").toggle(bShowFailureDDL);
});

$(document).on("click", ".HatchBTN", function () {
    setTimeout(BackupPage, 1000);
});

function Logout() {
    $('div[id^="Hatch"]').remove();
    $("#ProjectsList li").remove();
    Hatches = {};
    HatchStatusList = {};
    FailureTypeList = {};
    HatchDetails = {};
    sEmployeeID = "";
    CurrentPageData = [];
    bFirstLogin = false;
    Goto("LoginPage");
}

function BackupPage() {
    var Page = $.mobile.activePage;
    var PageID = $(Page).attr("id");
    var iHatchStatusID = $("#" + PageID + " .HatchStatusDDL option:selected").val();
    var iFailureTypeID = $("#" + PageID + " .FailureTypeDDL option:selected").val();
    var sComments = $("#" + PageID + " .HatchCommentsTB").val();
    CurrentPageData.push(PageID, iHatchStatusID, iFailureTypeID, sComments);
}

function RestorePage(pID) {
    var PageID = CurrentPageData[0];
    var sText = $("#" + PageID + " .HatchStatusDDL option[value='" + CurrentPageData[1] + "']").text();
    $("#" + PageID + " .HatchStatusDDL option[value='" + CurrentPageData[1] + "']").attr("selected", "selected");
    $.mobile.activePage.find(".HatchStatusParagraph span.ui-btn-text .HatchStatusDDL").text(sText);

    sText = $("#" + PageID + " .FailureTypeDDL option[value='" + CurrentPageData[2] + "']").text();
    $("#" + PageID + " .FailureTypeDDL option[value='" + CurrentPageData[2] + "']").attr("selected", "selected");
    $.mobile.activePage.find(".FailureTypeParagraph span.ui-btn-text .FailureTypeDDL").text(sText);

    $("#" + PageID + " .HatchCommentsTB").val(CurrentPageData[3]);
    window.location = "#HatchesOfProject" + pID;
}

function Login() {
    var sUsername = $.trim($("#UserName").val());
    var sPassword = $.trim($("#Password").val());
    if (IsEmpty(sUsername) || IsEmpty(sPassword)) {
        alert("אנא הזן שם משתמש וסיסמה לפני התחברות");
        return;
    }
    ShowLoading("מתחבר");
    dataString = "{ Username: '" + sUsername + "', Password: '" + sPassword + "' }";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/Login',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            if (data.d == "true") {
                $("#UserName, #Password").val("");
                HideLoading();

                BuildProjectsPage();
                if (!bFirstLogin)
                    $("#ProjectsList").listview("refresh"); // this is important for the jQueryMobile to assign the style to a dynamically added list
                Goto("ProjectsPage");

                BuildHatchesListPerProject();

                GetHatchStatusList();
                GetFailureTypeList();

                BuildHatchesPagePerProject();
            }
            else {
                HideLoading();
                alert("שם משתמש או סיסמה לא נכונים");
            }
        }, // end of success
        error: function (e) {
            HideLoading();
            alert("failed to login: " + e.responseText);
        } // end of error
    });               // end of ajax call
}

//-----------------------------------------------------------------------
// Load the projects to client-side
//-----------------------------------------------------------------------
function GetHatchStatusList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetHatchStatusList',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            HatchStatusList = $.parseJSON(data.d);
        }, // end of success
        error: function (e) {
            alert("failed to load Hatch status list :( " + e.responseText);
        } // end of error
    });           // end of ajax call
}

//-----------------------------------------------------------------------
// get failure type list
//-----------------------------------------------------------------------
function GetFailureTypeList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetFailureTypeList',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            FailureTypeList = $.parseJSON(data.d);
        }, // end of success
        error: function (e) {
            alert("failed to load Failure type list :( " + e.responseText);
        } // end of error
    });           // end of ajax call
}

//----------------------------------------------------------------------------
// build the Projects page
// ProjectsList contains all the names of the projects
//----------------------------------------------------------------------------
function BuildProjectsPage() {
    dataString = "";
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetHatchesForProdApp',
        data: dataString, // Send value of the project id
        dataType: 'json', // Choosing a JSON datatype for the data sent
        type: 'POST',
        async: false, // this is a synchronous call
        contentType: 'application/json; charset = utf-8', // for the data received
        success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
        {
            var h = $.parseJSON(data.d); // parse the data as json
            h = MergeInsideArrays(h);
            for (var i in h) {
                if (typeof Hatches[h[i].pID] != "object")
                    Hatches[h[i].pID] = [];
                Hatches[h[i].pID].push(h[i]);
            }
            $("#ProjectsList").html(BuildProjectsList());
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project details: " + e.responseText);
        } // end of error
    });           // end of ajax call
}

//------------------------------------------------------
// build projects list items
//------------------------------------------------------
function BuildProjectsList() {
    var str = "";
    for (var pID in Hatches) {
        str += "<li><a data-ajax = 'false' href= '#HatchesOfProject" + pID + "'>";
        str += "<h1>" + Hatches[pID][0].Name + "</h1>";
        str += "<p></p>";
        str += "</a></li>";
    }
    return str;
}

//------------------------------------------------------
// build Hatches list items
//------------------------------------------------------
function BuildHatchesList(ProjID) {
    var str = "";
    for (var h in Hatches[ProjID]) {
        str += "<li><a class = 'HatchBTN' data-ajax = 'false' href= '#Hatch" + Hatches[ProjID][h].HatchID + "'>";
        str += "<h1>פתח מס' " + Hatches[ProjID][h].HatchID + "</h1>";
        str += "<p>" + Hatches[ProjID][h].HatchType + " - " + Hatches[ProjID][h].HatchStatus + "</p>";
        str += "</a></li>";
    }
    return str;
}

//------------------------------------------------------
// build Hatches list per project
//------------------------------------------------------
function BuildHatchesListPerProject() {
    str = "";
    for (var pID in Hatches) {
        //Projects[pID][1].HatchesImageURL
        str += '<div data-role="page" id="HatchesOfProject' + pID + '">';
        str += '<div data-role="header" data-theme="a"><h1>' + Hatches[pID][0].Name + '</h1>';
        str += '<a href="#ProjectsPage" data-icon="back" data-iconpos="notext" style="border: none;"></a>';
        str += '<a href="#HatchesImage' + pID + '" data-rel="popup" data-icon="info" data-iconpos="notext" style="border: none;"></a></div>'; //end of header
        str += '<div data-role="content">';
        str += '<ul id="HatchesList" data-role="listview" data-theme="c" data-inset="true" data-filter="true" data-filter-placeholder = "חפש פתח...">';
        str += BuildHatchesList(pID);
        str += "</ul>"; // end of ul

        str += '<br/><div id="HatchesImage' + pID + '" data-role="popup" class = "photopopup">';
        str += '<a href="#HatchesOfProject' + pID + '" data-role = "button" data-icon="delete" data-iconpos = "notext" class="ui-corner-all ui-shadow ui-btn-a ui-btn-right" style = "border:none;" ></a>';
        str += '<img src = "' + Hatches[pID][0].HatchesImageURL + '" /></div>';

        str += "</div>"; // end of content
        str += "</div>"; // end of page
    }
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
}

//------------------------------------------------------
// build Hatches list per project
//------------------------------------------------------
function BuildHatchesPagePerProject() {
    str = "";
    for (var pID in Hatches) {
        for (var Hatch in Hatches[pID]) {
            str += '<div data-role="page" id="Hatch' + Hatches[pID][Hatch].HatchID + '">';
            str += "<div data-role='header' data-theme='a'><h1>פתח מס' " + Hatches[pID][Hatch].HatchID + '</h1>';
            //            str += '<a class = "HatchDetailsBackBTN" href="#HatchesOfProject' + pID + '" data-icon="back" data-iconpos="notext" style="border: none;"></a></div>';
            str += '<a onclick = "RestorePage(' + pID + ')" data-icon="back" data-iconpos="notext" style="border: none;"></a></div>';
            str += '<div data-role="content">';

            str += "<h2>פרטי הפתח</h2>";
            str += "<p><b>סוג הפתח: </b>" + Hatches[pID][Hatch].HatchType + "</p>";
            str += "<p class = 'HatchStatusParagraph'><b>סטטוס: </b>" + BuildHatchStatusDDL(Hatches[pID][Hatch].HatchStatus) + "</p>";
            str += "<p><b>העובד המדווח: </b>" + Hatches[pID][Hatch].EmployeeName + "</p>";
            str += "<p><b>תאריך דיווח: </b>" + ConvertToDate(Hatches[pID][Hatch].StatusLastModified) + "</p>";
            var bShowFailureDDL = Hatches[pID][Hatch].HatchStatus == "תקלה";
            str += "<p class = 'FailureTypeParagraph' " + (bShowFailureDDL ? '' : 'style = display:none;') + "><b>התקלה: </b>" + BuildFailureTypeDDL(Hatches[pID][Hatch].FtName) + "</p>";
            str += "<p><b>הערות: </b>" + BuildHatchCommentsTextArea(Hatches[pID][Hatch].Comments) + "</p></br>";
            str += "<a id = 'ReportBTN_Hatch" + Hatches[pID][Hatch].HatchID + "' class = 'HatchesReportBTN' onclick = 'PrepareHatchDetails(" + Hatches[pID][Hatch].HatchID + "," + pID + ")' data-role='button'>דווח</a>";

            str += "</div>"; // end of content
            str += "</div>"; // end of page
        }
    }
    newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);

    // initializing popup event for images
    $(document).on("pageinit", function () {
        $(".photopopup").on({
            popupbeforeposition: function () {
                var maxHeight = $(window).height() - 30 + "px";
                $(".photopopup img").css("max-height", maxHeight);
            }
        });
    });
}

function PrepareHatchDetails(hID, pID) {
    var HatchTypeElement = $("#Hatch" + hID + " p")[0];
    var sHatchType = $(HatchTypeElement).text();
    sHatchType = sHatchType.substring(10, str.length);
    var iHatchTypeID = sHatchType == "חלון" ? 1 : 2;
    var iHatchStatusID = $("#Hatch" + hID + " .HatchStatusDDL option:selected").val();
    var sHatchStatus = $("#Hatch" + hID + " .HatchStatusDDL option:selected").text();
    var iFailureTypeID = $("#Hatch" + hID + " .FailureTypeDDL option:selected").val();
    var sFailureType = $("#Hatch" + hID + " .FailureTypeDDL option:selected").text();
    var bReportFailureType = sHatchStatus == "תקלה";
    var sCurrentDate = GetCurrentDate();
    var sComments = $.trim($("#Hatch" + hID + " .HatchCommentsTB").val());
    GetUsernameID(); // Need to identify worker and send his ID to DB
    var HatchDetails = {
        HatchID: hID,
        HatchTypeID: iHatchTypeID,
        HatchStatusID: iHatchStatusID,
        FailureTypeID: (bReportFailureType ? iFailureTypeID : 0),
        EmployeeID: sEmployeeID,
        Date: sCurrentDate,
        Comments: (!IsEmpty(sComments) ? sComments : "")
    };
    UpdateHatchDetails(HatchDetails, pID, sHatchType, sHatchStatus, sFailureType, bReportFailureType);
}

function GetUsernameID() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetUsernameID',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'text',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            var obj = $.parseJSON(data);
            sEmployeeID = obj.d;
        }, // end of success
        error: function (e) {
            alert("failed to Get Username ID." + e.responseText);
        } // end of error
    });
}

function UpdateHatchDetails(oHatchDetails, pID, sHatchType, sHatchStatus, sFailureType, bStatusFailure) {
    function InsertHatchFailureNotification() {
        var Notification = {};
        Notification["Message"] = "לפתח מס' " + oHatchDetails.HatchID + " בפרויקט " + Hatches[pID][0].Name + " דווחה תקלה בייצור, התקלה היא " + sFailureType + ": " + oHatchDetails.Comments;
        Notification["MessageDate"] = oHatchDetails.Date;
        Notification["eID"] = "302042267";

        dataString = JSON.stringify(Notification);
        $.ajax({ // ajax call starts
            url: 'MaestroWS.asmx/InsertNewNotification',   // JQuery call to the server side method
            data: dataString,    // the parameters sent to the server
            type: 'POST',        // can be post or get
            dataType: 'json',    // Choosing a JSON datatype
            contentType: 'application/json; charset = utf-8', // of the data received
            async: false,
            success: function (data) // Variable data contains the data we get from serverside
            {
            }, // end of success
            error: function (e) {
                alert("failed to insert failure notification :( " + e.responseText);
            } // end of error
        });
    }

    dataString = JSON.stringify(oHatchDetails);
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/UpdateHatchDetails',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            CurrentPageData = [];
            alert("הדיווח נשלח בהצלחה");
            var sNewCaption = sHatchType + " - " + sHatchStatus;
            $('#HatchesList a.HatchBTN[href="#Hatch' + oHatchDetails.HatchID + '"] p').text(sNewCaption);
            Goto("HatchesOfProject" + pID);
            if (bStatusFailure) InsertHatchFailureNotification();
        }, // end of success
        error: function (e) {
            alert("failed to send a report :( " + e.responseText);
        } // end of error
    });                  // end of ajax call
}

function BuildHatchDetailsPage(oHatch) {
    var iHatchID = oHatch.HatchID;
    var iProjID = oHatch.ProjectID;
    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'Hatch" + iHatchID + "'>";

    // build the header
    var sHeaderText = "פתח מס' " + iHatchID;
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1>" + sHeaderText + "</h1>";
    str += "<a href='#HatchesOfProject" + iProjID + "' data-icon='back' data-iconpos = 'notext' style = 'border:none;'></a>";
    str += "</div>"; //close the header

    // add the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הפתח</h2>";
    str += "<p><b>סטטוס: </b>" + Hatches[iProjID][iHatchID].HatchStatus + "</p>";
    str += "<p><b>סוג הפתח: </b>" + Hatches[iProjID][iHatchID].HatchType + "</p>";

    //    str += '</br><a href = "#myPopup" data-role = "button" data-rel="popup">Popup Image</a>';

    //    str += '</br><div id="myPopup" data-role="popup" class = "photopopup">';
    //    str += '<a href="#Hatch' + iHatchID + '" data-role = "button" data-icon="delete" data-iconpos = "notext" class="ui-corner-all ui-shadow ui-btn-a ui-btn-right" style = "border:none;" ></a>';
    //    str += '<img src = "' + Projects[iProjID][1].HatchesImageURL + '" /></div>';

    str += "</div>" // close the content

    str += "<div data-role='footer' data-position='fixed' data-theme='a'>";
    str += "<div class = 'HatchNavbar' data-role='navbar'>";
    str += "<ul>";
    str += "<li><a data-ajax = 'false' href='#TakePhotoHatch" + iHatchID + "' class = 'ui-icon-camera-white'>צלם תמונה</a></li>";
    str += "<li><a data-ajax = 'false' href='#PicturesOfHatch" + iHatchID + "' class = 'HatchPicturesBTN' data-icon='grid' data-iconpos='left'>תמונות</a></li>";
    str += "<li><a data-ajax = 'false' href='#QAForHatch" + iHatchID + "' data-icon='star'>בקרת איכות</a></li>";
    str += "</ul>";
    str += "</div>"; // close the navbar
    str += "</div>"; // close the footer

    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
}

function BuildHatchStatusDDL(sHatchStatus) {
    var str = "";
    str += '<select name="HatchStatus" class = "HatchStatusDDL" >';
    for (var i in HatchStatusList) {
        bSelected = sHatchStatus == HatchStatusList[i];
        str += "<option value='" + i + "' " + (bSelected ? "selected='selected'" : "") + ">" + HatchStatusList[i] + "</option>";
    }
    str += '</select>';
    return str;
}

function BuildFailureTypeDDL(sFailureType) {
    var str = "";
    str += '<select name="FailureType" class = "FailureTypeDDL" >';
    for (var i in FailureTypeList) {
        bSelected = sFailureType == FailureTypeList[i];
        str += "<option value='" + i + "' " + (bSelected ? "selected='selected'" : "") + ">" + FailureTypeList[i] + "</option>";
    }
    str += '</select>';
    return str;
}

function BuildHatchCommentsTextArea(sComments) {
    var str = "<textarea class = 'HatchCommentsTB' cols = '5' rows = '3'>" + sComments + "</textarea>";
    return str;
}

//Misc
function IsEmpty(o) {
    return (o == "" || o == null || o == undefined);
}

function MakeAssociativeArray(Arr, bReturnObj, iInternalKeyIndex) {
    var UnifiedArr;
    bReturnObj ? UnifiedArr = {} : UnifiedArr = [];
    for (var i = 0; i < Arr.length; i++) {
        var TempArr = {};
        //        var counter = 0;
        for (var j = 0; j < Arr[i].length; j++) {
            for (var field in Arr[i][j]) {
                TempArr[field] = eval("Arr[i][j]." + field);
                //                counter++;
            }
        }
        UnifiedArr[Arr[i][iInternalKeyIndex].HatchID] = TempArr;
    }
    return UnifiedArr;
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

function Goto(sPage) {
    window.location = "#" + sPage;
}

function ConvertToDate(sDate) {
    //Remove all non-numeric (except the plus)
    sDate = sDate.replace(/[^0-9 +]/g, '');
    //Create date
    var date = new Date(parseInt(sDate));
    var sFixedDate = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
    return sFixedDate;
}

function GetCurrentDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function ShowLoading(sText) {
    $.mobile.loading('show', {
        text: sText,
        theme: 'c',
        textVisible: true
    });
} // loading

function HideLoading() {
    $.mobile.loading('hide');
} // Unload