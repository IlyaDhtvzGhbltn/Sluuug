var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');

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
        case 0:
            DecrementInto('.notify-decrement-message', 'notify-message');
            break;
        case 2: case 3: case 4:
            DecrementInto('.notify-decrement-secret', 'notify-secret');
            break;
        case 1:
            DecrementInto('.notify-decrement-video', 'notify-video');
            break;
        case 5: case 6:
            DecrementInto('.notify-decrement-contact', 'notify-contacts');
            break;
    }

    if (params !== null)
    {
        newInviteToCryptChatNotification(html, params);
    }


    let notifyAlertAllow = boolSetting('notifyalert');
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


function DecrementInto(element, elementCounterClassName) {
    var decrementedElement = $(element)[0];
    if (decrementedElement.innerHTML.length === 0) {
        decrementedElement.innerHTML = '<svg><circle></circle><span class="new-notify ' + elementCounterClassName+'">1</span></svg >';
    }
    else {
        var notivicationsCount = parseInt($('.new-notify.' + elementCounterClassName+'')[0].innerHTML);
        notivicationsCount = notivicationsCount + 1;
        $('.new-notify.' + elementCounterClassName + '')[0].innerHTML = notivicationsCount;
    }
}

function newInviteToCryptChatNotification(html, publicData) {
    var note = $('.incomming-notify');
    note[0].insertAdjacentHTML('beforeend', html);


    var currentLocation = window.location.href;
    var isScriptCryptoChatALreadyLoaded = currentLocation.includes('crypto_cnv');
    if (isScriptCryptoChatALreadyLoaded == false) {
        var script = document.createElement('script');
        script.src = "/Scripts/crypto_chat.js";
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