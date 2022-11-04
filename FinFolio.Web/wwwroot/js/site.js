// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(document).ajaxStart(
        function (event, request, settings) {
            $("#spinner").addClass("show");
            //$("#schemeDetailsDiv").addClass("visually-hidden");
        });
    $(document).ajaxComplete(
        function (event, request, settings) {
            $("#spinner").removeClass("show");
            //$("#schemeDetailsDiv").removeClass("visually-hidden");
        });
    $("#schemeDetailsCollapse").on("expand", function (event, id, name) {
        var url = $("#schemeDetailsUrl").val();
        $("#schemeDetailsDiv").load(url + '/' + id);
        
        $(".collapse").collapse();
    });
    $(document).on("showToast", function (isSucces, message) {
        if (isSuccess === true) {
            $("#toast").removeClass("alert-danger");
            $("#toast").addClass("alert-success");
        }
        $("#toastLabel").val(message);
        $("#toast").addClass("show");
    });
   
});
