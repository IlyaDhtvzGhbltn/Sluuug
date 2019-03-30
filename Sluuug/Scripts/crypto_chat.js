document.querySelector('#new_crypto_chat').addEventListener("click", function () {
    crypto();
    //localStorage.setItem('private_key', private_key());
});

function crypto() {
    g = 2;
    mod = -1;
    p = null;
    a = null;

    while (mod != 1) {
        p = generate_p();
        for (i = 2; i < 20; i++) {
            mod = (g ** (p - 1)) % p;
            g++;
            if (mod == 1) {
                console.log('p value = ' + p);
                console.log('g value = ' + g);
                break;
            }
        }
    }
    a = generate_a();
    localStorage.setItem('a',a);
    console.log('(secret)a value = ', a);
    publ_key = generate_public_key(g, a, p);
    console.log('public_key value = ' + publ_key);
}

function generate_p() {
    p = null;
    while (true) {
        p = Math.floor(Math.random() * 1000);
        if (p > 100 && p < 200)
            break;
    }
    return p;
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

function generate_public_key(gValue, aValue, pValue) {
    key = gValue ** aValue % pValue;
    return key;
}