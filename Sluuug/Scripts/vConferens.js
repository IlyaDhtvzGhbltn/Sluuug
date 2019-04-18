var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');
connection.start();
var audioContext = null;

const peerConnCfg =
    {
        'iceServers':
        [
            { url: 'stun:stun01.sipphone.com' },
            { url: 'stun:stun.ekiga.net' }
        ]
    };
let localStream;
var remoteVideo = document.querySelector("#remoteVideo");
var localVideo = document.querySelector("#localVideo");
var remoteAudio = document.querySelector("#remoteAudio");

const peerConn = new RTCPeerConnection(peerConnCfg);
peerConn.ontrack = function (event) {
    remoteVideo.srcObject = event.streams[0];
    remoteAudio.srcObject = event.streams[1];
}
peerConn.onicecandidate = function (event) {
    if (event.candidate) {
        videoChat.invoke('ExchangeICandidates', event.candidate);
        console.log('sending candidates start ...');
    }
    else {
        console.log('all candidates are set');
    }
}

videoChat.on('GotInvite', function (guidID, offer) {
    accept_send_answer(guidID, offer);
});
videoChat.on('ConfirmInvite', function (guid, answer) {
    got_ansfer(guid, answer);
});
videoChat.on('exchangeCandidates', function (candidate) {
    peerConn.addIceCandidate(candidate)
});


function initiate_call() {
    navigator.mediaDevices.getUserMedia({ audio: true, video: true })
        .then(function (stream) {
            localVideo.srcObject = stream;

            stream.getTracks().forEach(
                function (track) {
                    peerConn.addTrack(track, stream);
                }
            )
            return peerConn.createOffer();
        })
        .then(
        function (offer) {
            var off = new RTCSessionDescription(offer);
            videoChat.invoke('Invite', JSON.stringify(offer), getID() );
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
                    localVideo.srcObject = stream;

                    stream.getTracks().forEach(
                        function (track) {
                            peerConn.addTrack(track, stream);
                        }
                    )
                    return peerConn.createAnswer();
                })
                .then(function (answer) {
                    //hide_incoming_if_accept(caller_id);

                    videoChat.invoke('ConfirmInvite', guidID, JSON.stringify(answer));
                    //window.location.replace('/private/v_conversation?id=' + guidID)
                    return peerConn.setLocalDescription(answer);

                })
                .catch(function (err) {
                    console.log(err.message);
                });
        })


}

function got_ansfer(guid, answer) {
    peerConn.setRemoteDescription(
        new RTCSessionDescription(JSON.parse(answer)),
        function () {
            //window.location.replace('/private/v_conversation?id=' + guid)
        },
        function (err) {
            console.log(err.message);
        }
    )
}

window.addEventListener("load", onLoad());


function onLoad() {
    checType().then(function (result)
    {
        console.log(result);
        if (result == 'CALLER') {
            waitAnimation();
        }
        else if (result == 'CALLE') {
            initiate_call();
        }
    });
}

async function checType() {
    var id = getID();
    console.log(id);
    const this_type = await
        $.ajax({
        url: '/api/user_vc_role',
        type: 'post',
        data: { converenceID: id }
        })
    return this_type.type;
}

function waitAnimation() {
    console.log('await participant');
}

function getID() {
    var url = new URL(window.location);
    var id = url.searchParams.get("id");
    return id;
}

