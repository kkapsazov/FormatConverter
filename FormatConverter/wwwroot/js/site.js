$('.download-btn').click(function() {
    contentType =  'data:application/octet-stream,';
    var el = $('[data-file-name="'+ $(this).attr('download') +'"]');
    
    uriContent = contentType + encodeURIComponent(el.val());
    this.setAttribute('href', uriContent);
});