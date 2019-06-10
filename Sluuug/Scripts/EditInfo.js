async function newEvent() {
    fields_alert_visible('new_event_button', false);

    var ed = $('#add_event_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#events');
        ed_block[0].insertAdjacentHTML('beforeend', await getPartialView('/partial/mem_events_form'));
    }
    else {
        ed[0].innerHTML = await getPartialView('/partial/mem_events_form');
    }
    fields_alert_visible('event_requered_field', false);

}
async function newEducation() {

    fields_alert_visible('new_educatio_button', false);

    var ed = $('#new_education');
    if (ed[0] == undefined) {
        var ed_block = $('#education');
        ed_block[0].innerHTML = await getPartialView('/partial/add_education_form');
    }
    else {
        ed[0].innerHTML = await getPartialView('/partial/add_education_form');
    }
    fields_alert_visible('edu_requered_field', false);
}
async function newWork() {
    fields_alert_visible('new_work_button', false);
    var ed = $('#add_work_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#works');
        ed_block[0].insertAdjacentHTML('beforeend', await getPartialView('/partial/work_form'));
    }
    else {
        ed[0].innerHTML = await getPartialView('/partial/work_form');
    }
    fields_alert_visible('work_requered_field', false);
}
async function newLivePlace() {
    fields_alert_visible('new_place_button', false);
    var ed = $('#add_place_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#live_places');
        ed_block[0].insertAdjacentHTML('beforeend', await getPartialView('/partial/places_form'));
    }
    else {
        ed[0].innerHTML = await getPartialView('/partial/places_form');
    }
    fields_alert_visible('place_requered_field', false);
}
//////////////////////////////////////////////////////////////////
function fields_alert_visible(field_class, is_visible) {
    let req_fields = $('.' + field_class);
    [].forEach.call(req_fields, function (item) {
        if (is_visible) {
            item.style.display = "inline-block";
        }
        else {
            item.style.display = "none";
        }
    });
}

function dropEntry(entry) {
    let EntryId = entry.id;
    $.post("/api/drop_entry",
        {EntryId},
        function (result) {
            console.log(result);
            if (result == true) {
                document.location.reload();
            }
        })
}

function dropHTMLForm(formID, show_button_class) {
    var form = $('#' + formID)[0];
    form.remove();
    fields_alert_visible(show_button_class, true);
}

function send(api_url, formID, show_button_class, requred_field_alerr) {
    console.log('send edit info');

    let val = validate(formID);
    console.log('validation result ' + val);
    if (val == true) {
        let data = $('#' + formID).serializeArray();
        dropHTMLForm(formID, show_button_class);
        let json = parceJSON(data);
        if (json.UntilNow == "on") {
            json.UntilNow = true;
        }
        else {
            json.UntilNow = false;
        }
           $.ajax({
               type: "post",
               url: api_url,
                data: json,
               success: function (responce) {
                   console.log(responce);

                    if (responce == true) {
                        document.location.reload();
                    }
                }
        });
    }
    else {
        fields_alert_visible(requred_field_alerr, true);
    }
}
function validate(formID) {
    console.log('validete edit info');
    let validate_errors = 0;
    let data = $('#' + formID).serializeArray();
    console.log(data);
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

function untill_now_date(dateInputId, checkboxId) {
    let input_elem = $('#' + dateInputId)[0];
    let checbox_elem = $('#' + checkboxId)[0];
    if (checbox_elem.checked == true) {
        input_elem.disabled = true;
    }
    else {
        input_elem.disabled = false;
    }
}
function getCities(countryListBoxId, citiesListBoxId) {
    let countryCode = $("#" + countryListBoxId + " :selected").val();
    $.ajax({
        type: "post",
        url: "/api/cities",
        data: { countryCode },
        success: function (response) {
            console.log(response);

            let countryList = $("#" + citiesListBoxId);
            countryList.empty();
            for (let i = 0; i < response.Cities.length; i++) {
                countryList.append(new Option(response.Cities[i].Title, response.Cities[i].CityCode));
            }
        },
    });
}
//////////////////////////////////////////////////////////////////
function getPartialView(url_addres) {
    return $.ajax({
        url: url_addres,
        data: {},
        type: "post"
    });
}

function send_simple(api_url, formID, show_button, requred_field_alert) {
    console.log('send -education form');

    let val = validate(formID);
    if (val == true) {
        let data = $('#' + formID).serializeArray();
        let dt = parceJSON(data);

        console.log(data);
        close_form(formID, 'create_new_album_button', 'inline-block');
        drop_elem('create_album_form');

        $.ajax({
            url: api_url,
            type: "post",
            data: dt,
            success: function (response) {
                if (response) {
                    document.location.reload();
                }
            }
        });
    }
    else {
        changeElementVisibility('edu_requered_field', 'inline-block');
    }
}


function onEducationLevelChange(value) {
    let int = parseInt(value);
    switch (int) {
        case 0:
            drop_high_school_education();
            break;
        default:
            add_high_school_education();
            break;
    }
}

async function add_high_school_education() {
    let created_element = $('#high_school_education')[0];
    if (created_element == undefined) {
        let title = $('#level')[0];
        title.insertAdjacentHTML('beforeend', await getPartialView('/partial/hight_education_level_form'));
    }
}