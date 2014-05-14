﻿/// <reference path="jquery-1.11.0.js" />

var Projects = {};
var ProjectsList = {};
var ProjectsNamesList = {};
var Hatches = {};
var PicsAndPins = {};  //Pictures & Pins
var ServiceCallsList = {};  //ServiceCallsList[scID][0] - Service call details, ServiceCallsList[scID][1] - Customer details, ServiceCallsList[scID][2] - Project details
var Picture = {};

$(document).ready(function () {
    $("#LoginBTN").click(Login);

    //some css settings
    $("#LoginBTN").parent().css({ "width": "36%", "margin": "auto" });
    $("#ProblemDescriptionTA").val(""); //fix extra space issue

    //Google map for service calls and projects
    InitializeGoogleMap();
    $("#MapBTN").click(function () {
        Goto("ServiceCallsMap");
//        ResizeMapCanvas();
    });
});

$(window).resize(ResizeMapCanvas);

function ResizeMapCanvas() {
    var iWindowWidth = $.mobile.activePage.width();
    var iWindowHeight = $.mobile.activePage.height();
    $("#map-canvas").width(iWindowWidth).height(iWindowHeight);
}

$(document).on("click", ".HatchBTN", function () {
    var sHref = $(this).attr("href");
    var sHatchID = sHref.substring(6, sHref.length + 1);
    BuildHatchPage(sHatchID);
});

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
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/Login',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            if (data.d == "true") {
                $("#UserName, #Password").val("");
                HideLoading();
                window.location = "#MainMenuPage";
                GetProjectsList(); //  read all the projects
                GetOpenedServiceCalls();
                GetProjectsNamesList();
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
    });                   // end of ajax call
}

//-----------------------------------------------------------------------
// Load the projects to client-side
//-----------------------------------------------------------------------
function GetProjectsList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/GetProjectsList',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            ProjectsList = $.parseJSON(data.d);
            $("#ProjectsList").html(GetProjectDetails(ProjectsList));

            GetProjectHatches(ProjectsList);

            var newPage = $(BuildHatchesListPerProject());
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

            //$("#ProjectsList").listview("refresh"); // this is important for the jQueryMobile to assign the style to a dynamically added list
        }, // end of success
        error: function (e) {
            alert("failed to load projects :( " + e.responseText);
        } // end of error
    });       // end of ajax call
}

//----------------------------------------------------------------------------
// build the Projects page
// ProjectsList contains all the names of the projects
//----------------------------------------------------------------------------
function GetProjectDetails(ProjectsList) {
    for (var i = 0; i < ProjectsList.length; i++) { // run on all the files in the list
        $.ajax({ // ajax call start
            url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/GetProjectDetails',
            data: "{ pID: " + ProjectsList[i] + "}", // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                p = $.parseJSON(data.d); // parse the data as json
                Projects[p[1].pID] = MergeObjects(p);
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to get project details: " + e.responseText);
            } // end of error
        });         // end of ajax call
    } // end of loop on all the projects

    str = "";
    for (var pID in Projects) {
        str += BuildProjectsList(pID); // add item to the list in the main projects page
        BuildProjectPage(pID); // build a page for each project
    }
    return str;
}

//------------------------------------------------------
// build projects list items
//------------------------------------------------------
function BuildProjectsList(pID) {
    var str = "";
    str += "<li><a data-ajax = 'false' href= '#Project" + pID + "'>";
    str += "<h1>" + Projects[pID].Name + "</h1>";
    str += "<p>" + Projects[pID].StatusName + "</p>";
    str += "</a></li>";
    return str;
}

//----------------------------------------------------------------------------
// build a page per project
//----------------------------------------------------------------------------
function BuildProjectPage(pID) {
    var p = Projects[pID];

    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'Project" + pID + "'>";
    // build the header

    str += BuildProjectHeader(p.Name);

    // add the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הלקוח</h2>";
    str += "<p><b>שם הלקוח: </b>" + p.Fname + " " + p.Lname + "</p>";
    if (!IsEmpty(p.Phone)) str += "<p><b>טלפון: </b>" + p.Phone + "</p>";
    str += "<p><b>טלפון נייד: </b>" + p.Mobile + "</p>";
    if (!IsEmpty(p.Fax)) str += "<p><b>פקס: </b>" + p.Fax + "</p>";
    if (!IsEmpty(p.Email)) str += "<p><b>דוא&quot;ל: </b>" + p.Email + "</p>";

    str += "<h2>פרטי הפרויקט</h2>";
    str += "<p><b>סטטוס: </b>" + p.StatusName + "</p>";
    str += "<p><b>עלות: </b>" + p.Cost + "</p>";
    if (!IsEmpty(p.Comments)) str += "<p><b>הערות: </b>" + p.Comments + "</p>";

    str += "<h2>אנשי קשר</h2>";
    if (!IsEmpty(p.ContractorName)) str += "<p><b>קבלן: </b>" + p.ContractorName + "  " + p.ContractorPhone + "</p>";
    if (!IsEmpty(p.ArchitectName)) str += "<p><b>אדריכל: </b>" + p.ArchitectName + "  " + p.ArchitectPhone + "</p>";
    if (!IsEmpty(p.SupervisorName)) str += "<p><b>מפקח: </b>" + p.SupervisorName + "  " + p.SupervisorPhone + "</p><br/>";

    str += "<a class = 'HatchesBTN' href='#HatchesOfProject" + pID + "' data-role='button'>צפה בפתחים</a>";

    str += "</div>";  // close the content
    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
    //hatches button design
    $(".HatchesBTN").css({ "width": "55%", "margin": "auto" });
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
function GetProjectHatches(ProjectsList) {
    for (var i = 0; i < ProjectsList.length; i++) { // run on all the files in the list
        $.ajax({ // ajax call start
            url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/GetProjectHatches',
            data: "{ pID: " + ProjectsList[i] + "}", // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                var h = $.parseJSON(data.d); // parse the data as json
                if (h.length != 0) {
                    var ProjID = h[0][0].pID;
                    Hatches[ProjID] = MergeInsideArrays(h);
                }
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to get project's hatches: " + e.responseText);
            } // end of error
        });             // end of ajax call
    } // end of loop on all the hatches
}

//------------------------------------------------------
// build Hatches list items
//------------------------------------------------------
function BuildHatchesList(ProjID) {
    var str = "";
    for (var j in Hatches[ProjID]) {
        str += "<li><a class = 'HatchBTN' data-ajax = 'false' href = '#Hatch" + Hatches[ProjID][j].HatchID + "'>";
        str += "<h1>פתח מס' " + Hatches[ProjID][j].HatchID + "</h1>";
        str += "<p>" + Hatches[ProjID][j].HatchStatus + "</p>";
        str += "</a></li>";
    }
    return str;
}

//------------------------------------------------------
// build Hatches list per project
//------------------------------------------------------
function BuildHatchesListPerProject() {
    str = "";
    for (var pID in Projects) {
        //Projects[Project][1].HatchesImageURL
        str += '<div data-role="page" id="HatchesOfProject' + pID + '">';
        str += '<div data-role="header" data-theme="a"><h1>' + Projects[pID].Fname + ' ' + Projects[pID].Lname + '</h1>';
        str += '<a href="#Project' + pID + '" data-icon="back" data-iconpos="notext" style="border: none;"></a>';
        str += '<a href="#HatchesImage' + pID + '" data-rel="popup" data-icon="info" data-iconpos="notext" style="border: none;"></a></div>'; //end of header
        str += '<div data-role="content">';
        str += '<ul id="HatchesList" data-role="listview" data-theme="c" data-inset="true" data-filter="true" data-filter-placeholder = "חפש פתח...">';
        str += BuildHatchesList(pID);
        str += "</ul>"; // end of ul

        str += '</br><div id="HatchesImage' + pID + '" data-role="popup" class = "photopopup">';
        str += '<a href="#HatchesOfProject' + pID + '" data-role = "button" data-icon="delete" data-iconpos = "notext" class="ui-corner-all ui-shadow ui-btn-a ui-btn-right" style = "border:none;" ></a>';
        str += '<img src = "' + Projects[pID].HatchesImageURL + '" /></div>';

        str += "</div>"; // end of content
        str += "</div>"; // end of page
    }
    return str;
}

//----------------------------------------------------------------------------
// build the Hatch details page
//----------------------------------------------------------------------------
function BuildHatchPage(HatchID) {
    $.ajax({ // ajax call start
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/GetPicsAndPins',
        data: "{ HatchID : " + HatchID + "}", // Send value of the project id
        dataType: 'json', // Choosing a JSON datatype for the data sent
        type: 'POST',
        async: false, // this is a synchronous call
        contentType: 'application/json; charset = utf-8', // for the data received
        success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
        {
            var pnp = $.parseJSON(data.d); // parse the data as json
            if (!IsEmpty(pnp)) {
                pnp = MergeInsideArrays(pnp);
                var hID = pnp[0].HatchID;
                //                    if (typeof PicsAndPins[hID] != "object")
                PicsAndPins[hID] = {};
                for (var i in pnp) {
                    var picID = pnp[i].PictureID;
                    PicsAndPins[hID][picID] = pnp[i];
                }
            }
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get Pictures and Pins: " + e.responseText);
        } // end of error
    });                    // end of ajax call
    for (var pID in Projects)
        for (var hID in Hatches[pID])
            BuildHatchDetailsPage(Hatches[pID][hID]); // build a page for each hatch
//    $('.HatchNavbar').navbar('refresh');
}

function BuildHatchDetailsPage(oHatch) {
    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'Hatch" + oHatch.HatchID + "'>";

    // build the header
    var sHeaderText = "פתח מס' " + oHatch.HatchID;
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1>" + sHeaderText + "</h1>";
    str += "<a href='#HatchesOfProject" + oHatch.pID + "' data-icon='back' data-iconpos = 'notext' style = 'border:none;'></a>";
    str += "</div>"; //close the header

    // add the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הפתח</h2>";
    str += "<p><b>סטטוס: </b>" + oHatch.HatchStatus + "</p>";
    str += "<p><b>סוג הפתח: </b>" + oHatch.HatchType + "</p>";

    //    str += '</br><a href = "#myPopup" data-role = "button" data-rel="popup">Popup Image</a>';

    //    str += '</br><div id="myPopup" data-role="popup" class = "photopopup">';
    //    str += '<a href="#Hatch' + iHatchID + '" data-role = "button" data-icon="delete" data-iconpos = "notext" class="ui-corner-all ui-shadow ui-btn-a ui-btn-right" style = "border:none;" ></a>';
    //    str += '<img src = "' + Projects[iProjID][1].HatchesImageURL + '" /></div>';
    str += BuildHatchDialog(oHatch.HatchID);

    str += "</div>" // close the content
    str += "<div data-role='footer' data-position='fixed' data-theme='a'>";
    str += "<div class = 'HatchNavbar' data-role='navbar'>";
    str += "<ul>";
    str += "<li><a data-role = 'button' data-rel='popup' class = 'ui-icon-camera-white' href='#Hatch" + oHatch.HatchID + "Dialog' data-position-to='window'>צלם תמונה</a></li>";
    str += "<li><a data-ajax = 'false' href='#PicturesOfHatch" + oHatch.HatchID + "' class = 'HatchPicturesBTN' data-icon='grid' data-iconpos='left'>תמונות</a></li>";
    str += "<li><a data-ajax = 'false' href='#QAForHatch" + oHatch.HatchID + "' data-icon='star'>בקרת איכות</a></li>";
    str += "</ul>";
    str += "</div>"; // close the navbar
    str += "</div>"; // close the footer

    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
}

function BuildHatchDialog(hID) {
    var str = "";
    str += '<div data-role="popup" id="Hatch' + hID + 'Dialog" class = "CloseServiceCallsPopup">';
    str += '<div data-role="header">';
    str += "<h1>תמונה חדשה</h1>";
    str += '</div>';
    str += '<div data-role="main" class="ui-content">';
    str += "<p><b>תיאור התמונה: </b>" + BuildPictureDescTextBox(hID) + "</p></br>";
    str += '<a data-role="button" data-inline="true" data-theme="a" onclick="TakePicturePrepare(' + hID + ')">צלם תמונה</a>';
    str += '<a id = "Hatch' + hID + 'CancelButton" data-role="button" onclick = "CloseHatchDialog(' + hID + ')" data-inline="true">בטל</a>';
    str += '</div>';
    return str;
}

function CloseHatchDialog(hID) {
    $("#Hatch" + hID + "PicDesc").val("");
    Goto("Hatch" + hID);
}

function BuildPictureDescTextBox(hID) {
    var str = '<input type="text" id="Hatch' + hID + 'PicDesc" value="" />';
    return str;
}

function GetOpenedServiceCalls() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/GetOpenedServiceCalls',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            sc = $.parseJSON(data.d);
            for (var i in sc)
                ServiceCallsList[sc[i][0].ScID] = sc[i];
            $("#ServiceCallsList").html(BuildServiceCallsList(ServiceCallsList));
            for (var scID in ServiceCallsList)
                BuildServiceCallPage(ServiceCallsList[scID]);
            BuildServiceCallProjectsList();
            PopulateGoogleMap();
        }, // end of success
        error: function (e) {
            alert("failed to load Service calls" + e.responseText);
        } // end of error
    });               // end of ajax call
}

function BuildServiceCallsList(ServiceCallsList) {
    var str = "";
    for (var scID in ServiceCallsList) {
        str += "<li><a data-ajax = 'false' href= '#ServiceCall" + scID + "'>";
        str += "<h1>" + ServiceCallsList[scID][1].Fname + " " + ServiceCallsList[scID][1].Lname + "</h1>";
        //        str += "<p>" + ServiceCallsList[scID][1].City + "</p>";
        str += "</a></li>";
    }
    return str;
}

function BuildServiceCallPage(oServiceCall) {
    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'ServiceCall" + oServiceCall[0].ScID + "'>";
    // build the header

    var sHeaderText = oServiceCall[1].Fname + " " + oServiceCall[1].Lname;
    str += BuildServiceCallHeader(sHeaderText);

    // add the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הלקוח</h2>";
    str += "<p><b>שם הלקוח: </b>" + sHeaderText + "</p>";
    if (!IsEmpty(oServiceCall[1].Phone)) str += "<p><b>טלפון: </b>" + oServiceCall[1].Phone + "</p>";
    str += "<p><b>טלפון נייד: </b>" + oServiceCall[1].Mobile + "</p>";
    if (!IsEmpty(oServiceCall[1].Fax)) str += "<p><b>פקס: </b>" + oServiceCall[1].Fax + "</p>";
    if (!IsEmpty(oServiceCall[1].Email)) str += "<p><b>דוא&quot;ל: </b>" + oServiceCall[1].Email + "</p>";

    str += "<h2>פרטי הקריאה</h2>";
    if (oServiceCall[0].Urgent) str += "<p><b>*קריאה דחופה*</b></p>";
    str += "<p><b>תיאור התקלה: </b>" + oServiceCall[0].Description + "</p>";
    str += "<p><b>תאריך פתיחה: </b>" + ConvertToDate(oServiceCall[0].DateOpened) + "</p><br/>";
    //    if (!IsEmpty(oServiceCall[0].DateClosed)) str += "<p><b>תאריך סגירה: </b>" + ConvertToDate(oServiceCall[0].DateClosed) + "</p>";
    str += "<a data-role='button' data-rel='popup' class = 'half' href='#ServiceCall" + oServiceCall[0].ScID + "Dialog' data-position-to='window'>סגור קריאת שירות</a>";
    str += BuildServiceCallDialog(oServiceCall[0].ScID);

    str += "</div>";  // close the content
    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
}

function BuildServiceCallDialog(scID) {
    var str = "";
    str += '<div data-role="popup" id="ServiceCall' + scID + 'Dialog" class = "CloseServiceCallsPopup">';
    str += '<div data-role="header">';
    str += "<h1>סגור קריאה</h1>";
    str += '</div>';
    str += '<div data-role="main" class="ui-content">';
    str += '<p>האם אתה בטוח?</p>';
    str += '<a data-role="button" data-inline="true" data-theme="a" onclick="CloseServiceCall(' + scID + ')">סגור קריאה</a>';
    str += '<a href="#ServiceCall' + scID + '" data-inline="true" data-role="button">בטל</a>';
    str += '</div>';
    return str;
}

function CloseServiceCall(scID) {
    dataString = "{ scID: '" + scID + "'}";
    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/CloseServiceCall',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            var iRowsAffected = $.parseJSON(data.d);
            if (iRowsAffected > 0) {
                alert("קריאת השירות נסגרה בהצלחה");
                Goto("ServiceCallsPage");
                $("#ServiceCallsPage").find("[href='#ServiceCall" + scID + "']").closest("li").remove();
            }
            else
                alert("אירעה שגיאה בשרת, אנא נסה מאוחר יותר");
        }, // end of success
        error: function (e) {
            alert("failed to close Service call" + e.responseText);
        } // end of error
    });                       // end of ajax call
}

function BuildServiceCallHeader(sHeaderText) {
    var str = "";
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1>" + sHeaderText + "</h1>";
    str += "<a href='#ServiceCallsPage' data-icon='back' data-iconpos = 'notext' style = 'border:none;'></a>";
    str += "</div>"; //close the header
    return str;
}

function GetProjectsNamesList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/GetProjectsNames',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            ProjectsNamesList = $.parseJSON(data.d);
        }, // end of success
        error: function (e) {
            alert("failed to load Projects names list :( " + e.responseText);
        } // end of error
    });           // end of ajax call
}

function BuildServiceCallProjectsList() {
    var str = "";
    for (var i in ProjectsNamesList) {
        str += "<option value='" + i + "'>" + ProjectsNamesList[i] + "</option>";
    }
    $("#ServiceCallProjectsDDL").html(str);
}

function PrepareServiceCall() {
    var iProjectID = $("#ServiceCallProjectsDDL option:selected").val();
    var sProblemDescription = $.trim($("#ProblemDescriptionTA").val());
    var bUrgent = $("#UrgentCB").val() == "on" ? true : false;
    var sCurrentDate = GetCurrentDate();
    if (IsEmpty(sProblemDescription)) {
        alert("יש להזין את תיאור התקלה טרם פתיחת קריאה");
        return;
    }
    var ServiceCallDetails = {
        ProjectID: iProjectID,
        ProblemDescription: sProblemDescription,
        Date: sCurrentDate,
        Urgent: bUrgent
    };
    CreateServiceCall(ServiceCallDetails);
    Goto("ServiceCallsMainPage");
}

function CreateServiceCall(oServiceCallDetails) {
    dataString = JSON.stringify(oServiceCallDetails);
    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/CreateServiceCall',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            if (data.d > 0)
                alert("קריאת השירות נוצרה בהצלחה");
            //                CurrentPageData = [];
        }, // end of success
        error: function (e) {
            alert("failed to Create service call :( " + e.responseText);
        } // end of error
    });              // end of ajax call
}

//Misc
function IsEmpty(o) {
    return (o == "" || o == null);
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

function MergeObjects(Arr) {
    var ArrLength = Arr.length;
    for (var i = 0; i < ArrLength - 1; i++) {
        $.extend(Arr[0], Arr[i + 1]);
    }
    return Arr[0];
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

    if (dd < 10)
        dd = '0' + dd;

    if (mm < 10)
        mm = '0' + mm;

    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function GetObjectSize(obj) {
    var size = 0, key;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) size++;
    }
    return size;
}

/** Google API **/
var Map;
//var InfoWindow;
var oPosition = {};

function InitializeGoogleMap() {
    var mapOptions = {
        center: new google.maps.LatLng(32.434046, 34.919652),
        zoom: 12
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

function UploadPicture() {
    //    dataString = "{ HatchID: '" + Picture.HatchID + "', PictureDesc: '" + Picture.PictureDesc + "', DateTaken: '" + Picture.DateTaken + "', ImageURL: '" + Picture.ImageURL + "' }";
    dataString = JSON.stringify(Picture);
    //    alert(dataString);
    $.ajax({ // ajax call starts
        url: 'http://proj.ruppin.ac.il/igroup9/prod/MaestroWS.asmx/UploadPicture',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            HideLoading();
            data.d > 0 ? alert("התמונה הועלתה בהצלחה") : alert("אירעה שגיאה בשרת, אנא נסה מאוחר יותר");
        }, // end of success
        error: function (e) {
            alert("failed to upload picture: " + e.responseText);
        } // end of error
    });                       // end of ajax call
}

//PhoneGap functions

function TakePicturePrepare(hID) {
    Picture.HatchID = hID;
    Picture.PictureDesc = $.trim($("#Hatch" + hID + "PicDesc").val());
    $("#Hatch" + hID + "PicDesc").val("");
    $("#Hatch" + hID + "CancelButton").click();
    TakePicture();
}

function TakePicture() {
    navigator.camera.getPicture(
    uploadPhoto,
    function (message) { alert('get picture failed' + message); },
    {
        quality: 50,
        destinationType: navigator.camera.DestinationType.FILE_URI,
        sourceType: navigator.camera.PictureSourceType.CAMERA
    });  // PhoneGap method for retrieving an image from the phone's camera
} // Get Picture

function uploadPhoto(imageURI) {
    ShowLoading("מעלה תמונה"); // Start the spinning "working" animation
    var options = new FileUploadOptions(); // PhoneGap object to allow server upload
    options.fileKey = "file";
    var iPicIndex = GetObjectSize(PicsAndPins[Picture.hID]) + 1;
    //    alert("iPicIndex: " + iPicIndex);
    options.fileName = $.mobile.activePage.attr("id") + "_" + iPicIndex; // file name
    //    alert("filename: " + options.fileName);
    options.mimeType = "image/jpeg"; // file type
    var params = {}; // Optional parameters
    params.value1 = "test";
    params.value2 = "param";
    options.params = params; // add parameters to the FileUploadOptions object

    Picture.DateTaken = GetCurrentDate();
    Picture.ImageURL = "http://proj.ruppin.ac.il/igroup9/prod/images/hatches/" + options.fileName + ".jpg";
    //    alert("URL: " + Picture.ImageURL);
    var ft = new FileTransfer();
    ft.upload(imageURI, encodeURI("http://proj.ruppin.ac.il/igroup9/prod/images/hatches/ReturnValue.ashx"), win, fail, options); // Upload
} // Upload Photo

function win() {
    HideLoading();
    UploadPicture();
}

function fail(error) {
    HideLoading();
    alert("An error has occurred: Code = " + error.code);
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