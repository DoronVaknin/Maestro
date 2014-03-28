﻿// Tables Zebra design

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
    $(".datepicker").datepicker();
});

function ClickLoginBTN() {
    $("#LoginBTN").click();
}

function EnableCustomerDetails() {
    $("#CustomerDetailsTBL input").removeAttr("disabled");
    $("#ContentPlaceHolder3_EditCustomerDetailsBTN").hide();
    $("#ContentPlaceHolder3_SaveCustomerDetailsBTN").show();
}

function EnableProjectDetails() {
    $("#ProjectDetailsTBL *").removeAttr("disabled");
    $("#ContentPlaceHolder3_EditProjectDetailsBTN").hide();
    $("#ContentPlaceHolder3_SaveProjectDetailsBTN").show();
}

$(document).ready(function () {
    $("#CustomerDetailsTBL input").attr("disabled", "disabled");
    $("#ProjectDetailsTBL *").attr("disabled", "disabled");
});

function ValidateNewCustomer() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerId", function (s) { return s.length < 8 || !isInteger(s); },false,"יש להזין מספר ת.ז תקין");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerFirstName", function (s) { return s.length < 2; }, false, "השם הפרטי קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerLastName", function (s) { return s.length < 2; }, false, "שם המשפחה קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerAddress", function (s) { return s.length < 2; }, false, "כתובת המגורים קצרה מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerCity", function (s) { return s.length < 2; }, false, "שם העיר קצר מדי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_CustomerEmail", function (s) { !IsEmail(s); }, false, "יש להזין כתובת דוא&#34;ל חוקית");
    if (bIsValid)
        $("#ContentPlaceHolder3_CreateCustomer").click();
}

function ValidateNewProject() {
    var bIsValid = true;
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectDateOpened", function (s) { return !isValidDate(s); }, false, "יש להזין תאריך חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectPrice", function (s) { return !isNumber(s); }, false, "יש להזין מחיר חוקי");
    bIsValid &= MarkInvalid("#ContentPlaceHolder3_ProjectHatches", function (s) { return !isInteger(s); },false,"יש להזין מספר פתחים שלם");
    if (bIsValid)
        $("#ContentPlaceHolder3_CreateProject").click();
}

function MarkInvalid(id, cb, bSelector, sMessage) {
    var sValue = $.trim($(id).val());
    var bInvalid = cb(sValue);
    if (bSelector) {
        $(id).toggleClass("Invalid", bInvalid);
        //$(id).prev().find(".InvalidText").toggle(bInvalid);
    } else {
        $(id).toggleClass("Invalid", bInvalid);
        if (bInvalid && sMessage != "") {
            $(".ErrorLabel").html(sMessage);
            //$(id).parent().prev().find(".InvalidText").toggle(bInvalid);
        }
    }
    return !bInvalid;
}

function IsEmail(email) {
    if (email == "") return false;
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

//$(document).ready(function () {
//    if(!isPostBack())
//    for (var i = 0; i < 51; i++) {
//        $('#ContentPlaceHolder3_ShuttersCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_CollectedCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_ValimCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_UCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_ShoeingCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_EnginCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_ProtectedSpaceCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_GlassCount').append(new Option(i, i));
//        $('#ContentPlaceHolder3_BoxesCount').append(new Option(i, i));
//    }
//});

//function isPostBack() { //function to check if page is a postback-ed one
//  return document.getElementById('_ispostback').value == 'True';
//}


//DRAG & DROP
function sendFileToServer(formData, status) {
    var uploadURL = window.location.href.substring(0, window.location.href.lastIndexOf("/") + 1); //Upload URL
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

// datatables.net

//$(document).ready(function () {
//    $('#ContentPlaceHolder3_OrdersGrid').dataTable();
//});