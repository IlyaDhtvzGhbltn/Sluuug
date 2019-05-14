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

function send(formID, show_button, requred_field_alert) {
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
            url: "/api/create_album",
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

function upload_foto(alb) {
    console.log(alb);
}


const new_album_form =
    '<form action="" id="new_album">' +
    '<p><span>Album Title</span><input type="text" name="Title" required/><span id="album_field_requered" style="color:red; display:none"> * Requered Field</span></p>' + 
    '<p><span>Album Comment</span><input type="text" name="AuthorComment" id="album_comment"></p>' + 
    '<p><span>Album Label </span><input type="file" id="album_label" accept=".jpg, .bmp, .png"></p>' +
    '</form>' +
    '<button id="close_album_form" onclick="close_form(\'create_album_form\', \'create_new_album_button\', \'inline-block\')">Close</button>' +
    '<button onclick="send(\'new_album\', \'create_new_album_button\', \'album_field_requered\')">Create</button>'; 