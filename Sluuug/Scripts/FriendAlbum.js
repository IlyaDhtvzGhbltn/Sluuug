function ExpandFoto(number, fullFoto, title, comment, index) {
    $.ajax({
        data: { fotoID: number, fullFoto: fullFoto, titl: title, comm: comment},
        url: "/partial/friendexpand",
        type: "post",
        success: function (html) {
            let element = $('.full_view')[0];
            element.innerHTML = html;
        }
    });


}