//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
window.onload = function () {
    sendButton.onclick = function () {
        connection.start().done(function () {
            HUB.invoke('SendMessage', $('#new_msg').val(), sendButton.name, 0);
        });
        $('#new_msg')[0].value = '';
    }


    $('#new_msg')[0].addEventListener('input', function () {
        var url_string = window.location.href;
        var url = new URL(url_string);
        var id = url.searchParams.get("id");

        HUB.invoke('SendCutMessage', this.value, id);
    });
}

HUB.on('sendAsync', function (img, userName, userSurName, message, dateTime, convGuidId) {
    addMsg(img, userName, userSurName, message, dateTime, convGuidId);
});

HUB.on('getCutMessage', function (text) {
    getCutMessage(text);
});


function addMsg(img_src, name, surName, text, dateTime, guidId)
{
    var mylist = $('#dialog');

        mylist[0].insertAdjacentHTML('beforeend',
            '<div class="dialog_msg"><img src="' + img_src + '" height="45" width="45" /><span>' + name +
        ' ' + surName + '</span><span>____' + dateTime + '</span><p>' + text + '</p></div>');

        $('#remote_cut_msg')[0].innerHTML = '';
}

function getCutMessage(text) {
    $('#remote_cut_msg')[0].innerHTML = text;
}