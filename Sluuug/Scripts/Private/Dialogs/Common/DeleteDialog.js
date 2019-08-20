function deleteDialog(selector, dialogId) {
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