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

function addCommentEntry(comment) {
    var html = '<div class="comment_item" >';
    let date = new Date(parseInt(parseDate(comment.PostDate)));
    html += '<span>' + date.getFullYear() + '.' + date.getDate() + '.' + date.getDay() + ' ' + date.getHours() + ':' + date.getMinutes() + '</span>';
    html += '<a href="/private/user/' + comment.UserPostedID + '"><img src="' + comment.UserPostedAvatarUri + '" /></a>';
    html += '<span>' + comment.UserName + ' ' + comment.UserSurName + '</span><p>';
    html += '<b>' + comment.Text + '</b></p>';

    html += '</div>';
    return html;
}


function loadComments(fotoID) {
    $.ajax({
        type: "post",
        url: '/api/get_comments',
        data: { fotoID },
        success: function (resp) {
            let user_comments_form = $('#users_comments_' + fotoID)[0];
            [].forEach.call(resp.FotoComments, function (item) {
                user_comments_form.insertAdjacentHTML('afterbegin', addCommentEntry(item));
            });
        }
    });
}

function expandAlbum(album) {
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
                })

                let view_album = $('#view_' + album)[0];
                view_album.innerHTML = genAlbumView(resp);
            }
            else {
                console.log(resp.Comment);
            }
        }
    });
}