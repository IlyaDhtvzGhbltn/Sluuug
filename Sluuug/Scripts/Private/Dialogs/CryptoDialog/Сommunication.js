connection.qs = 'URL=' + window.location.href;

HUB.on('GetCryptoMessage', function (model) {
    console.log(model);
    var url = new URL(window.location);
    var id = url.searchParams.get("id");
    if (id == model.DialogId) {
        var decrypted = DecryptHashMessage(model.Text);
        var messModel = {
            wrapperType: 'dialog-msg-wrapper-in',
            conteinerClass: 'in-content-secret',
            message: decrypted,
            avatar: model.AvatatURI,
            name: model.Name,
            surName: model.SurName,
            senderId: model.UserSenderId,
            sendingDate: "только что"
        };
        var messageNode = CryptoDialogNode.ItemMessage(messModel);
        $('.dialog')[0].appendChild(messageNode);
        increaseCurrentMsgCount();

        //scrolling
        var objDiv = $(".dialog")[0];
        objDiv.scrollTop = objDiv.scrollHeight;
    }
    else {
        IncrementInto('.notify-container-increment-crypto', 'not-show-crypto-counter');
    }
});

HUB.on('MessageSendedResult', function (access, reason) {
    console.log('MessageSendedResult HUB!!!');
    if (!access) {
        $('.dialog')[0].insertAdjacentHTML(
            'beforeend',
            '<div class="dialog-msg-wrapper-out">' +
            '<div class="out-content-secret" style="border:1px solid red; background-color:white; padding:10px">' +
            '<span style="color:red; font-size:16px">' + reason + '</span></div></div>');
    }
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
});

function SendCryptoMessage() {
    var text = $('#new_text').val();
    var url = new URL(window.location.href);
    var id = url.searchParams.get('id');
    var skey = JSON.parse(localStorage.getItem('__' + id));
    console.log(skey);
    var hash = CryptoJS.AES.encrypt(text, skey.K.toString());
    console.log(hash);
    var cryptStr = hash.toString();
    HUB.invoke('SendMessage', cryptStr);
    $('#new_text').val('');

    var model = {
        wrapperType: 'dialog-msg-wrapper-out',
        conteinerClass: 'out-content-secret',
        message: text,
        avatar:$('#myAvatar')[0].innerHTML,
        name: 'Я',
        surName : "",
        senderId: 0,
        sendingDate : "только что"
    };
    var messageNode = CryptoDialogNode.ItemMessage(model);
    $('.dialog')[0].appendChild(messageNode);
    increaseCurrentMsgCount();

    //scrolling
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
}

function DecryptHashMessage(message) {
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
        let decryptText = DecryptHashMessage(cryptText);
        messages[i].innerHTML = decryptText;
    }
}

var TargetOffset = 0;
function ScrollingDialog() {
    var target = $('.old-dialog-page')[0];
    if (target != null) {
        var targetPosition = {
            bottom: target.getBoundingClientRect().bottom
        };
        if (TargetOffset == 0) {
            TargetOffset = Math.abs(targetPosition.bottom);
        }
        if (targetPosition.bottom >= 50) {
            var downloadedMess = parseInt($('.old-dialog-page')[0].getAttribute('current'));

            $('.old-dialog-page').remove();
            var url = new URL(window.location);
            var id = url.searchParams.get("id");
            $.ajax({
                type: 'post',
                url: '/api/getmorecryptomessages',
                data: { DialogId: id, LoadedMessages: downloadedMess },
                success: function (resp) {
                    console.log(resp);
                    var container = $('.dialog')[0];
                    [].forEach.call(resp.Messages, function (item) {
                        let decriptedMessage = DecryptHashMessage(item.Text);
                        let wrapperType = 'dialog-msg-wrapper-out';
                        let containerClass = 'out-content-secret';
                        if (item.IsIncomming) {
                            wrapperType = 'dialog-msg-wrapper-in';
                            containerClass = 'in-content-secret';
                        }

                        var node = CryptoDialogNode.ItemMessage(
                            {
                                wrapperType: wrapperType,
                                conteinerClass: containerClass,
                                message: decriptedMessage,
                                name: item.Name,
                                surName: item.SurName,
                                avatar: item.AvatatURI,
                                sendingDate: item.SendingDate,
                                senderId: item.UserSenderId
                            });
                        container.insertBefore(node, container.firstChild);
                    });

                    let dialogWindowHeight = $('.dialog')[0].scrollHeight;
                    let offset = dialogWindowHeight - TargetOffset;
                    container.scrollTop = offset;
                    TargetOffset = dialogWindowHeight;

                    var msgAfterDownload = downloadedMess + resp.Messages.length;
                    if (resp.DialogTotalMessageCount > msgAfterDownload) {
                        var oldDialogNode = createDiv('old-dialog-page');
                        oldDialogNode.setAttribute('current', msgAfterDownload);
                        container.insertBefore(oldDialogNode, container.firstChild);
                    }
                }
            });
        }
    }
}

function increaseCurrentMsgCount() {
    var dialogContainer = $('.old-dialog-page')[0];
    if (dialogContainer != null) {
        var count = dialogContainer.getAttribute('current');
        let newValue = parseInt(count) + 1;
        dialogContainer.setAttribute('current', newValue);
    }
}

window.ready = onLoad();
window.addEventListener("keydown", function (event) {
    if (event.key == 'Enter') {
        text = $('#new_text').val();
        if (text.length > 0) {
            SendCryptoMessage();
            $('#new_text').val('');
            event.preventDefault();
        }
    }
});
$('.dialog')[0].addEventListener('scroll', function () {
    ScrollingDialog();
});
