/// <reference path="jquery-1.11.0.js" />

var Projects = {};
var ProjectsNamesList = {};
var Hatches = {};
var PicsAndPins = {};  //Pictures & Pins
var ServiceCallsList = {};  //ServiceCallsList[scID][0] - Service call details, ServiceCallsList[scID][1] - Customer details, ServiceCallsList[scID][2] - Project details
var Picture = {};
var iCurrentTableID;

$(document).ready(function () {
    GetProjectsNamesList();
    GetProjects(); //  read all the projects
    GetHatches();
    GetOpenedServiceCalls();

    $("#ProblemDescriptionTA").val(""); //fix extra space issue
    $("#EditPinBTN").click(function () {
        $.mobile.changePage("#PinDialogMainPage", { role: "dialog" });
    });
});

$(document).on("mobileinit", function () {
    // Setting #Container div as a jQuery Mobile pageContainer
    $.mobile.pageContainer = $('#Container');
    // Setting default page transition to slide
    $.mobile.defaultPageTransition = 'slide';
});

$(window).resize(ResizeMapCanvas);

function ResizeMapCanvas() {
    var iWindowWidth = $.mobile.activePage.width();
    var iWindowHeight = $.mobile.activePage.height();
    $("#map-canvas").width(iWindowWidth).height(iWindowHeight);
}

$(document).on("click", ".HatchBTN", function () {
    var sHatchID = $(this).attr("href");
    sHatchID = sHatchID.substring(6, sHatchID.length + 1);
    var sProjectID = $.mobile.activePage.attr("id");
    sProjectID = sProjectID.substring(16, sProjectID.length + 1);
    var iLength = $('#Hatch' + sHatchID).length;
    if (iLength == 0) // Build Hatch page only once
        BuildHatchPage(sHatchID, sProjectID);
});

$(document).on("click", "#MapBTN", function () {
    Goto("HybridMap");
    setTimeout(ResizeMapCanvas, 200);
    setTimeout(InitializeGoogleMap, 500);
    setTimeout(PopulateGoogleMap, 800);
});

$(document).on("click", ".ImagesContainer img", function () {
    var $Image = $(this).clone();
    var sImageURL = $Image.attr("src");
    var iIndex = sImageURL.lastIndexOf("/");
    sImageURL = sImageURL.substr(iIndex + 1);
    iIndex = sImageURL.indexOf("_");
    var sPicID = sImageURL.substring(iIndex + 1, sImageURL.length - 4); // extract picture id only without the extension
    $Image.attr("id", "Picture" + sPicID);
    var sFullPathHatchID = $.mobile.activePage.attr("id");
    $("#PictureEdit a[data-icon='back']").attr("href", "#" + sFullPathHatchID); // #PicturesOfHatch_
    $Image.attr("onclick", "CreateNewPin(event, this)");
    $("#ImageHolder").html($Image);
    var sHatchID = sFullPathHatchID.substr(sFullPathHatchID.length - 1);
    Pins = {};
    $("#controls").html("");
    LoadPins(sHatchID, sPicID);
    Goto("PictureEdit");
});

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
            $("#ProjectsList").html(BuildProjectsList());
            for (var pID in Projects)
                BuildProjectPage(Projects[pID]);
            //            BuildServiceCallProjectsList();

        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project details: " + e.responseText);
        } // end of error
    });             // end of ajax call
}

//------------------------------------------------------
// build projects list items
//------------------------------------------------------
function BuildProjectsList() {
    var str = "";
    for (var pID in Projects) {
        str += "<li><a data-ajax = 'false' href= '#Project" + pID + "'>";
        str += "<h1>" + Projects[pID].Name + "</h1>";
        str += "<p>" + Projects[pID].StatusName + "</p>";
        str += "</a></li>";
    }
    return str;
}

//----------------------------------------------------------------------------
// build a page per project
//----------------------------------------------------------------------------
function BuildProjectPage(oProject) {
    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'Project" + oProject.pID + "'>";

    // build the header
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1>" + oProject.Name + "</h1>";
    str += "<a href='#ProjectsPage' data-icon='back' data-iconpos = 'notext' style = 'border:none;'></a>";
    str += "</div>"; //close the header

    // build the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הלקוח</h2>";
    str += "<p><b>שם הלקוח: </b>" + oProject.Fname + " " + oProject.Lname + "</p>";
    if (!IsEmpty(oProject.Phone)) str += "<p><b>טלפון: </b>" + oProject.Phone + "</p>";
    str += "<p><b>טלפון נייד: </b>" + oProject.Mobile + "</p>";
    if (!IsEmpty(oProject.Fax)) str += "<p><b>פקס: </b>" + oProject.Fax + "</p>";
    if (!IsEmpty(oProject.Email)) str += "<p><b>דוא&quot;ל: </b>" + oProject.Email + "</p>";

    str += "<h2>פרטי הפרויקט</h2>";
    str += "<p><b>סטטוס: </b>" + oProject.StatusName + "</p>";
    str += "<p><b>עלות: </b>" + oProject.Cost + "</p>";
    if (!IsEmpty(oProject.Comments)) str += "<p><b>הערות: </b>" + oProject.Comments + "</p>";

    str += "<h2>אנשי קשר</h2>";
    if (!IsEmpty(oProject.ContractorName)) str += "<p><b>קבלן: </b>" + oProject.ContractorName + "  " + oProject.ContractorPhone + "</p>";
    if (!IsEmpty(oProject.ArchitectName)) str += "<p><b>אדריכל: </b>" + oProject.ArchitectName + "  " + oProject.ArchitectPhone + "</p>";
    if (!IsEmpty(oProject.SupervisorName)) str += "<p><b>מפקח: </b>" + oProject.SupervisorName + "  " + oProject.SupervisorPhone + "</p><br>";

    str += "<a class = 'HatchesBTN' href='#HatchesOfProject" + oProject.pID + "' data-role='button'>צפה בפתחים</a>";

    str += "</div>";  // close the content
    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
    //hatches button design
    $(".HatchesBTN").css({ "width": "55%", "margin": "auto" });
}

//----------------------------------------------------------------------------
// build Hatch pages
//----------------------------------------------------------------------------
function GetHatches() {
    dataString = "";
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetHatches',
        data: dataString,
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
                    Hatches[h[i].pID] = {};
                Hatches[h[i].pID][h[i].HatchID] = h[i];
            }
            BuildHatchesListPerProject();
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get project's hatches: " + e.responseText);
        } // end of error
    });   // end of ajax call
}

//------------------------------------------------------
// build Hatches list items
//------------------------------------------------------
function BuildHatchesList(pID) {
    var sHTML = "";
    for (var hID in Hatches[pID]) {
        sHTML += "<li><a class = 'HatchBTN' data-ajax = 'false' href = '#Hatch" + Hatches[pID][hID].HatchID + "'>";
        sHTML += "<h1>פתח מס' " + Hatches[pID][hID].HatchID + "</h1>";
        sHTML += "<p>" + Hatches[pID][hID].HatchStatus + "</p>";
        sHTML += "</a></li>";
    }
    return sHTML;
}

//------------------------------------------------------
// build Hatches list per project
//------------------------------------------------------
function BuildHatchesListPerProject() {
    var str = "";
    for (var pID in Projects) {
        str += '<div data-role="page" id="HatchesOfProject' + pID + '">';
        str += '<div data-role="header" data-theme="a"><h1>' + Projects[pID].Fname + ' ' + Projects[pID].Lname + '</h1>';
        str += '<a href="#Project' + pID + '" data-icon="back" data-iconpos="notext" style="border: none;"></a>';
        if (!IsEmpty(Projects[pID].HatchesImageURL))
            str += '<a href="#HatchesImage' + pID + '" data-rel="popup" data-icon="info" data-iconpos="notext" style="border: none;"></a>';
        str += '</div>'; //end of header
        str += '<div data-role="content">';
        str += '<ul id="HatchesListProject' + pID + '" data-role="listview" data-theme="c" data-inset="true" data-filter="true" data-filter-placeholder="חפש פתח...">';
        str += BuildHatchesList(pID);
        str += "</ul>"; // end of ul

        str += '<br><div id="HatchesImage' + pID + '" data-role="popup" class = "photopopup">';
        str += '<a href="#HatchesOfProject' + pID + '" data-role = "button" data-icon="delete" data-iconpos = "notext" class="ui-corner-all ui-shadow ui-btn-a ui-btn-right" style = "border:none;" ></a>';
        str += '<img src = "' + Projects[pID].HatchesImageURL + '" /></div>';

        str += "</div>"; // end of content
        str += "</div>"; // end of page
    }
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
}

//----------------------------------------------------------------------------
// build the Hatch details page
//----------------------------------------------------------------------------
function BuildHatchPage(sHatchID, sProjectID) {
    $.ajax({ // ajax call start
        url: 'MaestroWS.asmx/GetPicsAndPins',
        data: "{ HatchID : " + sHatchID + "}", // Send value of the project id
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
                PicsAndPins[hID] = {};
                for (var i in pnp) {
                    var picID = pnp[i].PictureID;
                    if (typeof PicsAndPins[hID][picID] != "object")
                        PicsAndPins[hID][picID] = [];
                    PicsAndPins[hID][picID].push(pnp[i]);
                }
            }
        }, // end of success
        error: function (e) { // this function will be called upon failure
            alert("failed to get Pictures and Pins: " + e.responseText);
        } // end of error
    });  // end of ajax call

    BuildHatchDetailsPage(Hatches[sProjectID][sHatchID]);
    BuildHatchPicturesPage(sProjectID, sHatchID);
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

    str += BuildHatchDialog(oHatch.HatchID);

    str += "</div>" // close the content
    str += "<div data-role='footer' data-position='fixed' data-theme='a'>";
    str += "<div class = 'HatchNavbar' data-role='navbar'>";
    str += "<ul>";
    str += "<li><a data-role = 'button' data-rel='popup' class = 'ui-icon-camera-white' href='#Hatch" + oHatch.HatchID + "Dialog' data-position-to='window'>צלם תמונה</a></li>";
    str += "<li><a data-ajax = 'false' href='#PicturesOfHatch" + oHatch.HatchID + "' class = 'HatchPicturesBTN' data-icon='grid' data-iconpos='left'>תמונות</a></li>";
    //    str += "<li><a data-ajax = 'false' href='#QAForHatch" + oHatch.HatchID + "' data-icon='star'>בקרת איכות</a></li>";
    str += "</ul>";
    str += "</div>"; // close the navbar
    str += "</div>"; // close the footer

    str += "</div>";  // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);
}

//----------------------------------------------------------------------------
// build images page per hatch
//----------------------------------------------------------------------------
function BuildHatchPicturesPage(pID, hID) {
    var str = "";

    // build a page
    str += "<div data-role = 'page' id = 'PicturesOfHatch" + Hatches[pID][hID].HatchID + "'>";

    // build the header
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1> תמונות - פתח " + Hatches[pID][hID].HatchID + "</h1>";
    str += "<a href='#Hatch" + Hatches[pID][hID].HatchID + "' data-icon='back' data-iconpos = 'notext' style = 'border:none;'></a>";
    str += "</div>"; //close the header

    // build the content div
    str += "<div data-role = 'content'>";
    str += "<div class='ImagesContainer'>";

    for (var picID in PicsAndPins[hID])
        str += "<img id = 'Picture" + PicsAndPins[hID][picID][0].PictureID + "' src='" + PicsAndPins[hID][picID][0].ImageURL + "' />";

    str += "</div>";

    str += "</div>";  // close the content
    str += "</div>";  // close the page
    //        }
    //    }

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
    str += "<p><b>תיאור התמונה: </b>" + BuildPictureDescTextBox(hID) + "</p><br>";
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
            $("#ServiceCallsList").html(BuildServiceCallsList(ServiceCallsList));
            for (var scID in ServiceCallsList)
                BuildServiceCallPage(ServiceCallsList[scID]);
            BuildServiceCallProjectsList();
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
        str += "<p>" + ServiceCallsList[scID][1].Address + "</p>";
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
    str += "<p><b>תאריך פתיחה: </b>" + ConvertToDate(oServiceCall[0].DateOpened) + "</p><br>";
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
        url: 'MaestroWS.asmx/CloseServiceCall',   // JQuery call to the server side method
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
        url: 'MaestroWS.asmx/GetProjectsNames',   // JQuery call to the server side method
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
        url: 'MaestroWS.asmx/CreateServiceCall',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            if (data.d > 0)
                alert("קריאת השירות נוצרה בהצלחה");
        }, // end of success
        error: function (e) {
            alert("failed to Create service call :( " + e.responseText);
        } // end of error
    });              // end of ajax call
}

//Misc
function IsEmpty(o) {
    return (o === "" || o === null || o === undefined);
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

function GetTableCurrentIdentity(sTableName) {
    var oTableName = { TableName: sTableName };
    dataString = JSON.stringify(oTableName);
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/GetTableCurrentIdentity',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            iCurrentTableID = $.parseJSON(data.d);
        }, // end of success
        error: function (e) {
            alert("failed to get table identity: " + e.responseText);
        } // end of error
    });
    return iCurrentTableID;
}

/** Google Maps **/
var Map;
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
    var Image = "images/red-pin.png";
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
                '<p><b>כתובת: </b>' + ServiceCallsList[sID][1].Address + '</p>' +
                '<p><b>תיאור התקלה: </b>' + ServiceCallsList[sID][0].Description + '</p>' +
                '</div>' +
                '</div>';

    var InfoWindow = new google.maps.InfoWindow({
        content: sContent
    });

    google.maps.event.addListener(Marker, 'click', function () {
        InfoWindow.open(Map, Marker);
    });
}

function ShowProjectPin(oPosition, pID) {
    var Position = new google.maps.LatLng(oPosition.lat, oPosition.lng);
    var Image = "images/blue-pin.png";
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
    dataString = JSON.stringify(Picture);
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/UploadPicture',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        async: false,
        success: function (data) // Variable data contains the data we get from serverside
        {
            HideLoading();
            var iRowAffected = $.parseJSON(data.d);
            alert(iRowAffected > 0 ? "התמונה הועלתה בהצלחה" : "אירעה שגיאה בשרת, אנא נסה מאוחר יותר");
        }, // end of success
        error: function (e) {
            HideLoading();
            alert("failed to upload picture: " + e.responseText);
        } // end of error
    });                       // end of ajax call
}

/** PhoneGap **/
function TakePicturePrepare(hID) {
    Picture.HatchID = parseInt(hID);
    Picture.PictureDesc = $.trim($("#Hatch" + hID + "PicDesc").val());
    $("#Hatch" + hID + "PicDesc").val("");
    $("#Hatch" + hID + "CancelButton").click();
    TakePicture();
}

function TakePicture() {
    navigator.camera.getPicture(
    uploadPhoto,
    function (message) { alert('failed to get picture' + message); },
    {
        quality: 75,
        destinationType: navigator.camera.DestinationType.FILE_URI,
        sourceType: navigator.camera.PictureSourceType.CAMERA
    });  // PhoneGap method for retrieving an image from the phone's camera
} // Get Picture

function uploadPhoto(imageURI) {
    ShowLoading("מעלה תמונה"); // Start the spinning "working" animation
    var options = new FileUploadOptions(); // PhoneGap object to allow server upload
    options.fileKey = "file";
    var iPicIndex = GetTableCurrentIdentity("Picture") + 1;
    options.fileName = $.mobile.activePage.attr("id") + "_" + iPicIndex; // file name
    options.mimeType = "image/jpeg"; // file type
    var params = {}; // Optional parameters
    params.value1 = "test";
    params.value2 = "param";
    options.params = params; // add parameters to the FileUploadOptions object

    Picture.ImageURL = "http://proj.ruppin.ac.il/igroup9/prod/files/Hatches/" + options.fileName + ".jpg";
    var ft = new FileTransfer();
    ft.upload(imageURI, encodeURI("http://proj.ruppin.ac.il/igroup9/prod/files/Hatches/SaveImage.ashx"), win, fail, options); // Upload

    function win() {
        UploadPicture();
    }

    function fail(error) {
        HideLoading();
        alert("An error has occurred: Code = " + error.code);
    }
} // Upload Photo

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


/** Show Me How - Pin's Interface **/
var CurrentPinID;
var iNewPins = 0;
var aNewPinsIDs = [];
var aDeletedPinsIDs = [];
var Pins = {};

function pinObject(pinID, x, y, message, audioPath, videoPath, PictureID) {
    var oPin = new Object();
    oPin.pinID = pinID;
    oPin.x = x;
    oPin.y = y;
    oPin.message = message;
    oPin.audioPath = audioPath;
    oPin.videoPath = videoPath;
    oPin.PictureID = PictureID;
    return oPin;
}

function LoadPins(sHatchID, sPicID) {
    var hID = sHatchID;
    var picID = sPicID;
    for (var i in PicsAndPins[hID][picID]) {
        var x = PicsAndPins[hID][picID][i].CoordinateX;
        var y = PicsAndPins[hID][picID][i].CoordinateY;
        var Image = $("#ImageHolder img");
        CreatePin(x, y, Image, PicsAndPins[hID][picID][i]);
    }
}

function BuildPin(sID) {
    sHTML = "";
    sHTML += '<div id="pinWraper' + sID + '" style="position:absolute;">';
    sHTML += '<input type="image" src="images/pin.png" id="Pin' + sID + '" onclick="openPinDialog(' + sID + ')"/>';
    sHTML += '</div>';
    return sHTML;
}

function openPinDialog(pinID) {
    CurrentPinID = pinID;
    var sPinComment = !IsEmpty(Pins[pinID].message) ? "<b>הערה: </b>" + Pins[pinID].message : "טרם הוזנה הערה עבור פין זה";
    $("#PinComment").html(sPinComment); // Fill note

    var bShowAudioPlayer = !IsEmpty(Pins[pinID].audioPath);
    if (bShowAudioPlayer)
        $("#AudioPlayer").attr("src", Pins[pinID].audioPath).show(); // Fill audio recording
    else
        $("#AudioPlayer").hide();
    $("#AudioLabel").toggle(!bShowAudioPlayer);

    var bShowVideoPlayer = !IsEmpty(Pins[pinID].videoPath);
    if (bShowVideoPlayer)
        $("#VideoPlayer").attr("src", Pins[pinID].videoPath).show(); // Fill video recording
    else
        $("#VideoPlayer").hide();
    $("#VideoLabel").toggle(!bShowVideoPlayer);

    $.mobile.changePage(IsEmptyPin(Pins[pinID]) ? "#PinDialogMainPage" : "#PinDialogMultimedia", { role: "dialog" });
}

function pinSaveComment() {
    var sPinComment = $("#PinCommentTB").val();
    Pins[CurrentPinID].message = sPinComment;
    $("#PinComment").html("<b>" + sPinComment + "</b>"); // Fill note
    $("#PinCommentTB").val("");
    $('.ui-dialog').dialog('close');
}

function pinDelete() {
    $("#pinWraper" + CurrentPinID).remove();
    delete Pins[CurrentPinID];
    aDeletedPinsIDs.push(CurrentPinID);
    var bIsNewPin = IsKeyExists(aNewPinsIDs, CurrentPinID);
    if (bIsNewPin) {
        iNewPins--;
        delete aNewPinsIDs[CurrentPinID];
    }
    Goto("PictureEdit");
}

function CreateNewPin(e, oImage) { // Creates a new pin on image click
    var offset = $(oImage).offset();
    var x = ((e.clientX - offset.left) - 1);
    var y = ((e.clientY - offset.top) + 20);

    var pinID = CreatePin(x, y, oImage);
    openPinDialog(pinID);
}

function CreatePin(x, y, oImage, oPin) {
    var bIsNewPin = IsEmpty(oPin);
    var pinID = bIsNewPin ? GetTableCurrentIdentity("Pin") + 1 + iNewPins : oPin.PinID;

    $('#controls').append(BuildPin(pinID));
    $('#pinWraper' + pinID).css("left", x).css("top", y);

    var sPictureID = $(oImage).attr("id");
    sPictureID = sPictureID.substr(7);
    var iPictureID = parseInt(sPictureID);

    var sComment = bIsNewPin ? "" : oPin.Comment;
    var sAudioPath = bIsNewPin ? "" : oPin.AudioURL;
    var sVideoPath = bIsNewPin ? "" : oPin.VideoURL;

    var Pin = pinObject(pinID, x, y, sComment, sAudioPath, sVideoPath, iPictureID);
    Pins[pinID] = Pin;
    if (bIsNewPin) {
        iNewPins++;
        aNewPinsIDs.push(pinID);
    }
    return pinID;
}

function recordMessage() {
    navigator.device.capture.captureAudio(captureSuccess, captureError, { limit: 1 });

    function captureSuccess(mediaFiles) {
        var i, len;
        for (i = 0, len = mediaFiles.length; i < len; i++)
            uploadFile(mediaFiles[i]);
    }

    function captureError(error) {
        var msg = 'אירעה שגיאה בפתיחת הרשמקול: ' + error.code;
        navigator.notification.alert(msg, null, 'Uh oh!');
    }

    // Upload files to server
    function uploadFile(mediaFile) {
        ShowLoading("מעלה שמע");
        var ft = new FileTransfer(),
            path = mediaFile.fullPath,
            name = "Pin" + CurrentPinID;

        ft.upload(path,
            "http://proj.ruppin.ac.il/igroup9/prod/files/Audio/SaveAudio.ashx",
            function (result) {
                Pins[CurrentPinID].audioPath = "http://proj.ruppin.ac.il/igroup9/prod/files/Audio/" + name + ".mp3";
                HideLoading();
                alert("ההקלטה הועלתה בהצלחה");
            },
            function (error) {
                HideLoading();
                alert('Error uploading file ' + path + ': ' + error.code);
            },
            { fileName: name });
    }
}

//function recordVideo() {
//    navigator.device.capture.captureVideo(captureSuccess, captureError, { limit: 1 });

//    function captureSuccess(mediaFiles) {
//        var i, len;
//        for (i = 0, len = mediaFiles.length; i < len; i++)
//            uploadFile(mediaFiles[i]);
//    }

//    function captureError(error) {
//        var msg = 'אירעה שגיאה בפתיחת המצלמה: ' + error.code;
//        navigator.notification.alert(msg, null, 'Uh oh!');
//    }

//    function uploadFile(mediaFile) {
//        ShowLoading("מעלה סרטון");
//        var ft = new FileTransfer(),
//            path = mediaFile.fullPath,
//            name = "Pin" + CurrentPinID;

//        ft.upload(path,
//            "http://proj.ruppin.ac.il/igroup9/prod/files/Video/SaveVideo.ashx",
//            function (result) {
//                Pins[CurrentPinID].videoPath = "http://proj.ruppin.ac.il/igroup9/prod/files/Video/" + name + ".mp4";
//                HideLoading();
//                alert("הסרטון הועלה בהצלחה");
//            },
//            function (error) {
//                HideLoading();
//                alert('Error uploading file ' + path + ': ' + error.code);
//            },
//            { fileName: name });
//    }
//}

function SavePins() {
    function UpdateDeletedPins(iSaveSuccess) {
        var iDeleteSuccess = 0;
        for (var pinID in aDeletedPinsIDs) {
            dataString = { pinID: aDeletedPinsIDs[pinID] };
            dataString = JSON.stringify(dataString);
            $.ajax({ // ajax call start
                url: 'MaestroWS.asmx/DeletePin',
                data: dataString, // Send value of the project id
                dataType: 'json', // Choosing a JSON datatype for the data sent
                type: 'POST',
                async: false, // this is a synchronous call
                contentType: 'application/json; charset = utf-8', // for the data received
                success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
                {
                    if (data.d != "0")
                        iDeleteSuccess++;
                }, // end of success
                error: function (e) { // this function will be called upon failure
                    alert("failed to delete pins: " + e.responseText);
                } // end of error
            });                // end of ajax call
        }
        if (aDeletedPinsIDs.length == 0 && iSaveSuccess > 0 || iDeleteSuccess > 0) {
            alert("עריכת התמונה בוצעה בהצלחה");
            $("#PictureEdit a[data-icon=back]").click(); // Go to gallery page
        }
        else
            alert("עריכת התמונה נכשלה, אנא נסה מאוחר יותר");
    }
    var iSaveSuccess = 0;
    for (var pinID in Pins) {
        if (IsEmptyPin(Pins[pinID])) continue;
        dataString = JSON.stringify(Pins[pinID]);
        $.ajax({ // ajax call start
            url: 'MaestroWS.asmx/InsertNewPin',
            data: dataString, // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                if (data.d != "0")
                    iSaveSuccess++;
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to save pins: " + e.responseText);
            } // end of error
        });               // end of ajax call
    }
    if (iSaveSuccess > 0)
        UpdateDeletedPins(iSaveSuccess);
}

function IsEmptyPin(oPin) {
    return (IsEmpty(oPin.message) && IsEmpty(oPin.audioPath) && IsEmpty(oPin.videoPath));
}

function IsKeyExists(o, Key) {
    for (var k in o)
        if (k == Key) return true;
    return false;
}