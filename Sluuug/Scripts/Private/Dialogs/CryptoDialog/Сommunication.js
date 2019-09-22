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

function updateDialog(wrapperType, conteinerClass, message, avatar, name) {
    let messageNode = CryptoDialogNode.ItemMessage(wrapperType, conteinerClass, message, avatar, name);
    $('.dialog')[0].appendChild(messageNode);
}

function scrollDialog() {
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
}

window.ready = onLoad();

window.addEventListener("keydown", function (event) {
    if (event.key == 'Enter') {
        text = $('#new_text').val();
        if (text.length > 0) {
            crypt_send();
            $('#new_text').val('');
            event.preventDefault();
        }
    }
});