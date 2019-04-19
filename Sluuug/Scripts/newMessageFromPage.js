var connection = $.hubConnection();
var messagesChat = connection.createHubProxy('messagersHub');
connection.qs = 'URL=' + window.location.href;

connection.start();

function create_form(elem) {
    let button = document.getElementById(elem.id);
    button.hidden = true;
    let info = document.getElementById('user_cut_data');
    info.insertAdjacentHTML('beforeend',
        '<div id="new_message_' + elem.id + '">' +
        '<span> insert your message</span>' +
        '<p><input type="text" id ="form_' + elem.id + '"></p>' +
        '<p><button onclick="send_msg(' + elem.id + ')">Send</button>' +
        '<button onclick="drop_form(' + elem.id + ')">Close</button></div></p>'
    );
}

function drop_form(id) {
    document.getElementById('new_message_' + id).remove();
    let button = document.getElementById(id);
    button.hidden = false;
}

function send_msg(to_id) {
    let text = document.getElementById('form_' + to_id).value;
    if (!is_empty(text)) {
        messagesChat.invoke('SendMessage', text, 0, to_id);
        window.location.href = '/private/cnv';
    }
    else {
        alert('too small messsage');
    }
}

function is_empty(text) {
console.log(text);
    if (text.length >= 2)
        return false;
    else
        return true;
}