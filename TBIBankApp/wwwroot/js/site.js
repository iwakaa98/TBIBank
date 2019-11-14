// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.onkeyup = function (e) {
    if (e.shiftKey && e.which === 66) {
        window.location.replace("http://localhost:54266/Manager/Register");
    }
    if (e.shiftKey && e.which == 73) {
        window.location.replace("http://localhost:54266/Email/ListEmailsAsync?emailStatus=InvalidApplication")
    }
    if (e.shiftKey && e.which == 82) {
        window.location.replace("http://localhost:54266/Email/ListEmailsAsync?emailStatus=NotReviewed");
    }
    if (e.shiftKey && e.which == 79) {
        window.location.replace("http://localhost:54266/Email/ListEmailsAsync?emailStatus=Open");
    }
    if (e.shiftKey && e.which == 67) {
        window.location.replace("http://localhost:54266/Email/ListEmailsAsync?emailStatus=Closed");
    }
    if (e.shiftKey && e.which == 78) {
        window.location.replace("http://localhost:54266/Email/ListEmailsAsync?emailStatus=New");
    }
}