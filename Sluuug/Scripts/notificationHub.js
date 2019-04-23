var connection = $.hubConnection();
var hub = connection.createHubProxy('notificationHub');

window.onload = function () {
    connection.start().done(
        function () {
            hub.invoke('OpenConnect')
        });
}


window.onunload = function () {
    hub.invoke('CloseConnect');
}