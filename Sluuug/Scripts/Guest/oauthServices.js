function checkVkUser(vkcode) {
    $.ajax({
        type: 'post',
        url: '/public_api/is_vkuser_registered',
        data: { code : vkcode },
        success: function (resp) {
            console.log(resp);
            verifyUserStatus(resp);
            if (resp.status == 0) {
                vkUserRegister(vkcode);
            }
            if (resp.status == 1) {
                document.location.replace("/");
              }
        }
    });
}

function vkUserRegister(vkcode) {
    console.log(vkcode);
    $.ajax({
        type: 'post',
        url: '/public_api/register_new_vk',
        data: { code: vkcode },
        success: function (resp) {
            if (resp == "True") {
                console.log(resp);
                document.location.replace("/");
            }
        }
    });
}

function okCheckUser(okCode) {
    console.log(sendRequest(okCode, 'register_new_ok'));
}

function sendRequest(codeOauth, api_url) {
    return $.ajax({
        type: 'post',
        url: '/public_api/' + api_url,
        data: { code: codeOauth },
        async: !1
    });
}


function verifyUserStatus(resp) {
    if (resp.status == 0) {
        $("#status").text('Регестрируем нового пользователя...');
    }
    if (resp.status == 1) {
        $("#status").text('Открываем профиль...');
    }
    if (resp.status == 10) {
        $("#status").text('Возникла ошибка авторизации. Вернитесь на главную страницу и повторите попытку. Если ошибка повторяется попробуйте отключить прокси в браузере.');
        $('.await-anim').fadeOut();
        $('#status').css({ "color": "red" });
    }
}


//function checkFbUser(fbCode)
//{
//    console.log(fbCode);
//    $.ajax({
//        type: 'post',
//        url: '/public_api/verifyfbuser',
//        data: { code: fbCode },
//        success: function (resp) {
//            console.log(resp);
//            verifyUserStatus(resp);
//            if (resp.status == 0 || resp.status == 1) {
//                document.location.replace("/");
//            }
//        }
//    });
//}

//function okCheckUser(okCode)
//{
//    console.log(fbCode);
//    $.ajax({
//        type: 'post',
//        url: '/public_api/verifyfbuser',
//        data: { code: fbCode },
//        success: function (resp) {
//            console.log(resp);
//            verifyUserStatus(resp);
//            if (resp.status == 0 || resp.status == 1) {
//                document.location.replace("/");
//            }
//        }
//    });
//}