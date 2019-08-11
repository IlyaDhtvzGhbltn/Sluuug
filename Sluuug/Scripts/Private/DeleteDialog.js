function deleteDialog(dialogId) {
    $.ajax({
        type: 'post',
        url: '/api/delete_dialog',
        data: { ConversationId: dialogId },
        success: function (resp) {
            if (resp) {
                $('#' + dialogId).remove();
            }
            else _Alert('Произошла ошибка. Пожалуйста повторите попытку позднее.');
        }
    });
}