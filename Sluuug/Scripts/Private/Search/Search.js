window.addEventListener('scroll', function () {
    Scrolling($('.end-of-users')[0].id);
});

function getCities(countryListBoxId, citiesListBoxId, emptyValue) {
    let countryCode = $("#" + countryListBoxId + " :selected").val();
    $.ajax({
        type: "post",
        url: "/api/cities",
        data: { countryCode },
        success: function (response) {
            var cities = response.Cities;
            var countryList = $("#" + citiesListBoxId);
            countryList.empty();
            for (let i = 0; i < cities.length; i++) {
                countryList.append(new Option(cities[i].Title, cities[i].CityCode));
            }
            if (emptyValue) {
                countryList.append(new Option('Не важно', -1));
            }
            try { SetCitiesInSearch(cities); }
            catch (ex) { //
            }
            try { SetCitiesInMyProfile(cities); }
            catch (ec) { // 
            }

        }
    });
}
 
function serchUser(formId) {
    console.log($("#"+formId+"").serialize());

    let domain = window.location.host;
    let protocol = location.protocol;
    let url = protocol + '//' + domain + '/private/search_result?' + $("#" + formId + "").serialize() + '&page=1';
    window.open(url);
}

function ChangePurpose() {
    var currentPurpose = $('select[name="user_purpose"]')[0].value;
    if (currentPurpose == -1) {
        let userWhichSearchSex = $('select[name="user_search_sex"]')[0];
        let userWhichSearchAge = $('select[name="user_search_age"]')[0];

        userWhichSearchSex.disabled = true;
        userWhichSearchAge.disabled = true;

        userWhichSearchSex.value = -1;
        userWhichSearchAge.value = -1;
    }
    else {
        $('select[name="user_search_sex"]')[0].disabled = false;
        $('select[name="user_search_age"]')[0].disabled = false;
    }
}

function Scrolling(currentSearchPage) {
    var target = $('.end-of-users')[0];
    // Все позиции элемента
    var targetPosition = {
        top: window.pageYOffset + target.getBoundingClientRect().top,
        left: window.pageXOffset + target.getBoundingClientRect().left,
        right: window.pageXOffset + target.getBoundingClientRect().right,
        bottom: window.pageYOffset + target.getBoundingClientRect().bottom
    };
    var windowPosition = {
        top: window.pageYOffset,
        left: window.pageXOffset,
        right: window.pageXOffset + document.documentElement.clientWidth,
        bottom: window.pageYOffset + document.documentElement.clientHeight
    };

    if (targetPosition.bottom > windowPosition.top &&
        targetPosition.top < windowPosition.bottom &&
        targetPosition.right > windowPosition.left &&
        targetPosition.left < windowPosition.right)
    {
        var searchParameters = $('.search-link-container')[0].id;
        let nextSearch = parseInt(currentSearchPage) + 1;
        console.log(`строка поиска ${searchParameters + nextSearch}`);
    }
}