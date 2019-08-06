function saveNotifyType() {
    var data = $("#changeNotifyTypeSubmit").serializeArray();
    var json = parceJSON(data);
    send(json);
}

function saveEmail() {
    var data = $("#changeEmailSubmit").serializeArray();
    var json = parceJSON(data);

    let index = json.NewEmail.indexOf("@");
    if (index == -1) {
        console.log("invalid email address");
    }
    else {
        send(json);
    }
}

function savePassword() {
    var data = $("#changePasswSubmit").serializeArray();
    var json = parceJSON(data);
    if (json.NewPassw.length < 5) {
        console.log("to simple password " + json.NewPassw.length);
    }
    else if (json.OldPassw.length == 0) {
        console.log("old password invalid");
    }
    else if (json.OldPassw != json.OldPasswRep) {
        console.log("old passwords are different");
    }
    else {
        json.OldPassw = getSHA(json.OldPassw);
        json.OldPasswRep = getSHA(json.OldPasswRep);
        json.NewPassw = getSHA(json.NewPassw);
        send(json);
    }
}

function saveQuickMessage() {
    var data = new Object;
    data.QuickMessage = false;
    if ($('#fun')[0].checked == true)
        data.QuickMessage = true;

    var json = JSON.parse(JSON.stringify(data));
    console.log(json);
    send(json);
}

function send(request) {
    console.log('sending...');
    $.post("/api/save_settings", 
      request,
      function (result) {
        console.log(result);
    })
}