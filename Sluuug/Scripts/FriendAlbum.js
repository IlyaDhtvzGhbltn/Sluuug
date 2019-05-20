function expandFoto(number, fullFoto, title, comment, index) {
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

function genAlbumView(album) {
    var wrapper = '<div>';
    [].forEach.call(album.Photos, function (foto) {
        wrapper += '<div class="img_frame" style="display:inline-block;cursor:pointer">';

        if (foto.Title != null) {
            wrapper += '<p><b>' + foto.Title + '</b></p>';
        }
        let index = album.Photos.indexOf(foto);
        wrapper += '<img src="' + foto.SmallFotoUri + '" onclick="expandFoto(\'' + foto.Album + '\', \'' + foto.FullFotoUri + '\', \'' + foto.Title + '\', \'' + foto.AuthorComment + '\', \'' + index + '\')" />';

        if (foto.AuthorComment != null) {
            wrapper += '<p><span>' + foto.AuthorComment + '</span></p>';
        }
        wrapper += '</div>';

        wrapper += '<div class="comments" id="users_comments_' + foto.ID + '"></div>';
        wrapper += '<div class="my_comment"><div class="comment_form" id="cF_' + foto.ID + '">' +
            '<input type="text" id="fcomment_' + foto.ID + '" />' +
            '<button onclick="commentFoto(\'' + foto.ID + '\')">Комментировать</button></div></div>';
        loadComments(foto.ID);

    });
    wrapper += '<div class="full_view" id="f_' + album.Photos[0].Album + '"></div>';
    wrapper += '</div>';

    return wrapper;
}