var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');
connection.start();

const peerConnCfg =
{
    'iceServers':
    [
        { url: 'stun:stun01.sipphone.com' },
        { url: 'stun:stun.ekiga.net' }
    ]
};

var localVideo, remoteVideo, localVideoStream, videoCallButton, endCallButton,
    callerPeerConn, calleePeerConn = null;  

window.addEventListener("load", checkWebrtc());


videoChat.on('GotInvite', function (callerName, callerSurName, offer, userId)
{
    incomming(callerName, callerSurName, offer, userId);
});

videoChat.on('confirmInvite', function (answer) {
    got_ansfer(answer);
});

function initiate_call(friend_id)
{
    var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    callerPeerConn = new RTCPeerConnection(peerConnCfg);
    callerPeerConn.ontrack = function (event) {
        console.log('caller recived new stream');
        remoteVideo.srcObject = event.streams[0];
    }
    navigator.mediaDevices.getUserMedia({ "audio": true, "video": true })
        .then(function (stream) {
            localVideo.srcObject = stream;
            for (const track of stream.getTracks()) {
                callerPeerConn.addTrack(track, stream);
            }
            return callerPeerConn.createOffer();
        })
        .then(
        function (offer) {
            var off = new RTCSessionDescription(offer);
            callerPeerConn.setLocalDescription(
                new RTCSessionDescription(off),
                function () {
                    var offer_dictionary =
                        {
                            sdp: offer.sdp,
                            type: offer.type,
                            session: session_id,
                            to: friend_id
                        };
                    videoChat.invoke('Invite', offer_dictionary);
                    console.log('send offer');
                },
                function (err) {
                    console.log(err.message);
                }
            )
        });
}



function accept_send_answer(offer, userId, caller_id)
{
    calleePeerConn = new RTCPeerConnection(peerConnCfg);
    calleePeerConn.ontrack = function (event) {
        console.log('callee accept offer and got streams');
        remoteVideo.srcObject = event.streams[0];
    }
    calleePeerConn.setRemoteDescription(offer)
        .then(function () {
            return navigator.mediaDevices.getUserMedia({ "audio": true, "video": true });
        })
        .then(function (stream) {
            localVideo.srcObject = stream; 
            for (const track of stream.getTracks()) {
                calleePeerConn.addTrack(track, stream);
            }
            return calleePeerConn.createAnswer();
        })
        .then(function (answer) {
            var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
            var ansfer_dictionary =
                {
                    sdp: answer.sdp,
                    type: answer.type,
                    session: session_id,
                    to: userId
                };

            videoChat.invoke('ConfirmInvite', ansfer_dictionary);
            hide_incoming_if_accept(caller_id);

            console.log("sending ansfer");
            var remote_streams = calleePeerConn.getRemoteStreams();
            var local_streams = calleePeerConn.getLocalStreams();

            console.log("callee remote streams");
            console.log(remote_streams);
            console.log("callee local streams");
            console.log(local_streams);
        })
        .catch(function (err) {
            console.log(err.message);
        });
}

function got_ansfer(answer)
{
    callerPeerConn.setRemoteDescription(
        new RTCSessionDescription(answer),
        function () {
            var remote_streams = callerPeerConn.getRemoteStreams();
            var local_streams = callerPeerConn.getLocalStreams();

            console.log("caller remote streams");
            console.log(remote_streams);
            console.log("caller local streams");
            console.log(local_streams);
            navigator.mediaDevices.getUserMedia({ video: true, audio: true })
                .then(
                function (stream) {
                    localVideo.srcObject = stream;
                },
                function (err) {
                    console.log("set local video caller error");
                }
            );
        },
        function (err) {
            console.log(err.message);
        }
    );
}

function checkWebrtc() {
    if (navigator.getUserMedia) {
        localVideo = $('#me_video');
        remoteVideo = $('#friend_video');

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
    var incommingOffer = new Object();
    incommingOffer.type = offer.type;
    incommingOffer.sdp = offer.sdp;
    console.log("got offer");
    console.log(offer);
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
                accept_send_answer(incommingOffer, incommingUserId, caller_id);
            })
    }
}

function hide_incoming_if_accept(caller_id) {
    document.getElementById(caller_id).outerHTML = 'accepted...';
}