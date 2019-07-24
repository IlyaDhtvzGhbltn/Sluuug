//var connection = $.hubConnection();
//var videoChat = connection.createHubProxy('videoChatInviteHub');
//connection.start();
window.addEventListener("load", checkWebrtcAndLoad());

HUB.on('CallerGuidToRedirect', function (guid) {
    callerToRedirect(guid);
});

HUB.on('CalleInviteToRedirect', function (model) {
    calleInviteToRedirect(model);
});


function checkWebrtcAndLoad() {
    if (navigator.getUserMedia) {

        //var friend_divs = $('.friend_div');
        //[].forEach.call(friend_divs, function (item) {
        //    item.addEventListener('click', function () {
        //        createConference(this.id);
        //    });
        //});
    }
    else {
        alert("Sorry, your browser does not support WebRTC!");
    }

    //var call_imgs = $('.img_call_id');
    //[].forEach.call(call_imgs, function (item) {
    //    var friend_id = item.id;
    //    getInfoById(friend_id, 'AvatarResizeUri').then(function (avatar) {
    //        item.src = avatar;
    //    });
    //});

    //var call_names = $('.call_name');
    //[].forEach.call(call_names, function (item) {
    //    var call_name_id = item.id;
    //    getInfoById(call_name_id, 'Name').then(function (name) {
    //        item.innerHTML = name;
    //    });
    //});
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
    window.location.href = '/private/v_conversation?id=' + guid;
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
