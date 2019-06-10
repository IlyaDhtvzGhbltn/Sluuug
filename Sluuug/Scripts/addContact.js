function addToContacts() {
    HUB.invoke('AddFriend', getID());
}

HUB.on('AddUserResponce', function (responce_html) {
    $("#add_contact").html(responce_html);  

});

function getID() {
    var url = window.location;
    var params = /[0-9]*$/g.exec(url);
    return params[0];
}