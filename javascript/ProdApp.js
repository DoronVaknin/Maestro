/// <reference path="jquery-1.11.0.js" />

var ProjectsList = [];
var Hatches = {};
var HatchStatusList = {};
var FailureTypeList = {};
var HatchDetails = {};
var sEmployeeID = "";
var CurrentPageData = [];


$(document).ready(function () {
    $("#LoginBTN").click(function () {
        Login();
    });

    //some css settings
    $("#LoginBTN").parent().css({ "width": "36%", "margin": "auto" });
});

$(document).on("change", ".HatchStatusDDL", function () {
    var sHatchStatus = $(this).find(":selected").text();
    bShowFailureDDL = sHatchStatus == "תקלה";
    $.mobile.activePage.find(".FailureTypeParagraph").toggle(bShowFailureDDL);
});

$(document).on("click", ".HatchBTN", function () {
    setTimeout(BackupPage, 1000);
});

function Logout() {
    $('div[id^="Hatch"]').remove();
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

    var sText = $("#" + PageID + " .FailureTypeDDL option[value='" + CurrentPageData[2] + "']").text();
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
                Goto("ProjectsPage");
                LoadProjectsList(); //  read all the projects
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
    });             // end of ajax call
}

//-----------------------------------------------------------------------
// Load the projects to client-side
//-----------------------------------------------------------------------
function LoadProjectsList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetProjectListForProdApp',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            ProjectsList = $.parseJSON(data.d);
            $("#ProjectsList").html(BuildProjectsPage(ProjectsList));

            var newPage = $(BuildHatchesListPerProject());
            newPage.appendTo($.mobile.pageContainer);

            $("#ProjectsList").listview("refresh"); // this is important for the jQueryMobile to assign the style to a dynamically added list

            GetHatchStatusList();
            GetFailureTypeList();

            newPage = $(BuildHatchesPagePerProject());
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

        }, // end of success
        error: function (e) {
            alert("failed to load projects :( " + e.responseText);
        } // end of error
    });                 // end of ajax call
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
function BuildProjectsPage(ProjectsList) {
    for (var i = 0; i < ProjectsList.length; i++) { // run on all the files in the list
        $.ajax({ // ajax call start
            url: 'MaestroWS.asmx/GetProjectHatchesForProdApp',
            data: "{ pID: " + ProjectsList[i] + "}", // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                p = $.parseJSON(data.d); // parse the data as json
                Hatches[p[0][0].pID] = MergeInsideArrays(p);
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to get project details: " + e.responseText);
            } // end of error
        });         // end of ajax call
    } // end of loop on all the projects
    str = "";
    for (var i in Hatches) {
        str += BuildProjectsList(i); // add item to the list in the main projects page
        //        BuildProjectPage(Hatches[i][1].pID); // build a page for each project
    }
    return str;
}

//------------------------------------------------------
// build projects list items
//------------------------------------------------------
function BuildProjectsList(i) {
    var str = "";
    str += "<li><a data-ajax = 'false' href= '#HatchesOfProject" + Hatches[i][0].pID + "'>";
    str += "<h1>" + Hatches[i][0].Name + "</h1>";
    str += "<p></p>";
    str += "</a></li>";
    return str;
}

//----------------------------------------------------------------------------
// build a page per project
//----------------------------------------------------------------------------
function BuildProjectPage(ProjectID) {
    var p = Hatches[ProjectID];

    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'Project" + ProjectID + "'>";
    // build the header

    str += BuildProjectHeader(p.Name);

    // add the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הלקוח</h2>";
    str += "<p><b>שם הלקוח: </b>" + CustomerFullName + "</p>";
    if (!IsEmpty(Project.Phone)) str += "<p><b>טלפון: </b>" + Customer.Phone + "</p>";
    str += "<p><b>טלפון נייד: </b>" + Customer.Mobile + "</p>";
    if (!IsEmpty(Project.Fax)) str += "<p><b>פקס: </b>" + Customer.Fax + "</p>";
    if (!IsEmpty(Project.Email)) str += "<p><b>דוא&quot;ל: </b>" + Customer.Email + "</p>";

    str += "<h2>פרטי הפרויקט</h2>";
    str += "<p><b>סטטוס: </b>" + ProjectStatus.StatusName + "</p>";
    str += "<p><b>עלות: </b>" + Project.Cost + "</p>";
    if (!IsEmpty(Project.Comments)) str += "<p><b>הערות: </b>" + Project.Comments + "</p>";

    str += "<h2>אנשי קשר</h2>";
    if (!IsEmpty(Project.ContractorName)) str += "<p><b>קבלן: </b>" + Project.ContractorName + "  " + Project.ContractorPhone + "</p>";
    if (!IsEmpty(Project.ArchitectName)) str += "<p><b>אדריכל: </b>" + Project.ArchitectName + "  " + Project.ArchitectPhone + "</p>";
    if (!IsEmpty(Project.SupervisorName)) str += "<p><b>מפקח: </b>" + Project.SupervisorName + "  " + Project.SupervisorPhone + "</p><br/>";

    //    str += "<a class = 'HatchesBTN' href='#HatchesOfProject" + ProjectID + "' data-role='button'>צפה בפתחים</a>";

    str += "</div>";  // close the content
    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
    //hatches button design
    //    $(".HatchesBTN").css({ "width": "55%", "margin": "auto" });
}

//----------------------------------------------------------------------------
// build a common header for project page
//----------------------------------------------------------------------------
function BuildProjectHeader(sHeaderText) {
    var str = "";
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1>" + sHeaderText + "</h1>";
    str += "<a href='#ProjectsPage' data-icon='back' data-iconpos = 'notext' style = 'border:none;'></a>";
    str += "</div>"; //close the header
    //    str += "<div data-role='footer' data-position='fixed' data-theme='a'>";
    //    str += "<div data-role='navbar' >";
    //    str += "<ul>"
    //    str += "<li><a data-ajax = 'false' href='#" + "students" + p.groupCode + "' class = 'ui-icon-group'>Students</a></li>";
    //    str += "<li><a data-ajax = 'false'  href='#" + "Screenshots" + p.groupCode + " 'class = 'ui-icon-screenshot'>Screenshots</a></li>";
    //    str += "<li><a data-ajax = 'false'  href='#" + "Videos" + p.groupCode + "' class = 'ui-icon-video'>Videos</a></li>";
    //    str += "</ul>";
    //    str += "</div>";
    //    str += "</div>";
    return str;
}

//----------------------------------------------------------------------------
// build Hatch pages
//----------------------------------------------------------------------------
function BuildHatchesPage(ProjectsList) {
    for (var i = 0; i < ProjectsList.length; i++) { // run on all the files in the list
        $.ajax({ // ajax call start
            url: 'MaestroWS.asmx/GetHatches',
            data: "{ pID: " + ProjectsList[i].pID + "}", // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                var h = $.parseJSON(data.d); // parse the data as json
                if (h.length != 0) {
                    var ProjID = h[0][0].pID;
                    Hatches[ProjID] = MakeAssociativeArray(h, true, 1);
                }
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to get project's hatches: " + e.responseText);
            } // end of error
        });          // end of ajax call
    } // end of loop on all the hatches
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

        str += '</br><div id="HatchesImage' + pID + '" data-role="popup" class = "photopopup">';
        str += '<a href="#HatchesOfProject' + pID + '" data-role = "button" data-icon="delete" data-iconpos = "notext" class="ui-corner-all ui-shadow ui-btn-a ui-btn-right" style = "border:none;" ></a>';
        str += '<img src = "' + Hatches[pID][0].HatchesImageURL + '" /></div>';

        str += "</div>"; // end of content
        str += "</div>"; // end of page
    }
    return str;
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
    return str;
}

//----------------------------------------------------------------------------
// build the Hatch details page
//----------------------------------------------------------------------------
function BuildHatchPage(ProjectID) {
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetPicsAndPins',
        data: "{ ProjectID : " + ProjectID + "}", // Send value of the project id
        dataType: 'json', // Choosing a JSON datatype for the data sent
        type: 'POST',
        async: false, // this is a synchronous call
        contentType: 'application/json; charset = utf-8', // for the data received
        success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
        {
            var Arr = $.parseJSON(data.d); // parse the data as json
            var HatchID = Arr[0][0].HatchID;
            PNP = MakeAssociativeArray(Arr, true, 0);
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project details: " + e.responseText);
        } // end of error
    });             // end of ajax call

    for (var Hatch in PNP)
        BuildHatchDetailsPage(PNP[Hatch]); // build a page for each hatch
    //    $('.HatchNavbar').navbar('refresh');
}

function PrepareHatchDetails(hID, pID) {
    var sHatchID = hID;
    var iHatchStatusID = $("#Hatch" + hID + " .HatchStatusDDL option:selected").val();
    var iFailureTypeID = $("#Hatch" + hID + " .FailureTypeDDL option:selected").val();
    var sHatchStatus = $("#Hatch" + hID + " .FailureTypeDDL option:selected").text();
    var bReportFailureType = sHatchStatus == "תקלה";
    var sCurrentDate = GetCurrentDate();
    var sComments = $.trim($("#Hatch" + hID + " .HatchCommentsTB").val());
    GetUsernameID(); // Need to identify worker and send his ID to DB
    var HatchDetails = {
        HatchID: sHatchID,
        HatchStatusID: iHatchStatusID,
        FailureTypeID: (bReportFailureType ? iFailureTypeID : 0),
        EmployeeID: sEmployeeID,
        Date: sCurrentDate,
        Comments: (!IsEmpty(sComments) ? sComments : "")
    };
    UpdateHatchDetails(HatchDetails);
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

function UpdateHatchDetails(oHatchDetails) {
    dataString = JSON.stringify(oHatchDetails);
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/UpdateHatchDetails',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            CurrentPageData = [];
            alert("הדיווח נשלח בהצלחה");
            Goto("HatchesOfProject" + pID);
        }, // end of success
        error: function (e) {
            alert("failed to send a report :( " + e.responseText);
        } // end of error
    });             // end of ajax call
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