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

    static ItemMessage(model) {
        var dialogMsgWrapper = createDiv(model.wrapperType); 
        var contentSecretClass = createDiv(model.conteinerClass + " container-in-out"); 
        var messageHeader = createDiv("crypto-message-header");

        var nameNode = document.createElement("h4");
        nameNode.appendChild(document.createTextNode(`${model.name} ${model.surName}`));
        nameNode.addEventListener('click', function () {
            redirectToUser(model.senderId);
        });
        nameNode.style.cursor = 'pointer';
        var dataNode = createSpan(model.sendingDate);

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
        avatarI.src = model.avatar;
        avatarI.style.cursor = 'pointer';
        avatarI.addEventListener('click', function () {
            redirectToUser(model.senderId);
        });
        avatarWrapper.appendChild(avatarI);
        var messageTextNode = document.createElement("span");
        messageTextNode.className = "crypt-message";
        messageTextNode.appendChild(document.createTextNode(model.message));

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
        var date = new Date;
        var dateString = date.getHours() + ':' + date.getMinutes();
        dateNode.appendChild(document.createTextNode(dateString));

        dialogBodyNode.appendChild(nameHeadNode);
        dialogBodyNode.appendChild(messageNode);
        dialogBodyNode.appendChild(dateNode);

        dialogMsgWrapper.appendChild(avatarImgNode);
        dialogMsgWrapper.appendChild(dialogHeaderNode);
        dialogMsgWrapper.appendChild(dialogBodyNode);
        return dialogMsgWrapper;
    }

    static DialogMessage(model) {
        var rootClass = model.IsIncomming === true ? "dialog-msg-wrapper-in" : "dialog-msg-wrapper-out";
        var subClass = model.IsIncomming === true ? "in-content" : "out-content";

        var rootDiv = createDiv(rootClass);
        var subDiv = createDiv(subClass);

        var headDiv = createDiv("message-header");
        headDiv.onclick = function () {
            redirectToUser(model.SenderId);
        };
        var nameSpan = createSpan(`${model.UserName} ${model.UserSurname}`, 'user-name');
        var timeSpan = createSpan(`${model.SendTime}`, 'send-time');
        headDiv.appendChild(nameSpan);
        headDiv.appendChild(timeSpan);

        var messageTextDiv = createDiv('message-body');
        var avatarContainerDiv = createDiv('');
        var avatarImg = createImg("", model.AvatarPath);
        avatarContainerDiv.appendChild(avatarImg);
        messageTextDiv.appendChild(avatarContainerDiv);
        messageTextDiv.appendChild(createSpan(`${model.Text}`, '', ''));

        subDiv.appendChild(headDiv);
        subDiv.appendChild(messageTextDiv);

        rootDiv.appendChild(subDiv);
        return rootDiv;
    }
}

class PostNode {
    static ItemUserPost(title, text) {
        var postItem = document.createElement("div");
        postItem.className = "profile-full-info-item-wrapper post";

        var postTitleContainer = document.createElement("div");
        var titleNode = document.createElement("h2");
        titleNode.className = "post-title";
        titleNode.appendChild(document.createTextNode(title));
        postTitleContainer.appendChild(titleNode);

        var postTextContainer = document.createElement("div");
        postTextContainer.className = "post-text-container";
        var postTextNode = document.createElement("span");
        postTextNode.className = "post-text";
        postTextNode.appendChild(document.createTextNode(text));
        postTextContainer.appendChild(postTextNode);

        var postDateContainer = document.createElement("div");
        postDateContainer.className = "post-date-container";
        var dateNode = document.createElement("span");
        dateNode.className = "post-date";
        dateNode.appendChild(document.createTextNode('только что'));
        postDateContainer.appendChild(dateNode);

        postItem.appendChild(postTitleContainer);
        postItem.appendChild(postTextContainer);
        postItem.appendChild(postDateContainer);
        return postItem;
    }
    static ItemUserOldPost(title, text, date)
    {
        var postItem = document.createElement("div");
        postItem.className = "profile-full-info-item-wrapper post";

        var postTitleContainer = document.createElement("div");
        var titleNode = document.createElement("h2");
        titleNode.className = "post-title";
        titleNode.appendChild(document.createTextNode(title));
        postTitleContainer.appendChild(titleNode);

        var postTextContainer = document.createElement("div");
        postTextContainer.className = "post-text-container";
        var postTextNode = document.createElement("span");
        postTextNode.className = "post-text";
        postTextNode.appendChild(document.createTextNode(text));
        postTextContainer.appendChild(postTextNode);

        var postDateContainer = document.createElement("div");
        postDateContainer.className = "post-date-container";
        var dateNode = document.createElement("span");
        dateNode.className = "post-date";
        dateNode.appendChild(document.createTextNode(date));
        postDateContainer.appendChild(dateNode);

        postItem.appendChild(postTitleContainer);
        postItem.appendChild(postTextContainer);
        postItem.appendChild(postDateContainer);
        return postItem;
    }
    static ButtonMorePost(currentPostsCount, friendUserId = null) {

        console.log('button friend id ' + friendUserId);
        var morePostsButtonContainer = document.createElement("div");
        morePostsButtonContainer.className = "more-posts-container";
        var button = document.createElement("button");
        button.className = "more-posts-button";
        button.innerHTML = "Ещё";
        if (friendUserId == null) {
            button.onclick = function () {
                GetMoreOwnPosts(currentPostsCount);
            };
        }
        else {
            button.onclick = function () {
                GetMoreFriendPosts(currentPostsCount, friendUserId);
            };
        }
        morePostsButtonContainer.appendChild(button);
        return morePostsButtonContainer;
    }
}

class VIP {
    static VIPUserNode(item) {
        var rootDiv = createDiv('vip-user');
        var userNameDivContainer = createDiv('vip-username-container');
        var userNameDivCenter = createDiv('vipuser-name-center-continer');
        var nameSpan = createSpan(item.Name + " " + item.SurName, 'vip-namesurname', '');

        userNameDivCenter.appendChild(nameSpan);
        userNameDivContainer.appendChild(userNameDivCenter);
        rootDiv.appendChild(userNameDivContainer);

        var vipContainerDiv = createDiv('vip-container');

        var vipFrameDiv = createDiv('vip-frame-user-avatar');
        var userAvatar = createImg('user-avatar', item.MidAvatarUri);
        var vipFrame = createImg('vip-frame', 'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1578049174/system/vip_frame.png');
        vipFrameDiv.appendChild(vipFrame);
        vipFrameDiv.appendChild(userAvatar);
        vipContainerDiv.appendChild(vipFrameDiv);

        var vipInfoContainer = createDiv('vip-info-container');
        var info = createDiv('vip-info');
        var ageSpan = createSpan('Возраст ' + item.Age, '', '');
        var locationSpan = createSpan(item.Country + ' ⚈ ' + item.City, '', '');
        info.appendChild(ageSpan);
        info.appendChild(locationSpan);
        switch (item.DatingPurpose)
        {
            case 0:
                var purposeSpan = createSpan('Знакомлюсь для отношений', '', '');
                info.appendChild(purposeSpan);
                break;
            case 1:
                purposeSpan = createSpan('Знакомлюсь для общения', '', '');
                info.appendChild(purposeSpan);
                break;
            case 2:
                purposeSpan = createSpan('Знакомлюсь для секса', '', '');
                info.appendChild(purposeSpan);
                break;
            case 3:
                purposeSpan = createSpan('не знакомлюсь', '', '');
                info.appendChild(purposeSpan);
                break;
        }
        vipInfoContainer.appendChild(info);
        var contactButtonContainer = createDiv('contact-button-container');
        var btn = createButton(function () { redirectToFriend(item.Id); });
        var imgSend = createImg('', 'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1578055425/system/mail.png');
        var sendSpan = createSpan('Написать', '', '');
        btn.appendChild(imgSend);
        btn.appendChild(sendSpan);
        contactButtonContainer.appendChild(btn);
        vipInfoContainer.appendChild(contactButtonContainer);

        vipContainerDiv.appendChild(vipInfoContainer);
        rootDiv.appendChild(vipContainerDiv);

        return rootDiv;
    }
}

class SearchNode {
    static NewFoundedUserNode(user) {
        var rootDiv = createDiv("found_user");
        var divHead = createDiv("fount-user-header");
        divHead.onclick = function () {
            redirectToUser(user.UserId);
        };
        if (user.Vip) {
            var vipImg = createImg("vipuser-crown", "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1578220125/system/86f6452c09d91a8a507417a473d956d1.png");
            divHead.appendChild(vipImg);
        }
        var userAvatarContainer = createDiv("fount-user-img-container");
        var userAvatarImg = createImg("found-user-avatar", user.LargeAvatar);
        userAvatarContainer.appendChild(userAvatarImg);
        divHead.appendChild(userAvatarContainer);
        rootDiv.appendChild(divHead);

        var nameDiv = createDiv("fount-user-name");
        var spanName = createSpan(`${user.Name} ${user.SurName} ${user.Age}`, "user-info weight name");
        var ageEndSufix = createSpan(" лет", "weight name");
        spanName.appendChild(ageEndSufix);
        nameDiv.appendChild(spanName);
        var par = createParagraph("font-style:italic", "");
        var helloText = createSpan(user.HelloMessage);
        par.appendChild(helloText);
        nameDiv.appendChild(par);
        rootDiv.appendChild(nameDiv);

        var paraCity = createParagraph("", "");
        var citySpan = createSpan(`${user.Country} ⚈ ${user.City}`, "user-info", "");
        paraCity.appendChild(citySpan);
        rootDiv.appendChild(paraCity);

        var purpParagr = createParagraph("", "");
        if (user.purpose == 3) {
            var purposeSpan = createSpan("не знакомлюсь", "weight user-info", "");
            purpParagr.appendChild(purposeSpan);
            rootDiv.appendChild(purpParagr);
        }
        else {
            purposeSpan = createSpan(`Знакомлюсь для : ${datingEnumsParce.datingPurposeParse(user.purpose)}`, "weight user-info", "");
            purpParagr.appendChild(purposeSpan);
            rootDiv.appendChild(purpParagr);

            var searchFor = createParagraph("", "");
            var searchForSpan = createSpan(`Ищу : ${datingEnumsParce.datingGenderParse(user.userSearchSex)} ${datingEnumsParce.datingAgeParse(user.userSearchAge)}`, "user-info");
            searchFor.appendChild(searchForSpan);
            rootDiv.appendChild(searchFor);
        }


        return rootDiv;
    }

    static EndUsersDivBorder(currentPage, maxPage, jsonQuerry) {
        let idIndex = currentPage;
        if (currentPage == maxPage)
            idIndex = 0;
        var rootDiv = createDiv("end-of-users", idIndex);
        var queryDiv = createDiv("search-link-container", jsonQuerry);
        rootDiv.appendChild(queryDiv);
        return rootDiv;
    }
}

class datingEnumsParce {
    static datingPurposeParse(code) {
        switch (code) {
            case 0:
                return "отношений";
            case 1:
                return "общения";
            case 2:
                return "секса";
            case 3:
                return "не знакомлюсь";
        }
    }

    static datingGenderParse(code) {
        switch (code) {
            case 0:
                return "женжину";
            case 1:
                return "мужчину";
        }
    }

    static datingAgeParse(code) {
        switch (code) {
            case 0:
                return "16 - 20 лет";
            case 1:
                return "21 - 26 лет";
            case 2:
                return "27 - 32 лет";
            case 3:
                return "33 - 40 лет";
            case 4:
                return "41 - 49 лет";
            case 5:
                return "50 - 59 лет";
            case 6:
                return "60 - 69 лет";
            case 7:
                return "более 70";
        }
    }
}

function createSpan(text, className, id) {
    var span = document.createElement('span');
    span.appendChild(document.createTextNode(text));
    span.className = className;
    span.id = id;
    return span;
}

function createDiv(className, id) {
    var div = document.createElement('div');
    div.className = className;
    div.id = id;
    return div;
}

function createImg(className, src) {
    var img = document.createElement("img");
    img.className = className;
    img.src = src;
    return img;
}

function createButton(func) {
    var button = document.createElement("button");
    button.onclick = function () { func(); };
    return button;
}

function createParagraph(style, text) {
    var para = document.createElement("p");
    para.style = style;
    var node = document.createTextNode(text);
    para.appendChild(node);
    return para;
}