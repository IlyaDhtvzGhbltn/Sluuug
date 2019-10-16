function checkVkUser(vkcode) {
    $.ajax({
        type: 'post',
        url: '/public_api/is_vkuser_registered',
        data: { code : vkcode },
        success: function (resp) {
            console.log(resp);
            if (resp.status == 0) {
                $("#vk-status").text('Регестрируем нового пользователя...');
                vkUserRegister(vkcode);
            }
            if (resp.status == 1) {
                $("#vk-status").text('Открываем профиль...');
            }
            if (resp.status == 10) {
                $("#vk-status").text('Возникла ошибка авторизации.Вернитесь на главную страницу и повторите попытку.');
            }
        }
    });
}

function vkUserRegister(vkcode) {
    console.log(vkcode);
    $.ajax({
        type: 'post',
        url: '/public_api/register_new_vk',
        data: { vkOneTimeCode: vkcode },
        success: function (resp) {
            console.log(resp);
        }
    });
}
