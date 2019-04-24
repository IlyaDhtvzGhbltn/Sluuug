//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
//connection.start();

HUB.on('sendAsync', function (img, userName, userSurname, message, dateTime, convId)
{
    addMsgOrConversation(img, userName, userSurname, message, dateTime, convId);
});

function addMsgOrConversation(img, userName, userSurname, message, dateTime, convId) {

    var dialogBlock = $('#' + convId);

    if (dialogBlock.length > 0) {
        dialogBlock.html('<img height= "20" width= "20" src= ' + img + ' /><p>' + userName + '  ' + userSurname + '</p>' +
            '<p>' + message + '</p>');
    }
    else
    {
        var mylist = $('#conversations');
        mylist[0].insertAdjacentHTML('beforeend',
            '<div class="dialog_msg" style="background-color:cadetblue; cursor:grabbing"><img src="' + img + '" height="45" width="45" /><span>' + userName + '</span><span>____' + dateTime + '</span><p>' + message + '</p></div>');
    }

}