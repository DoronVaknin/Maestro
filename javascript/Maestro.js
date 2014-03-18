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


function ReadOnlyChanging() {
    document.getElementById('ContentPlaceHolder3_txtID').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtFirstName').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtLastName').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtArchitectName').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtContractorName').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtContractorMobile').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtArchitectMobile').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtCustomerFax').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtCustomerMobile').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtCustomerMobile').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtCustomerPhone').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtAdress').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtCity').removeAttribute('disabled', 'disabled');
    document.getElementById('ContentPlaceHolder3_txtEmail').removeAttribute('disabled', 'disabled');


}
