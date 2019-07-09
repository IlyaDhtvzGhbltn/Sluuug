window.onload = function () {
    var experation_chats = $('.experation-mins');
    var periods = [];
    for (var i = 0; i < experation_chats.length; i++) {

        var min = $('.experation-mins')[i].innerHTML;
        var sec = $('.experation-sec')[i].innerHTML;
        periods[i] = { min, sec };
    }

    var interval = setInterval(
        function () {
            var expiredAll = isTimedOut(periods);
            if (expiredAll == true) {
                clearTimeout(interval);
            }
            else {
                for (var i = 0; i < periods.length; i++) {
                    if (periods[i].min != 0 && periods[i].sec == 0) {
                        periods[i].min = periods[i].min - 1;
                        periods[i].sec = 60;

                        $('.experation-mins')[i].innerHTML = periods[i].min;
                        $('.experation-sec')[i].innerHTML = periods[i].sec;
                    }
                    else if (periods[i].min != 0 && periods[i].sec != 0) {
                        periods[i].sec = periods[i].sec - 1;
                        $('.experation-sec')[i].innerHTML = periods[i].sec;
                    }
                    else if (periods[i].min == 0 && periods[i].sec != 0) {
                        periods[i].sec = periods[i].sec - 1;
                        $('.experation-sec')[i].innerHTML = periods[i].sec;
                    }
                    else if (periods[i].min == 0 && periods[i].sec == 0) {
                    }
                }
            }
        }, 1000);
}
function isTimedOut(periods) {
    var active = 0;
    [].forEach.call(periods, function (item) {
        if (item.min != 0 || item.sec != 0) {
            active++;
        }
    })
    if (active == 0) {
        return true;
    }
    else {
        return false;
    }
}