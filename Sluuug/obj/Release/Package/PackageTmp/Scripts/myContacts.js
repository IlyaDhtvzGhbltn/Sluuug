const protocol = location.protocol;
const domain = window.location.host;

var friend_divs = $('.friend_div');
[].forEach.call(friend_divs, function (item) {
    item.addEventListener('click', function () {
        var fr_id = this.id;
        window.location = protocol + '//' + domain + '/private/friend/' + fr_id;
    })
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
    HUB.invoke('AcceptContact', userID).done(function () {
        $("#incomm_invitations_" + userID)[0].innerHTML = '';

        var acceptedFriends = $("#accepted_friends")[0];
        fetch('/api/get_info_other_user?id=' + userID, {
            method: 'post',
            body: '{}'
        })
            .then(function (resp) {
                return resp.json();
            })
            .then(function (json) {
                acceptedFriends.insertAdjacentHTML('beforeend',
                    '<div id="friend_' + userID + '"><div class="friend_div" id="' + userID + '">' +
                    '<img src="' + json.AvatarUri + '" height="50" width="50">' +
                    ' <span>' + json.Name + ' ' + json.SurName + '</span>' +
                    ' </div>' +
                    '<input type="button" value="drop from contacts" onclick="dropFromContacts(' + userID + ')">' +
                    '</div>');
            });

    });
}

function dropFromContacts(userID) {
    console.log('drop...' + userID);
    HUB.invoke('DropContact', userID).done(function () {
        $("#friend_" + userID)[0].innerHTML = '';
    })

}