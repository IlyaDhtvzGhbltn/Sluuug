var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');
const peerConnCfg =
{
    'iceServers':
    [
        { 'url': 'stun:stun.services.mozilla.com' },
        { 'url': 'stun:stun.l.google.com:19302' }
    ]
    };

var localVideoElem, remoteVideoElem, localVideoStream, videoCallButton, endCallButton, peerConn = null;  

window.addEventListener("load", checkWebrtc());

videoChat.on('SendInvite', function (callerName, callerSurName, offer, userId)
{
    incomming(callerName, callerSurName, offer, userId);
});

videoChat.on('confirmInvite', function (answer) {
    got_ansfer(answer);
});

function initiate_call(friend_id)
{
    var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    peerConn = new RTCPeerConnection(peerConnCfg);
    navigator.getUserMedia(
        { "audio": true, "video": true },
        function (stream)
        {
            localVideo.srcObject = stream;
            peerConn.addStream(stream);
        },
        function (err)
        {
            console.log(err.message);
        }
    );
    peerConn.createOffer()
        .then(
        function (offer) {
            var off = new RTCSessionDescription(offer);
            peerConn.setLocalDescription(
                new RTCSessionDescription(off),
                function () {
                    var offer_dictionary =
                        {
                            sdp: offer.sdp,
                            type: offer.type,
                            session: session_id,
                            to: friend_id
                        };
                    connection.start()
                        .done(
                        function () {
                            videoChat.invoke('Invite', offer_dictionary);
                            console.log('setLocalDescription');
                            console.log(offer);
                            console.log('send offer');
                        });
                },
                function (err) {
                    console.log("setLocalDescription fail");
                    console.log(err.message);
                }
            )
        });
}

function incomming(callerName, callerSurName, offer, incommingUserId) {
    var incommingOffer = new Object();
    incommingOffer.type = offer.type;
    incommingOffer.sdp = offer.sdp;
    console.log("got offer");
    console.log(offer);
    var caller_phone = document.getElementById("called__" + incommingUserId);
    if (caller_phone == null)
    {
        var mylist = $('#incomming');
        mylist[0].insertAdjacentHTML('beforeend',
            '<div class="incomming_call" id="called__' + incommingUserId + '">' +
            '<img src= "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1553527278/incomming_call.png" height= "45" width= "45"/>' +
            '<span>' + callerName + '  ' + callerSurName + '</span> call to you ... </div>');
        document.getElementById('called__' + incommingUserId).addEventListener(
            'click', function () {
                accept_send_answer(incommingOffer, incommingUserId);
            })
    }
 }

function accept_send_answer(offer, userId)
{
    try {
        video = document.querySelector("#me_video");
        navigator.mediaDevices.getUserMedia({ video, audio: true })
            .then(
            function (stream) {
                var peerConn = new RTCPeerConnection(peerConnCfg); // ??
            peerConn.addStream(stream);
            video.srcObject = stream;
            console.log('----');
            peerConn.setRemoteDescription(
                new RTCSessionDescription(offer),
                function () {
                    peerConn.createAnswer(
                        function (answer) {
                            peerConn.setLocalDescription(
                            new RTCSessionDescription(answer),
                            function () {
                                var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
                                var ansfer_dictionary =
                                    {
                                        sdp: answer.sdp,
                                        type: answer.type,
                                        session: session_id,
                                        to: userId
                                    };
                                videoChat.invoke('ConfirmInvite', ansfer_dictionary);
                                console.log("sending ansfer");
                                var remote_streams = peerConn.getRemoteStreams();
                                var local_streams = peerConn.getLocalStreams();

                                console.log("invited remote streams");
                                console.log(remote_streams);
                                console.log("invited local streams");
                                console.log(local_streams);

                            },
                            function (error) {
                                console.log("setLocalDescription fail : " + error.message);
                            });
                    }, function (error) {
                        console.log("create answer fail : " + error.message);
                        });
                },
                function (error) {
                    console.log("setRemoteDescription fail :"+error.message);
                });
        });
    }
    catch (error) {
        console.log(error);
    }
}

function got_ansfer(answer)
{
    peerConn.setRemoteDescription(
        answer,
        function () {
            console.log("setRemoteDescription from ansfer");

            var remote_streams = peerConn.getRemoteStreams();
            var local_streams = peerConn.getLocalStreams();

            console.log("caller remote streams");
            console.log(remote_streams);
            console.log("caller local streams");
            console.log(local_streams);
            video = document.querySelector("#me_video");
            navigator.mediaDevices.getUserMedia({ video, audio: true })
                .then(
                function (stream) {
                    video.srcObject = stream;
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
