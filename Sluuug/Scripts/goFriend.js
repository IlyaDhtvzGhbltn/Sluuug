﻿var friend_divs = $('.friend_div');
[].forEach.call(friend_divs, function (item) {
    item.addEventListener('click', function () {
        var fr_id = this.id;
        window.location = 'http://localhost:32033/private/friend/' + fr_id;
    })
});