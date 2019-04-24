var connection = $.hubConnection();
var HUB = connection.createHubProxy('notificationHub');
connection.start().done(function () {
    HUB.invoke('OpenConnect');
})


window.onunload = function () {
    HUB.invoke('CloseConnect');
}