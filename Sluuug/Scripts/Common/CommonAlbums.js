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

function loadComments(fotoID) {
    console.log('#users_comments_' + fotoID);
    $('#users_comments_' + fotoID)[0].innerHTML = '';
    $.ajax({
        type: "post",
        url: '/api/get_comments',
        data: { fotoID },
        success: function (resp) {
            console.log(resp.FotoComments[0].PostDate);
            let user_comments_form = $('#users_comments_' + fotoID)[0];
                $.ajax({
                    data: { comments : resp.FotoComments },
                    url:"/partial/commententry",
                    type:"post",
                    success: function (html) {
                        user_comments_form.insertAdjacentHTML('afterbegin', html);
                    }
                });
        }
    });
}

function expandAlbum(album) {
    let allBtns = $(".show_btn");
    [].forEach.call(allBtns, function (item) {
        item.style = "display: block";
    });

    let btn = $("#" + album)[0];
    btn.style = "display: none";

    $.ajax({
        type: "post",
        url: "/api/fotos",
        data: { album },
        success: function (resp) {
            if (resp.isSucces) {
                let albums = $('.fotos_view');
                let full = $('.full_view');
                [].forEach.call(albums, function (item) {
                    item.innerHTML = '';
                });
                [].forEach.call(full, function (item) {
                    item.innerHTML = '';
                });

                [].forEach.call(resp.Photos, function (foto) {
                    $.ajax({
                        url: "/partial/albumreview",
                        type: "post",
                        data: { album: foto },
                        success: function (html) {
                            let view_album = $('#view_' + album)[0];
                            view_album.insertAdjacentHTML('beforebegin', html);
                            loadComments(foto.ID)
                        }
                    });
                });
            }
            else {
                console.log(resp.Comment);
            }
        }
    });
}