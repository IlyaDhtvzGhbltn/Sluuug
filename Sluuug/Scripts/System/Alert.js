function CloseAlert() {
    $('.message-container').removeClass('show').addClass('hide');
    $('.grey-background').removeClass('show').addClass('hide');
}

function _Alert(message, alertTextColor = null) {
    if (alertTextColor != null) {
        $('.alert-text').css('color', alertTextColor);
    }
    $('.message-container').removeClass('hide').addClass('show');
    $('.grey-background').removeClass('hide').addClass('show');

    $('.alert-text')[0].innerHTML = message;
}

function _ALertForm(html) {
    $('.grey-background').removeClass('hide').addClass('show');
    $('.html-content')[0].innerHTML = html;
}