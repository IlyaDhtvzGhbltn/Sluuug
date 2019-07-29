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
        if (elem.value.length === 0) {
            validate_errors++;
        }
        else {
            if (elem.type === 'email') {
                let mailValidateSuccess = validateEmail(elem.value);
                if (!mailValidateSuccess) {
                    validate_errors++;
                }
            }
        }
    });
    if (validate_errors === 0) {
        return true;
    }
    else return false;
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function ValidateProfileInfoItem(formName) {
    var json = new Object;

    var validateErrors = 0;
    var errorProperties = [];

    var itemForm = $('*[name="' + formName + '"]');
    [].forEach.call(itemForm, function (item) {

        if (item.required) {
            let valueLength = item.value.length;
            let property = item.getAttribute('property');

            if (valueLength !== 0) {
                json[property] = item.value;
            }
            else {
                validateErrors++;
                errorProperties.push(property);
            }
        }
        else {
            let property = item.getAttribute('property');

            if (item.type === 'checkbox') {
                json[property] = item.checked;
            }
            else {
                json[property] = item.value;
            }
        }
    });
    if (validateErrors === 0) {
        return { successful: true, result: json };
    }
    else {
        return { successful: false, result: errorProperties };
    }
}

