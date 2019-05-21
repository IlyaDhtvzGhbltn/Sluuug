//var connection = $.hubConnection();
//var cryptoChat = connection.createHubProxy('cryptoMessagersHub');
//connection.start();

class public_data_crypto_conversation {
    convGuidId;
    p;
    g;
    creationDate;
    expireDate;
    participants;
    type;
    creatorUserId;
    creatorAvatar;
    creatorName;
}
class private_data_crypto_conversation {
    a;
    K;
}
class Participant {
    UserId;
    PublicKey;
}

class Crypto {

    generate_public_key(gValue, aValue, pValue) {
        console.log('public key');
        console.log('g = ' + gValue);
        console.log('a = ' + aValue);
        console.log('p = ' + pValue);

        var key = bigInt(gValue).pow(aValue);
        console.log('g(' + gValue + ') ** a(' + aValue + ') = ' + key);
        key = key.mod(pValue);
        console.log('g ** a mod p(' + pValue + ') = ' + key);
        console.log('------');
        show();
        return key;
    }

    check_mod(g, p) {
        var mod = bigInt(g).pow(p - 1).mod(p);
        console.log('mod = ' + mod);
        return mod;
    }

    generate_secret_key(AB, a, p) {
        var K = bigInt(AB).pow(a);
        console.log('publicKey(' + AB + ') ** secret a(' + a + ') = ' + K);
        K = K.mod(p);
        console.log('publicKey ** secret a mod p(' + p + ') = ' + K);
        return K;
    }

    generate_pre_value() {
        var mod = -1;
        var primes_mass = this.generate_p();
        while (mod != 1) {
            for (let i = primes_mass.length; i > 1; i--) {
                var g = getRandomInt(4, 8);
                var p = primes_mass[getRandomInt(0, primes_mass.length - 1)];
                console.log('p = ' + p + ' g = ' + g);
                mod = this.check_mod(g, p);
                if (mod == 1) {
                    return { 'p': p, 'g': g };
                }
                else {
                    g++;
                }
            }
        }
    }

    generate_p() {
        console.log('generate primires');
        var p = null;
        var max = getRandomInt(100000, 10000000);
        var sieve = [], i, j, primes = [];
        for (i = 2; i <= max; ++i) {
            if (!sieve[i]) {
                primes.push(i);
                for (j = i << 1; j <= max; j += i) {
                    sieve[j] = true;
                }
            }
        }
        console.log('generate primes completed with ' + primes.length + ' values');
        return primes;
    }

    generate_a() {
        var flag = false;
        var a = null;
        while (!flag) {
            a = Math.floor(Math.random() * 100);
            flag = true;
        }
        return a;
    }
}
class Invited {

    save_invitation(crypto_conversation) {
        localStorage.setItem(crypto_conversation.convGuidId, JSON.stringify(crypto_conversation));
    }

    got_invitation(crypto_conversation) {
        localStorage.setItem(crypto_conversation.convGuidId, JSON.stringify(crypto_conversation));

        document.querySelector('#currentSC').insertAdjacentHTML('beforeend',
            '<div class="cryptp_chat" style="cursor:pointer" >' +
            '<span>Opening date : ' + crypto_conversation.creationDate + ' </span>' +
            '<span>Chat Active : True</span>' +
            '<p><span>Inviter : </span></p>' +
            '<span>' + crypto_conversation.creatorName + '</span>' +
            '<img src="' + crypto_conversation.creatorAvatar + '" height="30" width="30">' +
            '<button onclick="accept_invite(this)" id="' + crypto_conversation.convGuidId +'" />Accept</button>' +
            '</div>');

    }

    accept_invitation(event_handler) {
        fetch('/api/get_user_info', {
            method: 'post',
            body: '{}'
        })
            .then(function (resp) {
                return resp.json();
            })
            .then(function (json) {
                var crypto = new Crypto();
                console.log(event_handler.id);
                var localPublicJSON = JSON.parse(localStorage.getItem(event_handler.id));

                var userId = json.UserId;
                let a = crypto.generate_a();
                console.log(localPublicJSON);
                let p = localPublicJSON.p;
                let g = localPublicJSON.g;

                let publicKey = crypto.generate_public_key(g, a, p);
                let foreignABKey = null;
                for (var i = 0; i < localPublicJSON.participants.length; i++) {
                    if (localPublicJSON.participants[i].UserId !== userId) {
                        foreignABKey = localPublicJSON.participants[i].PublicKey;
                    }
                }
                let privateKey = crypto.generate_secret_key(foreignABKey, a, localPublicJSON.p);
                console.log(privateKey);

                let privateData = new private_data_crypto_conversation();
                privateData.a = a;
                privateData.K = privateKey;
                let chatSecretName = '__' + event_handler.id;


                localStorage.setItem(chatSecretName, JSON.stringify(privateData));

                for (var i = 0; i < localPublicJSON.participants.length; i++) {
                    if (localPublicJSON.participants[i].UserId === userId) {
                        localPublicJSON.participants[i].PublicKey = publicKey;
                    }
                }

                localStorage.removeItem(event_handler.id);
                let strPublicData = JSON.stringify(localPublicJSON);
                localStorage.setItem(event_handler.id, strPublicData);
                HUB.invoke('AcceptInvite', strPublicData)
                    .then(function () {
                        let accept_btm = document.getElementById(event_handler.id);
                        accept_btm.parentNode.removeChild(accept_btm);

                    });
                location.reload();
            });

    }
}
class Inviter {

    create_new_crypto_conversation() {
        var invitersIds = [];
        var friends = $(".ready_to_invite");

        for (var i = 0; i < friends.length; i++) {
            if (friends[i].checked) {
                invitersIds.push(friends[i].value);
            }
        }
        if (invitersIds.length == 0) {
            console.log('no one friend selected');
        }
        else {
            hide();
            console.log(invitersIds);
            let crypto_cnv = new public_data_crypto_conversation();

            crypto_cnv.type = 0;
            crypto_cnv.participants = [];

            for (var i = 0; i < invitersIds.length; i++) {
                let participant = new Participant();
                participant.UserId = invitersIds[i];

                crypto_cnv.participants.push(participant);
            }
            HUB.invoke('CreateNewCryptoConversation', JSON.stringify(crypto_cnv));
        }
    }

    send_notivication_to_participants(crypto_cnv) {

        fetch('/api/get_user_info',
            {
                method: 'post',
                body: '{}'
            })
            .then(function (resp) {
                return resp.json();
            })
            .then(function (json) {
                var crypto = new Crypto();
                var userId = json.UserId;

                let public_pre_values = crypto.generate_pre_value();
                let a = crypto.generate_a();
                let public_key = crypto.generate_public_key(public_pre_values['g'], a, public_pre_values['p']);
                let privateData = new private_data_crypto_conversation();
                privateData.a = a;
                let chatSecretName = '__' + crypto_cnv.convGuidId;
                localStorage.setItem(chatSecretName, JSON.stringify(privateData));


                crypto_cnv.p = public_pre_values['p'];
                crypto_cnv.g = public_pre_values['g'];

                let participants = crypto_cnv.participants;
                for (let i = 0; i < participants.length; i++) {
                    if (participants[i].UserId == userId) {
                        participants[i].PublicKey = public_key;
                    }
                }
                HUB.invoke('InviteUsersToCryptoChat', JSON.stringify(crypto_cnv), crypto_cnv.convGuidId);
                localStorage.setItem(crypto_cnv.convGuidId, JSON.stringify(crypto_cnv));

                document.querySelector('#self_created').insertAdjacentHTML('beforeend',
                    '<div class="cryptp_chat" style="background-color:azure; cursor:pointer">'+
                   '<span>Ваш собственный чат с пользователями :</span></div>');
            });
    }

    got_invited_answer(crypto_cnv) {

        fetch('/api/get_user_info', {
            method: 'post',
            body: '{}'
        })
            .then(function (resp) {
                return resp.json();
            })
            .then(function (json) {
                var crypto = new Crypto();
                var userId = json.UserId;

                var jsonPublicData = JSON.parse(crypto_cnv);
                let guidID = jsonPublicData.convGuidId;
                localStorage.removeItem(guidID);
                localStorage.setItem(guidID, JSON.stringify(jsonPublicData));

                let foreignABKey = null;
                for (var i = 0; i < jsonPublicData.participants.length; i++) {
                    if (jsonPublicData.participants[i].UserId !== userId) {
                        foreignABKey = jsonPublicData.participants[i].PublicKey;
                    }
                }
                let chatSecretName = '__' + jsonPublicData.convGuidId;

                let localePrivateJson = JSON.parse(localStorage.getItem(chatSecretName));
                console.log(localePrivateJson);

                let privateKey = crypto.generate_secret_key(foreignABKey, localePrivateJson.a, jsonPublicData.p);

                localePrivateJson.K = privateKey;

                localStorage.removeItem(chatSecretName);
                localStorage.setItem(chatSecretName, JSON.stringify(localePrivateJson));
                location.reload();
            });
    }

}

var inviter = new Inviter();
var invited = new Invited();

function invite() {
    inviter.create_new_crypto_conversation();
}

function accept_invite(object) {
    invited.accept_invitation(object);
}

function hide() {
    $('#new_crypto_chat')[0].type = 'hidden';
    $('#new_crypto_chat')[0].onclick = null;
    document.querySelector('#create_new').insertAdjacentHTML('beforeend',
        '<div id="wait_div"><p><span>key generation... please be patient.</span></p></div>');
}

function show() {
    $('#new_crypto_chat')[0].onclick = 'invite()';
    $('#new_crypto_chat')[0].type = 'button';
    let wait = $('#wait_div')[0];
    if (wait !== undefined) {
        wait.innerHTML = '';
    }
}

HUB.on('NewCryptoConversationCreated', function (crypto_cnv) {
    inviter.send_notivication_to_participants(crypto_cnv);
});

HUB.on('ObtainNewInvitation', function (crypto_cnv) {
    invited.got_invitation(crypto_cnv);
});

HUB.on('AcceptInvitation', function (crypto_cnv) {
    inviter.got_invited_answer(crypto_cnv);
});

HUB.on('NewMessage', function (crypto_msg, avatar, name, date, guidChatId) {
    got_message(crypto_msg, guidChatId);
});

function ready() {
    var elements = document.getElementsByClassName('cryptp_chat');
    [].forEach.call(elements, function (elem) {
        elem.addEventListener('click', function () {
            window.location.href = "/private/c_msg?id=" + elem.id;
        });
        let span_msg = getElementByXpath('//*[@id="' + elem.id + '"]/span[2]');
        let cryptText = span_msg.textContent;
        let decryptText = decryption(cryptText, elem.id);
        span_msg.textContent = decryptText;
    });
}
document.addEventListener('onload', ready());

function getElementByXpath(path) {
    return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
}

function decryption(message, id) {
    console.log(id);
    var skey = JSON.parse(localStorage.getItem('__' + id));
    if (skey.K != undefined) {
        var decrypted = CryptoJS.AES.decrypt(message, skey.K.toString());
        return decrypted.toString(CryptoJS.enc.Utf8);
    }
    else return '...';
}

function got_message(crypto_msg, guidChatId) {

    let span_msg = getElementByXpath('//*[@id="' + guidChatId + '"]/span[2]');
    let cryptText = span_msg.textContent;
    let decryptText = decryption(crypto_msg, guidChatId);
    span_msg.textContent = decryptText;
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}