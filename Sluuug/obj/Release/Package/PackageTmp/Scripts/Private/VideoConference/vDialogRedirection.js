window.addEventListener("load", checkWebrtcAndLoad());

function checkWebrtcAndLoad() {
    navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia;
    console.log(navigator);
    if (navigator.getUserMedia) {
        var friend_divs = $('.friend_div');
        [].forEach.call(friend_divs, function (item) {
            item.addEventListener('click', function () {
                initiate_call(this.id);
            });
        })
    }
    else {
        _Alert("Нет разрешения для камеры и микрофона. ");
    }
}
