var idNumber = 0;
var pinObjArray = [];
var currentPinId;

function pinObject(x, y, message, audioPath, videoPath) {
	var obj = new Object();
	obj.x = x;
	obj.y = y;
	obj.message = message;
	obj.audioPath = audioPath;
	obj.videoPath = videoPath;
	return obj;
}

function pinHtmlString(id) {
	htmlString = "";
	htmlString += '<div id="pinWraper' + id + '"';
	htmlString += ' style="top:0; left:0; position:absolute;"><input type="image" src="images/pin.png" width="18" height="18" id="pinId'
			+ id + '"';
	htmlString += ' onclick="openPinDialog(' + id + ')"/></div>';
	return htmlString;
}

function pinDialogHtml() {
    htmlString = "";
    htmlString += "<div data-role='page' id='PinDialogMainPage'>";
    htmlString += "<div data-role='header'><h1>הוסף מזכר</h1></div>";
    htmlString += "<div data-role='main' class='ui-content'>";
    htmlString += "<a data-rel='dialog' href='#PinDialogWriteMessage' data-role='button' data-transition='flip'>כתוב הערה</a>";
    htmlString += "<a onclick='recordMessage()' data-transition='flip' data-role='button'>הקלט הערה</a>";
    htmlString += "<a data-rel='dialog' onclick='recordVideo()' data-transition='flip' data-role='button'>צלם וידאו</a>";
    htmlString += "<a data-rel='dialog' onclick='pinDelete()' data-transition='flip' data-role='button'>מחק</a>";
    htmlString += "</div></div>";

    htmlString += "<div data-role='page' id='PinDialogWriteMessage'>";
    htmlString += "<div data-role='header'><h1>הוסף מזכר</h1></div>";
    htmlString += "<div data-role='main' class='ui-content'>";
    htmlString += "<div style='width: 100%;'><input id='inputMessage' style='width: 100%; display: block; margin-left: auto; margin-right: auto;' type='text' name='message' value=''/></div>";
    htmlString += "<a onclick='pinSaveMessage()' data-role='button'>שמור</a>";
    htmlString += "</div></div>";

    return htmlString;
}

function recordMessage() {
    navigator.device.capture.captureAudio(captureSuccess, captureError, { limit: 2 });

    function captureSuccess(mediaFiles) {
        var i, len;
        for (i = 0, len = mediaFiles.length; i < len; i += 1) {
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
        for (i = 0, len = mediaFiles.length; i < len; i += 1) {
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
    currentPinId = id;
    $("body").append(pinDialogHtml());
	$.mobile.changePage( "#PinDialogMainPage", { role: "dialog" } );
}

function pinSaveMessage() {
    var inputMessage = document.getElementById('inputMessage').value;
    pinObjArray[currentPinId].message = inputMessage;
    $('.ui-dialog').dialog('close');
}

function pinDelete() {
    (elem = document.getElementById("pinWraper" + currentPinId)).parentNode
			.removeChild(elem);
    delete pinObjArray[currentPinId];
    $('.ui-dialog').dialog('close');
}

function imgOnclick(e, img) {
	var offset = $(img).offset();
	var x = ((e.clientX - offset.left) -1);
	var y = ((e.clientY - offset.top) - 18);

	var controlsElm = document.getElementById('controls');
	controlsElm.insertAdjacentHTML('beforeend', pinHtmlString(idNumber));

	var pinWraper = document.getElementById('pinWraper' + idNumber);
	pinWraper.style.left = x + "px";
	pinWraper.style.top = y + "px";

	pinObjArray.push(pinObject(x, y, "", "", ""));
	openPinDialog(idNumber);

	idNumber++;
}







function loadPins() {
    var controlsElem = document.getElementById('controls1');
    loadPins1(pinObjArray, controlsElem);
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

function pinHtmlString1(id, boolean, which) {
    htmlString = "";
    htmlString += '<div id="pinWraper1' + id + '"';
    htmlString += ' style="top:0; left:0; position:absolute;"><input type="image" src="images/pin.png" width="18" height="18"';
    htmlString += ' onclick="openPinDialog1(' + id + ',' + boolean + ',' + which + ')"/></div>';
    return htmlString;
}

function openPinDialog1(id, boolean, which) {
    var message = pinObjArray[id].message;
    var audioPath = pinObjArray[id].audioPath;
    var videoPath = pinObjArray[id].videoPath;
    
    if (boolean) {
        switch (which) {
            case 0:
                document.getElementById('messageStr').innerHTML = message;
                $('#onlyMessagePopup').popup('open'); 
                break;
            case 1:
                document.getElementById('audioPlayer').src = audioPath;
                $('#onlyAudioPopup').popup('open');
                break;
            case 2:
                document.getElementById('videoPlayer').src = videoPath;
                $('#onlyVideoPopup').popup('open');
                break;
        }     
    } else {
        $("body").append(pinDialogHtml1(id, message));
        $.mobile.changePage("#PinDialogMainPage1", { role: "dialog" });
    }
}


function pinDialogHtml1(id, message) {
    htmlString = "";
    htmlString += "<div data-role='page' id='PinDialogMainPage1'>";
    htmlString += "<div data-role='header'><h1>תבחר משהו</h1></div>";
    htmlString += "<div data-role='main' class='ui-content'>";
    htmlString += '<a href="#myPopup" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">הערה</a>';
    htmlString += '<div data-role="popup" id="myPopup"><p>' + message + '</p></div>';
    htmlString += "</div></div>";

    htmlString += "<div data-role='page' id='PinDialogWriteMessage'>";
    htmlString += "<div data-role='header'><h1>תבחר משהו</h1></div>";
    htmlString += "<div data-role='main' class='ui-content'>";
    htmlString += "<div style='width: 100%;'><input style='width: 100%; display: block; margin-left: auto; margin-right: auto;' type='text' name='message' value=''/></div>";
    htmlString += "</div></div>";

    htmlString += "<div data-role='page' id='PinDialogRecoredMessage'>";
    htmlString += "<div data-role='header'><h1>תבחר משהו</h1></div>";
    htmlString += "<div data-role='main' class='ui-content'>";
    htmlString += "<a data-rel='dialog' href='#PinDialogMainPage1' data-role='button' data-transition='flip'>תכתוב הערה</a>";
    htmlString += "<a onclick='dialogRecoredMessage()' data-role='button'>תקליט הערה</a><a onclick='dialogRecoredVideo()' data-role='button'>צלם וידיאו</a><a onclick='pinDelete()' data-role='button'>תמחק</a>";
    htmlString += "</div></div>";

    htmlString += "<div data-role='page' id='PinDialogRecoredVideo'>";
    htmlString += "<div data-role='header'><h1>תבחר משהו</h1></div>";
    htmlString += "<div data-role='main' class='ui-content'>";
    htmlString += "<a data-rel='dialog' href='#PinDialogMainPage1' data-role='button' data-transition='flip'>תכתוב הערה</a>";
    htmlString += "<a onclick='dialogRecoredMessage()' data-role='button'>תקליט הערה</a><a onclick='dialogRecoredVideo()' data-role='button'>צלם וידיאו</a><a onclick='pinDelete()' data-role='button'>תמחק</a>";
    htmlString += "</div></div>";

    return htmlString;
}




$(document).ready(function () {
    $('#loadFile').on("change", gotPic);
    $('#takePic').on("change", gotPic);
    $('#').on("change", gotVideo);
});

function gotPic(event) {
    if (event.target.files.length == 1 && event.target.files[0].type.indexOf("image/") == 0) {
        $("#myImage").attr("src", URL.createObjectURL(event.target.files[0]));
    }
}

function gotVideo(event) {
    if (event.target.files.length == 1 &&
           event.target.files[0].type.indexOf("mp4/") == 0) {
        $("#videoId").attr("src", URL.createObjectURL(event.target.files[0]));
    }
}



function phonegapTakePic() {
    navigator.camera.getPicture(onSuccess, onFail, {
        quality: 50
    });

    function onSuccess(imageData) {
        var image = document.getElementById('myImage');
        image.src = "data:image/jpeg;base64," + imageData;
    }

    function onFail(message) {
        alert('Failed because: ' + message);
    }
}

function phonegapUploadPic() {

}

















function takepic() {
    navigator.camera.getPicture(onSuccess, onFail, {
        quality: 50
    });

    function onSuccess(imageData) {
        var image = document.getElementById('myImage');
        image.src = "data:image/jpeg;base64," + imageData;
    }

    function onFail(message) {
        alert('Failed because: ' + message);
    }
}