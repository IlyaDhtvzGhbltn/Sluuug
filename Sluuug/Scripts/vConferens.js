var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');
connection.start();
window.addEventListener("load", checkWebrtc());
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
    } else {
         console.log('all candidates are set');
    }
}

remoteVideo.addEventListener('loadedmetadata', function () {
    console.log('media data load');
});
videoChat.on('GotInvite', function (callerName, callerSurName, offer, userId)
{
    incomming(callerName, callerSurName, offer, userId);
});
videoChat.on('confirmInvite', function (answer) {
    got_ansfer(answer);
});
videoChat.on('exchangeCandidates', function (candidate) {
    console.log('obtain candidates ...');
    peerConn.addIceCandidate(candidate);
});


function initiate_call(friend_id)
{
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
            videoChat.invoke('Invite', offer);
            return peerConn.setLocalDescription(off);
        });
}



function accept_send_answer(offer, userId, caller_id)
{
    peerConn.setRemoteDescription(offer)
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
                    hide_incoming_if_accept(caller_id);

                    videoChat.invoke('ConfirmInvite', answer);
                    return peerConn.setLocalDescription(answer);




                    console.log("sending ansfer");
                    var remote_streams = peerConn.getRemoteStreams();
                    var local_streams = peerConn.getLocalStreams();

                    console.log("callee remote streams");
                    console.log(remote_streams);
                    console.log("callee local streams");
                    console.log(local_streams);
                })
                .catch(function (err) {
                    console.log(err.message);
                });
        })

 
}

function got_ansfer(answer)
{
    peerConn.setRemoteDescription(
        new RTCSessionDescription(answer),
        function () {
            var remote_streams = peerConn.getRemoteStreams();
            var local_streams = peerConn.getLocalStreams();

            console.log("caller remote streams");
            console.log(remote_streams);
            console.log("caller local streams");
            console.log(local_streams);
        },
        function (err) {
            console.log(err.message);
        }
    )
}



function checkWebrtc() {
    if (navigator.getUserMedia) {
        var friend_divs = $('.friend_div');
        [].forEach.call(friend_divs, function (item) {
            item.addEventListener('click', function () {
                initiate_call(this.id);
            });
        })
    }
    else {
        alert("Sorry, your browser does not support WebRTC!");
    }
}

function incomming(callerName, callerSurName, offer, incommingUserId) {

    console.log("got offer+");
    var caller_phone = document.getElementById("called__" + incommingUserId);
    if (caller_phone == null) {
        var mylist = $('#incomming');
        mylist[0].insertAdjacentHTML('beforeend',
            '<div class="incomming_call" id="called__' + incommingUserId + '">' +
            '<img src= "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1553527278/incomming_call.png" height= "45" width= "45"/>' +
            '<span>' + callerName + '  ' + callerSurName + '</span> call to you ... </div>');
        document.getElementById('called__' + incommingUserId).addEventListener(
            'click', function () {
                var caller_id = "called__" + incommingUserId;
                accept_send_answer(offer, incommingUserId, caller_id);
            })
    }
}

function hide_incoming_if_accept(caller_id) {
    document.getElementById(caller_id).outerHTML = 'accepted...';
}