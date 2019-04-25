function addToContacts() {
    console.log(HUB);
    HUB.invoke('AddFriend', getID());
}

function getID() {
    var url = window.location;
    var params = /[0-9]*$/g.exec(url);
    console.log(params[0]);
    return params[0];
}