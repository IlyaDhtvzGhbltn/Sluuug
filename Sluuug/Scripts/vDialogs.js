//var connection = $.hubConnection();
//var videoChat = connection.createHubProxy('videoChatInviteHub');
//connection.start();
window.addEventListener("load", checkWebrtcAndLoad());

HUB.on('CallerGuidToRedirect', function (guid) {
    callerToRedirect(guid);
});

HUB.on('CalleInviteToRedirect', function (guid, inviterId) {
    calleInviteToRedirect(guid, inviterId);
});


function checkWebrtcAndLoad() {
    if (navigator.getUserMedia) {

        var friend_divs = $('.friend_div');
        [].forEach.call(friend_divs, function (item) {
            item.addEventListener('click', function () {
                createConference(this.id);
            });
        })
    }
    else {
        alert("Sorry, your browser does not support WebRTC!");
    }

    var call_imgs = $('.img_call_id');
    [].forEach.call(call_imgs, function (item) {
        var friend_id = item.id;
        getInfoById(friend_id, 'AvatarUri').then(function (avatar) {
            item.src = avatar;

        });
    });

    var call_names = $('.call_name');
    [].forEach.call(call_names, function (item) {
        var call_name_id = item.id;
        getInfoById(call_name_id, 'Name').then(function (name) {
            item.innerHTML = name;
        });
    });
}

function createConference(friend_id) {
    HUB.invoke('CreateAndInvite', friend_id);
}

function calleInviteToRedirect(guid, inviterId) {
    fetch('/api/get_info_other_user?id=' + inviterId, {
        method: 'post',
        body: '{}'
    })
        .then(function (resp) {
            return resp.json();
        })
        .then(function (json) {
            var caller_phone = document.getElementById("called__" + inviterId);
            if (caller_phone == null) {
                var mylist = $('#incomming');
                mylist[0].insertAdjacentHTML('beforeend',
                    '<div class="incomming_call" id="called__' + inviterId + '">' +
                    '<img src= "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1553527278/incomming_call.png" height= "45" width= "45"/>' +
                    '<span>' + json.Name + '  ' + json.SurName + '</span> call to you ... </div>');

                document.getElementById('called__' + inviterId).addEventListener(
                    'click', function () {
                        acceptInvite(inviterId, guid);
                    })
            }
        })
}

function callerToRedirect(guid) {
    window.location.href = '/private/v_conversation?id=' + guid;
}

function acceptInvite(inviterId, guidID) {
    document.getElementById('called__' + inviterId).innerHTML = "";
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
