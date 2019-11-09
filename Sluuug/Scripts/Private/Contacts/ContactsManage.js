//const protocol = location.protocol;
//const domain = window.location.host;

//var friend_divs = $('.friend_div');
//[].forEach.call(friend_divs, function (item) {
//    item.addEventListener('click', function () {
//        var fr_id = this.id;
//        window.location = protocol + '//' + domain + '/private/friend/' + fr_id;
//    });
//});

//var friend_divs = $('.invitation');
//[].forEach.call(friend_divs, function (item) {
//    item.addEventListener('click', function () {
//        var fr_id = this.id;
//        window.location = protocol + '//' + domain + '/private/user/' + fr_id;
//    })
//});

function acceptFriendshipInvitation(userID) {
    console.log('accepted - ' + userID);
    HUB.invoke('AcceptContact', userID)
        .done(function () {
            _Alert('Пользователь добавлен в ваши контакты!', '#7f7f7f');
            setTimeout(
                function () {
                    window.location.replace('/private/contacts?type=accept');
                }, 2000
            );
    });
}

function dropFromContacts(userID) {
    HUB.invoke('DropContact', userID).done(function () {
        try {
            $(".accepted_user_#" + userID)[0].remove();
        }
        catch{ }
        _Alert('Пользователь удалён из ваших контактов!', 'red');
        setTimeout(
            function () {
                window.location.reload();
            }, 2000
        );
    }

    );
}

function blockUser(id, message) {
    $.ajax({
        type: 'post',
        url: '/api/block_user',
        data: { BlockedUserId: id, HateMessage: message },
        success: function () {
            window.location.reload();
        }
    });
}

function UnblockUser(Id) {
    $.ajax({
        type: 'post',
        url: '/api/unblockuser',
        data: { UserNeedUnblockId: Id },
        success: function () {
            window.location.reload();
        }
    });
}

function addToContacts() {
    HUB.invoke('AddFriend', getID());
    _Alert('Ваше приглашение отправлено', '#7f7f7f');
    window.location.reload();
}

function getID() {
    var url = window.location;
    var params = /[0-9]*$/g.exec(url);
    return params[0];
}