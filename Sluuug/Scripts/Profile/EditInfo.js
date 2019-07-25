function CancelNewEntry(checkboxId) {
    $('#' + checkboxId)[0].checked = false;
}

function CancelNewAlbum(checkboxId) {
    $('#album-label-input')[0].value = null;
    $('#album-cover-img-name')[0].innerHTML = null;
    $('#album-title')[0].value = null;
    $('#album-description')[0].value = null;
    CancelNewEntry(checkboxId);
}

function SelectFileAvatar()
{
    var fileName = $('#album-label-input')[0].value;
    if (fileName !== null)
        $('#album-cover-img-name')[0].innerHTML = fileName;
    else
        $('#album-cover-img-name')[0].innerHTML = null;
}

function untill_now_date(dateInputId, checkboxId) {
    let input_elem = $('#' + dateInputId)[0];
    let checbox_elem = $('#' + checkboxId)[0];
    if (checbox_elem.checked === true) {
        input_elem.disabled = true;
    }
    else {
        input_elem.disabled = false;
    }
}

function SaveEditedProfileInfo(parameter, value) {
    console.log(parameter);
    console.log(value);
    $.ajax({
        url: "/api/edit_profile",
        data: { paramNumer: parameter, newValue: value },
        type: "post"
    })
        .done(function (resp) {
            if (resp.IsSuccess) {
                document.location.reload();
            }
        });
}

function VisibleHightShoolParameterInNewEntry(selectedValue) {
    if (selectedValue > 0)
        $('#speciality-new-education-entry')[0].style.display = 'flex';
    else
        $('#speciality-new-education-entry')[0].style.display = 'none';
}


//////////////////////////////////////////////////////////////////

//function send_simple(api_url, formID, show_button, requred_field_alert) {
//    console.log('send -education form');

//    let val = validate(formID);
//    if (val === true) {
//        let data = $('#' + formID).serializeArray();
//        let dt = parceJSON(data);

//        console.log(data);
//        close_form(formID, 'create_new_album_button', 'inline-block');
//        drop_elem('create_album_form');

//        $.ajax({
//            url: api_url,
//            type: "post",
//            data: dt,
//            success: function (response) {
//                if (response) {
//                    document.location.reload();
//                }
//            }
//        });
//    }
//    else {
//        changeElementVisibility('edu_requered_field', 'inline-block');
//    }
//}



