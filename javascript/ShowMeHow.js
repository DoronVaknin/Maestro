
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