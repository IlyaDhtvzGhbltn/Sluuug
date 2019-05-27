function newEvent() {
    fields_alert_visible('new_event_button', false);

    var ed = $('#add_event_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#events');
        ed_block[0].insertAdjacentHTML('beforeend', mem_events_form);
    }
    else {
        ed[0].innerHTML = mem_events_form;
    }
    fields_alert_visible('event_requered_field', false);

}
function newEducation() {

    fields_alert_visible('new_educatio_button', false);

    var ed = $('#add_education_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#education');
        ed_block[0].insertAdjacentHTML('beforeend', education_form);
    }
    else {
        ed[0].innerHTML = education_form;
    }
    fields_alert_visible('edu_requered_field', false);
}
function newWork() {
    fields_alert_visible('new_work_button', false);
    var ed = $('#add_work_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#works');
        ed_block[0].insertAdjacentHTML('beforeend', work_form);
    }
    else {
        ed[0].innerHTML = work_form;
    }
    fields_alert_visible('work_requered_field', false);
}
function newLivePlace() {
    fields_alert_visible('new_place_button', false);
    var ed = $('#add_place_entry');
    if (ed[0] == undefined) {
        var ed_block = $('#live_places');
        ed_block[0].insertAdjacentHTML('beforeend', places_form);
    }
    else {
        ed[0].innerHTML = places_form;
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
    console.log(formID);

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

function onEducationLevelChange(value) {
    let int = parseInt(value);
    switch (int) {
        case 0:
            drop_high_school_education();
            break;
        case 1: 
            add_high_school_education();
            break;
        case 2:
            add_high_school_education();
            break;
    }
}

function add_high_school_education() {
    let created_element = $('#high_school_education')[0];
    if (created_element == undefined) {
        let title = $('#level')[0];
        title.insertAdjacentHTML('beforeend', high_level_form);
    }
}
function drop_high_school_education() {
    var form = $('#high_school_education')[0];
    form.remove();
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
const high_level_form = '<div id="high_school_education">' +
    '<p><span>Faculty <input type="text" name="Faculty"></p>' +
    '<p><span>Specialty <input type="text" name="Specialty"></p>' +
    '</div>';

const education_form = '<div id="add_education_entry"><form action="" id="edu_form">' +
    '<p id="level"><span>Education Level <select name="EducationType" onchange="onEducationLevelChange(this.value)"><option value=0>School</option><option value=1>Colledje</option><option value=2>University</option></select></span></p>' +
    '<p><span>Education Title <input type="text" name="Title" required></span><span class="edu_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>Start Education Date <input type="date" name="Start" required><span class="edu_requered_field" style="color:red"> * Field is requered</span></span></p>' +
    '<p><span>End Education Date <input type="date" name="End" id="end_education"></span> Untill Now : <input type="checkbox" id="education_till_now" name="UntilNow" onClick="untill_now_date(end_education.id, this.id)"></p>' +
    '<p><span>Country <select id="user_search_country" name="Country" onchange="getCities(user_search_country.id, user_search_city.id)"><option value="7">Россия</option><option value = "1" >USA</option></select></span></p>' +
    '<p><span>Country <select id="user_search_city" name="Sity"><option value="495">Москва</option></select></span></p>' +
    '<p><span>Your Comment <input type="text" name="Comment"></span></p>' +

    '<div id="command_form"><input type="button" value="Close" onclick="dropHTMLForm(add_education_entry.id, \'new_educatio_button\')">' +
    '<input type="button" onclick="send(\'/api/add_education\', \'edu_form\', \'new_educatio_button\', \'edu_requered_field\')" value="Send"></div>' +
    '</form></div>';

const mem_events_form = '<div id="add_event_entry"><form action="" id="event_form">' +
    '<p><span>Event Title <input type="text" name="EventTitle" required></span><span class="event_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>Event Date <input type="date" name="DateEvent" required></span><span class="event_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>Event Comment <input type="text" name="EventComment"></span></p>' +

    '<div id="command_form"><input type="button" value="Close" onclick="dropHTMLForm(add_event_entry.id,\'new_event_button\')">' +
    '<input type="button" onclick="send(\'/api/add_event\', \'event_form\', \'new_event_button\', \'event_requered_field\')" value="Send"></form></div>';

const work_form = '<div id="add_work_entry"><form action="" id="work_form">' +
    '<p><span>Company Title<input type="text" name="CompanyTitle" required></span><span class="work_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>Position <input type="text" name="Position" required></span><span class="work_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>Start Date <input type="date" name="Start" required></span><span class="work_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>End Date<input type="date" name="End" id="end_work"></span> Untill Now : <input type="checkbox" id="work_till_now" name="UntilNow" onClick="untill_now_date(end_work.id, this.id)"></p>' +
    '<p><span>Country <select id="user_search_country_w" name="Country" onchange="getCities(user_search_country_w.id, user_search_city_w.id)"><option value="7">Россия</option><option value = "1" >USA</option></select></span></p>' +
    '<p><span>City <select id="user_search_city_w" name="Sity"><option value="495">Москва</option></select></span></p>' +
    '<p><span>Comment <input type="text" name="Comment"></span></p>' +

    '<div id="command_form"><input type="button" value="Close" onclick="dropHTMLForm(add_work_entry.id, \'new_work_button\')">' +
    '<input type="button" onclick="send(\'/api/add_works\', \'work_form\',\'new_work_button\',\'work_requered_field\')" value="Send"></div>';
'</div></form></div>';

const places_form = '<div id="add_place_entry"><form action="" id="place_form">' +

    '<p><span>Comment <input type="text" name="Comment"></span></p>' +
    '<p><span>Start Date <input type="date" name="Start" required></span><span class="place_requered_field" style="color:red"> * Field is requered</span></p>' +
    '<p><span>End Date <input type="date" name="End" id="end_live_l"></span> Untill Now : <input type="checkbox" id="live_till_now" name="UntilNow" onClick="untill_now_date(end_live_l.id, this.id)"></p>' +
    '<p><span>Country <select id="user_search_country_l" name="Country" onchange="getCities(user_search_country_l.id, user_search_city_l.id)"><option value="7">Россия</option><option value = "1" >USA</option></select></span></p>' +
    '<p><span>City <select id="user_search_city_l" name="Sity"><option value="495">Москва</option></select></span></p>' +

    '<div id="command_form"><input type="button" value="Close" onclick="dropHTMLForm(add_place_entry.id, \'new_place_button\')">' +
    '<input type="button" onclick="send(\'/api/add_places\', \'place_form\',\'new_place_button\',\'place_requered_field\')" value="Send"></div>';
    '</form></div>';