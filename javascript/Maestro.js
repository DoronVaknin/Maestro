$(function () {
    /* For zebra striping */
    $("table tr:nth-child(odd)").addClass("odd-row");
    /* For cell text alignment */
    $("table td:first-child, table th:first-child").addClass("first");
    /* For removing the last border */
    $("table td:last-child, table th:last-child").addClass("last");
});

$(document).ready(function () {
    var sFileName = $(location).attr('href').replace(/^.*[\\\/]/, '');
    sFileName = sFileName.substring(0, sFileName.length - 5);
    $("#" + sFileName).addClass("current");
});