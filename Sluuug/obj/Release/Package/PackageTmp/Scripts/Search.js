function getCities() {
    let countryCode = $("#user_search_country :selected").val();
    $.ajax({
        type: "post",
        url: "/api/cities",
        data: { countryCode },
        success: function (response) {
            console.log(response);

            let countryList = $("#user_search_city");
            countryList.empty();
            for (let i = 0; i < response.Cities.length; i++){
                countryList.append(new Option(response.Cities[i].Title, response.Cities[i].CityCode));
            }
        },
    });
}

function serchUser() {
    var data = $("#searchSubmit").serializeArray();
    var json = parceJSON(data);
    window.location.replace('/private/search_result?' + $("#searchSubmit").serialize());


}


function parceJSON(array) {
    var obj = new Object();
    for (let i = 0; i < array.length; i++) {
        let nm = array[i].name;
        let vl = array[i].value;
        obj[nm] = vl;
    }
    return obj;
}