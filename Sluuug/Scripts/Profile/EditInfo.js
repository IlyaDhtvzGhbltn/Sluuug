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
    let checbox_elem = $('#' + checkboxId)[0];
    if (checbox_elem.checked === true) {
        $('#' + dateInputId).val(null);
        $('#' + dateInputId)[0].disabled = true;
    }
    else {
        $('#' + dateInputId)[0].disabled = false;
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

function AddProfileEntry(formName, tooltipContainerId, successfulController) {
    let validateResult = ValidateProfileInfoItem(formName);
    if (!validateResult.successful) {
        [].forEach.call(validateResult.result, function (property) {
            var emptyRequeredProperty = $('*[name="' + property + '"][id="' + tooltipContainerId+'"]');
            var inputContainer = $('*[name="'+formName+'"][property="' + property + '"]');
            var message = emptyRequeredProperty[0].getAttribute('tooltip_message');

            emptyRequeredProperty[0].setAttribute('tooltip', message);
            inputContainer[0].style.border = "1px solid #ff5151";
            setTimeout(function () {
                inputContainer[0].style.border = "1px solid #7f7f7f";
                emptyRequeredProperty[0].removeAttribute('tooltip');
            }, 5000)
        });
    }
    else {
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
}

function DeleteProfileEntry(guid) {
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