//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
window.onload = function () {
    sendButton.onclick = function () {
        var text = $('#new_msg').val();
        if (/\S/.test(text)) {
            connection.start().done(function () {
                HUB.invoke('SendMessage', text, sendButton.name, 0);
            });
            $('#new_msg')[0].value = '';
        }
    }

    $('#new_msg')[0].addEventListener('input', function () {
        var url_string = window.location.href;
        var url = new URL(url_string);
        var id = url.searchParams.get("id");
        HUB.invoke('SendCutMessage', this.value, id);
    });
}

HUB.on('sendAsync', function (html, convGuidId) {
    addMsg(html, convGuidId);
});

HUB.on('getCutMessage', function (text) {
    getCutMessage(text);
});


function addMsg(html, guidId) {
    var mylist = $('#dialog');
    mylist[0].insertAdjacentHTML('beforeend', html);
    $('#remote_cut_msg')[0].innerHTML = '';
}

function getCutMessage(text) {
    $('#remote_cut_msg')[0].innerHTML = text;
}