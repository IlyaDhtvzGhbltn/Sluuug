function addToContacts() {
    HUB.invoke('AddFriend', getID()).done(function () {
        $("#add_contact").html("User not accept your invitation");
    });
}

function getID() {
    var url = window.location;
    var params = /[0-9]*$/g.exec(url);
    return params[0];
}