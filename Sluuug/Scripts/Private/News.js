function getNotReadedMessages() {
    $.ajax({
        type : 'post',
        url : '/api/not_readed',
        success : function (resp) {
            if (resp.NotReadedConversations > 0) {
                IncrementInto('.notify-container-increment-message', 'not-show-message-counter', resp.NotReadedConversations);
            }

            if (resp.NotReadedCryptoConversations > 0) {
                IncrementInto('.notify-container-increment-crypto', 'not-show-crypto-counter', resp.NotReadedCryptoConversations);
            }
        }
    });
}