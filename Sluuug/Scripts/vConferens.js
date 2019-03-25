var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');

videoChat.on('sendInvite', function (callerName, callerSurName, offer, userId) {
    incomming(callerName, callerSurName, offer, userId);
});

var video = document.querySelector("#me_video");
if (navigator.mediaDevices.getUserMedia) {

    navigator.mediaDevices.getUserMedia({ video, audio : true })
        .then(function (stream) {
            video.srcObject = stream;
        })
        .catch(function (error) {
            console.log(error);
        });
}
var friend_divs = $('.friend_div');
[].forEach.call(friend_divs, function (item)
{
    item.addEventListener('click', function () {
        initiate_call(this.id);
    });
})


var peerConn = new RTCPeerConnection();
function initiate_call(friend_id) {
    var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    peerConn.createOffer()
        .then(
        function (offer) {
            return peerConn.setLocalDescription(
                new RTCSessionDescription(offer),
                function () {
                    var offer_dictionary =
                        {
                            sdp: offer.sdp,
                            type: offer.type,
                            session: session_id,
                            to: friend_id
                        };
                    connection.start().done(function () {
                        videoChat.invoke('Invite', offer_dictionary);
                    });
                },
                function (err)
                {
                    console.log(err);
                });
        });
}

function incomming(callerName, callerSurName, offer, incommingUserId)
{
    var incommingOffer = new Object();
    incommingOffer.type = offer.type;
    incommingOffer.sdp = offer.sdp;

    var mylist = $('#incomming');
    mylist[0].insertAdjacentHTML('beforeend',
        '<div class="incomming_call" id="called__' + incommingUserId +'">' +
        '<img src= "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1553527278/incomming_call.png" height= "45" width= "45"/>' +
        '<span>' + callerName + '  ' + callerSurName + '</span> call to you ... </div>');
    document.getElementById('called__'+ incommingUserId).addEventListener(
        'click', function ()
        {
            send_answet(offer);
        }
    )
}

function send_answet(offer)
{
    try {
        navigator.mediaDevices.getUserMedia({ video, audio: true })
            .then(function (stream) {
            var peerConn = new RTCPeerConnection(); // ??
            peerConn.addStream(stream);
            peerConn.setRemoteDescription(
                new RTCSessionDescription(offer),
                function () {
                    peerConn.createAnswer(function (answer) {
                        peerConn.setLocalDescription(
                            new RTCSessionDescription(answer),
                            function () {
                                //send ansfer
                                console.log(answer);
                            },
                            function (error) {
                                console.log(error.message);
                            });
                    }, function (error) {
                        console.log(error.message);
                        });
                },
                function (error) {
                    console.log(error.message);
                });
        });
    }
    catch (error) {
        console.log(error);
    }
}
