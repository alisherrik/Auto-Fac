// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(document).ready(function() {
    $('.custom-file-input').on("change",function() {
        var fileName =$(this).val().split("\\").pop();
        $(this).next('.custom-file-label').html(fileName);
    });
});