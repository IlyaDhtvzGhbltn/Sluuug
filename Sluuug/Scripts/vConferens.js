//var connection = $.hubConnection();
//var videoChat = connection.createHubProxy('videoChatInviteHub');
//connection.start();
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
let localStream;
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