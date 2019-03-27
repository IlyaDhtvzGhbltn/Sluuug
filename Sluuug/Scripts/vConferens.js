var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');
connection.start();

const peerConnCfg =
{
    'iceServers':
    [
        { url: 'stun:stun01.sipphone.com' },
        { url: 'stun:stun.ekiga.net' },
        { url: 'stun:stun.fwdnet.net' },
        { url: 'stun:stun.ideasip.com' },
        { url: 'stun:stun.iptel.org' },
        { url: 'stun:stun.rixtelecom.se' },
        { url: 'stun:stun.schlund.de' },
        { url: 'stun:stun.l.google.com:19302' },
        { url: 'stun:stun1.l.google.com:19302' },
        { url: 'stun:stun2.l.google.com:19302' },
        { url: 'stun:stun3.l.google.com:19302' },
        { url: 'stun:stun4.l.google.com:19302' },
        { url: 'stun:stunserver.org' },
        { url: 'stun:stun.softjoys.com' },
        { url: 'stun:stun.voiparound.com' },
        { url: 'stun:stun.voipbuster.com' },
        { url: 'stun:stun.voipstunt.com' },
        { url: 'stun:stun.voxgratia.org' },
        { url: 'stun:169.254.135.71:64609'},
        { url: 'stun:stun.xten.com' },
        {
            url: 'turn:numb.viagenie.ca',
            credential: 'muazkh',
            username: 'webrtc@live.com'
        },
        {
            url: 'turn:192.158.29.39:3478?transport=udp',
            credential: 'JZEOEt2V3Qb0y27GRntt2u2PAYA=',
            username: '28224511:1379330808'
        },
        {
            url: 'turn:192.158.29.39:3478?transport=tcp',
            credential: 'JZEOEt2V3Qb0y27GRntt2u2PAYA=',
            username: '28224511:1379330808'
        }
    ]
    };

var localVideoElem, remoteVideoElem, localVideoStream, videoCallButton, endCallButton,
    callerPeerConn, calleePeerConn = null;  

window.addEventListener("load", checkWebrtc());
console.log(videoChat);



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
    callerPeerConn.onaddstream = function (event) {
        alert(1);
    }
    callerPeerConn.ontrack = function (event) {
        alert(21);
    }

    navigator.getUserMedia(
        {
            "audio": true, "video": { width: 200, height:200 }
        },
        function (stream)
        {
            localVideo.srcObject = stream;
            callerPeerConn.addStream(stream);
        },
        function (err)
        {
            console.log(err.message);
        }
    );

    callerPeerConn.createOffer()
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
                            console.log('setLocalDescription');
                            console.log(offer);
                            console.log('send offer');
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
                var caller_id = "called__" + incommingUserId;
                accept_send_answer(incommingOffer, incommingUserId, caller_id);
            })
    }
 }

function accept_send_answer(offer, userId, caller_id)
{
    try {
        video = document.querySelector("#me_video");
        navigator.mediaDevices.getUserMedia({ video: { width: 200, height: 200 }, audio: true })
            .then(
            function (stream) {
                calleePeerConn = new RTCPeerConnection(peerConnCfg); // ??
                calleePeerConn.onaddstream = function (event) {
                    alert(1);
                }
                calleePeerConn.ontrack = function (event) {
                    alert(21);
                }
                calleePeerConn.addStream(stream);
            video.srcObject = stream;
            console.log('----');


            calleePeerConn.setRemoteDescription(
                new RTCSessionDescription(offer),
                function () {
                    calleePeerConn.createAnswer(
                        function (answer) {
                            calleePeerConn.setLocalDescription(
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
                                var remote_streams = calleePeerConn.getRemoteStreams();
                                var local_streams = calleePeerConn.getLocalStreams();

                                console.log("invited remote streams");
                                console.log(remote_streams);
                                console.log("invited local streams");
                                console.log(local_streams);
                                console.log(calleePeerConn);
                                hide_incoming_if_accept(caller_id);
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
    callerPeerConn.setRemoteDescription(
        new RTCSessionDescription(answer),
        function () {
            console.log("setRemoteDescription from ansfer");

            var remote_streams = callerPeerConn.getRemoteStreams();
            var local_streams = callerPeerConn.getLocalStreams();

            console.log("caller remote streams");
            console.log(remote_streams);
            console.log("caller local streams");
            console.log(local_streams);
            video = document.querySelector("#me_video");
            navigator.mediaDevices.getUserMedia({ video : { width: 200, height: 200 }, audio: true })
                .then(
                function (stream) {
                    console.log("++");
                    video.srcObject = stream;
                    console.log(callerPeerConn);
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

function hide_incoming_if_accept(caller_id) {
    document.getElementById(caller_id).outerHTML = 'accepted...';
}
