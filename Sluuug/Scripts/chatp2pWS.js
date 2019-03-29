﻿var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
var connection = $.hubConnection();
var messagesChat = connection.createHubProxy('messagersHub');

messagesChat.on('sendAsync', function (img, userName, userSurName, message, dateTime) {
    addMsg(img, userName, message, dateTime);
});

connection.start()
    .done(function () {
        $('#sendButton')
            .click(function () {
        if (session_id.length == 120) {
            messagesChat.invoke('SendMessage', session_id, $('#new_msg').val(), this.name);
        }
        else {
            window.location.href = 'http://localhost:32033/private/my';
        }
    });
})

function addMsg(img_src, name, text, dateTime)
{
    var mylist = $('#dialog');
    mylist[0].insertAdjacentHTML('beforeend',
        '<div class="dialog_msg"><img src="' + img_src + '" height="45" width="45" /><span>' + name + '</span><span>____' + dateTime + '</span><p>' + text + '</p></div>');
}