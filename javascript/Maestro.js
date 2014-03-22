//$(function () {
//    /* For zebra striping */
//    $(".DataTables tr:nth-child(odd)").addClass("odd-row");
//    /* For cell text alignment */
//    $(".DataTables td:first-child, table th:first-child").addClass("first");
//    /* For removing the last border */
//    $(".DataTables td:last-child, table th:last-child").addClass("last");
//});

$(document).ready(function () {
    var sFileName = $(location).attr('href').replace(/^.*[\\\/]/, '');
    sFileName = sFileName.substring(0, sFileName.length - 5);
    $("#" + sFileName).addClass("current");
});

function ClickLoginBTN() {
    $("#LoginBTN").click();
}



function EnableTextBoxes() {
    $("#CustomerDetails input").removeAttr("disabled");
}

function EnableProjectStatus() {
    $("#ContentPlaceHolder3_ProjectStatusDDL").removeAttr("disabled");
    $("#ContentPlaceHolder3_txtProjectPrice").removeAttr("disabled");
    $("#ContentPlaceHolder3_txtProjectComment").removeAttr("disabled");
    
}



$(document).ready(function () {
    $("#CustomerDetails input").prop("disabled", true);
    $("#ContentPlaceHolder3_ProjectStatusDDL").prop("disabled", true);
    $("#ContentPlaceHolder3_txtHatchesNum").prop("disabled", true);
    $("#ContentPlaceHolder3_txtProjectPrice").prop("disabled", true);
    $("#ContentPlaceHolder3_txtProjectComment").prop("disabled", true);
    
});


