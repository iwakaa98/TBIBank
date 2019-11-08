// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.onkeyup = function (e) {
    if (e.ctrlKey && e.which === 66) {
        $.ajax(
            {
                type: "Post",
                url: "Manager/RegisterUser"
            }
        )
    }
}