function getCities(countryListBoxId, citiesListBoxId, emptyValue) {
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
            if (emptyValue) {
                countryList.append(new Option('Не важно', -1));
            }
        },
    });
}
 
function serchUser(formId) {
    console.log($("#"+formId+"").serialize());

    let domain = window.location.host;
    let protocol = location.protocol;
    let url = protocol + '//' + domain + '/private/search_result?' + $("#" + formId + "").serialize() + '&page=1';
    window.open(url);
}