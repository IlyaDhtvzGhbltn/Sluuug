function getSHA(text) {
    var shaObj = new jsSHA("SHA-512", "TEXT");
    shaObj.update(text);
    var hash = shaObj.getHash("HEX");
    return hash;
}