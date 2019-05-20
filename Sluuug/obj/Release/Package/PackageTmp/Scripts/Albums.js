const editIMG = 'http://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1558014890/system/fgy1pkgfwhlemkvatoiu.png';
const endEDIT = 'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1558016108/system/images.png';

function ShowAlbumCreateForm() {
    let div_form = $('#create_album_form')[0];
    if (div_form.innerHTML.length == 0) {
        changeElementVisibility('create_new_album_button', 'none');
        div_form.insertAdjacentHTML('beforeend', new_album_form);
    }
}
/////////////////////////////////////
function changeElementVisibility(element_id, v_style) {
    let div_form = $('#' + element_id)[0];
    div_form.style.display = v_style;
}

function send(api_url, formID, show_button, requred_field_alert) {
    let val = validate(formID);
    if (val == true) {
        let data = $('#' + formID).serializeArray();

        var form = new FormData();
        [].forEach.call(data, function (item) {
            form.append(item.name, item.value);
        });

        let upload_file = $('#album_label')[0].files[0];

        close_form(formID, 'create_new_album_button', 'inline-block');
        drop_elem('create_album_form');

        form.append('album_label', upload_file);
        $.ajax({
            url: api_url,
            type: "post",
            processData: false,
            contentType: false,
            data: form,
            success: function (response) {
                if (response) {
                    document.location.reload();
                }
            }
        });
    }
    else {
        changeElementVisibility(requred_field_alert, 'inline-block');
    }
}

function upload(album) {
    let album_form = '#upload_foto_form_' + album;
    let data = $(album_form).serializeArray();
    data.push({name:"Album", value:album}); 

    let selected_files = $('#input_photo')[0].files;
    if (selected_files.length == 0) {
        changeElementVisibility('foto_not_upload', 'inline-block');
    }
    else {
        var form = new FormData();
        [].forEach.call(selected_files, function (file) {
            form.append('Files', file);
        });

        [].forEach.call(data, function (item) {
            form.append(item.name, item.value);
        });
        $.ajax({
            type: "post",
            url: "/api/upload_foto",
            processData: false,
            contentType: false,
            data: form,
            success: function (resp) {
                if (resp) {
                    window.location.reload();
                }
            }
        });
    
        drop_elem('upload_foto_div_' + album);
    }
}

function send_edit(photoID, type) {
    let idtype = 'inp_t_';
    let imgtype = 'edit_tit_';
    let newdiv = 'newt_';

    if (type == 1) {
        idtype = 'inp_d_';
        imgtype = 'edit_desc_';
        newdiv = 'newd_';
    }
    let elementID = idtype + photoID;
    let new_value = $('#' + elementID)[0].value;
    if (new_value.length > 0) {
        $.ajax({
            type: "post",
            url: "/api/edit",
            data: { PhotoGUID: photoID, EditMode: type, NewValue: new_value },
            success: function (resp) {
                if (resp.isSuccess) {
                    document.location.reload();
                }
            }
        });
    }
    $('#' + imgtype + photoID)[0].src = editIMG;
    $('#' + newdiv + photoID)[0].remove();
}

function drop_album(albumID) {
    $.ajax({
        type: "post",
        url: "/api/drop_album",
        data: { albumID },
        success: function (resp) {
            console.log(resp);
            if (resp.isSuccess) {
                document.location.reload();
            }
        }
    });
}

function validate(formID) {
    let validate_errors = 0;
    let data = $('#' + formID).serializeArray();
    
    let required_elems = [];
    [].forEach.call(data, function (form_input) {
        let input = document.getElementsByName(form_input.name)[0];
        if (input.required) {
            required_elems.push(input.name);
        }
    });
    [].forEach.call(required_elems, function (item) {
        let elem = document.getElementsByName(item)[0];
        if (elem.value.length == 0) {
            validate_errors++;
        }
    });
    if (validate_errors == 0) {
        return true;
    }
    else return false;
}

function parceJSON(array) {
    var obj = new Object();
    for (let i = 0; i < array.length; i++) {
        let nm = array[i].name;
        let vl = array[i].value;
        obj[nm] = vl;
    }
    return obj;
}

function close_form(formID, call_form_btn_ID, style) {
    drop_elem(formID);
    $('#' + call_form_btn_ID)[0].style.display = style;
}

function drop_elem(formID) {
    var form = $('#' + formID)[0];
    form.innerHTML = '';
}

function show_form_upload_foto(alb) {
    let div_form = $('#upload_foto_div_' + alb)[0];
    if (div_form.innerHTML.length == 0) {
        div_form.insertAdjacentHTML('beforeend', genPhotoUploadForm(alb));
    }}


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
function add_info(fotoID, type) {
    switch (type)
    {
        case 0 :
            {
                let elem = $('#newt_' + fotoID)[0];
                if (elem == undefined) {
                    let div = $('#title_' + fotoID)[0];
                    div.insertAdjacentHTML('afterend', getEditInfo(fotoID, 0));
                    $('#edit_tit_' + fotoID)[0].src = endEDIT;

                }
                else {
                    new_tit = $('#newt_' + fotoID)[0];
                    new_tit.remove();
                    $('#edit_tit_' + fotoID)[0].src = editIMG;

                }
                break;
            }
        case 1:
            {
                let elem = $('#newd_' + fotoID)[0];
                if (elem == undefined) {
                    let div = $('#desc_' + fotoID)[0];
                    div.insertAdjacentHTML('afterend', getEditInfo(fotoID, 1));
                    $('#edit_desc_' + fotoID)[0].src = endEDIT;
                }
                else {
                    new_tit = $('#newd_' + fotoID)[0];
                    new_tit.remove();
                    $('#edit_desc_' + fotoID)[0].src = editIMG;
                }
                break;
            }
    }
}

function drop_foto(fotoID) {
    console.log(fotoID);
    $.ajax({
        type: "post",
        url: '/api/drop_foto',
        data: { fotoID },
        success: function (resp) {
            console.log(resp);
            if (resp.isSuccess) {
                window.location.reload();
            }
        }
    });
}

function loadComments(fotoID) {
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
 

function drop_edit_info(id) {
    $('#t_' + id).remove();
    $('#d_' + id).remove();
}

function show_full_img(number, fullFoto, title, comment, index, fotoId) {
    let elem = $('#f_' + number)[0];
    var titl = 'add title';
    if (title != 'null') {
        titl = title;
    } 
    var comm = 'add description';
    if (comment != 'null') {
        comm = comment;
    }
    elem.innerHTML =
    '<p><div id="title_' + fotoId + '" onclick="add_info(\'' + fotoId + '\', 0)"><b>' + titl + '</b><img id="edit_tit_' + fotoId + '" src="' + editIMG+'"/></div></p>' +
    '<p><img src="' + fullFoto + '"/><button onclick="drop_foto(\'' + fotoId +'\')">Удалить</button></p>' +
    '<p><div id="desc_' + fotoId + '" onclick="add_info(\'' + fotoId + '\', 1)"><span>' + comm + '</span><img id="edit_desc_' + fotoId + '" src="' + editIMG+'"/></div></p>' +
        '';
}

const new_album_form =
    '<form action="" id="new_album">' +
    '<p><span>Album Title </span><input type="text" name="Title" required/><span id="album_field_requered" style="color:red; display:none"> * Requered Field</span></p>' + 
    '<p><span>Album Comment </span><input type="text" name="AuthorComment" id="album_comment"></p>' + 
    '<p><span>Album Label </span><input type="file" id="album_label" accept=".jpg, .bmp, .png"></p>' +
    '</form>' +
    '<button id="close_album_form" onclick="close_form(\'create_album_form\', \'create_new_album_button\', \'inline-block\')">Close</button>' +
    '<button onclick="send( \'/api/create_album\' ,\'new_album\', \'create_new_album_button\', \'album_field_requered\')">Create</button>'; 

function genPhotoUploadForm(album) {
    return '<form action="" id="upload_foto_form_' + album + '">' +
        '<p><span>Photo Title </span><input type="text" name="Title"/></p>' +
        '<p><span>Photo Comment </span><input type="text" name="AuthorComment"/></p>' +
        '<p><span>Select Images </span><input type="file" enctype="multipart/form-data" id="input_photo" accept=".jpg, .bmp, .png" required multiple/><span id="foto_not_upload" style="color:red; display:none">* Field requred</span></p>' +
        '</form>' +

        '<button id="close_album_form" onclick="close_form(\'upload_foto_div_' + album + '\', \'create_new_album_button\', \'inline-block\')">Close</button>' +
        '<button onclick="upload(\'' + album + '\')">Create</button>'; 
}

function getEditInfo(fotoID, type) {
    let idtype = 'newt_';
    let inptyper = 'inp_t_';
    if (type == 1) {
        idtype = 'newd_';
        inptyper = 'inp_d_';
    }
    let html = '<div id="' + idtype + fotoID + '"><input id="'+inptyper + fotoID + '" type="text"/><button onclick="send_edit(\'' + fotoID + '\', \'' + type +'\')">Сохранить</button></div>'
    return html;
}


function genAlbumView(album) {

    var wrapper = '<div>';
    [].forEach.call(album.Photos, function (foto) {
        wrapper += '<div class="img_frame" style="display:inline-block;cursor:pointer;margin:10">';
        if (foto.Title != null) {
            wrapper += '<p><b>' + foto.Title + '</b></p>';
        }
        let index = album.Photos.indexOf(foto);
        wrapper += '<img src="' + foto.SmallFotoUri + '" onclick="show_full_img(\'' + foto.Album + '\', \'' + foto.FullFotoUri + '\', \'' + foto.Title + '\', \'' + foto.AuthorComment + '\', \'' + index + '\', \'' + foto.ID + '\')" />';
        if (foto.AuthorComment != null) {
            wrapper += '<p><span>' + foto.AuthorComment + '</span></p>';
        }
        wrapper += '<div class="comments" id="users_comments_' + foto.ID+'"></div>';
        wrapper += '<div class="my_comment"><div class="comment_form" id="cF_' + foto.ID + '">' +
            '<input type="text" id="fcomment_' + foto.ID+'" />'+
            '<button onclick="comment_now(\'' + foto.ID + '\')">Комментировать</button></div></div>';

        wrapper += '</div>';
        loadComments(foto.ID);
    });
    wrapper += '<div class="full_view" id="f_' + album.Photos[0].Album + '"></div>';
    wrapper += '</div>';

    return wrapper;
}

function genComment(comment) {
    var html = '<div class="comment_item" >';
    let date = new Date(parseInt(getDate(comment.PostDate)));
    html += '<span>' + date.getFullYear() + '.' + date.getDate() + '.' + date.getDay() + ' ' + date.getHours() + ':' + date.getMinutes() + '</span>';
    html += '<a href="/private/user/' + comment.UserPostedID + '"><img src="'+comment.UserPostedAvatarUri+'" /></a>';
    html += '<span>' + comment.UserName + ' ' + comment.UserSurName + '</span><p>';
    html += '<b>' + comment.Text + '</b></p>';

    html += '</div>';
    return html;
}

function getDate(dateformat) {
    let inds = dateformat.indexOf('(');
    let inde = dateformat.indexOf(')');
    return dateformat.substring(inds+1, inde);
}