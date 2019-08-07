//var connection = $.hubConnection();
//var cryptoChat = connection.createHubProxy('cryptoMessagersHub');
//connection.start();
connection.qs = 'URL=' + window.location.href;

HUB.on('NewMessage', function (message, avatar, name, date, guidChatId) {
    get_new(message, avatar, name, date);
});


function crypt_send() {
    let text = $('#new_text').val();
    let url = new URL(window.location.href);
    let id = url.searchParams.get('id');
    let skey = JSON.parse(localStorage.getItem('__' + id));
    console.log(skey);
    var hash = CryptoJS.AES.encrypt(text, skey.K.toString());
    console.log(hash);
    var cryptStr = hash.toString();
    HUB.invoke('SendMessage', cryptStr);
}

function get_new(message, avatar, name, date) {
    let decrypted = decryption(message);
    var mylist = $('.dialog');
    mylist[0].insertAdjacentHTML('beforeend',
        '<div class="c_msg"><img width="30" height="30" src="' + avatar + '" /><span>' + name + '</span>' +
        '<p>' + date + '</p><span>' + decrypted + '</span></div > ');
}

function decryption(message) {
    let url = new URL(window.location.href);
    let id = url.searchParams.get('id');
    if (id === null) {
        let regex = /(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}/i;
        id = url.toString().match(regex)[0];
    }
    let skey = JSON.parse(localStorage.getItem('__' + id));
    console.log(skey);
    var decrypted = CryptoJS.AES.decrypt(message, skey.K.toString());
    return decrypted.toString(CryptoJS.enc.Utf8);
}

function onLoad() {
    var messages = $('.crypt-message');

    for (let i = 0; i < messages.length; i++){
        let cryptText = messages[i].innerHTML;
        let decryptText = decryption(cryptText);
        messages[i].innerHTML = decryptText;
    }
}

function getElementByXpath(path) {
    return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
}
window.ready = onLoad();
