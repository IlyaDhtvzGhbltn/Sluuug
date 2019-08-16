function getPartialView(url_addres) {
    return $.ajax({
        url: url_addres,
        data: {},
        type: "post"
    });
}