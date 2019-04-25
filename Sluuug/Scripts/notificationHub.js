var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');
var delay = 5000;

connection.start().done(function () {
    HUB.invoke('OpenConnect');
})
window.onunload = function () {
    HUB.invoke('CloseConnect');
}

HUB.on('NotifyAbout', function (type, name, surname, avatar, params) {
    switch (type) {
        case 'MSG':
            newMSGNotification(name, surname, avatar);
            break;
        case 'VC':
            newVideoConferenceNotification(name, surname, avatar);
            break;
        case 'ICC':
            newInviteToCryptChatNotification(name, surname, avatar, params);
            break;
        case 'C_MSG':
            newC_MSGNotification(name, surname, avatar);
            break;
        case 'ICC_A':
            newC_MSGInviteAcceptedNotification(name, surname, avatar);
            break;
        case 'FRND':
            newFriendshipNotification(name, surname, avatar);
            break;
        case 'ACC_FRND':
            newFriendshipAcceptNotification(name, surname, avatar);
            break;
    }
});

function newMSGNotification(name, surname, avatar) {
    var note = $('#notification_div');

    var htmlContent = '<span>You have new message from ' + name + ' ' + surname + '</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/cnv">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, delay)
}

function newVideoConferenceNotification(name, surname, avatar) {
    var note = $('#notification_div');

    var htmlContent = '<span>' + name + ' ' + surname + ' invite you to video chat' + '</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/invite_video_conversation">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, delay)
}

function newInviteToCryptChatNotification(name, surname, avatar, publicData) {
    var note = $('#notification_div');

    var htmlContent = '<span>You have new crypto chat invite from ' + name + ' ' + surname + '</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/crypto_cnv">crypto chats here</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);

    var script = document.createElement('script');
    script.src = "/Scripts/crypto_chat.js";
    document.documentElement.appendChild(script);
    script.onload = function () {
        console.log('download new script');
        invited.save_invitation(publicData);
    }

    setTimeout(clearNotificationDiv, delay)
}

function newC_MSGNotification(name, surname, avatar) {
    var note = $('#notification_div');

    var htmlContent = '<span>You have new message in crypto chat from ' + name + ' ' + surname + '</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/crypto_cnv">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, delay)
}

function newC_MSGInviteAcceptedNotification(name, surname, avatar) {
    var note = $('#notification_div');

    var htmlContent = '<span>' + name + ' ' + surname + ' accept your invite to crypto chat </span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/crypto_cnv">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, delay)
}

function newFriendshipNotification(name, surname, avatar) {
    var note = $('#notification_div');

    var htmlContent = '<span>' + name + ' ' + surname + ' invited you to friends</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/cnv">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, delay)
}

function newFriendshipAcceptNotification(name, surname, avatar)
{
    var note = $('#notification_div');

    var htmlContent = '<span>Your invitation was acceptet by ' + name + ' ' + surname + '</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/cnv">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, delay)
}

function clearNotificationDiv() {
    var note = $('#notification_div');
    note[0].innerHTML = '';
}