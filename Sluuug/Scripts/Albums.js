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
        console.log(upload_file);

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
                console.log(response)
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
            console.log(form.get(file.name));
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
        url: "/api/my_fotos",
        data: { album },
        success: function (resp) {
            if (resp.isSucces) {
                console.log(resp);
                let albums = $('.fotos_view');
                [].forEach.call(albums, function (item) {
                    item.innerHTML = '';
                });

                let view_album = $('#view_' + album)[0];
                view_album.innerHTML = '<hr/>';
            }
            else {
                console.log(resp.Comment);
            }
        }
    });
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

function genAlbumView(album) {
    var wrapper = '';
}


