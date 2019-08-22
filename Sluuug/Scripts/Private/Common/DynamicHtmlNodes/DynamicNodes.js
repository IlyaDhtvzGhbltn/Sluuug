class CryptoDialogNode {

    static ItemToDialogsList(model, avatar, minLeft, secLeft, expiredDate, cutDecryptMsg ) {
        var dialogMsgWrapper = document.createElement("div");
        dialogMsgWrapper.className = 'chat-wrapper dialog-' + model.DialogId;
        dialogMsgWrapper.addEventListener('click', function () {
            relocateToCryptoChat(model.DialogId);
            event.stopPropagation();
        });

        var avatarContainerNode = document.createElement("div");
        var avatarImgNode = document.createElement("img");
        avatarImgNode.className = "interlocutor-avatar-container";
        avatarImgNode.src = avatar;
        avatarContainerNode.appendChild(avatarImgNode);

        var bodyNode = document.createElement("div");
        bodyNode.className = 'conversation-body-container';

        var interlocutorNameNode = document.createElement("div");
        interlocutorNameNode.className = 'interlocutor-name-container';
        var nam = document.createElement("H2");
        nam.appendChild(document.createTextNode(model.Name + ' ' + model.SurName));
        interlocutorNameNode.appendChild(nam);
        var msgCounterContainer = document.createElement('div');
        msgCounterContainer.className = 'counter-container crypto-dialog-not-read-msg-' + model.DialogId;
        interlocutorNameNode.appendChild(msgCounterContainer);

        var lastMessageNode = document.createElement("div");
        lastMessageNode.className = 'last-message-container';
        var na = document.createElement("H4");
        na.appendChild(document.createTextNode(model.Name + ' ' + model.SurName));
        lastMessageNode.appendChild(na);
        var spanNode = document.createElement("span");
        spanNode.className = 'last_msg_crypto';
        spanNode.id = model.DialogId;
        spanNode.appendChild(document.createTextNode(cutDecryptMsg));
        lastMessageNode.appendChild(spanNode);
        var now = document.createElement('span');
        now.className = 'last-msg-date';
        now.appendChild(document.createTextNode('сейчас'));
        lastMessageNode.appendChild(now);
        bodyNode.appendChild(interlocutorNameNode);
        bodyNode.appendChild(lastMessageNode);


        var expirationPeriodNode = document.createElement("div");
        expirationPeriodNode.className = 'expiration-period-container';
        var sp = document.createElement("span");
        sp.appendChild(document.createTextNode('Закроется ' + expiredDate));
        expirationPeriodNode.appendChild(sp);
        var spanExpirationNode = document.createElement("span");
        var greenSpanNode = document.createElement("span");
        greenSpanNode.style.color = 'green';
        greenSpanNode.appendChild(document.createTextNode('Осталось : '));
        spanExpirationNode.appendChild(greenSpanNode);
        var sp = document.createElement("span");
        sp.setAttribute('class', 'experation-mins');
        spanExpirationNode.appendChild(sp.appendChild(document.createTextNode(minLeft + ' мин ')));
        var sp = document.createElement("span");
        sp.setAttribute('class', 'experation-mins');
        spanExpirationNode.appendChild(sp.appendChild(document.createTextNode(secLeft + ' сек')));
        expirationPeriodNode.appendChild(spanExpirationNode);
        var deleteNode = document.createElement("span");
        deleteNode.className = "delete-dialog";
        deleteNode.appendChild(document.createTextNode('x'));
        deleteNode.addEventListener('click', function () {
            deleteDialog('.dialog-' + model.DialogId, model.DialogId);
            event.stopPropagation();
        });
        expirationPeriodNode.appendChild(deleteNode);

        dialogMsgWrapper.appendChild(avatarContainerNode);
        dialogMsgWrapper.appendChild(bodyNode);
        dialogMsgWrapper.appendChild(expirationPeriodNode);
        return dialogMsgWrapper;
    }

    static ItemMessage(wrapperType, conteinerClass, message, avatar, name) {
        var dialogMsgWrapper = document.createElement("div");
        dialogMsgWrapper.className = wrapperType;
        var contentSecretClass = document.createElement("div");
        contentSecretClass.className = conteinerClass + " container-in-out";
        var messageHeader = document.createElement("div");
        messageHeader.className = "crypto-message-header";

        var nameNode = document.createElement("h4");
        nameNode.appendChild(document.createTextNode(name));

        var dataNode = document.createElement("span");
        dataNode.appendChild(document.createTextNode("только что"));

        var lockImgNode = document.createElement("img");
        lockImgNode.className = "lock-msg-img";
        lockImgNode.src = "https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25,w_25/v1562229078/lock-msg.png";
        messageHeader.appendChild(nameNode);
        messageHeader.appendChild(dataNode);
        messageHeader.appendChild(lockImgNode);

        var messageBody = document.createElement("div");
        messageBody.className = "crypto-message-body";

        var avatarWrapper = document.createElement("div");
        var avatarI = document.createElement("img");
        avatarI.src = avatar;

        avatarWrapper.appendChild(avatarI);
        var messageTextNode = document.createElement("span");
        messageTextNode.className = "crypt-message";
        messageTextNode.appendChild(document.createTextNode(message));

        messageBody.appendChild(avatarWrapper);
        messageBody.appendChild(messageTextNode);

        contentSecretClass.appendChild(messageHeader);
        contentSecretClass.appendChild(messageBody);
        dialogMsgWrapper.appendChild(contentSecretClass);
        return dialogMsgWrapper;
    }

}

class SimpleDialogNode {
    static ItemToDialogsList(model, bigAvatarUrl) {
        var dialogMsgWrapper = document.createElement("div");
        dialogMsgWrapper.className = 'simple-dialog-wrapper dialog-' + model.ConversationId;
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
        var messagesCounterContainer = document.createElement('div');
        messagesCounterContainer.className = 'dialog-not-read-msg-' + model.ConversationId;

        var deleteNode = document.createElement("span");
        deleteNode.appendChild(document.createTextNode("x"));
        deleteNode.className = 'delete-dialog';
        deleteNode.addEventListener('click', function () {
            deleteDialog('.dialog-' + model.ConversationId, model.ConversationId);
            event.stopPropagation();
        });

        dialogHeaderNode.appendChild(nameNode);
        dialogHeaderNode.appendChild(messagesCounterContainer);
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
        return dialogMsgWrapper;

    }
}