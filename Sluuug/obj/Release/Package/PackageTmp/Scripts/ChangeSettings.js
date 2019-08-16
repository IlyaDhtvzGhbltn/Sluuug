function saveNotifyType() {
    var data = $("#changeNotifyTypeSubmit").serializeArray();
    var json = parceJSON(data);
    send(json);
}

function saveEmail() {
    var data = new Object;
    try {
        let newMail = $("#new-email").val();
        let index = newMail.indexOf("@");
        console.log(index);
        if (index == -1) {
            console.log("invalid email address");
        }
        else {
            data.NewEmail = $("#new-email").val();
            var json = JSON.parse(JSON.stringify(data));
            send(json);
        }
    }
    catch{
        console.log("invalid email address");
    }
}

function savePassword() {
    var data = $("#changePasswSubmit").serializeArray();
    var json = parceJSON(data);
    if (json.NewPassw.length < 5) {
        _Alert('Новый пароль слишком простой.', 'red' );
    }
    else if (json.OldPassw.length == 0) {
        _Alert('Старый пароль неверен.', 'red');
    }
    else if (json.OldPassw != json.OldPasswRep) {
        _Alert('Пароли не совпадают.', 'red');
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
    data.QuickMessageNeedChange = true;
    if ($('input[name=NotifyType][id=fun]')[0].checked == true)
        data.QuickMessage = true;

    var json = JSON.parse(JSON.stringify(data));
    console.log(json);
    send(json);
}


function SaveNotifySoundBechavior() {
    if ($('input[name=Sound][id=fun-sound]')[0].checked == true)
        setSettings('notifysound', true);
    else
        setSettings('notifysound', false);
    _Alert('Настройка успешно изменена', '#7f7f7f');
}

function SaveNotifyAlertBechavior() {
    if ($('input[name=Alert][id=fun-alert]')[0].checked == true)
        setSettings('notifyalert', true);
    else
        setSettings('notifyalert', false);
    _Alert('Настройка успешно изменена', '#7f7f7f');

}



function send(request) {
    console.log('sending...');
    $.post("/api/save_settings", 
      request,
      function (result) {
          console.log('resp' + result);
          if (result) {
              _Alert('Настройка успешно изменена', '#7f7f7f');
          }
          else {
              _Alert('Настройка не была изменена, проверьте правильность введенных данных', 'red');
          }
    })
}