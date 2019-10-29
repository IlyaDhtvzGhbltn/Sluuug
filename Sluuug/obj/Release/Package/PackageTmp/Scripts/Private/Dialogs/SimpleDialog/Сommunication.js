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

HUB.on('MessageSendedResult', function (result) {
    if (!result) {
        $('.dialog')[0].insertAdjacentHTML(
            'beforeend',
            '<div class="dialog-msg-wrapper-out">' +
            '<div class="out-content" style="border:1px solid red; background-color:white; padding:10px">' +
            '<span style="color:red; font-size:16px">Вы не можете отправить сообщение пользователю</span></div></div>');
    }
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
});

function SendMessageFromChat(conversationId) {
    console.log('TODO change implementation to DynamicNodes.js');

    connection.start().done(function () {
        text = $('#new_msg').val();
        if (text.length > 0) {
            HUB.invoke('SendMessage', text, conversationId, 0);
            $('#new_msg').val('');
            var date = new Date();
            var stringDate = date.getHours() + ':' + date.getMinutes();
            var ownAvatar = $('.own-avatar-span')[0].innerHTML;
            $('.dialog')[0].insertAdjacentHTML(
                'beforeend',
                '<div class="dialog-msg-wrapper-out">' +
                '<div class="out-content">' +
                '<div class="message-header"><h4>Я</h4><span>'+stringDate+'</span></div>' +
                '<div class="message-body"><div><img alt="avatar" src="' + ownAvatar + '" /></div><span>' + text + '</span></div></div></div>');

            var objDiv = $(".dialog")[0];
            objDiv.scrollTop = objDiv.scrollHeight + 100;

        }
    });
}

function UpdateDialogInCnv(message) {
    console.log('TODO change implementation to DynamicNodes.js');
    var Dialog = $('.dialog')[0];
    var date = new Date();
    var stringDate = date.getHours() + ':' + date.getMinutes();
    Dialog.insertAdjacentHTML('beforeend',
        '<div class="dialog-msg-wrapper-in"><div class="in-content">'
        + '<div class="message-header" onclick="redirectToUser(' + message.SenderId + ')">'
        + '<h4>' + message.UserName + '</h4>'
        +'<span>'+stringDate+'</span>'
        + '</div>'
        + '<div class="message-body">'
        + '<div><img alt="avatar" onclick="redirectToUser(' + message.SenderId + ')" src="' + message.AvatarPath + '"/></div>'
        + '<span>' + message.Text + '</span>'
        + '</div></div></div>');

        var objDiv = $(".dialog")[0];
        objDiv.scrollTop = objDiv.scrollHeight;
} 

$('#new_msg')[0].addEventListener('input', function () {
    var url_string = window.location.href;
    var url = new URL(url_string);
    var id = url.searchParams.get("id");
    HUB.invoke('SendCutMessage', this.value, id);
});

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