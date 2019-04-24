//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
window.onload = function () {
    sendButton.onclick = function () {
        connection.start().done(function () {
            HUB.invoke('SendMessage', $('#new_msg').val(), sendButton.name, 0);
        });
    }
}

HUB.on('sendAsync', function (img, userName, userSurName, message, dateTime, convGuidId) {
    addMsg(img, userName, userSurName, message, dateTime, convGuidId);
});


function addMsg(img_src, name, surName, text, dateTime, guidId)
{
    var mylist = $('#dialog');

        mylist[0].insertAdjacentHTML('beforeend',
            '<div class="dialog_msg"><img src="' + img_src + '" height="45" width="45" /><span>' + name +
            ' ' + surName + '</span><span>____' + dateTime + '</span><p>' + text + '</p></div>');
}