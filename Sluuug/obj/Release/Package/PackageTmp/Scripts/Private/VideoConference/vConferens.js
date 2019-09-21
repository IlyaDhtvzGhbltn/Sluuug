var audioContext = null;
window.addEventListener("load", onLoad());
const peerConnCfg =
    {
        'iceServers':
        [
            { url: 'stun:stun01.sipphone.com' },
            { url: 'stun:stun.ekiga.net' }
        ]
    };
var localStream;
var remoteVideo = document.querySelector("#remoteVideo_1");
var remoteAudio = document.querySelector("#remoteAudio_1");

const peerConn = new RTCPeerConnection(peerConnCfg);
peerConn.ontrack = function (event) {
    $("#remoteVideo_1")[0].srcObject = event.streams[0];
    $("#remoteAudio_1")[0].srcObject = event.streams[1];
};
peerConn.onicecandidate = function (event) {
    if (event.candidate) {
        
        HUB.invoke('ExchangeICandidates', event.candidate, getGuidID());
        console.log('sending candidates start ...');
    }
    else {
        console.log('all candidates are set');
    }
};

HUB.on('GotInvite', function (guidID, offer) {
    accept_send_answer(guidID, offer);
});
HUB.on('ConfirmInvite', function (guid, answer) {
    got_ansfer(guid, answer);
});
HUB.on('exchangeCandidates', function (candidate) {
    peerConn.addIceCandidate(candidate)
});
HUB.on('Close', function () {
    callClose();
});
HUB.on('SendName', function (name) {
    console.log(name);
    $('.partisipant-name')[0].innerHTML = name;
});


function initiate_call() { 
    navigator.mediaDevices.getUserMedia({ audio: true, video: true })
        .then(function (stream) {
            $("#localVideo")[0].srcObject = stream;

            stream.getTracks().forEach(
                function (track) {
                    peerConn.addTrack(track, stream);
                }
            );
            return peerConn.createOffer();
        })
        .then(
        function (offer) {
            var off = new RTCSessionDescription(offer);
            HUB.invoke('Invite', JSON.stringify(offer), getGuidID() );
            console.log('send invite');
            return peerConn.setLocalDescription(off);
        });
}

function accept_send_answer(guidID, offer) {
    console.log('send answer');
    peerConn.setRemoteDescription(JSON.parse(offer))
        .then(function () {
            navigator.mediaDevices.getUserMedia({ audio: true, video: true })
                .then(function (stream) {
                    $("#localVideo")[0].srcObject = stream;

                    stream.getTracks().forEach(
                        function (track) {
                            peerConn.addTrack(track, stream);
                        }
                    )
                    return peerConn.createAnswer();
                })
                .then(function (answer) {
                    HUB.invoke('ConfirmInvite', guidID, JSON.stringify(answer));
                    return peerConn.setLocalDescription(answer);
                })
                .catch(function (err) {
                    console.log('error!');
                    console.log(err.message);
                });
        })


}

function got_ansfer(guid, answer) {
    peerConn.setRemoteDescription(
        new RTCSessionDescription(JSON.parse(answer)),
        function () {
        },
        function (err) {
            console.log(err.message);
        });
}

async function checType() {
    var id = getGuidID();
    console.log(id);
    const this_type = await
        $.ajax({
            url: '/api/user_vc_role',
            type: 'post',
            data: { converenceID: id }
        });
    return this_type.type;
}

function waitAnimation() {
    console.log('await participant');
}

function getGuidID() {
    var url = new URL(window.location);
    var id = url.searchParams.get("id");
    return id;
}

function closeCallImmediately() {
    connection.start().done(function () {
        HUB.invoke('CloseVideoConverence', getGuidID());
    });
}

function callClose() {
    location.replace('/private/invite_video_conversation');
    console.log('connection lost.');
}

function stopMyVideo() {
    var videoTracs = localVideo.srcObject.getVideoTracks();
    videoTracs[0].enabled = false;
}

function startMyVideo() {
    var videoTracs = localVideo.srcObject.getVideoTracks();
    videoTracs[0].enabled = true;
}

function stopRemoteVideo() {
    var remoteVideo = remoteVideo_1.srcObject.getVideoTracks();
    remoteVideo[0].enabled = false;
}

function startRemoteVideo() {
    var remoteVideo = remoteVideo_1.srcObject.getVideoTracks();
    remoteVideo[0].enabled = true;
}

function stopMyAudio() {
    var myAudio = localVideo.srcObject.getAudioTracks();
    myAudio[0].enabled = false;
}

function startMyAudio() {
    var myAudio = localVideo.srcObject.getAudioTracks();
    myAudio[0].enabled = true;
}

function stopRemoteAudio() {
    var myAudio = remoteVideo_1.srcObject.getAudioTracks();
    myAudio[0].enabled = false;
}

function startRemoteAudio() {
    var myAudio = remoteVideo_1.srcObject.getAudioTracks();
    myAudio[0].enabled = true;
}

function resizeDocument(element, button) {
    if (document.fullscreenElement) {
        closeFullScreen(element, button);
    }
    else {
        fullScreen(element, button);
    }
}

function fullScreen(element, button) {
    button.innerHTML = "<img alt='close full screen' src='https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_45/v1562427121/Close-Full-Screen-icon.png'/>";

    if (element.requestFullScreen) {
        element.requestFullScreen();
    }
    else if (element.webkitRequestFullScreen) {
        element.webkitRequestFullScreen();
    }
    else if (element.mozRequestFullScreen) {
        element.mozRequestFullScreen();
    }
}
function closeFullScreen(element, button) {
    button.innerHTML = "<img alt='full screen ico' src='https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_45/v1562426165/Full-Screen-icon.png'/>";

    if (element.requestFullScreen) {
        document.cancelFullScreen();
    }
    else if (element.webkitRequestFullScreen) {
        document.webkitCancelFullScreen();
    }
    else if (element.mozRequestFullScreen) {
        document.mozCancelFullScreen();
    }
}
function closeOrOpenPanel() {
    var panel = $('.conference-manage');
    var opacity = panel.css("opacity");

    if (opacity == 1) {
        panel.addClass("hide-panel");
        panel.removeClass("visibility-panel");

        $('.triangle-panel-close').css('display', 'none');
        $('.triangle-panel-open').css('display', 'block');
    }
    else {
        panel.removeClass("hide-panel");
        panel.addClass("visibility-panel");
        $('.triangle-panel-open').css('display', 'none');
        $('.triangle-panel-close').css('display', 'block');

    }
}

function togleRemoteVideo(imgElem, videoStreamIdElem) {
    var img = $('#' + imgElem.id)[0];

    if (img.src == "https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1562506866/system/video-on.png") {
        img.src = "https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1562509299/system/video-off.png";
        stopRemoteVideo();
    }
    else {
        img.src = "https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1562506866/system/video-on.png";
        startRemoteVideo();
    }
}

function togleMyVolume() {
    var img = $('#panel-ico-sound')[0];
    var button = $('#my-Volume-Togle')[0];

    if (img.src == 'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_22/v1562433516/system/micro.png') {
        button.innerHTML = '<img alt="sound panel" id="panel-ico-sound" src="https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_22/v1562433516/system/micro-off.png"/>Транслировать мой звук';
        stopMyAudio();
    }
    else {
        button.innerHTML = '<img alt="sound panel" id="panel-ico-sound" src="https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_22/v1562433516/system/micro.png"/>Выключить мой звук';
        startMyAudio();
    }
}
function togleMyVideo() {
    var img = $('#panel-ico-video')[0];
    var button = $('#my-Video-Togle')[0];
    if (img.src == 'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1562509801/system/video-white.png') {
        button.innerHTML = '<img alt="sound panel" id="panel-ico-video" src="https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1562509299/system/video-off.png"/>Транслировать моё видео';
        stopMyVideo();
    }
    else {
        button.innerHTML = '<img alt="sound panel" id="panel-ico-video" src="https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1562509801/system/video-white.png"/>Выключить моё видео';
        startMyVideo();
    }
}

function closeCall() {
    closeCallImmediately();
}
function onLoad() {
    connection.start().done(function () {
        HUB.invoke('GetVideoParticipantName', getGuidID());
    });

    checType().then(function (result) {
        console.log(result);
        if (result == 'CALLER') {
            waitAnimation();
        }
        else if (result == 'CALLE') {
            initiate_call();
        }
    });
}