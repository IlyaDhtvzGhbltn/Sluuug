var session_id = document.cookie.replace(/(?:(?:^|.*;\s*)session_id\s*\=\s*([^;]*).*$)|^.*$/, "$1");
var connection = $.hubConnection();
var messagesChat = connection.createHubProxy('p2pChat');

messagesChat.on('sendAsync', function (img, userName, message) {

    //console.log('msg was recived');
    addMsg(img, userName, message);
});

connection.start().done(function () {
    $('#sendButton').click(function () {
        if (session_id.length == 120) {
            messagesChat.invoke('SendMessage', session_id, $('#new_msg').val());
            //console.log('msg was sended');
        }
        else {
            window.location.href = 'http://localhost:32033/private/my';
        }
    });
})

function addMsg(img_src, name, text )
{
    var mylist = $('#dialog');
    mylist[0].insertAdjacentHTML('beforeend',
        '<div class="dialog_msg"><img src="' + img_src + '" height="45" width="45" /><span>' + name + '</span><p>' + text + '</p></div>');
}