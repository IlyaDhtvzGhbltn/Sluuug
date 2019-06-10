const protocol = location.protocol;
const domain = window.location.host;

var friend_divs = $('.friend_div');
[].forEach.call(friend_divs, function (item) {
    item.addEventListener('click', function () {
        var fr_id = this.id;
        window.location = protocol + '//' + domain + '/private/friend/' + fr_id;
    });
});

var friend_divs = $('.invitation');
[].forEach.call(friend_divs, function (item) {
    item.addEventListener('click', function () {
        var fr_id = this.id;
        window.location = protocol + '//' + domain + '/private/user/' + fr_id;
    })
});

function acceptFriendshipInvitation(userID) {
    console.log('accept...' + userID);
    HUB.invoke('AcceptContact', userID)
        .done(function () {
            window.location.reload();
    });
}

function dropFromContacts(userID) {
    console.log('drop...' + userID);
    HUB.invoke('DropContact', userID).done(function () {
        $("#friend_" + userID)[0].innerHTML = '';
    })

}