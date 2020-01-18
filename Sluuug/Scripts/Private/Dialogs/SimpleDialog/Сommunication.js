HUB.on('getCutMessage', function (text) {
    $('#remote_cut_msg')[0].innerHTML = text;
});

HUB.on('GetMessage', function (object, bigAvatarUrl) {
    console.log(object.ConversationId);

    var currentUri = new URL(window.location.href);
    var currentConversation = currentUri.searchParams.get('id');
    if (currentConversation != object.ConversationId) {
        IncrementInto('.notify-container-increment-message', 'not-show-message-counter');
    }
    if (currentConversation == object.ConversationId) {
        UpdateDialogInCnv(object);
    }
});

HUB.on('MessageSendedResult', function (access, reason) {
    if (!access) {
        $('.dialog')[0].insertAdjacentHTML(
            'beforeend',
            '<div class="dialog-msg-wrapper-out">' +
            '<div class="out-content" style="border:1px solid red; background-color:white; padding:10px">' +
            '<span style="color:red; font-size:16px">' + reason + '</span></div></div>');
    }
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
});

function SendMessageFromChat(conversationId) {
    connection.start().done(function () {
        text = $('#new_msg').val();
        if (text.length > 0) {
            HUB.invoke('SendMessage', text, conversationId, 0);
            $('#new_msg').val('');
            var date = new Date();
            var stringDate = date.getHours() + ':' + date.getMinutes();
            var ownAvatar = $('.own-avatar-span')[0].innerHTML;
            var node = SimpleDialogNode.DialogMessage(
                {
                    IsIncomming: false,
                    SenderId: -1, UserName: "Я",
                    UserSurname: "", 
                    SendTime: stringDate,
                    AvatarPath: ownAvatar,
                    Text: text
                }
            );
            $('.dialog')[0].insertAdjacentHTML('beforeend', node.innerHTML);
            increaseCurrentMsgCount();

            var objDiv = $(".dialog")[0];
            objDiv.scrollTop = objDiv.scrollHeight + 100;

        }
    });
}

function UpdateDialogInCnv(message) {
    var Dialog = $('.dialog')[0];
    var date = new Date();
    var stringDate = date.getHours() + ':' + date.getMinutes();
    var node = SimpleDialogNode.DialogMessage(
        {
            IsIncomming: true,
            SenderId: message.SenderId,
            UserName: message.UserName,
            UserSurname: "",
            SendTime: stringDate,
            AvatarPath: message.AvatarPath,
            Text: message.Text
        }
    );
    Dialog.insertAdjacentHTML('beforeend', node.innerHTML);
    increaseCurrentMsgCount();
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
} 

function LoadEmoji() {
    var conteiner = $('.emoji-container')[0];
    if (conteiner.innerHTML.length == 0) {
        $.ajax({
            type: 'post',
            url: '/api/emoji',
            success: function (resp) {
                [].forEach.call(resp, function (emoji) {
                    conteiner.insertAdjacentHTML('afterbegin', '<span class="emoji-span-container" id="' + emoji+'" onclick="InsertEmojiTo(this)">' + emoji + '</span>');
                });
            }
        });
    }
}

function InsertEmojiTo(elem) {
    var char = elem.innerHTML;
    var cursorPos = $('#new_msg').prop('selectionStart');
    var v = $('#new_msg').val();
    var textBefore = v.substring(0, cursorPos);
    var textAfter = v.substring(cursorPos, v.length);
    $('#new_msg').val(textBefore + char + textAfter);
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
                url: '/api/getmoremessages',
                data: { DialogId: id, LoadedMessages: downloadedMess },
                success: function (resp) {
                    console.log(resp);
                    var container = $('.dialog')[0];
                    [].forEach.call(resp.Messages, function (item) {
                        var node = CryptoDialogNode.ItemMessage(item);
                        container.insertBefore(node, container.firstChild);
                    });
                    let dialogWindowHeight = $('.dialog')[0].scrollHeight;
                    let offset = dialogWindowHeight - TargetOffset;
                    container.scrollTop = offset;
                    TargetOffset = dialogWindowHeight;

                    var msgAfterDownload = downloadedMess + resp.Messages.length;
                    if (resp.DialogMessageCount > msgAfterDownload) {
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
    var count = dialogContainer.getAttribute('current');
    let newValue = parseInt(count) + 1;
    dialogContainer.setAttribute('current', newValue);
}

window.addEventListener("keydown", function (event) {
    if (event.key == 'Enter') {
        text = $('#new_msg').val();
        if (text.length > 0) {
            var url = new URL(this.window.location);
            var id = url.searchParams.get("id");
            SendMessageFromChat(id);
            $('#new_msg').val('');
            event.preventDefault();
        }
    }
});
$('#new_msg')[0].addEventListener('input', function () {
    var url_string = window.location.href;
    var url = new URL(url_string);
    var id = url.searchParams.get("id");
    HUB.invoke('SendCutMessage', this.value, id);
});
$('.dialog')[0].addEventListener('scroll', function () {
    ScrollingDialog();
});
