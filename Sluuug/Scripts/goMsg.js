var msg_divs = $('.conver_div');
[].forEach.call(msg_divs, function (item) {
    item.addEventListener('click', function () {
        var ids = this.id;
        window.location = 'http://localhost:32033/private/msg?id=' + ids + '&page=1';
    })
});