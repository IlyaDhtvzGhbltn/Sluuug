HUB.on('GetMessage', function (object, convId)
{
    UpdateDialog(object.Text, convId);
});

function UpdateDialog(message, convId) {
    $('#' + convId).css({ 'background-color': 'rgb(162, 236, 243)'});
    var messageContainer = $('#' + convId).children('.confersation-body').children('span')[0];
    var dateContainer = $('#' + convId).children('.confersation-body').children('span')[1];
    dateContainer.innerHTML = 'Только что';
    if (message < 30) {
        messageContainer.innerHTML = message;
    }
    else {
        message = message.substring(0, 27) + '...';
        messageContainer.innerHTML = message;
    }
}