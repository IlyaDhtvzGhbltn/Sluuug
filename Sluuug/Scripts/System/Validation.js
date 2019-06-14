function validateFormById(formID) {
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
        else {
            if (elem.type == 'email') {
                let mailValidateSuccess = validateEmail(elem.value);
                if (!mailValidateSuccess) {
                    validate_errors++;
                }
            }
        }
    });
    if (validate_errors == 0) {
        return true;
    }
    else return false;
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}