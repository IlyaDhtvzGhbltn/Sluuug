function redirectToUser(id) {
    let domain = window.location.host;
    let protocol = location.protocol;
    let url = protocol + '//' + domain + '/private/user/' + id;
    console.log(url);
    window.location.replace(url)
}