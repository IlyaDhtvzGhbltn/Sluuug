const editIMG = 'http://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1558014890/system/fgy1pkgfwhlemkvatoiu.png';
const endEDIT = 'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_25/v1558016108/system/images.png';

var edit = [];
edit[0] = 'newt_';
edit[1] = 'newd_';
var inp = [];
inp[0] = 'edit_tit_';
inp[1] = 'edit_desc_';
var elem = [];
elem[0] = 'title_';
elem[1] = 'desc_';

function ShowAlbumCreateForm() {
    let div_form = $('#create_album_form')[0];
    if (div_form.innerHTML.length == 0) {
        changeElementVisibility('create_new_album_button', 'none');
        let htmlForm = $.get('/partial/ownalbum', function (new_album_form) {
            div_form.insertAdjacentHTML('beforeend', new_album_form);
        });
    }
}

function ExpandFoto(fotoId, fullFoto, title, comment) {
    var titl = 'add title';
    if (title != 'null') {
        titl = title;
    }
    var comm = 'add description';
    if (comment != 'null') {
        comm = comment;
    }
    $.ajax({
        type : "post",
        url: "/partial/expandfoto", 
        data: { fotoID: fotoId, fullFoto: fullFoto, titl: title, comm: comment },
        success: function (html) {
            let elem = $('#f_' + fotoId)[0];
            elem.innerHTML = html;
        }
    }); 
}

function show_form_upload_foto(alb) {
    let div_form = $('#upload_foto_div_' + alb)[0];
    if (div_form.innerHTML.length == 0) {
        $.ajax({
            type: "post",
            url: "/partial/uploadfotoform",
            data: { albumID: alb },
            success: function (html) {
                div_form.insertAdjacentHTML('beforeend', html);
            }
        });
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

function uploadFotoToAlbum(album) {
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
    let elementID = inp[type] + photoID;
    let new_value = $('#' + elementID)[0].value;
    console.log(elementID);

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
    $('#' + inp[type] + photoID)[0].src = editIMG;
    $('#' + edit[type] + photoID)[0].remove();
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

function close_form(formID, call_form_btn_ID, style) {
    drop_elem(formID);
    $('#' + call_form_btn_ID)[0].style.display = style;
}

function drop_elem(formID) {
    var form = $('#' + formID)[0];
    form.innerHTML = '';
}

function add_info(fotoID, type) {
    let elem_ = $('#' + edit[type] + fotoID)[0];
    if (elem_ == undefined) {
        $.ajax({
            type: "post",
            url: "/partial/editinfo",
            data: { fotoID: fotoID, type: type },
        })
            .then(function (html) {
                let div = $('#' + elem[type] + fotoID)[0];
                div.insertAdjacentHTML('afterend', html);
                $('#' + inp[type] + fotoID)[0].src = endEDIT;
            });
    }
    else {
        new_tit = $('#' + edit[type] + fotoID)[0];
        new_tit.remove();
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

function drop_edit_info(id) {
    $('#t_' + id).remove();
    $('#d_' + id).remove();
}