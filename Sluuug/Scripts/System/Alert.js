function CloseAlert() {
    $('.alert').removeClass('show');
    $('.alert').addClass('hide');
}

function _Alert(message, alertTextColor = null) {
    if (alertTextColor != null) {
        $('.alert-text').css('color', alertTextColor);
    }
    $('.alert').removeClass('hide');
    $('.alert').addClass('show');
    $('.alert-text')[0].innerHTML = message;
}