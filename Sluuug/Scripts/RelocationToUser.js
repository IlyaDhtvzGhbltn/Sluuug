var msg_divs = $('.conver_div');
[].forEach.call(msg_divs, function (item) {
    item.addEventListener('click', function () {
        var ids = this.id;
        let protocol = location.protocol;
        let domain = window.location.host;
        let cnv_url = domain + '/private/msg?id=' + ids + '&page=1';
        let url = protocol + '//' + cnv_url;
        console.log(url);
        window.location.replace(url);
    });
});

function redirectToUser(id) {
    let domain = window.location.host;
    let protocol = location.protocol;
    let url = protocol + '//' + domain + '/private/user/' + id;
    window.open(url, "_blank");
}

function redirectToFriend(id) {
    let domain = window.location.host;
    let protocol = location.protocol;
    let url = protocol + '//' + domain + '/private/friend/' + id;
    window.open(url, "_blank");
}

function redirectToMe() {
    let domain = window.location.host;
    let protocol = location.protocol;
    let url = protocol + '//' + domain + '/private/my';
    window.open(url, "_blank");
}