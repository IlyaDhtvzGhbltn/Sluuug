function deleteDialog(selector, dialogId) {

    let item = localStorage.getItem('__' + dialogId)
    if (item != undefined)
        localStorage.removeItem('__' + dialogId);
    let itemK = localStorage.getItem(dialogId);
    if (itemK != undefined)
        localStorage.removeItem(dialogId);
    $.ajax({
        type: 'post',
        url: '/api/delete_dialog',
        data: { ConversationId: dialogId },
        success: function (resp) {
            if (resp) {
                $(selector).remove();
            }
            else _Alert('Произошла ошибка. Пожалуйста повторите попытку позднее.');
        }
    });
}