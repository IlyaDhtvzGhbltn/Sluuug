connection.qs = 'URL=' + window.location.href;

HUB.on('GetCryptoMessage', function (model, avatar, minLeft, secLeft) {
    gotNewInDialog(model);
});

HUB.on('MessageSendedResult', function (result) {
    if (!result) {
        $('.dialog')[0].insertAdjacentHTML(
            'beforeend',
            '<div class="dialog-msg-wrapper-out">' +
            '<div class="out-content-secret" style="border:1px solid red; background-color:white; padding:10px">' +
            '<span style="color:red; font-size:16px">Вы не можете отправить сообщение пользователю</span></div></div>');
    }
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
});


function crypt_send() {
    let text = $('#new_text').val();
    let url = new URL(window.location.href);
    let id = url.searchParams.get('id');
    let skey = JSON.parse(localStorage.getItem('__' + id));
    console.log(skey);
    var hash = CryptoJS.AES.encrypt(text, skey.K.toString());
    console.log(hash);
    var cryptStr = hash.toString();
    HUB.invoke('SendMessage', cryptStr);
    $('#new_text').val('');

    updateDialog('dialog-msg-wrapper-out', 'out-content-secret', text, $('#myAvatar')[0].innerHTML, 'Я' );
    scrollDialog();
}

function gotNewInDialog(model) {
    let decrypted = decryption(model.Text);
    updateDialog('dialog-msg-wrapper-in', 'in-content-secret', decrypted, model.AvatatURI, model.Name);
    scrollDialog();
}

function decryption(message) {
    let url = new URL(window.location.href);
    let id = url.searchParams.get('id');
    if (id === null) {
        let regex = /(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}/i;
        id = url.toString().match(regex)[0];
    }
    let skey = JSON.parse(localStorage.getItem('__' + id));
    console.log(skey);
    var decrypted = CryptoJS.AES.decrypt(message, skey.K.toString());
    return decrypted.toString(CryptoJS.enc.Utf8);
}

function onLoad() {
    var messages = $('.crypt-message');

    for (let i = 0; i < messages.length; i++){
        let cryptText = messages[i].innerHTML;
        let decryptText = decryption(cryptText);
        messages[i].innerHTML = decryptText;
    }
}

function getElementByXpath(path) {
    return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
}

function updateDialog(wrapperType, conteinerClass, message, avatar, name) {

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

    $('.dialog')[0].appendChild(dialogMsgWrapper);
}

function scrollDialog() {
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
}

window.ready = onLoad();
