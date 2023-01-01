// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SetCulture(selectedValue) {
    alert(selectedValue);
    var url = window.location.href.split('?')[0];
    var culture = "?culture=" + selectedValue + "&ui-culture=" + selectedValue;
    window.location.href = url + culture;
}