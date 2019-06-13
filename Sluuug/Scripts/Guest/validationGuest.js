var validateLoginError = 0;
var validateEmailError = 0;

$('#reg_subm').bind("submit", function (e) {
    var val = document.getElementById("psw").value;
    var shaObj = new jsSHA("SHA-512", "TEXT");
    shaObj.update(val);
    var hash = shaObj.getHash("HEX");
    document.getElementById("psw").value = hash;
});

function register() {
    let validationResult = validateFormById('register_form');
    if (validationResult) {
        var data = $("#register_form").serializeArray();
        var json = parceJSON(data);
        let passHash = getSHA(json.PasswordHash);
        json.PasswordHash = passHash;


        $('.alert_form')[0].style.display = 'none';
        $('#circular3dG').fadeIn();
        $.ajax({
            url: '/guest/userconfirmation',
            data:  json , 
            method: 'post',
            success: function (resp) {
                console.log('access to register ' + resp);
                $('#circular3dG').fadeOut();
                let registerResultForm = $('.result_empty').offset().top;

                window.scrollTo({
                    top: registerResultForm + 100,
                    behavior: 'smooth'});

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
        $('#circular3dG')[0].style.display = 'none';
        $('.alert_form').fadeOut();
        $('.alert_form').fadeIn();
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
        }
    });
}