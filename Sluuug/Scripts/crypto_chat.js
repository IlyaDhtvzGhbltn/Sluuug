document.querySelector('#new_crypto_chat').addEventListener("click", function () {
    invite();
});

var connection = $.hubConnection();
var cryptoChat = connection.createHubProxy('cryptoMessagersHub');
connection.start();
cryptoChat.on('Invite', function (offer) {
    got_invite(offer);
});
cryptoChat.on('NewCryptoChatResponce', function (guid) {
    console.log('created new crypto chat : ' + guid);
    public_pre_values = generate_pre_value();

    var offer_toCryptoChat =
        {
            pValue: public_pre_values['p'],
            gValue: public_pre_values['g'],
            guid: guid
        };

    console.log(offer_toCryptoChat);
    cryptoChat.invoke('InviteToCreatedNew', offer_toCryptoChat);
});


function invite()
{
    inviters = []; 
    friends = $(".ready_to_invite");

    for (var i = 0; i < friends.length; i++) {
        if (friends[i].checked) {
            inviters.push(friends[i].value);
        }
    }
    if (inviters.length == 0) {
        console.log('no one selected');
    }
    else {
        console.log(inviters);
        var create_request =
            {
                'type': 0,
                'inviters': inviters,
            };
        cryptoChat.invoke('CreateRequest', JSON.stringify( create_request ));
    }
}

function generate_pre_value()
{
    g = 2;
    mod = -1;
    p = null;

    while (mod != 1) {
        g = 3;
        p = generate_p();
        for (i = 1; i < 10; i++) {
            mod = check_mod(g, p);
            if (mod == 1) {
                console.log('p value = ' + p);
                console.log('g value = ' + g);
                break;
            }
            g++;
        }
    }

    return { 'p': p, 'g': g };
}


function generate_p() {
    p = null;
    max = Math.floor(Math.random() * 100);
    var sieve = [], i, j, primes = [];
    for (i = 2; i <= max; ++i) {
        if (!sieve[i]) {
            primes.push(i);
            for (j = i << 1; j <= max; j += i) {
                sieve[j] = true;
            }
        }
    }
    var randPrime = primes[Math.floor(Math.random() * primes.length)];
    return randPrime;
}

function generate_a() {
    flag = false;
    a = null;
    while (!flag) {
        a = Math.floor(Math.random() * 1000);
        if (a > 300 && a < 400) {
            flag = true;
        }
    }
    return a;
}

function secret_key(publKey) {

}

function check_mod(g, p) {
    mod = (g ** (p - 1)) % p;
    return mod;
}

function generate_public_key(gValue, aValue, pValue) {
    key = gValue ** aValue % pValue;
    return key;
}

function got_invite(offer) {
    console.log(offer);
    document.querySelector('#currentSC').insertAdjacentHTML('beforeend',
        '<div class="cryptp_chat"></div>');
}
