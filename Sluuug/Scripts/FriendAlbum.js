function show_album(album) {
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

function show_full_img(number, fullFoto, title, comment, index) {
    let elem = $('#f_' + number)[0];
    console.log(elem);
    var titl = '';
    if (title != 'null') {
        titl = title;
    }
    var comm = '';
    if (comment != 'null') {
        comm = comment;
    }
    elem.innerHTML = '<p><b>' + titl + '</b></p>' +
        '<p><img src="' + fullFoto + '"/></p><p><span>' + comm + '</span></p>' +
        '';
}

function comment_now(fotoID) {
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
    console.log(fotoID);
    $.ajax({
        type: "post",
        url: '/api/get_comments',
        data: { fotoID },
        success: function (resp) {
            let user_comments_form = $('#users_comments_' + fotoID)[0];
            [].forEach.call(resp.FotoComments, function (item) {
                user_comments_form.insertAdjacentHTML('afterbegin', genComment(item));
            });
        }
    });
}


function genComment(comment) {
    var html = '<div class="comment_item" >';
    let date = new Date(parseInt(getDate(comment.PostDate)));
    html += '<span>' + date.getFullYear() + '.' + date.getDate() + '.' + date.getDay() + ' ' + date.getHours() + ':' + date.getMinutes() + '</span>';
    html += '<a href="/private/user/' + comment.UserPostedID + '"><img src="' + comment.UserPostedAvatarUri + '" /></a>';
    html += '<span>' + comment.UserName + ' ' + comment.UserSurName + '</span><p>';
    html += '<b>' + comment.Text + '</b></p>';

    html += '</div>';
    return html;
}

function genAlbumView(album) {
    var wrapper = '<div>';
    [].forEach.call(album.Photos, function (foto) {
        wrapper += '<div class="img_frame" style="display:inline-block;cursor:pointer">';

        if (foto.Title != null) {
            wrapper += '<p><b>' + foto.Title + '</b></p>';
        }
        let index = album.Photos.indexOf(foto);
        wrapper += '<img src="' + foto.SmallFotoUri + '" onclick="show_full_img(\'' + foto.Album + '\', \'' + foto.FullFotoUri + '\', \'' + foto.Title + '\', \'' + foto.AuthorComment + '\', \'' + index + '\')" />';

        if (foto.AuthorComment != null) {
            wrapper += '<p><span>' + foto.AuthorComment + '</span></p>';
        }
        wrapper += '</div>';

        wrapper += '<div class="comments" id="users_comments_' + foto.ID + '"></div>';
        wrapper += '<div class="my_comment"><div class="comment_form" id="cF_' + foto.ID + '">' +
            '<input type="text" id="fcomment_' + foto.ID + '" />' +
            '<button onclick="comment_now(\'' + foto.ID + '\')">Комментировать</button></div></div>';
        loadComments(foto.ID);

    });
    wrapper += '<div class="full_view" id="f_' + album.Photos[0].Album + '"></div>';
    wrapper += '</div>';

    return wrapper;
}

function getDate(dateformat) {
    let inds = dateformat.indexOf('(');
    let inde = dateformat.indexOf(')');
    return dateformat.substring(inds + 1, inde);
}