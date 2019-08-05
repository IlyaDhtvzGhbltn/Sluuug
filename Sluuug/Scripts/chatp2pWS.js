﻿//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
function SendMessageFromChat(conversationId) {
    connection.start().done(function () {
        text = $('#new_msg').val();
        HUB.invoke('SendMessage', text, conversationId, 0);
    });
}


$('#new_msg')[0].addEventListener('input', function () {
    var url_string = window.location.href;
    var url = new URL(url_string);
    var id = url.searchParams.get("id");
    HUB.invoke('SendCutMessage', this.value, id);
});


HUB.on('getCutMessage', function (text) {
    $('#remote_cut_msg')[0].innerHTML = text;
});

HUB.on('MessageSendedResult', function (result) {
    var text = $('#new_msg')[0].value;
    $('#new_msg')[0].value = '';

    if (!result) {
        $('.dialog')[0].insertAdjacentHTML(
            'beforeend',
            '<div class="dialog-msg-wrapper-out">' +
            '<div class="out-content" style="border:1px solid red; background-color:white; padding:10px">' +
            '<span style="color:red; font-size:16px">Вы не можете отправить сообщение пользователю</span></div></div>');
    }
    else {
        var ownAvatar = $('.own-avatar')[0].src;
        $('.dialog')[0].insertAdjacentHTML(
            'beforeend',
            '<div class="dialog-msg-wrapper-out">' +
            '<div class="out-content">' +
            '<div class="message-header"><h4>Я</h4><span>только что</span></div>' +
            '<div class="message-body"><div><img src="' + ownAvatar + '" /></div><span>' + text + '</span></div></div></div>');
    }
    var objDiv = $(".dialog")[0];
    objDiv.scrollTop = objDiv.scrollHeight;
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