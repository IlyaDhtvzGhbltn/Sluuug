var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');

connection.start().done(function () {
    HUB.invoke('OpenConnect');
});

window.onunload = function () {
    HUB.invoke('CloseConnect');
};

HUB.on('NotifyAbout', function (html, params, notifyCode) {
    console.log(html);
    switch (notifyCode) {
        case 0:
            DecrementInto('.notify-decrement-message', 'notify-message');
            break;
        case 2:
            DecrementInto('.notify-decrement-secret', 'notify-secret');
            break;
        case 3:
            DecrementInto('.notify-decrement-secret', 'notify-secret');
            break;
        case 4:
            DecrementInto('.notify-decrement-secret', 'notify-secret');
            break;
        case 1:
            DecrementInto('.notify-decrement-video', 'notify-video');
            break;
        case 5:
            DecrementInto('.notify-decrement-contact', 'notify-contacts');
            break;
        case 6:
            DecrementInto('.notify-decrement-contact', 'notify-contacts');
            break;
    }

    if (params !== null)
    {
        newInviteToCryptChatNotification(html, params);
    }
    else
    {
        var note = $('.notify-alert');
        note[0].innerHTML = html;
        note.css({ opacity: 1, left: 200 });
        var audio = new Audio('/resources/audio/new-notify-sound.wav');
        audio.play();
        setTimeout(clearNotificationDiv, 4000);
    }
});


function DecrementInto(element, elementCounterClassName) {
    console.log('index ' + elementCounterClassName);
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
    var note = $('.notify-alert');
    note[0].insertAdjacentHTML('beforeend', html);

    var script = document.createElement('script');
    script.src = "/Scripts/crypto_chat.js";
    document.documentElement.appendChild(script);
    script.onload = function () {
        console.log('download new script');
        invited.save_invitation(publicData);
    };

    setTimeout(clearNotificationDiv, 4000);
}


function clearNotificationDiv() {
    var note = $('.notify-alert');
    note.css({ opacity: 0, left: -400 });
    note[0].innerHTML = '';
}