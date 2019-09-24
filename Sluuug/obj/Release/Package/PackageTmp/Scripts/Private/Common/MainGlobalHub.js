var connection = $.hubConnection();
var HUB = connection.createHubProxy('MainGlobalHub');
connection.start().done(function () {
    HUB.invoke('OpenConnect');
});
window.onunload = function () {
    HUB.invoke('CloseConnect');
};
HUB.on('CatchException', function (message) {
    _Alert(message);
});

HUB.on('NotifyAbout', function (html, params, notifyCode) {
    switch (notifyCode) {
        //case 0:
        //    IncrementInto('.notify-container-increment-message', 'not-show-message-counter');
        //    break;
        case 2: case 4:
            IncrementInto('.notify-container-increment-crypto', 'not-show-crypto-counter');
            break;
        case 1:
            IncrementInto('.notify-container-increment-video', 'not-show-video-counter');
            break;
        case 5: case 6:
            IncrementInto('.notify-container-increment-contact', 'not-show-contact-counter');
            break;
    }

    if (params !== null)
    {
        newInviteToCryptChatNotification(html, params);
    }

    let notifyAlertAllow = boolSetting('notifyalert');
    console.log(notifyAlertAllow);
    if (notifyAlertAllow) {
        var note = $('.incomming-notify');
        note[0].innerHTML = html;
        note.css({ opacity: 1, left: 200 });
        setTimeout(clearNotificationDiv, 4000);
    }

    let soundAllow = boolSetting('notifysound');
    if (soundAllow) {
        var audio = new Audio('/resources/audio/new-notify-sound.wav');
        audio.play();
    }
});

function IncrementInto(elementContainer, counterSelector, forceSetValue = null) {
    if (forceSetValue == null) {
        var decrementedElement = $(elementContainer)[0];
        if (decrementedElement.innerHTML.length === 0) {
            decrementedElement.innerHTML = '<div class="new"><span id="' + counterSelector + '">1</span></div>';
        }
        else {
            var notivicationsCount = parseInt($('#' + counterSelector)[0].innerHTML);
            notivicationsCount = notivicationsCount + 1;
            $('#'+counterSelector)[0].innerHTML = notivicationsCount;
        }
    }
    else {
        var decrementedElement = $(elementContainer)[0];
        decrementedElement.innerHTML = '<div class="new"><span id="' + counterSelector + '">' + forceSetValue + '</span></div>';
    }
}
function newInviteToCryptChatNotification(html, publicData) {
    var note = $('.incomming-notify');
    note[0].insertAdjacentHTML('beforeend', html);


    var currentLocation = window.location.href;
    var isScriptCryptoChatALreadyLoaded = currentLocation.includes('crypto_cnv');
    if (isScriptCryptoChatALreadyLoaded == false) {
        var script = document.createElement('script');
        script.src = "/Scripts/Private/Dialogs/CryptoDialog/CryptoDialogInitialization.js";
        document.documentElement.appendChild(script);

        script.onload = function () {
            console.log('script was added');
            var invited = new Invited();
            invited.save_invitation(publicData);
        };
    }
    else {
        var invited = new Invited();
        invited.save_invitation(publicData);
    }
    setTimeout(clearNotificationDiv, 4000);
}
function clearNotificationDiv() {
    var note = $('.incomming-notify');
    note.css({ opacity: 0, left: -400 });
    note[0].innerHTML = '';
}