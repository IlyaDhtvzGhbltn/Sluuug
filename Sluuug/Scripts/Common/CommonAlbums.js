function parseDate(dateformat) {
    let inds = dateformat.indexOf('(');
    let inde = dateformat.indexOf(')');
    return dateformat.substring(inds + 1, inde);
}

function commentFoto(fotoID) {
    let elem = $('#fcomment_' + fotoID)[0];
    let text = elem.value;
    let dt = { FotoID: fotoID, CommentText: text };
    $.ajax({
        url: '/api/post_comments',
        type: "post",
        data: dt,
        success: function (resp) {
            if (resp.isSuccess) {
                window.location.reload();
            }
        }
    });
    elem.value = '';
}

function expandAlbum(album) {
    let allBtns = $(".show_btn");
    [].forEach.call(allBtns, function (item) {
        item.style = "display: block";
    });

    let btn = $("#" + album)[0];
    btn.style = "display: none";

    $.ajax({
        url: "/partial/albumreview",
        type: "post",
        data: { album: album },
        success: function (html) {
            let albums = $('.fotos_view');
            let full = $('.full_view');
            [].forEach.call(albums, function (item) {
                item.innerHTML = '';
            });
            [].forEach.call(full, function (item) {
                item.innerHTML = '';
            });

            let view_album = $('#view_' + album)[0];
            view_album.insertAdjacentHTML('beforebegin', html);
        }
    });
}

function rollUpFoto(ID) {
    $('#' + ID)[0].innerHTML = '';
}