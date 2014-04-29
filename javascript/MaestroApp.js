﻿/// <reference path="jquery-1.11.0.js" />

var Projects = {};  //Projects[ProjectID][0] - Customer details, Projects[ProjectID][1] - Project details, Projects[ProjectID][2] - Project status details
var ProjectsList = {};
var Hatches = {};
var PNP = {}; //Pictures & Pins


$(document).ready(function () {
    $("#LoginBTN").click(function () {
        Login();
    });

    //some css settings
    $("#LoginBTN").parent().css({ "width": "36%", "margin": "auto" });
});

function Login() {
    var sUsername = $.trim($("#UserName").val());
    var sPassword = $.trim($("#Password").val());
    if (IsEmpty(sUsername) || IsEmpty(sPassword)) {
        alert("אנא הזן שם משתמש וסיסמה לפני התחברות");
        return;
    }
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
                window.location = "#MainMenuPage";
                GetProjectsList(); //  read all the projects
            }
            else alert("Username or password is incorrect");
        }, // end of success
        error: function (e) {
            alert("failed to login: " + e.responseText);
        } // end of error
    });            // end of ajax call
}

//-----------------------------------------------------------------------
// Load the projects to client-side
//-----------------------------------------------------------------------
function GetProjectsList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetProjectsList',   // JQuery call to the server side method
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

            $(".HatchesBTN").click(function () {
                var sHref = $(this).attr("href");
                var sProjectID = sHref.substring(17, sHref.length + 1);
                BuildHatchPage(sProjectID);
            });

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
            url: 'MaestroWS.asmx/GetProjectDetails',
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
        });        // end of ajax call
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
            url: 'MaestroWS.asmx/GetProjectHatches',
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
        str += "<li><a class = 'HatchBTN' data-ajax = 'false' href= '#Hatch" + Hatches[ProjID][j].HatchID + "'>";
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
            var pnp = $.parseJSON(data.d); // parse the data as json
            if (!IsEmpty(pnp)) {
                var HatchID = pnp[0][0].HatchID;
                PNP = MergeInsideArrays(pnp);
            }
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project details: " + e.responseText);
        } // end of error
    });               // end of ajax call
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

    str += "</div>" // close the content

    str += "<div data-role='footer' data-position='fixed' data-theme='a'>";
    str += "<div class = 'HatchNavbar' data-role='navbar'>";
    str += "<ul>";
    str += "<li><a data-ajax = 'false' href='#TakePhotoHatch" + oHatch.HatchID + "' class = 'ui-icon-camera-white' >צלם תמונה</a></li>";
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

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    today = dd + '/' + mm + '/' + yyyy;
    return today;
}