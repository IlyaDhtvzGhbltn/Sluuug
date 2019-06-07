var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');
var delay = 5000;

connection.start().done(function () {
    HUB.invoke('OpenConnect');
});

window.onunload = function () {
    HUB.invoke('CloseConnect');
};

HUB.on('NotifyAbout', function (html, params) {
    if (params !== null)
    {
        newInviteToCryptChatNotification(html, params);
    }
    else
    {
        var note = $('#notification_div');
        note[0].insertAdjacentHTML('beforeend', html);
        setTimeout(clearNotificationDiv, delay);
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
    var note = $('#notification_div');
    note[0].innerHTML = '';
}