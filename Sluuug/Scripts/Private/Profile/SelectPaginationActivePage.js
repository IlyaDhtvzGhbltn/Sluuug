window.onloadend = function () {
    alert(1);
    var url_string = window.location.href;
    var url = new URL(url_string);
    var pageNum = url.searchParams.get("page");
    if (pageNum !== null) {
        let query = '#pagination-link-' + pageNum;
        var elem = $(query);
        elem.addClass('selected-page-container');
    }
};