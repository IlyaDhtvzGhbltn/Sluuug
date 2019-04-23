﻿var connection = $.hubConnection();
var cryptoChat = connection.createHubProxy('cryptoMessagersHub');
connection.qs = 'URL=' + window.location.href;
connection.start();

cryptoChat.on('NewMessage', function (message, avatar, name, date, guidChatId) {
    get_new(message, avatar, name, date);
});


function crypt_send() {
    let text = $('#new_text').val();
    let url = new URL(window.location.href);
    let id = url.searchParams.get('id');
    let skey = JSON.parse(localStorage.getItem('__' + id));

    var hash = CryptoJS.AES.encrypt(text, skey.K.toString());
    var cryptStr = hash.toString();
    cryptoChat.invoke('SendMessage', cryptStr);
}

function get_new(message, avatar, name, date) {

    let decrypted = decryption(message);
    var mylist = $('#c_msgs');
    mylist[0].insertAdjacentHTML('beforeend', '<div class="c_msg"><img width="30" height="30" src="' + avatar + '" /><span>' + name + '</span>' +
        '<p>' + date + '</p><span>' + decrypted + '</span></div > ');
}

function decryption(message) {
    let url = new URL(window.location.href);
    let id = url.searchParams.get('id');
    let skey = JSON.parse(localStorage.getItem('__' + id));
    var decrypted = CryptoJS.AES.decrypt(message, skey.K.toString());
    return decrypted.toString(CryptoJS.enc.Utf8);
}

function onLoad() {
    var messages = $('.c_msg');
    for (let i = 1; i <= messages.length; i++){
        let span_msg = getElementByXpath('//*[@id="c_msgs"]/div[' + i + ']/span[2]');
        let cryptText = span_msg.textContent;
        let decryptText = decryption(cryptText);
        span_msg.textContent = decryptText;
    }
}

function getElementByXpath(path) {
    return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
}
window.ready = onLoad();