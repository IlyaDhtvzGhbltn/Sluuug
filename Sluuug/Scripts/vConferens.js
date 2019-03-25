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

function initiate_call(friend_id) {

    var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var peerConn = new RTCPeerConnection();
    peerConn.createOffer()
        .then(
        function (offer) {
            return peerConn.setLocalDescription(
                new RTCSessionDescription(offer),
                function () {
                    console.log(offer);
                    $.post(
                        "/private/video_offer",
                        {
                            sdp: offer.sdp,
                            type: offer.type,
                            session: session_id,
                            to: friend_id
                        }
                    );
                },
                function (err)
                {
                    console.log(err);
                });
        });
}

function 