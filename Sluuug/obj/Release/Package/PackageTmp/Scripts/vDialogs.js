window.addEventListener("load", checkWebrtcAndLoad());

HUB.on('CallerGuidToRedirect', function (guid) {
    callerToRedirect(guid);
});

HUB.on('CalleInviteToRedirect', function (model) {
    calleInviteToRedirect(model);
});


function checkWebrtcAndLoad() {
    if (navigator.getUserMedia) {

    }
    else {
        alert("Sorry, your browser does not support WebRTC!");
    }
}

function createConference() {
    var friends = $('.fr');
    var friend_id = null;
    [].forEach.call(friends, function (item) {
        if (item.checked) {
            friend_id = item.value;
            console.log(friend_id);
        }
    });
    if (friend_id !== null) {
        HUB.invoke('CreateAndInvite', friend_id);
    }
}

function calleInviteToRedirect(modeljson) {
    var model = JSON.parse(modeljson);
    var caller_phone = document.getElementById("called__" + model.inviterId);
    if (caller_phone === null) {
        var mylist = $('#incomming');
        mylist[0].insertAdjacentHTML('beforeend', model.html);

        document.getElementById('called__' + model.inviterId).addEventListener(
            'click', function () {
                acceptInvite(model.inviterId, model.conferenceID);
            });
    }
}

function callerToRedirect(guid) {
    window.open('/private/v_conversation?id=' + guid);
}

function closeCallImmediately(blockID, guid) {
    console.log('conference was closed ' + guid);
    connection.start().done(function () {
        HUB.invoke('CloseVideoConverence', guid);
        $('#' + blockID).remove();
    });
}

function acceptInvite(inviterId, guidID) {
    document.getElementById('called__' + inviterId).remove();
    window.open('/private/v_conversation?id=' + guidID);
}

async function getInfoById(id, parameter) {
    const info = await
        $.ajax({
            url: '/api/get_info_other_user?id=' + id,
            type: 'post',
            data: '{}'
        });
    return info[parameter];
}
