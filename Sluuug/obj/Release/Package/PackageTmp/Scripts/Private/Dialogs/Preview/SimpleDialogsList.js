HUB.on('GetMessage', function (object, bigAvatarUrl)
{
    UpdateDialog(object, bigAvatarUrl);
});

function UpdateDialog(model, bigAvatarUrl) {
    var dateContainer = $('.dialog-' + model.ConversationId).children('.confersation-body').children('span')[1];

    if (dateContainer != undefined) {
        dateContainer.innerHTML = 'Только что';
        var messageContainer = $('.dialog-' + model.ConversationId).children('.confersation-body').children('span')[0];
        if (model.Text < 30) {
            messageContainer.innerHTML = model.Text;
        }
        else {
            var message = model.Text.substring(0, 27) + '...';
            messageContainer.innerHTML = message;
        }
        IncrementInto('.dialog-not-read-msg-' + model.ConversationId, model.ConversationId);
    }
    else {

        let itemNode = SimpleDialogNode.ItemToDialogsList(model, bigAvatarUrl);
        let dialogList = $('#conversations')[0];
        dialogList.insertBefore(itemNode, dialogList.firstChild);
    }
    $('.dialog-' + model.ConversationId).css({ 'animation': 'AlertGotMessage', 'animation-iteration-count': 'infinite', 'animation-duration': '1s' });

}