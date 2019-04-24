var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');
var notificationTypesArray

connection.start().done(function () {
    HUB.invoke('OpenConnect');
})
window.onunload = function () {
    HUB.invoke('CloseConnect');
}

HUB.on('NotifyAbout', function (type, name, surname, avatar, videoChat) {
    switch (type) {
        case 'MSG':
            newMSGNotification(name, surname, avatar);
            break;
    }

});

function newMSGNotification(name, surname, avatar) {
    var note = $('#notification_div');

    var htmlContent = '<span>You have new message from ' + name + ' ' + surname + '</span >' +
        '<img src="' + avatar + '" height="30" width="30"/>' +
        '<p><a href="/private/cnv">click here to answer</a> </p>';
    note[0].insertAdjacentHTML('beforeend', htmlContent);
    setTimeout(clearNotificationDiv, 5000)
}

function clearNotificationDiv() {
    var note = $('#notification_div');
    note[0].innerHTML = '';
}