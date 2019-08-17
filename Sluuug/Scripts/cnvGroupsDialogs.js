HUB.on('GetMessage', function (object, bigAvatarUrl)
{
    UpdateDialog(object, bigAvatarUrl);
});

function UpdateDialog(model, bigAvatarUrl) {
    $('#' + model.ConversationId).css({ 'animation': 'AlertGotMessage', 'animation-iteration-count': 'infinite', 'animation-duration': '1s' });
    var messageContainer = $('#' + model.ConversationId).children('.confersation-body').children('span')[0];
    var dateContainer = $('#' + model.ConversationId).children('.confersation-body').children('span')[1];
    if (dateContainer != undefined) {
        dateContainer.innerHTML = 'Только что';
        if (model.Text < 30) {
            messageContainer.innerHTML = model.Text;
        }
        else {
            var message = model.Text.substring(0, 27) + '...';
            messageContainer.innerHTML = message;
        }
            IncrementInto('.not-read-msg-in-pre-dialog-container', 'not_read_counter_' + model.ConversationId);
    }
    else {
        var dialogMsgWrapper = document.createElement("div");
        dialogMsgWrapper.className = 'conver_div';
        dialogMsgWrapper.id = model.ConversationId;
        dialogMsgWrapper.addEventListener('click', function () {
            redirectToDialog(model.ConversationId);
            event.stopPropagation();
        });
        dialogMsgWrapper.style.animation = 'AlertGotMessage';
        dialogMsgWrapper.style.animationIterationCount = 'infinite';
        dialogMsgWrapper.style.animationDuration = '1s';

        var avatarImgNode = document.createElement("img");
        avatarImgNode.src = bigAvatarUrl;

        var dialogHeaderNode = document.createElement("div");
        dialogHeaderNode.className = 'dialog-preview-header';
        var nameNode = document.createElement("h2");
        nameNode.appendChild(document.createTextNode(model.UserName + ' ' + model.UserSurname));
        var deleteNode = document.createElement("span");
        deleteNode.appendChild(document.createTextNode("x"));
        deleteNode.className = 'delete-dialog';
        deleteNode.addEventListener('click', function () {
            deleteDialog(model.ConversationId);
            event.stopPropagation();
        });

        dialogHeaderNode.appendChild(nameNode);
        dialogHeaderNode.appendChild(deleteNode);

        var dialogBodyNode = document.createElement("div");
        dialogBodyNode.className = 'confersation-body';
        var nameHeadNode = document.createElement("h4");
        nameHeadNode.appendChild(document.createTextNode(model.UserName + ' ' + model.UserSurname));
        var messageNode = document.createElement("span");
        messageNode.appendChild(document.createTextNode(model.Text));
        var dateNode = document.createElement("span");
        dateNode.appendChild(document.createTextNode('только что'));

        dialogBodyNode.appendChild(nameHeadNode);
        dialogBodyNode.appendChild(messageNode);
        dialogBodyNode.appendChild(dateNode);

        dialogMsgWrapper.appendChild(avatarImgNode)
        dialogMsgWrapper.appendChild(dialogHeaderNode)
        dialogMsgWrapper.appendChild(dialogBodyNode)

        let dialogList = $('#conversations')[0];
        dialogList.insertBefore(dialogMsgWrapper, dialogList.firstChild);
    }
}