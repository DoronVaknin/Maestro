/// <reference path="jquery-1.11.0.js" />

var Projects = {};  //Projects[ProjectID][0] - Customer details, Projects[ProjectID][1] - Project details, Projects[ProjectID][2] - Project status details
var ProjectsList = {};
var Hatches = {};

$(document).ready(function () {
    $("#LoginBTN").click(function () {
        Login();
    });
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
                LoadProjectsList(); //  read all the projects
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
function LoadProjectsList() {
    dataString = "";
    $.ajax({ // ajax call starts
        url: 'MaestroWS.asmx/LoadProjectsList',   // JQuery call to the server side method
        data: dataString,    // the parameters sent to the server
        type: 'POST',        // can be post or get
        dataType: 'json',    // Choosing a JSON datatype
        contentType: 'application/json; charset = utf-8', // of the data received
        success: function (data) // Variable data contains the data we get from serverside
        {
            ProjectsList = $.parseJSON(data.d);
            $("#ProjectsList").append(buildProjectsPage(ProjectsList));

            BuildHatchesPage(ProjectsList);
            $("body").append(BuildHatchesListPerProject());

            //$("#ProjectsList").listview("refresh"); // this is important for the jQueryMobile to assign the style to a dynamically added list
        }, // end of success
        error: function (e) {
            alert("failed to load projects :( " + e.responseText);
        } // end of error
    });     // end of ajax call
}

//----------------------------------------------------------------------------
// build the Projects page
// ProjectsList contains all the names of the projects
//----------------------------------------------------------------------------
function buildProjectsPage(ProjectsList) {
    for (var i = 0; i < ProjectsList.length; i++) { // run on all the files in the list
        $.ajax({ // ajax call start
            url: 'MaestroWS.asmx/GetProjectDetails',
            data: "{ pID: " + ProjectsList[i].Pid1 + "}", // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                p = $.parseJSON(data.d); // parse the data as json
                Projects[p[1].Pid1] = p;
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to get project details: " + e.responseText);
            } // end of error
        });    // end of ajax call
    } // end of loop on all the projects

    str = "";
    for (var i in Projects) {
        str += BuildProjectsList(i); // add item to the list in the main projects page
        BuildProjectPage(Projects[i][1].Pid1); // build a page for each project
    }
    return str;
}

//------------------------------------------------------
// build projects list items
//------------------------------------------------------
function BuildProjectsList(i) {
    var str = "";
    str += "<li><a data-ajax = 'false' href= '#Project" + Projects[i][1].Pid1 + "'>";
    str += "<h1>" + Projects[i][0].Fname + " " + Projects[i][0].Lname + "</h1>";
    str += "<p>" + Projects[i][2].Statusname + "</p>";
    str += "</a></li>";
    return str;
}

//----------------------------------------------------------------------------
// build a page per project
//----------------------------------------------------------------------------
function BuildProjectPage(ProjectID) {
    var p = Projects[ProjectID];
    var Customer = p[0];
    var Project = p[1];
    var ProjectStatus = p[2];

    var str = "";
    // build a page
    str += "<div data-role = 'page' id = 'Project" + ProjectID + "'>";
    // build the header

    var CustomerFullName = Customer.Fname + " " + Customer.Lname;
    str += BuildHeader(CustomerFullName);

    // add the content div
    str += "<div data-role = 'content'>";
    str += "<h2>פרטי הלקוח</h2>";
    str += "<p><b>שם הלקוח: </b>" + CustomerFullName + "</p>";
    if (!IsEmpty(Project.Phone)) str += "<p><b>טלפון: </b>" + Customer.Phone + "</p>";
    str += "<p><b>טלפון נייד: </b>" + Customer.Mobile + "</p>";
    if (!IsEmpty(Project.Fax)) str += "<p><b>פקס: </b>" + Customer.Fax + "</p>";
    if (!IsEmpty(Project.Email)) str += "<p><b>דוא&quot;ל: </b>" + Customer.Email + "</p>";

    str += "<h2>פרטי הפרויקט</h2>";
    str += "<p><b>סטטוס: </b>" + ProjectStatus.Statusname + "</p>";
    str += "<p><b>עלות: </b>" + Project.Price + "</p>";
    if (!IsEmpty(Project.Comment1)) str += "<p><b>הערות: </b>" + Project.Comment1 + "</p>";

    str += "<h2>אנשי קשר</h2>";
    if (!IsEmpty(Project.ContractorName1)) str += "<p><b>קבלן: </b>" + Project.ContractorName1 + "  " + Project.ContractorPhone1 + "</p>";
    if (!IsEmpty(Project.ArchitectName1)) str += "<p><b>אדריכל: </b>" + Project.ArchitectName1 + "  " + Project.ArchitectPhone1 + "</p>";
    if (!IsEmpty(Project.SupervisorName1)) str += "<p><b>מפקח: </b>" + Project.SupervisorName1 + "  " + Project.SupervisorPhone1 + "</p><br/>";

    str += "<a class = 'HatchesBTN' href='#HatchesOfProject" + ProjectID + "' data-role='button'>צפה בפתחים</a>";

    str += "</div>" // close the content
    str += "</div>" // close the page

    //append it to the page container
    var newPage = $(str);
    newPage.appendTo($.mobile.pageContainer);

    //    buildStudentsPage(p.groupCode);
    //    buildScreenShotsPage(p.groupCode);
}

//----------------------------------------------------------------------------
// build a common header for the page
//----------------------------------------------------------------------------
function BuildHeader(headerText) {
    var str = "";
    str += "<div data-role = 'header' data-position='fixed' data-theme='a'>";
    str += "<h1>" + headerText + "</h1>";
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
            data: "{ pID: " + ProjectsList[i].Pid1 + "}", // Send value of the project id
            dataType: 'json', // Choosing a JSON datatype for the data sent
            type: 'POST',
            async: false, // this is a synchronous call
            contentType: 'application/json; charset = utf-8', // for the data received
            success: function (data) // this method is called upon success. Variable data contains the data we get from serverside
            {
                var h = $.parseJSON(data.d); // parse the data as json
                if (h.length != 0) {
                    var ProjID = h[0][0].Pid1;
                    Hatches[ProjID] = MakeAssociativeArray(h);
                }
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to get project's hatches: " + e.responseText);
            } // end of error
        });          // end of ajax call
    } // end of loop on all the projects

    //    str = "";
    //    for (var ProjID in Hatches) {
    //        str += BuildHatchesList(ProjID); // add item to the list in the main projects page
    //        //        BuildHatchPage(Hatches[ProjID]); // build a page for each project
    //    }
    //    return str;
}

//------------------------------------------------------
// build Hatches list items
//------------------------------------------------------
function BuildHatchesList(ProjID) {
    var str = "";
    for (var j in Hatches[ProjID]) {
        str += "<li><a data-ajax = 'false' href= '#Hatch" + Hatches[ProjID][j].HatchID + "'>";
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
    for (var Project in Projects) {
        str += '<div data-role="page" id="HatchesOfProject' + Projects[Project][1].Pid1 + '">';
        str += '<div data-role="header" data-theme="a"><h1>' + Projects[Project][0].Fname + ' ' + Projects[Project][0].Lname + '</h1>';
        str += '<a href="#Project' + Projects[Project][1].Pid1 + '" data-icon="back" data-iconpos="notext" style="border: none;"></a></div>';
        str += '<div data-role="content">';
        str += '<ul id="HatchesList" data-role="listview" data-theme="c" data-inset="true" data-filter="true">';
        str += BuildHatchesList(Projects[Project][1].Pid1);
        str += "</ul>"; // end of ul
        str += "</div>"; // end of content
        str += "</div>"; // end of page
    }
    return str;
}


//Utility Functions
function IsEmpty(o) {
    return (o == "" || o == null);
}

function MakeAssociativeArray(Arr) {
    var UnifiedArr = {};
    for (var i = 0; i < Arr.length; i++) {
        var TempArr = {};
        //        var counter = 0;
        for (var j = 0; j < Arr[i].length; j++) {
            for (var field in Arr[i][j]) {
                TempArr[field] = eval("Arr[i][j]." + field);
                //                counter++;
            }
        }
        UnifiedArr[Arr[i][1].HatchID] = TempArr;
    }
    return UnifiedArr;
}