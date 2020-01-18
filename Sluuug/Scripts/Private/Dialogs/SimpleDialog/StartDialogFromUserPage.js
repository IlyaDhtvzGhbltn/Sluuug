//connection.qs = 'URL=' + window.location.href;

//connection.start();

function SendMessage(to_id, value) {
    if (value.length > 0) {
        HUB.invoke('SendMessage', value, 0, to_id);
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

HUB.on('MessageSendedResult', function (access, reason) {
    console.log(access);
    console.log(reason);
    if (!access) {
        _Alert(reason, 'red');
    }
    else {
        _Alert("Ваше сообщение отправлено!", 'green');
    }
});

function is_empty(text) {
console.log(text);
    if (text.length >= 2)
        return false;
    else
        return true;
}