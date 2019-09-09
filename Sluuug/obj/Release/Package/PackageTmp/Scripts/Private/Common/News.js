function getNotSeenNotify() {
    $.ajax({
        type : 'post',
        url : '/api/not_readed',
        success: function (resp) {
            console.log(Object.keys(resp.NotReadedConversations)[0]);
            console.log(resp.NotReadedConversations[Object.keys(resp.NotReadedConversations)[0]]);

            var simpleMessCount = Object.keys(resp.NotReadedConversations).length;
            if (simpleMessCount > 0) {
                IncrementInto('.notify-container-increment-message', 'not-show-message-counter', simpleMessCount);
                if (window.location.href.includes('/cnv')) {
                    [].forEach.call(Object.keys(resp.NotReadedConversations), function (key) {
                        IncrementInto('.dialog-not-read-msg-' + key, key, resp.NotReadedConversations[key]);
                    });
                }

            }
            var cryptoMessCount = Object.keys(resp.NotReadedCryptoConversations).length;
            if (cryptoMessCount > 0) {
                IncrementInto('.notify-container-increment-crypto', 'not-show-crypto-counter', cryptoMessCount);
                if (window.location.href.includes('/crypto_cnv')){
                    [].forEach.call(Object.keys(resp.NotReadedCryptoConversations), function (key) {
                        IncrementInto('.crypto-dialog-not-read-msg-' + key, key, resp.NotReadedCryptoConversations[key]);
                    })
                }
            }
            var newContacts = resp.NewInviteToContacts;
            if (newContacts > 0)
            {
                IncrementInto('.notify-container-increment-contact', 'not-show-contact-counter', newContacts);
            }
        }
    });
}