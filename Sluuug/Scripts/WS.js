var connection = $.hubConnection();
var contosoChatHubProxy = connection.createHubProxy('contosoChatHub');
contosoChatHubProxy.on('addContosoChatMessageToPage', function (userName, message) {
    console.log(userName + ' ' + message);
});

connection.start().done(function () {
    console.log('Now connected, connection ID=' + connection.id);

    $('#sendButton').click(function () {
        contosoChatHubProxy.invoke('newContosoChatMessage', "fei han", $('#messageInput').val());
        $('#messageInput').val('').focus();
    });
}).fail(function () { console.log('Could not connect'); });