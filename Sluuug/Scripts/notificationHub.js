var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');

connection.start().done(function () {
    HUB.invoke('OpenConnect');
});

window.onunload = function () {
    HUB.invoke('CloseConnect');
};

HUB.on('NotifyAbout', function (html, params) {
    console.log(html);
    if (params !== null)
    {
        newInviteToCryptChatNotification(html, params);
    }
    else
    {
        var note = $('.notify-alert');
        note[0].innerHTML = html;
        note.css({ opacity: 1, left: 200 });
        var audio = new Audio('/resources/audio/new-notify-sound.wav');
        audio.play();
        setTimeout(clearNotificationDiv, 4000);
    }
});

function newInviteToCryptChatNotification(html, publicData) {
    var note = $('#notification_div');
    note[0].insertAdjacentHTML('beforeend', html);

    var script = document.createElement('script');
    script.src = "/Scripts/crypto_chat.js";
    document.documentElement.appendChild(script);
    script.onload = function () {
        console.log('download new script');
        invited.save_invitation(publicData);
    };

    setTimeout(clearNotificationDiv, delay);
}


function clearNotificationDiv() {
    var note = $('.notify-alert');
    note.css({ opacity: 0, left: -400 });
    note[0].innerHTML = '';
}