function getSetting(name) {
    return localStorage.getItem(name);
}

function setSettings(name, value) {
    localStorage.setItem(name, value);
}

function boolSetting(name) {
    var sett = getSetting(name);
    if (sett == 'true') {
        return true;
    }
    else return false;
}

function isSettingSaved(name) {
    var item = localStorage.getItem(name);
    if (item.length == 0)
        return true;
    else return false;
}