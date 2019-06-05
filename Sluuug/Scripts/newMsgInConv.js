//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
//connection.start();

HUB.on('sendAsync', function (html, convId)
{
    addMsgOrConversation(html, convId);
});

function addMsgOrConversation(html, convId) {

    var dialogBlock = $('#' + convId);

    if (dialogBlock.length > 0) {
        dialogBlock.html(html);
    }
    else
    {
        var mylist = $('#conversations');
        mylist[0].insertAdjacentHTML('beforeend', html);
    }

}