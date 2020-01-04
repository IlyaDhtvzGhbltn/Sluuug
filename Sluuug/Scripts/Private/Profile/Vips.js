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
                [].forEach.call(resp, function (item) {
                    var vipUserNode = VIP.VIPUserNode(item);
                    $('.vip-list')[0].insertBefore(vipUserNode, $('.vip-list')[0].firstChild);
                });
            }
        }
    });
}