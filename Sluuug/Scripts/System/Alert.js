function CloseAlert() {
    $('.alert').removeClass('show');
    $('.alert').addClass('hide');
}

function _Alert(message) {
    $('.alert').removeClass('hide');
    $('.alert').addClass('show');
    $('.alert-text')[0].innerHTML = message;
}