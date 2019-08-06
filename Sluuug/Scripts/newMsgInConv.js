//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
//connection.start();

HUB.on('sendAsync', function (object, convId)
{
    console.log(object);
    UpdateDialog(object);
});

function UpdateDialog(message) {

    var Dialog = $('.dialog')[0];
    Dialog.insertAdjacentHTML('beforeend',
        '<div class="dialog-msg-wrapper-in"><div class="in-content">'
        + '<div class="message-header" onclick="redirectToUser(' + message.SenderId + ')">'
        + '<h4>' + message.UserName + '</h4>'
        +'<span>Только что</span>'
        + '</div>'
        + '<div class="message-body">'
        + '<div><img src="' + message.AvatarPath + '"/></div>'
        + '<span>' + message.Text + '</span>'
        + '</div></div></div>');
    setTimeout(function () {
        $(document).ready(function () {
            var objDiv = $(".dialog")[0];
            objDiv.scrollTop = objDiv.scrollHeight;
        });
    }, 100)

}