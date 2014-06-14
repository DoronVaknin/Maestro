/// <reference path="jquery-1.11.0.js" />
/// <reference path="MaestroApp.js" />

var CurrentPinID;
var iNewPins = 0;
var aNewPinsIDs = [];
var iLastPinIdentity = GetTableCurrentIdentity("Pin");
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
    $.mobile.changePage("#PinDialogMainPage", { role: "dialog" });
}

function pinSaveMessage() {
    var sMessage = $("#inputMessage").val();
    Pins[CurrentPinID].message = sMessage;
    $('.ui-dialog').dialog('close');
}

function pinDelete() {
    $("#pinWraper" + CurrentPinID).remove();
    delete Pins[CurrentPinID];
    var bIsNewPin = IsKeyExists(aNewPinsIDs, CurrentPinID);
    if (bIsNewPin) iNewPins--;
    $('.ui-dialog').dialog('close');
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
    debugger;
    var pinID = bIsNewPin ? iLastPinIdentity + 1 + iNewPins : oPin.PinID;

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
    navigator.device.capture.captureAudio(captureSuccess, captureError, { limit: 2 });

    function captureSuccess(mediaFiles) {
        var i, len;
        for (i = 0, len = mediaFiles.length; i < len; i++)
            uploadFile(mediaFiles[i]);
    }

    function captureError(error) {
        var msg = 'אירעה שגיאה בהקלטת ההודעה: ' + error.code;
        navigator.notification.alert(msg, null, 'Uh oh!');
    }

    // Upload files to server
    function uploadFile(mediaFile) {
        path = mediaFile.fullPath;
        name = mediaFile.name;
    }
}

function recordVideo() {
    navigator.device.capture.captureVideo(captureSuccess, captureError, { limit: 2 });

    function captureSuccess(mediaFiles) {
        var i, len;
        for (i = 0, len = mediaFiles.length; i < len; i++)
            uploadFile(mediaFiles[i]);
    }

    function captureError(error) {
        var msg = 'An error occurred during capture: ' + error.code;
        navigator.notification.alert(msg, null, 'Uh oh!');
    }

    function uploadFile(mediaFile) {
        path = mediaFile.fullPath,
            name = mediaFile.name;
    }
}

function SavePins() {
    var iSuccess = 0;
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
                    iSuccess++;
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to save pins: " + e.responseText);
            } // end of error
        });               // end of ajax call
    }
    if (iSuccess > 0)
        alert("עריכת התמונה בוצעה בהצלחה");
}

function IsEmptyPin(oPin) {
    return (IsEmpty(oPin.message) && IsEmpty(oPin.audioPath) && IsEmpty(oPin.videoPath));
}

function IsKeyExists(o, Key) {
    for (var k in o)
        if (k == Key) return true;
}

//function loadPins1(pinObj, controlsElem) {
//    for (var i = 0; i < pinObj.length; i++) {
//        if (pinObj[i] != undefined) {
//            var x = pinObj[i].x;
//            var y = pinObj[i].y;

//            var boolean = false;
//            var which;
//            if (pinObj[i].message != "" && (pinObj[i].audioPath != "" || pinObj[i].videoPath != "")) {
//                boolean = false;

//            } else if (pinObj[i].audioPath != "" && pinObj[i].videoPath != "") {
//                boolean = false;
//            } else if (pinObj[i].message != "") {
//                boolean = true;
//                which = 0; //message
//            } else if (pinObj[i].audioPath != "") {
//                boolean = true;
//                which = 1; //audio path
//            } else if (pinObj[i].videoPath != "") {
//                boolean = true;
//                which = 2; //video path
//            }

//            controlsElem.insertAdjacentHTML('beforeend', pinHtmlString1(i, boolean, which));

//            var pinWraper = document.getElementById('pinWraper1' + i);
//            pinWraper.style.left = x + "px";
//            pinWraper.style.top = y + "px";
//        }
//    }
//}

//function pinHtmlString1(id, boolean, which) {
//    sHTML = "";
//    sHTML += '<div id="pinWraper1' + id + '"';
//    sHTML += ' style="top:0; left:0; position:absolute;"><input type="image" src="images/pin.png" width="18" height="18"';
//    sHTML += ' onclick="openPinDialog1(' + id + ',' + boolean + ',' + which + ')"/></div>';
//    return sHTML;
//}

//function openPinDialog1(id, boolean, which) {
//    var message = Pins[id].message;
//    var audioPath = Pins[id].audioPath;
//    var videoPath = Pins[id].videoPath;

//    if (boolean) {
//        switch (which) {
//            case 0:
//                document.getElementById('messageStr').innerHTML = message;
//                $('#onlyMessagePopup').popup('open');
//                break;
//            case 1:
//                document.getElementById('audioPlayer').src = audioPath;
//                $('#onlyAudioPopup').popup('open');
//                break;
//            case 2:
//                document.getElementById('videoPlayer').src = videoPath;
//                $('#onlyVideoPopup').popup('open');
//                break;
//        }
//    } else {
//        $("body").append(pinDialogHtml1(id, message));
//        $.mobile.changePage("#PinDialogMainPage1", { role: "dialog" });
//    }
//}

//function pinDialogHtml1(id, message) {
//    sHTML = "";
//    sHTML += "<div data-role='page' id='PinDialogMainPage1'>";
//    sHTML += "<div data-role='header'><h1>תבחר משהו</h1></div>";
//    sHTML += "<div data-role='main' class='ui-content'>";
//    sHTML += '<a href="#myPopup" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">הערה</a>';
//    sHTML += '<div data-role="popup" id="myPopup"><p>' + message + '</p></div>';
//    sHTML += "</div></div>";

//    sHTML += "<div data-role='page' id='PinDialogWriteMessage'>";
//    sHTML += "<div data-role='header'><h1>תבחר משהו</h1></div>";
//    sHTML += "<div data-role='main' class='ui-content'>";
//    sHTML += "<div style='width: 100%;'><input style='width: 100%; display: block; margin-left: auto; margin-right: auto;' type='text' name='message' value=''/></div>";
//    sHTML += "</div></div>";

//    sHTML += "<div data-role='page' id='PinDialogRecoredMessage'>";
//    sHTML += "<div data-role='header'><h1>תבחר משהו</h1></div>";
//    sHTML += "<div data-role='main' class='ui-content'>";
//    sHTML += "<a data-rel='dialog' href='#PinDialogMainPage1' data-role='button' data-transition='flip'>תכתוב הערה</a>";
//    sHTML += "<a onclick='dialogRecoredMessage()' data-role='button'>תקליט הערה</a><a onclick='dialogRecoredVideo()' data-role='button'>צלם וידיאו</a><a onclick='pinDelete()' data-role='button'>תמחק</a>";
//    sHTML += "</div></div>";

//    sHTML += "<div data-role='page' id='PinDialogRecoredVideo'>";
//    sHTML += "<div data-role='header'><h1>תבחר משהו</h1></div>";
//    sHTML += "<div data-role='main' class='ui-content'>";
//    sHTML += "<a data-rel='dialog' href='#PinDialogMainPage1' data-role='button' data-transition='flip'>תכתוב הערה</a>";
//    sHTML += "<a onclick='dialogRecoredMessage()' data-role='button'>תקליט הערה</a><a onclick='dialogRecoredVideo()' data-role='button'>צלם וידיאו</a><a onclick='pinDelete()' data-role='button'>תמחק</a>";
//    sHTML += "</div></div>";

//    return sHTML;
//}

//$(document).ready(function () {
//    $('#loadFile').on("change", gotPic);
//    $('#takePic').on("change", gotPic);
//    $('#').on("change", gotVideo);
//});

//function gotPic(event) {
//    if (event.target.files.length == 1 && event.target.files[0].type.indexOf("image/") == 0) {
//        $(".myImage").attr("src", URL.createObjectURL(event.target.files[0]));
//    }
//}

//function gotVideo(event) {
//    if (event.target.files.length == 1 &&
//           event.target.files[0].type.indexOf("mp4/") == 0) {
//        $("#videoId").attr("src", URL.createObjectURL(event.target.files[0]));
//    }
//}

//function phonegapTakePic() {
//    navigator.camera.getPicture(onSuccess, onFail, {
//        quality: 50
//    });

//    function onSuccess(imageData) {
//        var image = document.getElementById('myImage');
//        image.src = "data:image/jpeg;base64," + imageData;
//    }

//    function onFail(message) {
//        alert('Failed because: ' + message);
//    }
//}

//function phonegapUploadPic() {

//}

//function takepic() {
//    navigator.camera.getPicture(onSuccess, onFail, {
//        quality: 50
//    });

//    function onSuccess(imageData) {
//        var image = document.getElementById('myImage');
//        image.src = "data:image/jpeg;base64," + imageData;
//    }

//    function onFail(message) {
//        alert('Failed because: ' + message);
//    }
//}