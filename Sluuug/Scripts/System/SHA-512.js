function getSHA(text) {
    var shaObj = sha512(text);
    return shaObj;
}