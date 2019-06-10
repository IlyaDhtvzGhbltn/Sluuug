//var connection = $.hubConnection();
//var messagesChat = connection.createHubProxy('messagersHub');
connection.qs = 'URL=' + window.location.href;

connection.start();

function create_form() {
    let elem = document.getElementById("new_message_to_user");
    elem.hidden = false;
}

function drop_elem() {
    let elem = document.getElementById("new_message_to_user");
    elem.hidden = true;
    let error_elem = document.getElementById("error");
    error_elem.hidden = true;
}

function send_msg(to_id) {
    let text = document.getElementById('form_' + to_id).value;
    if (text.length > 0) {
        HUB.invoke('SendMessage', text, 0, to_id);
        window.location.href = '/private/cnv';
    }
    else {
        let elem = document.getElementById("error");
        elem.hidden = false;
        setTimeout(function () {
            let elem = document.getElementById("error");
            elem.hidden = true;
        }, 4000);
    }
}

function is_empty(text) {
console.log(text);
    if (text.length >= 2)
        return false;
    else
        return true;
}