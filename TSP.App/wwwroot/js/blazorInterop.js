var blazorInterop = blazorInterop || {};

blazorInterop.initializeSummernote = function (id,content) {
    var element = "#summernote" + id;
    $(element).summernote({
        placeholder: 'Your content',
        tabsize: 2,
        height: 200
    });
    $(element).summernote('code', content);
};
blazorInterop.initializeSummernoteGet = (id) => {
    var element = "#summernote" + id;
    return $(element).summernote('code');
}