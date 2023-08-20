function changepage(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;

    //changePageId
    search_params.set('pageId', pageId);
    url.search = search_params.toString();

    //the new url string
    var new_url = url.toString();

    window.location.replace(new_url);

}