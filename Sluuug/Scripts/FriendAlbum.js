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
    });

    wrapper += '<div class="full_view" id="f_' + album.Photos[0].Album + '"></div>';
    wrapper += '</div>';

    return wrapper;
}