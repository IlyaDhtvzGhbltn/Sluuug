document.querySelector('#new_crypto_chat').addEventListener("click", function () {
    invite();
});

var connection = $.hubConnection();
var cryptoChat = connection.createHubProxy('cryptoMessagersHub');
connection.start();

cryptoChat.on('Invite', function (p, g) {
    got_invited(p, g);
});
function invite()
{
    inviters = []; 
    friends = $(".ready_to_invite");

    for (var i = 0; i < friends.length; i++) {
        if (friends[i].checked) {
            inviters.push(friends[i]);
        }
    }
    if (inviters.length == 0) {
        alert('no one friend selected!');
    }
    else {
        algo = crypto();
        console.log(algo);
        cryptoChat.invoke('CreateNew', inviters, algo[0], algo[1]);
    }
}

function crypto() {
    g = 2;
    mod = -1;
    p = null;
    a = null;

    while (mod != 1) {
        p = generate_p();
        if (p > 8) {
            for (i = 1; i < 100; i++) {
                mod = check_mod(g, p);
                if (mod == 1) {
                    console.log('p value = ' + p);
                    console.log('g value = ' + g);
                    break;
                }
                g++;
            }
        }
    }
    a = generate_a();
    localStorage.setItem('a',a);
    console.log('(secret)a value = ', a);
    publ_key = generate_public_key(g, a, p);
    console.log('public_key value = ' + publ_key);
    return p, g;
}

function generate_p() {
    p = null;
    max = Math.floor(Math.random() * 100000);
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
    a = null;
    while (true) {
        a = Math.floor(Math.random() * 100);
        if (a > 10 && a < 20)
            break;
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

function got_invited(p, g) {
    console.log(p);
    console.log(g);
}