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
            try {
                SetCitiesInSearch(cities);
            }
            catch (ex) {
                //
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