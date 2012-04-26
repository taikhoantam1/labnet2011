$(document).ready(function () {


    $(".date").datepicker({ dateFormat: 'dd/mm/yy' });

    // prevent all action of element has class disabled
    $(".disabled").unbind("click").click(function (event) {
        event.preventDefault();
    });

    $(".disabled").unbind("change").change(function (event) {
        event.preventDefault();
    });

    $(".disabled").unbind("keyup").keyup(function (event) {
        event.preventDefault();
    });

    $(".disabled").unbind("keydown").keydown(function (event) {
        event.preventDefault();
    });
});
