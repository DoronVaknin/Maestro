var pinID = 0;
var CurrentPinID;
var aPins = [];

function pinObject(x, y, message, audioPath, videoPath, PictureID) {
    var obj = new Object();
    obj.x = x;
    obj.y = y;
    obj.message = message;
    obj.audioPath = audioPath;
    obj.videoPath = videoPath;
    obj.PictureID = PictureID;
    return obj;
}

function pinHtmlString(id) {
    sHTML = "";
    sHTML += '<div id="pinWraper' + id + '"';
    sHTML += ' style="top:0; left:0; position:absolute;"><input type="image" src="images/pin.png" width="18" height="18" id="pinId'
			+ id + '"';
    sHTML += ' onclick="openPinDialog(' + id + ')"/></div>';
    return sHTML;
}

function pinDialogHtml() {
    sHTML = "";
    sHTML += "<div data-role='page' id='PinDialogMainPage'>";
    sHTML += "<div data-role='header'><h1>הוסף מזכר</h1></div>";
    sHTML += "<div data-role='main' class='ui-content'>";
    sHTML += "<a data-rel='dialog' href='#PinDialogWriteMessage' data-role='button' data-transition='flip'>כתוב הערה</a>";
    sHTML += "<a onclick='recordMessage()' data-transition='flip' data-role='button'>הקלט הערה</a>";
    sHTML += "<a data-rel='dialog' onclick='recordVideo()' data-transition='flip' data-role='button'>צלם וידאו</a>";
    sHTML += "<a data-rel='dialog' onclick='pinDelete()' data-transition='flip' data-role='button'>מחק</a>";
    sHTML += "</div></div>";

    sHTML += "<div data-role='page' id='PinDialogWriteMessage'>";
    sHTML += "<div data-role='header'><h1>הוסף מזכר</h1></div>";
    sHTML += "<div data-role='main' class='ui-content'>";
    sHTML += "<div style='width: 100%;'><input id='inputMessage' style='width: 100%; display: block; margin-left: auto; margin-right: auto;' type='text' name='message' value=''/></div>";
    sHTML += "<a onclick='pinSaveMessage()' data-role='button'>שמור</a>";
    sHTML += "</div></div>";

    return sHTML;
}

function recordMessage() {
    navigator.device.capture.captureAudio(captureSuccess, captureError, { limit: 2 });

    function captureSuccess(mediaFiles) {
        var i, len;
        for (i = 0, len = mediaFiles.length; i < len; i++) {
            uploadFile(mediaFiles[i]);
        }
    }

    function captureError(error) {
        var msg = 'An error occurred during capture: ' + error.code;
        navigator.notification.alert(msg, null, 'Uh oh!');
    }

    // Upload files to server
    function uploadFile(mediaFile) {
        path = mediaFile.fullPath,
            name = mediaFile.name;
    }
}

function recordVideo() {
    navigator.device.capture.captureVideo(captureSuccess, captureError, { limit: 2 });

    function captureSuccess(mediaFiles) {
        var i, len;
        for (i = 0, len = mediaFiles.length; i < len; i++) {
            uploadFile(mediaFiles[i]);
        }
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

function openPinDialog(id) {
    CurrentPinID = id;
    $("#Container").append(pinDialogHtml());
    $.mobile.changePage("#PinDialogMainPage", { role: "dialog" });
}

function pinSaveMessage() {
    var sMessage = $("#inputMessage").val();
    aPins[CurrentPinID].message = sMessage;
    $('.ui-dialog').dialog('close');
}

function pinDelete() {
    (elem = document.getElementById("pinWraper" + CurrentPinID)).parentNode.removeChild(elem);
    delete aPins[CurrentPinID];
    $('.ui-dialog').dialog('close');
}

function imgOnclick(e, img) {
    var offset = $(img).offset();
    var x = ((e.clientX - offset.left) - 1);
    var y = ((e.clientY - offset.top) - 18);

    var controlsElm = document.getElementById('controls');
    controlsElm.insertAdjacentHTML('beforeend', pinHtmlString(pinID));

    var pinWraper = document.getElementById('pinWraper' + pinID);
    pinWraper.style.left = x + "px";
    pinWraper.style.top = y + "px";

    var sPictureID = $(img).attr("id");
    sPictureID = sPictureID.substr(7);
    var iPictureID = parseInt(sPictureID);

    aPins.push(pinObject(x, y, "", "", "", iPictureID));
    openPinDialog(pinID);

    pinID++;
}

function SavePins() {
    for (var pinID in aPins) {
        if (IsEmptyPin(aPins[pinID])) continue;
        dataString = JSON.stringify(aPins[pinID]);
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
                    alert("עריכת התמונה בוצעה בהצלחה");
            }, // end of success
            error: function (e) { // this function will be called upon failure
                alert("failed to save pins: " + e.responseText);
            } // end of error
        });              // end of ajax call
    }

    //    var controlsElem = document.getElementById('controls1');
    //    loadPins1(aPins, controlsElem);
}

function IsEmptyPin(oPin) {
    return (IsEmpty(oPin.message) && IsEmpty(oPin.audioPath) && IsEmpty(oPin.videoPath));
}

function IsEmpty(o) {
    return (o == "" || o == null);
}

function loadPins1(pinObj, controlsElem) {
    for (var i = 0; i < pinObj.length; i++) {
        if (pinObj[i] != undefined) {
            var x = pinObj[i].x;
            var y = pinObj[i].y;

            var boolean = false;
            var which;
            if (pinObj[i].message != "" && (pinObj[i].audioPath != "" || pinObj[i].videoPath != "")) {
                boolean = false;

            } else if (pinObj[i].audioPath != "" && pinObj[i].videoPath != "") {
                boolean = false;
            } else if (pinObj[i].message != "") {
                boolean = true;
                which = 0; //message
            } else if (pinObj[i].audioPath != "") {
                boolean = true;
                which = 1; //audio path
            } else if (pinObj[i].videoPath != "") {
                boolean = true;
                which = 2; //video path
            }

            controlsElem.insertAdjacentHTML('beforeend', pinHtmlString1(i, boolean, which));

            var pinWraper = document.getElementById('pinWraper1' + i);
            pinWraper.style.left = x + "px";
            pinWraper.style.top = y + "px";
        }
    }
}

//function pinHtmlString1(id, boolean, which) {
//    sHTML = "";
//    sHTML += '<div id="pinWraper1' + id + '"';
//    sHTML += ' style="top:0; left:0; position:absolute;"><input type="image" src="images/pin.png" width="18" height="18"';
//    sHTML += ' onclick="openPinDialog1(' + id + ',' + boolean + ',' + which + ')"/></div>';
//    return sHTML;
//}

//function openPinDialog1(id, boolean, which) {
//    var message = aPins[id].message;
//    var audioPath = aPins[id].audioPath;
//    var videoPath = aPins[id].videoPath;

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