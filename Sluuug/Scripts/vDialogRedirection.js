var connection = $.hubConnection();
var videoChat = connection.createHubProxy('videoChatInviteHub');
connection.start();

window.addEventListener("load", checkWebrtcAndLoad());

function checkWebrtcAndLoad() {
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
