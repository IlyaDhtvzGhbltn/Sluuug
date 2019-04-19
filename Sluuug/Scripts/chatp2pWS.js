var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
var connection = $.hubConnection();
var messagesChat = connection.createHubProxy('messagersHub');

messagesChat.on('sendAsync', function (img, userName, userSurName, message, dateTime, convGuidId) {
    addMsg(img, userName, userSurName, message, dateTime, convGuidId);
});

connection.start()
    .done(function () {
        $('#sendButton')
            .click(function () {
            messagesChat.invoke('SendMessage', $('#new_msg').val(), this.name, 0);
    });
})

function addMsg(img_src, name, surName, text, dateTime, guidId)
{
    var mylist = $('#dialog');

        mylist[0].insertAdjacentHTML('beforeend',
            '<div class="dialog_msg"><img src="' + img_src + '" height="45" width="45" /><span>' + name +
            ' ' + surName + '</span><span>____' + dateTime + '</span><p>' + text + '</p></div>');
}