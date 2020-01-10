function uploadVIP() {
    $.ajax({
        type: "post",
        url: "/api/getvips",
        success: function (resp) {
            console.log(resp);
            $(".await-vip-upload").remove();
            if (resp.length === 0) {
                $('.null-vips-msg')[0].innerHTML = '<span>VIP пользователей не найдено</span>';
                $('#vip-title-text').remove();
            }
            else {
                [].forEach.call(resp.Users, function (item) {
                    var vipUserNode = VIP.VIPUserNode(item);
                    $('.vip-list')[0].insertBefore(vipUserNode, $('.vip-list')[0].firstChild);
                });
                $('#vip-city')[0].innerHTML = `${resp.City}`;
                if (resp.AlreadyVIP) {
                    $('.new-vip-activate-button')[0].remove();
                    $('.new-vip-help-container')[0].remove();
                }
            }
        }
    });
}

function setVip() {
    $.ajax({
        type: 'post',
        url: '/partial/payment',
        success: function (respHtml) {
            _ALertForm(respHtml);
            $('.payment-root')[0].scrollIntoView();
        }
    });
}