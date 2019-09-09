//connection.qs = 'URL=' + window.location.href;

//connection.start();

function SendMessage(to_id, value) {
    if (value.length > 0) {
        HUB.invoke('SendMessage', value, 0, to_id);
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