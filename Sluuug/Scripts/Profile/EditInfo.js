function CancelNewEntry(checkboxId) {
    $('#' + checkboxId)[0].checked = false;
}

//function CancelNewAlbum(checkboxId) {
//    $('#album-label-input')[0].value = null;
//    $('#album-cover-img-name')[0].innerHTML = null;
//    $('#album-title')[0].value = null;
//    $('#album-description')[0].value = null;
//    CancelNewEntry(checkboxId);
//}

function SelectFileAvatar()
{
    var fileName = $('#album-label-input')[0].value;
    if (fileName !== null)
        $('#album-cover-img-name')[0].innerHTML = fileName;
    else
        $('#album-cover-img-name')[0].innerHTML = null;
}

function untill_now_date(dateInputId, checkboxId) {
    let checbox_elem = $('#' + checkboxId)[0];
    if (checbox_elem.checked === true) {
        $('#' + dateInputId).val(null);
        $('#' + dateInputId)[0].disabled = true;
    }
    else {
        $('#' + dateInputId)[0].disabled = false;
    }
}

function SaveEditedProfileInfo(parameter, value, optionalData = null, isAlert = false, isReload = false) {
    console.log('alert - ' + isAlert);
    console.log('reload - ' + isReload);
    $.ajax({
        url: "/api/edit_profile",
        data: { paramNumer: parameter, newValue: value, additionParameter: optionalData },
        type: "post"
    })
        .done(function (resp) {
            if (resp.IsSuccess) {
                if (isAlert) {
                    _Alert("Данные успешно изменены", '#439d44');
                }
                if (isReload) {
                    window.location.reload();
                }
            }
        });
}

function VisibleHightShoolParameterInNewEntry(selectedValue) {
    if (selectedValue > 0)
        $('#speciality-new-education-entry')[0].style.display = 'flex';
    else
        $('#speciality-new-education-entry')[0].style.display = 'none';
}

var dropedFilesBySize = [];
function AddProfileEntry(formName, tooltipContainerId, successfulController, inputUploaded) {
    let validateResult = ValidateProfileInfoItem(formName);
    if (!validateResult.successful) {
        [].forEach.call(validateResult.result, function (property) {
            var emptyRequeredProperty = $('*[name="' + property + '"][id="' + tooltipContainerId+'"]');
            var inputContainer = $('*[name="'+formName+'"][property="' + property + '"]');
            var message = emptyRequeredProperty[0].getAttribute('tooltip_message');
            if (inputContainer[0].type == 'date') {
                inputContainer[0].type = 'number';
            }

            emptyRequeredProperty[0].setAttribute('tooltip', message);
            inputContainer[0].style.border = "1px solid #ff5151";
            setTimeout(function () {
                inputContainer[0].style.border = "1px solid #7f7f7f";
                emptyRequeredProperty[0].removeAttribute('tooltip');
                if (inputContainer[0].type == 'number') {
                    inputContainer[0].type = 'date';
                }
            }, 5000)
        });
    }
    else {
        _Alert('Добавляем данные, пожалуйста подождите...', '#7f7f7f');
        if (inputUploaded == null) {
            $.ajax({
                type: 'post',
                url: successfulController,
                data: { model: validateResult.result },
                success: function (resp) {
                    if (resp) {
                        window.location.reload();
                    }
                }
            });
        }
        else {
            var form = new FormData();
            console.log(dropedFilesBySize);
            [].forEach.call(inputUploaded.files, function (file) {

                if (!dropedFilesBySize.includes(file.size)) {
                    form.append('uploadPhotos', file);
                }
            });
            [].forEach.call(Object.getOwnPropertyNames(validateResult.result), function (item) {
                form.append(item, validateResult.result[item]);
            });

            $.ajax({
                type: 'post',
                processData: false,
                contentType: false,
                url: successfulController,
                data: form,
                success: function (resp) {
                    if (resp) {
                        window.location.reload();
                    }
                }
            });
        }
    }
}


function WriteSelectedFiles(input) {
    console.log(dropedFilesBySize);
    if (input.files.length > 0) {
        dropedFilesBySize = [];
        $('.uploaded-files-to-event')[0].innerHTML = '';
        [].forEach.call(input.files, function (file) {
            $('.uploaded-files-to-event')[0].insertAdjacentHTML(
                'beforeend',
                '<div id="' + file.size + '"><p><span>' + file.name + '</span><span class="delete-file" onclick="DeleteUploadFile(\'' + file.size + '\')">❌</span></p></div>');
        });
    }
}

function DeleteUploadFile(fileSize) {
    $('#' + fileSize).remove();
    if (!dropedFilesBySize.includes(fileSize)) {
        dropedFilesBySize.push(parseInt(fileSize));
    }
}

function DeleteProfileEntry(guid) {
    _Alert('Удаляем, пожалуйста подождите...');
    $.ajax({
        type: 'post',
        url: '/api/drop_entry',
        data: { EntryId: guid },
        success: function (resp) {
            if (resp) {
                window.location.reload();
            }
        }
    });
}