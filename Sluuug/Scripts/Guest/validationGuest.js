var validateLoginError = 0;
var validateEmailError = 0;


function register() {
    let validationResult = validateFormById('validate_register_form');
    if (validationResult) {
        var data = $("#validate_register_form").serializeArray();
        var json = parceJSON(data);

        let passHash = getSHA(json.PasswordHash);
        json.PasswordHash = passHash;

        correctInput('reg_subm');
        console.log('submit to register');
        $.ajax({
            url: '/guest/userconfirmation',
            data:  json , 
            method: 'post',
            success: function (resp) {
                $('#reg_subm').attr("disabled", false);
                $('#circular3dG').fadeOut();
                console.log(resp);
                if (resp) {
                    showResultForm('/partial/success_register');
                }
                else {
                    showResultForm('/partial/fail_register');
                }
            }
        });

    }
    else {
        incorrectInput();
    }
}
function logIn() {
    let validationResult = validateFormById('validate_login_form');
    if (validationResult) {
        correctInput('log_subm');

        var data = $("#validate_login_form").serializeArray();
        var json = parceJSON(data);
        let passHash = getSHA(json.hashPassword);
        json.hashPassword = passHash;

        $.ajax({
            method: 'post',
            url: '/guest/auth',
            data: json,
            success: function (resp) {
                $('#log_subm').attr("disabled", false);
                $('#circular3dG').fadeOut();

                console.log(resp);
                if (resp === false) {
                    $('#invalid_credentials').fadeIn();
                }
                else {
                    window.location.replace('/private/my');
                }
            }
        });
    }
    else {
        incorrectInput();
    }
}
function resetPassword() {
    let validate = validateFormById('reset_password_form');
    if (validate) {
        $('#invalid_emeil').fadeOut();
        $('#reset_emeil').fadeOut();
        $('#reset_emeil').fadeIn();

        $.ajax({
            method: 'post',
            url: '/public_api/resetpassword',
            data: { email: $('#oldEmailToReset').val() }
        });

        $('#oldEmailToReset').val('');
    }
    else {
        $('#invalid_emeil').fadeOut();
        $('#invalid_emeil').fadeIn();
    }
}
function sendFeedback() {
    $('#success').fadeOut();
    $('#error').fadeOut();

    let validation = validateFormById('feedBackForm');
    var len = $('#feed_back_text')[0].textLength;
    console.log(len);
    var json = parceJSON($('#feedBackForm').serializeArray());
    if (validation && len >= 100) {
        $.ajax({
            url: '/public_api/feed_back',
            method: 'post',
            data:  json ,
            success: function (resp) {
                if (resp.IsSuccess) {
                    $('#success').fadeIn();
                }
            }
        });
    }
    else {
        $('#error').fadeIn();
    }
}

async function verify_login(elem) {
    let n_login = elem.value;
    $.ajax({
        url: "/public_api/verify_login",
        type: "post",
        data: { login: n_login }
    })
        .done(function (resp) {
            if (resp == false) {
                validateLoginError = 1;
                getLoginAlreadyTakenView();
            }
            else {
                validateLoginError = 0;
                $('#login_space')[0].innerHTML = '';
                if (validateEmailError == 0 && validateLoginError == 0) {
                    $('#reg_subm')[0].disabled = false;
                }
            }
        });
}
async function verify_email(elem) {
    let n_em = elem.value;
    $.ajax({
        url: "/public_api/verify_email",
        type: "post",
        data: { email: n_em }
    })
        .done(function (resp) {
            if (resp == false) {
                validateEmailError = 1;
                getEmailAlreadyTakenView();
            }
            else {
                validateEmailError = 0;
                $('#email_space')[0].innerHTML = '';
                if (validateEmailError == 0 && validateLoginError == 0) {
                    $('#reg_subm')[0].disabled = false;
                }
            }
        });
}
function getLoginAlreadyTakenView() {
    $.ajax({
        url: "/partial/login_already_taken",
        type: "post",
        data: {},
        success: function (response) {
            $('#reg_subm')[0].disabled = true;
            $('#login_space')[0].innerHTML = response;
        }
    });
}
function getEmailAlreadyTakenView() {
    $.ajax({
        url: "/partial/email_already_taken",
        type: "post",
        data: {},
        success: function (response) {
            $('#reg_subm')[0].disabled = true;
            $('#email_space')[0].innerHTML = response;
        }
    });
}

async function showResultForm(addres) {
    await $.ajax({
        url: addres,
        method: 'post',
        success: function (resp) {
            $('.result_empty')[0].style.display = 'none';
            $('.result_empty')[0].innerHTML = resp;
            $('.result_empty').addClass('result');
            $('.result').fadeIn();
            $(window).scrollTop($('.register-result').offset().top);
        }
    });
}

function incorrectInput() {
    $('#circular3dG')[0].style.display = 'none';
    $('#not_filled').fadeOut();
    $('#not_filled').fadeIn();
}
function correctInput(disableId) {
    $('#not_filled')[0].style.display = 'none';
    $('#circular3dG').fadeIn();
    $('#' + disableId).attr("disabled", true);
}